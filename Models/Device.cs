using System.ComponentModel.DataAnnotations;

namespace GamesMvc.Models
{
    public class Device : BaseModel
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
    }
}
