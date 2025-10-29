namespace BugStore.Requests.Customers;

public class Create : Request
{ 
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; } = new DateTime();
}