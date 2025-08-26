using System;
using System.Security.Claims;

namespace API.Extentions;

public static class ClaimPrincipleExtention
{
    public static string GetMemberId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("No member id found");
    }

}
