using System;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applications.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handler()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Name==Model.Name);
            if(author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut.");
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
    }
}