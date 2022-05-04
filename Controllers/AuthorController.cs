using AutoMapper;
using BookStore.Applications.AuthorOperations.Command.CreateAuthor;
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
            var result = detail.Handler();
            validations.ValidateAndThrow(detail);
            return Ok(result);
        }
        [HttpPost("id")]
        public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor, int id)
        {
            CreateAuthorCommand command = new(_context,_mapper);
            command.AuthorId=id;
            command.Model=newAuthor;
            CreateAuthorCommandValidation validations = new();
            command.Handler();
            validations.ValidateAndThrow(command);
            return Ok();
        }

    }
}