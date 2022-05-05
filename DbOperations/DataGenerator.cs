using System;
using System.Linq;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void initialise(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange
                (
                    new Author
                    {
                        Name="William",
                        Surname="Golding",
                        birthday= new DateTime(1911,09,19)
                    },
                    new Author
                    {
                        Name="David",
                        Surname="Eagleman",
                        birthday= new DateTime(1971,04,25)
                    },
                    new Author
                    {
                        Name="Frans",
                        Surname="De Waal",
                        birthday=new DateTime(1948,10,29)
                    }
                );
                    context.Genres.AddRange
                (
                        new Genre
                    {
                        Name="Personal Growth",

                    },
                    new Genre
                    {
                        Name="Sciene Fiction"
                    },
                    new Genre
                    {
                        Name="Novel"
                    }
                );
                context.Books.AddRange
                (
                    new Book
                    {
                        Title="Lord Of The Rings",
                        GenreId=1,
                        AuthorId=1,
                        PageCount=800,
                        PublishDate= new DateTime(2001,11,07)
                    },
                    new Book
                    {
                        Title="Incognito",
                        GenreId=2,
                        PageCount=300,
                        PublishDate= new DateTime(2016,6,11),
                        AuthorId=2
                    },
                    new Book
                    {
                        Title="Our Inner Ape",
                        GenreId=2,
                        AuthorId=3,
                        PageCount=400,
                        PublishDate= new DateTime(2017,1,18)
                    }
                );
                    context.SaveChanges();
            }
        }
    }
}