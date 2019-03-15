using BatchProcessingEngine;
using BatchProcessingEngine.Extensions;
using System;

namespace BatchETL
{
    public static class DataProcessingPipeLineMiddlewareExtensions
    {
        public static IProcessingPipeLineBuilder UserDataHandler(this IProcessingPipeLineBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseMiddleware<DataProcessingPipeLineMiddleware>(Array.Empty<object>());
        }
    }
}
