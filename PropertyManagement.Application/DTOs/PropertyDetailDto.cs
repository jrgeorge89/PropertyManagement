namespace PropertyManagement.Application.DTOs;

public class PropertyDetailDto
{
    public string IdProperty { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Price { get; set; }
    public string CodeInternal { get; set; }
    public int YearBuilt { get; set; }
    public string? Type { get; set; }
    public string? Zone { get; set; }
    public string? City { get; set; }
    public OwnerDto Owner { get; set; }
    public List<ImageDto> Images { get; set; }

    public PropertyDetailDto()
    {
        IdProperty = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        Price = 0;
        CodeInternal = string.Empty;
        YearBuilt = 0;
        Type = null;
        Zone = null;
        City = null;
        Owner = new OwnerDto();
        Images = new List<ImageDto>();
    }

    public PropertyDetailDto(string idProperty, string name, string address, decimal price,
        string codeInternal, int year, OwnerDto owner, List<ImageDto> images,
        string? type = null, string? zone = null, string? city = null)
    {
        IdProperty = idProperty;
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        YearBuilt = year;
        Type = type;
        Zone = zone;
        City = city;
        Owner = owner;
        Images = images ?? new List<ImageDto>();
    }
}