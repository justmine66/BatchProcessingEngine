using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingPipeLineBuilder : IProcessingPipeLineBuilder
    {
        private readonly List<Func<ProcessingDelegate, ProcessingDelegate>> _middlewares = new List<Func<ProcessingDelegate, ProcessingDelegate>>();

        public IServiceProvider Services { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IProcessingPipeLineBuilder Use(Func<ProcessingDelegate, ProcessingDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public ProcessingDelegate Build()
        {
            _middlewares.Reverse();
            return context =>
            {
                ProcessingDelegate next = _ => Task.CompletedTask;
                foreach (var middleware in _middlewares)
                    next = middleware(next);

                return next(context);
            };
        }
    }
}
