using System;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applications.AuthorOperations.Querries.GetAuthorDetails
{
    public class GetAuthorDetail
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetail(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetAuthorDetailViewModel Handler()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id==AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");
            var mapped =_mapper.Map<GetAuthorDetailViewModel>(author);
            return mapped;
        }
    }
    public class GetAuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime birthday { get; set; }
    }
}