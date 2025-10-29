using System.Collections.Generic;
using System.Threading.Tasks;
using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace BugStore.Endpoints.Customers;

public class GetCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("GetCustomers")
            .WithDescription("Lista todos os clientes")
            .WithOrder(4)
            .Produces<PagedResponse<List<Customer>>>();
    
    private static async Task<IResult> HandleAsync(
        ICustomerHandler handler,
        int pageNumber = Configuration.DefaultCurrentPage,
        int pageSize = Configuration.DefaultPageSize)
    {
        var result = await handler.GetAllAsync(pageNumber, pageSize);
        return result.IsSuccess 
            ? Results.Ok(result) 
            : Results.BadRequest(result.Data);
    }
}