using System.Security.Claims;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Extentions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize]
    public class MembersController(IMemberRepository memberRepository) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepository.GetAllMembersAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);
            return member != null ? Ok(member) : NotFound();
        }

        [HttpGet("{memberId}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(string memberId)
        {
            return Ok(await memberRepository.GetMemberPhotosAsync(memberId));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMember([FromBody] MemberUpdateDto memberUpdateDto)
        {
            var memberId = User.GetMemberId();
            var member = await memberRepository.GetMemberForupdate(memberId);
            if (member == null)
            {
                return BadRequest("Opps! No member found ");
            }
            member.DisplayName = memberUpdateDto.DisplayName ?? member.DisplayName;
            member.Description = memberUpdateDto.Description ?? member.Description;
            member.City = memberUpdateDto.City ?? member.City;
            member.Country = memberUpdateDto.Country ?? member.Country;
            member.User.DisplayName = memberUpdateDto.DisplayName ?? member.User.DisplayName;
            memberRepository.UpdateMember(member);
            if (await memberRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update member");
        }

    }
}
