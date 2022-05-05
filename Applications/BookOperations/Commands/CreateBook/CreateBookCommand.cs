using System;
using System.Linq;
using AutoMapper;
using BookStore.Applications.AuthorOperations.Command.CreateAuthor;
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
            var author = _dbContext.Authors.SingleOrDefault(x=>x.Name==Model.Author.Name && x.Surname==Model.Author.Surname);

            book = mapper.Map<Book>(Model); //CreateBookModel tipinden Book tipine mapledik.
            if(author is not null)
                book.Author=author;
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
        public CreateAuthorModel Author { get; set; }
    }
}