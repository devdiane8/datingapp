using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize]
    public class MembersController(AppDBContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return new ActionResult<IEnumerable<UserDto>>(members.Select(member => new UserDto
            {
                Id = member.Id,
                DisplayName = member.DisplayName,
                Email = member.Email,
                Token = string.Empty // Token will be handled separately
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);
            return member != null ? Ok(member) : NotFound();
        }


    }
}
