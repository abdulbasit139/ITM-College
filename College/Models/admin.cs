using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
