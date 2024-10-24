namespace WebMVC.DTOs;

public class PlateBasicDto
{
    public Guid Id { get; set; }

    public string? Registration { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public int Status { get; set; }
}
