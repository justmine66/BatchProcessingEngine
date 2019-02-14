using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BatchProcessingEngine.Extensions
{
    /// <summary>
    /// Extension methods for adding typed middleware.
    /// </summary>
    public static class UseMiddlewareExtensions
    {
        internal const string InvokeMethodName = "Invoke";
        internal const string InvokeAsyncMethodName = "InvokeAsync";

        private static readonly MethodInfo GetServiceInfo = typeof(UseMiddlewareExtensions).GetMethod("GetService", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Adds a middleware type to the engine's processing pipeline.
        /// </summary>
        public static IProcessingPipeLineBuilder UseMiddleware<TMiddleware>(this IProcessingPipeLineBuilder pipeLine, params object[] args)
        {
            return pipeLine.UseMiddleware(typeof(TMiddleware), args);
        }

        public static IProcessingPipeLineBuilder UseMiddleware(this IProcessingPipeLineBuilder pipeLine, Type middleware,
            params object[] args)
        {
            if (typeof(IMiddleware).GetTypeInfo().IsAssignableFrom(middleware.GetTypeInfo()))
            {
                // 中间件直接从容器中激活，不需要传递参数。
                if (args.Length != 0)
                    throw new NotSupportedException(string.Format(Resources.Exception_UseMiddlewareExplicitArgumentsNotSupported, typeof(IMiddleware)));

                return UseMiddlewareInterface(pipeLine, middleware);
            }

            var applicationServices = pipeLine.Services;
            return pipeLine.Use(next =>
            {
                var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m =>
                    string.Equals(m.Name, InvokeMethodName, StringComparison.Ordinal)
                    || string.Equals(m.Name, InvokeAsyncMethodName, StringComparison.Ordinal)
                ).ToArray();

                if (invokeMethods.Length == 0)
                {
                    throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareNoInvokeMethod, InvokeMethodName, InvokeAsyncMethodName, middleware));
                }

                if (invokeMethods.Length > 1)
                {
                    throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddleMutlipleInvokes, InvokeMethodName, InvokeAsyncMethodName));
                }

                var methodInfo = invokeMethods[0];
                if (!typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
                {
                    throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareNonTaskReturnType, InvokeMethodName, InvokeAsyncMethodName, nameof(Task)));
                }

                var parameters = methodInfo.GetParameters();
                if (parameters.Length == 0 || parameters[0].ParameterType != typeof(ProcessingContext))
                {
                    throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareNoParameters, InvokeMethodName, InvokeAsyncMethodName, nameof(ProcessingContext)));
                }

                var ctorArgs = new object[args.Length + 1];
                ctorArgs[0] = next;
                Array.Copy(args, 0, ctorArgs, 1, args.Length);
                var instance = ActivatorUtilities.CreateInstance(pipeLine.Services, middleware, ctorArgs);
                if (parameters.Length == 1)
                {
                    return (ProcessingDelegate)methodInfo.CreateDelegate(typeof(ProcessingDelegate), instance);
                }

                var factory = Compile<object>(methodInfo, parameters);
                return context =>
                {
                    var serviceProvider = context.Services ?? applicationServices;
                    if (serviceProvider == null)
                    {
                        throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareIServiceProviderNotAvailable, nameof(IServiceProvider)));
                    }

                    return factory(instance, context, serviceProvider);
                };
            });
        }

        private static IProcessingPipeLineBuilder UseMiddlewareInterface(IProcessingPipeLineBuilder pipeLine, Type middlewareType)
        {
            return pipeLine.Use(next =>
            {
                return async context =>
                {
                    var middlewareFactory = (IMiddlewareFactory)context.Services.GetService(typeof(IMiddlewareFactory));
                    if (middlewareFactory == null)
                    {
                        // No middleware factory
                        throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareNoMiddlewareFactory, typeof(IMiddlewareFactory)));
                    }

                    var middleware = middlewareFactory.Create(middlewareType);
                    if (middleware == null)
                    {
                        // The factory returned null, it's a broken implementation
                        throw new InvalidOperationException(string.Format(Resources.Exception_UseMiddlewareUnableToCreateMiddleware, middlewareFactory.GetType(), middlewareType));
                    }

                    try
                    {
                        await middleware.InvokeAsync(context, next);
                    }
                    finally
                    {
                        middlewareFactory.Release(middleware);
                    }
                };
            });
        }

        private static Func<T, ProcessingContext, IServiceProvider, Task> Compile<T>(MethodInfo methodInfo,
            ParameterInfo[] parameters)
        {
            var middleware = typeof(T);
            var httpContextArg = Expression.Parameter(typeof(ProcessingContext), "processingContext");
            var providerArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
            var instanceArg = Expression.Parameter(middleware, "middleware");
            var methodArguments = new Expression[parameters.Length];
            methodArguments[0] = httpContextArg;
            for (var i = 1; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef)
                {
                    throw new NotSupportedException(string.Format(Resources.Exception_InvokeDoesNotSupportRefOrOutParams, InvokeMethodName));
                }

                var parameterTypeExpression = new Expression[]
                {
                    providerArg,
                    Expression.Constant(parameterType, typeof(Type)),
                    Expression.Constant(methodInfo.DeclaringType, typeof(Type))
                };

                var getServiceCall = Expression.Call(GetServiceInfo, parameterTypeExpression);
                methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
            }

            Expression middlewareInstanceArg = instanceArg;
            if (methodInfo.DeclaringType != typeof(T))
            {
                middlewareInstanceArg = Expression.Convert(middlewareInstanceArg, methodInfo.DeclaringType);
            }

            var body = Expression.Call(middlewareInstanceArg, methodInfo, methodArguments);

            var lambda = Expression.Lambda<Func<T, ProcessingContext, IServiceProvider, Task>>(body, instanceArg, httpContextArg, providerArg);

            return lambda.Compile();
        }
    }
}
