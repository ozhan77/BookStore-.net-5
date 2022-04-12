using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.CreateBook
{
    public class UpdateBookCommand{
        private readonly BookStoreDbContext _context;
        public int BookId { get; set; }
        public updateBookModel Model {get;set;}
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x=> x.Id == BookId);

            if(book is null)
                throw new InvalidCastException("Kitap yok gazla");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
        }

        public class updateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
        
    }
        

}