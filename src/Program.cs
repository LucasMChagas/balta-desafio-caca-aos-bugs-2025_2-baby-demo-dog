using BugStore.Data;
using BugStore.Endpoints;
using BugStore.Handlers.Contracts;
using Microsoft.EntityFrameworkCore;
using CreateCustomerRequest = BugStore.Requests.Customers.Create;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? String.Empty;
builder.Services.AddScoped<ICustomerHandler, BugStore.Handlers.Customers.Handler>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

app.MapEndpoints();
app.MapGet("/", () => "Hello World!");

app.MapGet("/v1/products", () => "Hello World!");
app.MapGet("/v1/products/{id}", () => "Hello World!");
app.MapPost("/v1/products", () => "Hello World!");
app.MapPut("/v1/products/{id}", () => "Hello World!");
app.MapDelete("/v1/products/{id}", () => "Hello World!");

app.MapGet("/v1/orders/{id}", () => "Hello World!");
app.MapPost("/v1/orders", () => "Hello World!");

app.Run();
