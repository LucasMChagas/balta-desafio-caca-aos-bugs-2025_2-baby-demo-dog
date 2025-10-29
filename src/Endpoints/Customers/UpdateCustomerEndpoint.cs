using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;

namespace BugStore.Endpoints.Customers;

public class UpdateCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("UpdateCustomer")
            .WithDescription("Atualiza um cliente")
            .WithOrder(2)
            .Produces<Response<Customer?>>();
    
    private static async Task<IResult> HandleAsync(ICustomerHandler handler, Update request, Guid id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? Results.Ok(result.Data) 
            : Results.BadRequest(result.Data);
    }
   
}