using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.CreateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController (BookStoreDbContext context)
        {
            _context = context;

        }
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id = 1,
        //         Title = "Kaşağı",
        //         GenreId = 1,
        //         PageCount ="200",
        //         PublishDate = new DateTime(2031,01,23)
        //     },
        //     new Book{
        //         Id = 2,
        //         Title = "nasrettin hoca",
        //         GenreId = 1,
        //         PageCount ="200",
        //         PublishDate = new DateTime(1954,04,13)
        //     },
        //     new Book{
        //         Id = 3,
        //         Title = "aşkı memnu",
        //         GenreId = 2,
        //         PageCount ="200",
        //         PublishDate = new DateTime(1999,10,03)
        //     }
        // };

        // [HttpGet]
        // public List<Book> GetBooks(){
        //     var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        //     return bookList;
        // }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){

            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        //[HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = _context.Books.OrderBy(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook (int Id, [FromBody] updateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = Id;
                command.Model= updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int Id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand (_context);
                command.BookId=Id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
        
    }

    
}