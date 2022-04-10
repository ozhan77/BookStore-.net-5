using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                   return;  
                }
                context.Books.AddRange(
                    new Book{
                Id = 1,
                Title = "Kaşağı",
                GenreId = 1,
                PageCount ="200",
                PublishDate = new DateTime(2031,01,23)
                },
                new Book{
                    Id = 2,
                    Title = "nasrettin hoca",
                    GenreId = 1,
                    PageCount ="200",
                    PublishDate = new DateTime(1954,04,13)
                },
                new Book{
                    Id = 3,
                    Title = "aşkı memnu",
                    GenreId = 2,
                    PageCount ="200",
                    PublishDate = new DateTime(1999,10,03)
                }
                );

                context.SaveChanges();
            }

        }
    }
}