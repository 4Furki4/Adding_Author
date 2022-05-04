using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applications.GenreOperations.Queries.GetGenre
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handler()
        {
            var genres = _context.Genres.Where(x=> x.IsActive==true).OrderBy(x=>x.Id);
            List<GenresViewModel> genresViews= _mapper.Map<List<GenresViewModel>>(genres);
            return genresViews;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}