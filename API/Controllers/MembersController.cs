using System.Security.Claims;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Extentions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize]
    public class MembersController(IMemberRepository memberRepository, IPhotoService photoService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers([FromQuery] MemberParams memberParams)

        {
            memberParams.CurrentMemberId = User.GetMemberId();
            return Ok(await memberRepository.GetAllMembersAsync(memberParams));

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
        [HttpPost("add-photo")]
        public async Task<ActionResult<Photo>> AddPhoto([FromForm] IFormFile file)
        {
            var member = await memberRepository.GetMemberForupdate(User.GetMemberId());
            if (member == null)
            {
                return BadRequest("Connot Update member");
            }
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                MemberId = member.Id
            };
            if (member.ImageUrl == null)
            {
                member.ImageUrl = photo.Url;
                member.User.ImageUrl = photo.Url;

            }
            member.Photos.Add(photo);
            await memberRepository.SaveAllAsync();
            return photo;




        }
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var member = await memberRepository.GetMemberForupdate(User.GetMemberId());
            if (member == null)
            {
                return BadRequest("Connot Update member");
            }
            var photo = member.Photos.SingleOrDefault(x => x.Id == photoId);
            if (photo == null || member.ImageUrl == photo?.Url)
            {
                return BadRequest("Cannot set it to be the main image");
            }
            member.ImageUrl = photo?.Url;
            member.User.ImageUrl = photo?.Url;

            if (await memberRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update member");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var member = await memberRepository.GetMemberForupdate(User.GetMemberId());
            if (member == null)
            {
                return BadRequest("Connot Update member");
            }
            var photo = member.Photos.SingleOrDefault(x => x.Id == photoId);
            if (photo == null || member.ImageUrl == photo?.Url)
            {
                return BadRequest("this photo can not be deleted");
            }
            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }
            }
            member.Photos.Remove(photo);

            if (await memberRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Problem deleting the photo");
        }
    }


}


