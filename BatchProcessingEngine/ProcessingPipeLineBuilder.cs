using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingPipeLineBuilder : IProcessingPipeLineBuilder
    {
        private readonly List<Func<ProcessingDelegate, ProcessingDelegate>> _middlewares = new List<Func<ProcessingDelegate, ProcessingDelegate>>();

        public IProcessingPipeLineBuilder Use(Func<ProcessingDelegate, ProcessingDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public ProcessingDelegate Build()
        {
            _middlewares.Reverse();
            return (context, payloads) =>
            {
                ProcessingDelegate next = (_, data) => { return Task.CompletedTask; };
                foreach (var middleware in _middlewares)
                    next = middleware(next);

                return next(context, payloads);
            };
        }
    }
}
