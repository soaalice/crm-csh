namespace crm_csh.Models;

public class DashboardDto
{
    public int TotalCustomers { get; set; } = 0;
    public decimal TotalLeadExpenses { get; set; } = 0;
    public decimal TotalTicketExpenses { get; set; } = 0;
    public decimal TotalBudget { get; set; } = 0;
}