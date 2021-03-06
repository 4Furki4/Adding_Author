using System;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _dbContext;
        public int BookId {get; set;}
        private readonly IMapper mapper;
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }
        public BookDetailViewModel Handler()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).Where(x=>x.Id==BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamad─▒");
            BookDetailViewModel vm = mapper.Map<BookDetailViewModel>(book);
            return vm;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
        public Author Author { get; set; }
    }
}