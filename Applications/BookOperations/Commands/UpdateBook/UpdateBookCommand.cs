using System.Linq;
using BookStore.DbOperations;
using System;

namespace BookStore.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model{get; set;}
        
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id == BookId);
            if (book is null)
            {
               throw new InvalidOperationException("Güncellenecek kitap bulunamadı.");
            }
            book.GenreId = Model.GenreId!= default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title!= default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; } //Yazar güncellemesini buradan yapmaktan ziyade kendi endpointleriyle yapmak iyi olur, Author eklemedim.

    }
}