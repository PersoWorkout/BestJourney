using Domain.Journeys;
using Domain.Orders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Users;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
    public string? CompanyName { get; set; }
    public string? WebsiteName { get; set; }
    public string? WebsiteUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public List<Order>? Orders { get; set; }
    [JsonIgnore]
    public List<Journey>? Journeys { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public User(string firstname, string lastname, string email, string password, string phone)
    {
        Id = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        Phone = phone;
        Role = UserRole.Customer;
        CreatedAt = DateTime.Now;
    }

    public User(string firstname, string lastname, string email, string password, string phone, string companyName, string websiteName, string websiteUrl)
    {
        Id = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        Phone = phone;
        Role = UserRole.Supplier;
        CompanyName = companyName;
        WebsiteName = websiteName;
        WebsiteUrl = websiteUrl;
        CreatedAt = DateTime.Now;
    }

    public void UpdateCustomer(
        string firstname = "", 
        string lastname = "", 
        string email = "", 
        string password = "", 
        string phone = "")
    {
        if (!string.IsNullOrEmpty(firstname))
            Firstname = firstname;
        if (!string.IsNullOrEmpty(lastname))
            Lastname = lastname;
        if (!string.IsNullOrEmpty(email))
            Email = email;
        if (!string.IsNullOrEmpty(password))
            Password = password;
        if (!string.IsNullOrEmpty(phone))
            Phone = phone;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateSupplier(
        string firstname = "",
        string lastname = "",
        string email = "",
        string password = "",
        string phone = "",
        string companyName = "",
        string websiteName = "",
        string websiteUrl = "")
    {
        if (!string.IsNullOrEmpty(firstname))
            Firstname = firstname;
        if (!string.IsNullOrEmpty(lastname))
            Lastname = lastname;
        if (!string.IsNullOrEmpty(email))
            Email = email;
        if (!string.IsNullOrEmpty(password))
            Password = password;
        if (!string.IsNullOrEmpty(phone))
            Phone = phone;
        if (!string.IsNullOrEmpty(companyName))
            CompanyName = companyName;
        if (!string.IsNullOrEmpty(websiteName))
            WebsiteName = websiteName;
        if (!string.IsNullOrEmpty(websiteUrl))
            WebsiteUrl = websiteUrl;
        UpdatedAt = DateTime.Now;
    }
}
