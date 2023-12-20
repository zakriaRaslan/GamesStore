using System.ComponentModel.DataAnnotations;

namespace GamesMvc.Models
{
    public class Game : BaseModel
    {
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevices> Devices { get; set; } = new List<GameDevices>();
    }
}
