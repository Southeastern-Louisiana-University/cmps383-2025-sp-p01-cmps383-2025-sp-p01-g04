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

        // ✅ CREATE A NEW THEATER (WITH FIXED NULL HANDLING)
        [HttpPost]
        public IActionResult CreateTheater([FromBody] TheaterCreateDto createDto)
        {
            var errors = new List<string>();

            // Validate Name
            if (string.IsNullOrWhiteSpace(createDto.Name) || createDto.Name.Length > 120)
            {
                errors.Add("Name must be between 1 and 120 characters.");
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(createDto.Address))
            {
                errors.Add("Address cannot be empty.");
            }

            // ✅ Validate SeatCount
            if (!createDto.SeatCount.HasValue || createDto.SeatCount <= 0)
            {
                errors.Add("A theater must have at least one seat.");
            }

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation failed.", errors });
            }

            // ✅ Fixed null assignments
            var newTheater = new Theater
            {
                Name = createDto.Name?.Trim() ?? "Default Name",
                Address = createDto.Address?.Trim() ?? "Unknown Address",
                SeatCount = createDto.SeatCount.GetValueOrDefault(1) // Ensures SeatCount is never null
            };

            _dataContext.Theaters.Add(newTheater);
            _dataContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newTheater.Id }, new TheaterGetDto
            {
                Id = newTheater.Id,
                Name = newTheater.Name,
                Address = newTheater.Address,
                SeatCount = newTheater.SeatCount
            });
        }

        // ✅ UPDATE EXISTING THEATER (WITH FIXED NULL HANDLING)
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

            // ✅ Validate SeatCount
            if (!updateDto.SeatCount.HasValue || updateDto.SeatCount <= 0)
            {
                errors.Add("A theater must have at least one seat.");
            }

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation failed.", errors });
            }

            // ✅ Fixed null handling
            theaterToUpdate.Name = updateDto.Name?.Trim() ?? theaterToUpdate.Name;
            theaterToUpdate.Address = updateDto.Address?.Trim() ?? theaterToUpdate.Address;
            theaterToUpdate.SeatCount = updateDto.SeatCount.GetValueOrDefault(theaterToUpdate.SeatCount);

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
