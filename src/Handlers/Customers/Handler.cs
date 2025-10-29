using BugStore.Data;
using BugStore.Handlers.Contracts;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class Handler(AppDbContext context) : ICustomerHandler
{
    public async Task<Response<Customer?>> CreateAsync(Create request)
    {
        try
        {
            var customer = new Customer()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                BirthDate = request.BirthDate,
            };
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return new Response<Customer?>(customer, 201, "Cliente criado com sucesso.");
        }
        catch
        {
            return new Response<Customer?>(null, 500, "Erro ao criar o cliente.");
        }
    }

    public async Task<Response<Customer?>> UpdateAsync(Update request)
    {
        try
        {
            var customer =
                await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (customer == null)
                return new Response<Customer?>(null, 404, "Cliente não encontrado!");

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.BirthDate = request.BirthDate;

            context.Customers.Update(customer);
            await context.SaveChangesAsync();

            return new Response<Customer?>(customer, message: "Cliente atualizado com sucesso!");
        }
        catch 
        {
            return new Response<Customer?>(null, 500, "Erro ao atualizar o cliente.");
        }
    }

    public async Task<Response<Customer?>> DeleteAsync(Guid id)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            
            if (customer == null)
                return new Response<Customer?>(null, 404, "Cliente não encontrado!");
            
            context.Customers.Remove(customer);
            context.SaveChanges();
            
            return new Response<Customer?>(customer, message: "Cliente deletado com sucesso!");
        }
        catch
        {
            return new Response<Customer?>(null, 500, "Erro ao excluír o cliente.");
        }
    }

    public async Task<Response<Customer?>> GetByIdAsync(Guid id)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            
            if (customer == null)
                return new Response<Customer?>(null, 404, "Cliente não encontrado!");
            
            return new Response<Customer?>(customer, message: "Cliente encontrado com sucesso!");
        }
        catch
        {
            return new Response<Customer?>(null, 500, "Erro ao buscar o cliente.");
        }
    }

    public async Task<PagedResponse<List<Customer>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            var skip = (pageNumber - 1) * pageSize;
            var query = context.Customers.AsNoTracking();
            
            var total = await query.CountAsync();
            var customers = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResponse<List<Customer>>(
                customers,
                total,
                pageNumber,
                pageSize);
        }
        catch
        {
            return new PagedResponse<List<Customer>>(
                null,
                500,
                "Erro ao buscar os clientes.");
        }
    }
}