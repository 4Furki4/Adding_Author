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
            var bookList = _dbContext.Books.Include(x=>x.Genre).OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList); //new List<BooksViewModel>();
            // foreach (var book in bookList)
            // {
            //     vm.Add(new BooksViewModel() //View Modelde hangi verilerin ne biçimde döndürüleceği belirlendi ve buna uygun bir biçimde Book nesnesi değil, 
            //     {                           //modele uygun BooksViewModel nesnesi listemize eklendi
            //         Title= book.Title,
            //         Genre= ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //         PageCount=book.PageCount
            //     });
            // }
            // return bookList; istediğimiz veri tipinin her zaman UI'a döndüğünden emin olmak için viewModel oluşturacağız ve geri dönüş tipi düzenlenmiş BookViewModel listesi olacak
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title  { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre  {get; set;}
    }
}