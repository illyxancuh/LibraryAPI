namespace LibraryAPI.BusinessLogic.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public bool IsReserved { get; set; }

        public bool IsArchived { get; set; }

        public GenreDTO Genre { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string ReservedBy { get; set; }

        public int ReservedById { get; set; }
    }
}
