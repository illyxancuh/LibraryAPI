using System.IO;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Contracts
{
    public interface ICSVGenerator
    {
        public Task<Stream> GenerateBooksCSV();
    }
}
