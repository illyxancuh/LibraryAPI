using CsvHelper.Configuration.Attributes;

namespace LibraryAPI.BusinessLogic.Services
{
    public class CSVBook
    {
        [Name("ID")]
        public int Id { get; set; }

        [Name("Title")]
        public string Title { get; set; }

        [Name("Author")]
        public string AuthorFullName { get; set; }
    }
}
