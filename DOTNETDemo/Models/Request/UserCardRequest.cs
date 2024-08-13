namespace DOTNETDemo.Models.Request;

public class UserCardRequest
{
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public string? CVC { get; set; }
    public DateTime ExpiryDate { get; set; }
}
