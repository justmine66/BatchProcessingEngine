using System.Collections.Immutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessingEngine.WorkPool
{
    public class BasicExecutor : IExecutor
    {
        private readonly ImmutableList<Thread> _threads;

        public BasicExecutor()
        {
            _threads = ImmutableList.Create<Thread>();
        }

        public override string ToString()
        {
            return $"{{threads:{DumpThreadInfo()}}}";
        }

        public Task ExecuteAsync(IProcessor command, ProcessingContext context)
        {
            return Task.Factory.StartNew(async state => await InnerExecuteAsync(command, (ProcessingContext)state), context, TaskCreationOptions.LongRunning);
        }

        private async Task InnerExecuteAsync(IProcessor command, ProcessingContext context)
        {
            var workerThread = Thread.CurrentThread;
            _threads.Add(workerThread);

            try
            {
                await command.RunAsync(context);
            }
            finally
            {
                _threads.Remove(workerThread);
            }
        }

        private string DumpThreadInfo()
        {
            var sb = new StringBuilder();
            foreach (var t in _threads)
            {
                sb.Append("{");
                sb.Append("name=").Append(t.Name).Append(",");
                sb.Append("id=").Append(t.ManagedThreadId).Append(",");
                sb.Append("state=").Append(t.ThreadState);
                sb.Append("},");
            }

            var output = sb.ToString();

            return $"[{output.Substring(0, output.Length - 1)}]";
        }
    }
}
