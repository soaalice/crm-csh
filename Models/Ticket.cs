namespace crm_csh.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }

    public string Status { get; set; }
    public string Priority { get; set; }
    public User Manager { get; set; }
    public User Employee { get; set; }

    public Customer Customer { get; set; }

    public DateTime CreatedAt { get; set; }
    public decimal? ExpenseAmount { get; set; }

    public Ticket()
    {
    }

    public Ticket(string subject, string description, string status, string priority, User manager, User employee, Customer customer, DateTime createdAt)
    {
        Subject = subject;
        Description = description;
        Status = status;
        Priority = priority;
        Manager = manager;
        Employee = employee;
        Customer = customer;
        CreatedAt = createdAt;
    }

    public Ticket(string subject, string description, string status, string priority, User manager, User employee, Customer customer, DateTime createdAt, decimal? expenseAmount)
    {
        Subject = subject;
        Description = description;
        Status = status;
        Priority = priority;
        Manager = manager;
        Employee = employee;
        Customer = customer;
        CreatedAt = createdAt;
        ExpenseAmount = expenseAmount;
    }
}