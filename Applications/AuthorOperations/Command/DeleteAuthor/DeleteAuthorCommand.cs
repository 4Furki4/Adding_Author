using System.Linq;
using BookStore.DbOperations;
using System;

namespace BookStore.Applications.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handler()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id==AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silmek istediğiniz kitap bulunamadı.");
            if(_context.Books.SingleOrDefault(x=>x.Id==AuthorId) is not null)
                throw new InvalidOperationException("Silmek istediğiniz yazarın kitabı var! Önce Kitabı silmelisiniz.");
            else
                _context.Authors.Remove(author);
                _context.SaveChanges();
        }
    }
}