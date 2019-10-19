using Library.API.Entities;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Library.API.Helpers;
using AutoMapper;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Library.API.Controllers
{
    [Route("api/authors")] // [Route("api/[controller]")] to include the underlying controller class name
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult getAuthors()
        {
            var authors = libraryRepository.GetAuthors();
            /*var taperedAuthors = new List<AuthorDto>();

            foreach (var author in authors)
            {
                taperedAuthors.Add(new AuthorDto
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    Genere = author.Genre,
                    Age = author.DateOfBirth.GetCurrentAge()
                });

            }*/
            var taperedAuthors = Mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(taperedAuthors);
        }

        [HttpGet("{id}", Name ="getAuthor")]
        public IActionResult getAuthor(Guid id)
        {
            var authorFromRepo = libraryRepository.GetAuthor(id);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var author = Mapper.Map<AuthorDto>(authorFromRepo);

            return Ok(author);
        }
        [HttpPost]
        public IActionResult createAuthor([FromBody] AuthorCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var authorEntity = Mapper.Map<Author>(author);

            libraryRepository.AddAuthor(authorEntity);

            if (!libraryRepository.Save())
            {
                return StatusCode(500, "Error in creating Author");
            }

            var authorDto = Mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("getAuthor", new { id = authorDto.Id}, authorDto);
        }
    }
}
