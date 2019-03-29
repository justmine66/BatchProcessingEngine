using System.Collections.Generic;

namespace BatchETL
{
    internal static class InMemoryDb
    {
        internal static List<PayLoad> Payloads = new List<PayLoad>();

        static InMemoryDb()
        {
            var length = 20;
            for (var i = 1; i <= length; i++)
                Payloads.Add(new PayLoad() { Id = i, Content = $"第{i}条内容" });
        }
    }
}
