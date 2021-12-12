namespace LibraryAPI.Presentation.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public bool IsReserved { get; set; }

        public bool IsArchived { get; set; }

        public GenreModel Genre { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string ReservedBy { get; set; }
    }
}
