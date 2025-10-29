using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;

namespace BugStore.Handlers.Contracts;

public interface ICustomerHandler
{
    Task<Response<Customer?>> CreateAsync(Create request);
    Task<Response<Customer?>> UpdateAsync(Update request);
    Task<Response<Customer?>> DeleteAsync(Guid id);
    Task<Response<Customer?>> GetByIdAsync(Guid id);
    Task<PagedResponse<List<Customer>>> GetAllAsync(int pageNumber, int pageSize);
}