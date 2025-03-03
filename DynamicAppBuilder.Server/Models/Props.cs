using System.ComponentModel.DataAnnotations;

namespace DynamicAppBuilder.Server.Models
{
    public class Props
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
    }
}
