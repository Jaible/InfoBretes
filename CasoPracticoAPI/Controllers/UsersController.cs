using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPracticoAPI.Context;
using CasoPracticoAPI.Models;
using CasoPracticoAPI.DTO;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/users/login
        [HttpPost("login")]
        public ActionResult<User> Login(LoginDto user)
        {
            var response = _context.Users.Where(f => f.Name == user.Name)
                                         .Where(f => f.Password == user.Password)
                                         .FirstOrDefault();

            return response == null ? NotFound() : Ok(response);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
