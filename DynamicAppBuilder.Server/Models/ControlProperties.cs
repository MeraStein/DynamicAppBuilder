using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicAppBuilder.Server.Models
{
    public class ControlProperties
    {
        [Key]
        public int Id { get; set; }

        public Coordinates coordinates { get; set; }
        public string Type { get; set; }
        public string? Placeholder { get; set; }
        public string? defaultValue { get; set; }
        public List<string>? Options { get; set; }

        [ForeignKey("PropId")]
        public int PropId { get; set; }  
    }


    public class Coordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
