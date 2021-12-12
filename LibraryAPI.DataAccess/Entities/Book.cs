using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DataAccess.Entities
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorFullName { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        [Required]
        public Genre Genre { get; set; }
        
        [MaxLength(5120)]
        public string Description { get; set; }

        public int Year { get; set; }

        public User ReservedBy { get; set; }

        public int? ReservedById { get; set; }
    }
}
