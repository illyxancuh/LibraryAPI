using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DataAccess.Entities
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
