using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;

namespace BugStore.Endpoints.Customers;

public class DeleteCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("DeleteCustomer")
            .WithDescription("Remove um cliente")
            .WithOrder(3)
            .Produces<Response<Customer?>>();
    
    private static async Task<IResult> HandleAsync(ICustomerHandler handler,  Guid id)
    {
        var result = await handler.DeleteAsync(id);
        return result.IsSuccess 
            ? Results.Ok(result.Data) 
            : Results.BadRequest(result.Data);
    }
}