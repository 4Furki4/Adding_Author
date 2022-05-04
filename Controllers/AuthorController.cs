using AutoMapper;
using BookStore.Applications.AuthorOperations.Command.CreateAuthor;
using BookStore.Applications.AuthorOperations.Command.DeleteAuthor;
using BookStore.Applications.AuthorOperations.Command.UpdateAuthor;
using BookStore.Applications.AuthorOperations.Querries.GetAuthorDetails;
using BookStore.Applications.AuthorOperations.Querries.GetAuthors;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthor()
        {
            GetAuthorsQuerry querry = new(_context,_mapper);
            var authors= querry.Handler();
            return Ok(authors);
        }
        [HttpGet("id")]
        public IActionResult GetAuthorById(int id)
        {
            GetAuthorDetail detail = new(_context, _mapper);
            GetAuthorDetailValidation validations = new();
            detail.AuthorId=id;
            validations.ValidateAndThrow(detail);
            var result = detail.Handler();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new(_context,_mapper);
            command.Model=newAuthor;
            CreateAuthorCommandValidation validations = new();
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpPut("id")]
        public IActionResult UpdateAuthor([FromBody]UpdateAuthorViewModel updatedAuthor, int id)
        {
            UpdateAuthorCommandValidator validations=new();
            UpdateAuthorCommand command = new(_context);
            command.AuthorId=id;
            command.Model=updatedAuthor;
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new(_context);
            DeleteAuthorCommandValidator validation= new();
            command.AuthorId=id;
            validation.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }

    }
}