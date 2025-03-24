namespace crm_csh.Models;

public class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Position { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string Description { get; set; }

    public string Twitter { get; set; }

    public string Facebook { get; set; }

    public string Youtube { get; set; }

    public User User { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Customer()
    {
    }

    public Customer(string name, string email, string position, string phone, string address, string city, string state, string country,
                    string description, string twitter, string facebook, string youtube, 
                    User user,
                    DateTime? createdAt)
    {
        Name = name;
        Email = email;
        Position = position;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        Country = country;
        Description = description;
        Twitter = twitter;
        Facebook = facebook;
        Youtube = youtube;
        User = user;
        CreatedAt = createdAt;
    }
}