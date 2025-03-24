namespace crm_csh.Models;

public class DashboardDto
{
    public int TotalCustomers { get; set; } = 0;
    public decimal TotalLeadExpenses { get; set; } = 0;
    public decimal TotalTicketExpenses { get; set; } = 0;
    public decimal TotalBudget { get; set; } = 0;
    public Dictionary<string, long> TicketByStatus { get; set; } = new Dictionary<string, long>();
    public Dictionary<string, long> LeadByStatus { get; set; } = new Dictionary<string, long>();
}