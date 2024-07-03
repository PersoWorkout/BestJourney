namespace Domain.Users.Requests;

public class UpdateSupplierRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string CompanyName { get; set; }
    public string WebsiteName { get; set; }
    public string WebsiteUrl { get; set; }
}
