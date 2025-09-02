using System;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IMemberRepository
{


    void UpdateMember(Member member);
    Task<bool> SaveAllAsync();
    Task<PaginatedResult<Member>> GetAllMembersAsync( MemberParams memberParams);
    Task<Member?> GetMemberByIdAsync(string id);

    Task<IReadOnlyList<Photo>> GetMemberPhotosAsync(string memberId);

    Task<Member?> GetMemberForupdate(string id);




}
