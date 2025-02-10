using Microsoft.AspNetCore.Mvc;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Entities;
using Selu383.SP25.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Selu383.SP25.Api.Controllers
{
    [ApiController]
    [Route("/api/theaters")]
    public class TheaterController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TheaterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // ✅ LIST ALL THEATERS
        [HttpGet]
        public IActionResult GetAll()
        {
            var theaters = _dataContext.Theaters
                .Select(t => new TheaterGetDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Address = t.Address,
                    SeatCount = t.SeatCount
                }).ToList();

            return Ok(theaters);
        }

        // ✅ GET THEATER BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var theater = _dataContext.Theaters
                .Where(t => t.Id == id)
                .Select(t => new TheaterGetDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Address = t.Address,
                    SeatCount = t.SeatCount
                })
                .FirstOrDefault();

            if (theater == null)
            {
                return NotFound(new { message = "Theater not found." });
            }

            return Ok(theater);
        }

        // ✅ CREATE A NEW THEATER
        [HttpPost]
        public IActionResult CreateTheater([FromBody] TheaterCreateDto createDto)
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(createDto.Name) || createDto.Name.Length > 120)
            {
                return BadRequest(new { message = "Name must be between 1 and 120 characters." });
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(createDto.Address))
            {
                return BadRequest(new { message = "Address cannot be empty." });
            }

            // ✅ Create & Save the new theater
            var newTheater = new Theater
            {
                Name = createDto.Name,
                Address = createDto.Address,
                SeatCount = createDto.SeatCount
            };

            _dataContext.Theaters.Add(newTheater);
            _dataContext.SaveChanges();

            // Return the new theater with 201 Created
            return CreatedAtAction(nameof(GetById), new { id = newTheater.Id }, new TheaterGetDto
            {
                Id = newTheater.Id,
                Name = newTheater.Name,
                Address = newTheater.Address,
                SeatCount = newTheater.SeatCount
            });
        }

        // ✅ UPDATE EXISTING THEATER
        [HttpPut("{id}")]
        public IActionResult UpdateTheater([FromBody] TheaterUpdateDto updateDto, int id)
        {
            var theaterToUpdate = _dataContext.Theaters.Find(id);

            if (theaterToUpdate == null)
            {
                return NotFound(new { message = "Theater not found." });
            }

            var errors = new List<string>();

            // Validate Name
            if (string.IsNullOrWhiteSpace(updateDto.Name) || updateDto.Name.Length > 120)
            {
                errors.Add("Name must be between 1 and 120 characters.");
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(updateDto.Address))
            {
                errors.Add("Address cannot be empty.");
            }

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation failed.", errors });
            }

            theaterToUpdate.Name = updateDto.Name;
            theaterToUpdate.Address = updateDto.Address;
            theaterToUpdate.SeatCount = updateDto.SeatCount;

            _dataContext.SaveChanges();

            return Ok(new TheaterGetDto
            {
                Id = theaterToUpdate.Id,
                Name = theaterToUpdate.Name,
                Address = theaterToUpdate.Address,
                SeatCount = theaterToUpdate.SeatCount
            });
        }

        // ✅ DELETE A THEATER
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var theater = _dataContext.Theaters.Find(id);

            if (theater == null)
            {
                return NotFound(new { message = "Theater not found." });
            }

            _dataContext.Theaters.Remove(theater);
            _dataContext.SaveChanges();

            return Ok(new { message = "Theater deleted successfully." });
        }
    }
}
