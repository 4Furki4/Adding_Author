using AutoMapper;
using BookStore.Applications.AuthorOperations.Querries.GetAuthorDetails;
using BookStore.Applications.AuthorOperations.Querries.GetAuthors;
using BookStore.Applications.BookOperations.Commands.CreateBook;
using BookStore.Applications.BookOperations.Queries.GetBookDetail;
using BookStore.Applications.BookOperations.Queries.GetBooks;
using BookStore.Applications.GenreOperations.Queries.GetGenre;
using BookStore.Applications.GenreOperations.Queries.GetGenreDetail;
using BookStore.Entities;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {               //Kaynak-Hedef
            CreateMap<CreateBookModel,Book>(); //CreateBookModelde elle tek tek maplediğimiz propları oto mapleyeceğiz bu şekilde.  For HTTP Post
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name)); //For HTTP GetById
                                                            //Destination Genre'ya git ve opt.MapFrom'daki gibi maple demek.
            CreateMap<Book, BooksViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name)); //For HTTP Get

            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<Author,GetAuthorsModel>();
            CreateMap<Author, GetAuthorDetailViewModel>();
        }
    }
}