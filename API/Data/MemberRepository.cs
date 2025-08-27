using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRepository(AppDBContext context) : IMemberRepository
{
    public async Task<IReadOnlyList<Member>> GetAllMembersAsync()
    {
        return await context.Members.ToListAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await context.Members
            .FirstOrDefaultAsync(x => x.Id.ToLower() == id.ToLower());
    }

    public async Task<Member?> GetMemberForupdate(string id)
    {
        return await context.Members
            .Include(x => x.User)
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Photo>> GetMemberPhotosAsync(string memberId)
    {
        return await context.Members.Where(x => x.Id == memberId)
             .SelectMany(x => x.Photos)
             .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;

    }

    public void UpdateMember(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }
}
