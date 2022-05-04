using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applications.AuthorOperations.Querries.GetAuthors
{
    public class GetAuthorsQuerry
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuerry(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GetAuthorsModel> Handler()
        {
            var authors= _context.Authors.OrderBy(x=>x.Id);
            List<GetAuthorsModel> authorsModels= _mapper.Map<List<GetAuthorsModel>>(authors);
            return authorsModels;
        }
    }
    public class GetAuthorsModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime birthday { get; set; }
    }
}