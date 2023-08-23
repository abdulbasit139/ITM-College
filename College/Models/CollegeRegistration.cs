using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class CollegeRegistration
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string fullName { get; set; }
        [Required]
        public string fatherName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string address { get; set; }
        public string imagePath { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string subject { get; set; }
        public string status { get; set; } = "Pending";
    }
}
