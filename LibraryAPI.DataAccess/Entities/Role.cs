using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DataAccess.Entities
{
    public class Role
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}