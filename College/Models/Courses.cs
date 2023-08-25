using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class Courses
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public string duration { get; set; }
        public string imagePath { get; set; }
        [Required]
        public string description { get; set; }
    }
}
