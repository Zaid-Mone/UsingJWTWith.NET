using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingJWT.Data;
using UsingJWT.Models;
using UsingJWT.Validation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsingJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataDbContext _context;

        public BookController(DataDbContext context)
        {
            _context = context;
        }

        // GET: api/<BookController>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var book = _context.Books.ToList();
            return book;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Book> Post([FromBody] Book value)
        {
            value.Id = new Guid();
            // Fluent Validate
            var validator = new BookValidator();
            var result = validator.Validate(value);
            if (result.IsValid)
            {
                _context.Books.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    return BadRequest(new { message =item});
                }
                return BadRequest();
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
