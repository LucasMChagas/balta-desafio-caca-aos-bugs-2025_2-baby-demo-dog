using BugStore.Models;
using BugStore.Responses;

namespace BugStore.Requests.Customers;

public class Get : Request
{
    public Get(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
}