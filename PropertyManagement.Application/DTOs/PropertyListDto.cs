namespace PropertyManagement.Application.DTOs;

public class PropertyListDto
{
    public string IdProperty { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string OwnerName { get; set; }

    public PropertyListDto(string idProperty, string name, string address, decimal price, string? imageUrl, string ownerName)
    {
        IdProperty = idProperty;
        Name = name;
        Address = address;
        Price = price;
        ImageUrl = imageUrl;
        OwnerName = ownerName;
    }

    public PropertyListDto()
    {
        IdProperty = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        Price = 0;
        ImageUrl = null;
        OwnerName = string.Empty;
    }
}