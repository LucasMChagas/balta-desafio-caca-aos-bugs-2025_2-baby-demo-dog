using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;

namespace BugStore.Endpoints.Customers;

public class CreateCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapPost("/", HandleAsync )
            .WithName("Create Customer")
            .WithSummary("Cria um novo Cliente.")
            .WithOrder(1)
            .Produces<Response<Customer?>>();
    private static async Task<IResult> HandleAsync(ICustomerHandler handler, Create request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? Results.Created($"{result.Data?.Id}", result.Data ) 
            : Results.BadRequest();
    }
}