using System.Linq;
using AutoMapper;
using BookStore.DbOperations;
using System;

namespace BookStore.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handler()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            if (_context.Genres.Any(x=>x.Name.ToLower()== Model.Name.ToLower()&& x.Id!= GenreId))
            {
                throw new InvalidOperationException("Aynı isimde bir kitap türü zaten mevcut!");
            }
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) == default ? genre.Name  : Model.Name;
            genre.IsActive= Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}