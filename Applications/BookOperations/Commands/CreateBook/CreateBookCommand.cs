using System;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x=>x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut!");
            }
            book = mapper.Map<Book>(Model); //CreateBookModel tipinden Book tipine mapledik.
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}