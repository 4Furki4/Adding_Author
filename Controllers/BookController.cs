using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.Applications.BookOperations.Commands.CreateBook;
using BookStore.Applications.BookOperations.Commands.DeleteBook;
using BookStore.Applications.BookOperations.Commands.UpdateBook;
using BookStore.Applications.BookOperations.Queries.GetBookDetail;
using BookStore.Applications.BookOperations.Queries.GetBooks;
using BookStore.DbOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, mapper);
            var result = query.Handler();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new(_context,mapper);
            query.BookId=id;
            GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
            validations.ValidateAndThrow(query);
            BookDetailViewModel result=query.Handler();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context, mapper);
            command.Model= newBook;
            CreateBookCommandValidator validations = new();    //validasyonu içeren constructor metot çalıştırıldı.
            validations.ValidateAndThrow(command); //Burada tasdik başarısız olursa exeption ile yakalayıp bunu catch blokunda UI'a bad request döndürerek mesajını yazdıracağız.
            command.Handler();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new(_context);
            UpdateBookCommandValidator validations=new();
            command.BookId=id;
            command.Model=updatedBook;
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new(_context);
            command.BookId=id;
            DeleteBookCommandValidator validations = new();
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }

    }

}