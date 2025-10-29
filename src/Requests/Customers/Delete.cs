namespace BugStore.Requests.Customers;

public class Delete : Request
{
    public Guid Id { get; set; }
}