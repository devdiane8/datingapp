using System;

namespace API.Helpers;

public class PagingParams
{

    private const int MaxPageSize = 10;
    public int PageNumber { get; set; } = 1;
    private int _PageSize = 10;

    public int PageSize
    {
        get => _PageSize;
        set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;

    }
}
