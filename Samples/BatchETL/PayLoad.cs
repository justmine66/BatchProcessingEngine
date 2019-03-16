namespace BatchETL
{
    public class PayLoad
    {
        public int No { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{{No: {No},Id: {Id},Content: {Content}}}";
        }
    }
}
