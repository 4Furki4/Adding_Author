using System.Linq;
using AutoMapper;
using BookStore.DbOperations;
using System;

namespace BookStore.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }
        public int MyProperty { get; set; }

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handler()
        {
            var genre= _context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(genre is null)
            throw new InvalidOperationException("Silinecek kitap türü bulunamadı.");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

    }
    
}