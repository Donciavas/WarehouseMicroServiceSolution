namespace BusinessLogic.Configuration
{
    public class OrderProcessingServiceOptions
    {
        public string DatabaseConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}