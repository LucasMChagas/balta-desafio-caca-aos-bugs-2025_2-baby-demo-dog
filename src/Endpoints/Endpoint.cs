using BugStore.Endpoints.Customers;

namespace BugStore.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/customers")
            .WithTags("customers")
            .MapEndpoint<CreateCustomerEndpoint>()
            .MapEndpoint<UpdateCustomerEndpoint>()
            .MapEndpoint<DeleteCustomerEndpoint>()
            .MapEndpoint<GetCustomerEndpoint>()
            .MapEndpoint<GetByIdCustomerEndpoint>();
    }
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}