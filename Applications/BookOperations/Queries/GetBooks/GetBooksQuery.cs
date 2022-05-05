using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }
        public List<BooksViewModel> Handler()
        {
            var bookList = _dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList);
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title  { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre  {get; set;}
        public Author Author {get; set;}
    }
}