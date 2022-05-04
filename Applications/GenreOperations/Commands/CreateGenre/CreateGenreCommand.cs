using System.Linq;
using AutoMapper;
using System;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handler()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Name==Model.Name); //Aynı türde kitap varsa hata fırtlatmamız lazım.
            if (genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut!"); // Hata fırlatıldı.
            genre = new Genre{Name=Model.Name}; // Bir Genre entity'si oluşturuyoruz ve bunu Genres DBsine ekliyoruz.
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}