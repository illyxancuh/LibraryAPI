using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.Entities;
using LibraryAPI.DataAccess.Queries;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Services
{
    public class CSVGenerator : ICSVGenerator
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CSVGenerator(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Stream> GenerateBooksCSV()
        {
            IReadOnlyCollection<Book> books =
                await _unitOfWork.Books.GetAll(false, SortOrder.Asc);

            IReadOnlyCollection<CSVBook> csvbooks =
                _mapper.Map<IReadOnlyCollection<Book>, IReadOnlyCollection<CSVBook>>(books);

            CsvConfiguration configuration = new CsvConfiguration(CultureInfo.CurrentCulture);

            var memoryStream = new MemoryStream();

            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, configuration);

            csvWriter.WriteRecords(csvbooks);
            await csvWriter.FlushAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
    }
}
