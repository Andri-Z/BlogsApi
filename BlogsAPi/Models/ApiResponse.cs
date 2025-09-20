using System;

namespace BlogsAPi.Models;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public int Total { get; set; }
    public ApiResponse(int total, Pagination? pag, T data)
    {
        Data = data;
        Page = pag?.Page;
        Limit = pag?.Limit;
        Total = total;
    }
}
