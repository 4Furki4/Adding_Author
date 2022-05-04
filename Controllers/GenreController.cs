using System.Collections.Generic;
using AutoMapper;
using BookStore.Applications.GenreOperations.Commands.CreateGenre;
using BookStore.Applications.GenreOperations.Commands.DeleteGenre;
using BookStore.Applications.GenreOperations.Commands.UpdateGenre;
using BookStore.Applications.GenreOperations.Queries.GetGenre;
using BookStore.Applications.GenreOperations.Queries.GetGenreDetail;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new(_context, _mapper);
            var genres = query.Handler();
            return Ok(genres);
        }

        [HttpGet("id")]
        public ActionResult GetGenreDetails(int id)
        {
            GetGenreDetailsQuery query = new(_context, _mapper);
            query.GenreId=id;
            GetGenreDetailsQueryValidator validations = new();
            validations.ValidateAndThrow(query);
            var genre=query.Handler();
            return Ok(genre);
        }
        [HttpPost]
        public IActionResult CreateGenres([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new(_context);
            command.Model=newGenre;
            CreateGenreCommandValidator validations = new();
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpPut("id")]
        public IActionResult UpdateGenres([FromBody] UpdateGenreModel updateGenre, int id)
        {
            UpdateGenreCommand command = new(_context);
            command.GenreId=id;
            UpdateGenreCommandValidator validations = new();
            command.Model=updateGenre;
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteGenres(int id)
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId=id;
            DeleteGenreCommandValidator validations = new();
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
    }
}