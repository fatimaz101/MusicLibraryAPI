using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Versioning;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    
    public class SongsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<SongsController>
        [HttpGet]
        public IActionResult Get()
        {

            var songs = _context.Songs.ToList();

             return StatusCode(200,songs);
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var song = _context.Songs.Where(sng => sng.Id == id).FirstOrDefault();

            if (song != null)
            {

                return StatusCode(200, song);
            }
            else
            {
                return NotFound();
            }

           
        }

        // POST api/<SongsController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {

            _context.Add(song);
            _context.SaveChanges();
            return StatusCode(201, song);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {


            var updatedSong = _context.Songs.Where(sng => sng.Id == id).First();
            

           
            try
            {
                

                updatedSong.Title = song.Title;
                updatedSong.Artist = song.Artist;
                updatedSong.Album = song.Album;
                updatedSong.ReleaseDate = song.ReleaseDate;
                updatedSong.Genre = song.Genre;


              

                _context.SaveChanges();
                return StatusCode(200, song);
            }
            catch
            {
                 return NotFound();
            }

        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _context.Songs.Where(sng => sng.Id == id).First();
            _context.Songs.Remove(song);
            _context.SaveChanges();
            return StatusCode(200, NotFound());

        }
    }
}
