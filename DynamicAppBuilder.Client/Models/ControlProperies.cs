namespace DynamicAppBuilder.Client.Models
{
    public class ControlProperties
    {
        public Coordinates coordinates { get; set; }
        public string Type { get; set; }
        public string? Placeholder { get; set; }
        public string? defaultValue { get; set; }
        public List<String>? Options { get; set; }
    }
}
