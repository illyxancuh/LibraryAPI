using AutoMapper;
using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.Presentation.Controllers
{
    public class BooksController : APIControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<BookModel>> GetAll([FromQuery]bool availableOnly, 
                                                                 [FromQuery]SortOrderModel sortOrder)
        {
            SortOrderDTO sortOrderDTO = 
                _mapper.Map<SortOrderModel, SortOrderDTO>(sortOrder);

            IReadOnlyCollection<BookDTO> booksDTO = await _bookService.GetAll(availableOnly, sortOrderDTO);

            IReadOnlyCollection<BookModel> models = 
                _mapper.Map<IReadOnlyCollection<BookDTO>, IReadOnlyCollection<BookModel>>(booksDTO);

            return models;
        } 

        [HttpGet("{id}")]
        public async Task<BookModel> GetById([FromRoute] int id)
        {
            BookDTO book = await _bookService.GetById(id);

            return _mapper.Map<BookDTO, BookModel>(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody]CreateBookModel createBook)
        {
            CreateBookDTO createBookDto = _mapper.Map<CreateBookModel, CreateBookDTO>(createBook);

            var bookId = await _bookService.AddBook(createBookDto);

            return CreatedAtAction(nameof(GetById), new { id = bookId }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookModel updateBook)
        {
            var updateBookDto = _mapper.Map<UpdateBookModel, UpdateBookDTO>(updateBook);

            await _bookService.UpdateBook(id, updateBookDto);

            return NoContent();
        }

        [HttpPost("{id}/archive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchiveBook([FromRoute]int id)
        {
            await _bookService.ArchiveBook(id);

            return NoContent();
        }

        [HttpPost("{id}/reserve")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ReserveBook([FromRoute] int id)
        {
            await _bookService.ReserveBook(id);

            return NoContent();
        }

        [HttpPost("{id}/unreserve")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnreserveBook([FromRoute] int id)
        {
            await _bookService.UnreserveBook(id);

            return NoContent();
        }
    }
}
