using System;
using System.Linq;
using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applications.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel Model { get; set; }

        public void Handler()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id==AuthorId);
            if (author is null)
                throw new InvalidOperationException("Değişiklik yapmak istediğiniz yazar bulunamadı.");
            author.Name = Model.Name!=default ? Model.Name : author.Name;
            author.Surname = Model.Surname!=default ? Model.Surname : author.Surname;
            author.birthday=Model.BirthDay!= default ? Model.BirthDay: author.birthday;
            _context.SaveChanges();
        }

    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
    }
}