using System.Text.Json;
using mywebapi2.Models;
using Microsoft.AspNetCore.Mvc;

namespace mywebapi2.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController: ControllerBase
{
    private static List<Books> _books = new List<Books>();
    public BooksController()
    {
        if (_books.Count != 0) return;
        _books.Add(new Books { Id = 1, Title = "Book 1", Description = "Book" });
        _books.Add(new Books { Id = 2, Title = "Book 2", Description = "Book" });
    }
    
    [HttpGet("")]
    public IActionResult GetAllBooks()
    {
        return Ok(_books);
    }
    
    [HttpPost("add")]
    public IActionResult AddBook([FromBody] Books newBook)
    {
        if (newBook == null)
        {
            return BadRequest("Invalid book data.");
        }

        newBook.Id = _books.Count + 1;
        _books.Add(newBook);
        return Ok(newBook);
    }
}