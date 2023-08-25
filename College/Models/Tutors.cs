using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class Tutors
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string subject { get; set; }
        public string imagePath { get; set; }
        [Required]
        public int salary { get; set; }
        [Required]
        public string descirption { get; set; }
    }
}
