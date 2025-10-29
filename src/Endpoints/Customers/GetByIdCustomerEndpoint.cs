using System;
using System.Threading.Tasks;
using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace BugStore.Endpoints.Customers;

public class GetByIdCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("GetCustomerById")
            .WithDescription("Obtém um cliente pelo ID")
            .WithOrder(5)
            .Produces<Response<Customer?>>();
    
    private static async Task<IResult> HandleAsync(
        ICustomerHandler handler,
        Guid id)
    {
        var result = await handler.GetByIdAsync(id);
        return result.IsSuccess 
            ? Results.Ok(result.Data) 
            : Results.NotFound(result.Data);
    }
}