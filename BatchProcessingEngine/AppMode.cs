namespace BatchProcessingEngine
{
    public static class AppMode
    {
        public static string Environment { get; set; }

        public static bool IsDevelopment() => Environment == "Development";
        public static bool IsStaging() => Environment == "Staging";
        public static bool IsProduction() => Environment == "Production";
    }
}
