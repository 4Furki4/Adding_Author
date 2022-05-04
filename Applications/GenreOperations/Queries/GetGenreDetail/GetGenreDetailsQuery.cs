using System;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailsQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handler()
        {
            var genres = _context.Genres.SingleOrDefault(x=>x.IsActive==true &&x.Id==GenreId);
            if(genres is null)
                throw new InvalidOperationException("Kitap bulunamadı.");
            var returnObj= _mapper.Map<GenreDetailViewModel>(genres);
            return returnObj;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}