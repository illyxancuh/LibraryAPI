namespace LibraryAPI.Presentation.Models
{
    public class CreateBookModel
    {
        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public GenreModel Genre { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }
    }
}
