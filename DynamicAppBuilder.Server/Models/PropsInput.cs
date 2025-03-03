namespace DynamicAppBuilder.Server.Models
{
    public class PropsInput
    {
        public string name { get; set; }
        public List<ControlPropertiesInput?> ControlsProperties { get; set; }
    }
}
