using System.ComponentModel.DataAnnotations;

namespace GamesMvc.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
