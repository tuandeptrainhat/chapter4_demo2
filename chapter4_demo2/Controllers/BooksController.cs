using chapter4_demo2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace chapter4_demo2.Controllers
{
    public class BooksController : ODataController
    {
        private readonly BookStoreContext _db;

        public BooksController(BookStoreContext context)
        {
            _db = context;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (_db.Books.Count() == 0)
            {
                foreach (var b in chapter4_demo2.DataSource.GetBooks())
                {
                    _db.Books.Add(b);
                    _db.Presses.Add(b.Press);
                }
                _db.SaveChanges();
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == key);
            return book == null ? NotFound() : Ok(book);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == key);
            if (book == null)
                return NotFound();

            _db.Books.Remove(book);
            _db.SaveChanges();
            return Ok();
        }
    }
}
