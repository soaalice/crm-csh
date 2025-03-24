namespace crm_csh.Models;

public class Lead
{
    public int LeadId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public Customer Customer { get; set; }
    public decimal? ExpenseAmount { get; set; }

    public Lead(){}

    
}