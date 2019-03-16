using System.Collections.Generic;

namespace BatchETL
{
    internal static class InMemoryDb
    {
        internal static List<PayLoad> Payloads = new List<PayLoad>();

        static InMemoryDb()
        {
            for (var i = 1; i <= 400; i++)
                Payloads.Add(new PayLoad() { Id = i, Content = $"第{i}条内容" });
        }
    }
}
