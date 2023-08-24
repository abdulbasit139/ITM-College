using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class Feedback 
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string description { get; set; }
    }
}
