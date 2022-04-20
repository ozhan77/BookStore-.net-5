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
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                GetBookValidator validator = new GetBookValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            
            
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
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
          
                command.Model = newBook;
                //Validasyonun tanımlanması gereken bölüm handle dan hemen önce model oluştuğunda olması gerekiyor.
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
            //     if(!result.IsValid)
            //     foreach (var item in result.Errors)
            //     {
            //         Console.WriteLine("Özellik"+item.PropertyName+"-Error Message:"+item.ErrorMessage);
            //     }
            //     else command.Handle();
            // }
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook (int Id, [FromBody] updateBookModel updateBook)
        {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = Id;
                command.Model= updateBook;
                command.Handle();
            
            return Ok();
            

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int Id)
        {
                DeleteBookCommand command = new DeleteBookCommand (_context);
                command.BookId=Id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            
            return Ok();

        }
        
    }

    
}