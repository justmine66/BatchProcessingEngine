namespace BatchETL
{
    public class PayLoad
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{{Id: {Id},Content: {Content}}}";
        }
    }
}
