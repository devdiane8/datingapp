using System;
using API.Entities;

namespace API.Interfaces;

public interface IMemberRepository
{


    void UpdateMember(Member member);
    Task<bool> SaveAllAsync();
    Task<IReadOnlyList<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(string id);

    Task<IReadOnlyList<Photo>> GetMemberPhotosAsync(string memberId);

    Task<Member?> GetMemberForupdate(string id);




}
