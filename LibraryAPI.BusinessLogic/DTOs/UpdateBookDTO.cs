namespace LibraryAPI.BusinessLogic.DTOs
{
    public class UpdateBookDTO
    {
        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public GenreDTO Genre { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }
    }
}
