using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DynamicAppBuilder.Server.Models
{
    public class ControlPropertiesInput
    {

        public Coordinates coordinates { get; set; }
        public string Type { get; set; }
        public string? Placeholder { get; set; }
        public string? defaultValue { get; set; }
        public List<string>? Options { get; set; }
    }
}
