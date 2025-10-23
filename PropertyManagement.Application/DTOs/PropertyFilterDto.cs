using System.ComponentModel.DataAnnotations;
using PropertyManagement.Domain.Common;

namespace PropertyManagement.Application.DTOs;

public class PropertyFilterDto : IPropertyFilter
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El número de página debe ser mayor a 0")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "El tamaño de página debe estar entre 1 y 100")]
    public int PageSize { get; set; } = 10;

    public PropertyFilterDto()
    {
    }

    public PropertyFilterDto(string? name, string? address, decimal? minPrice, decimal? maxPrice, int pageNumber = 1, int pageSize = 10)
    {
        Name = name;
        Address = address;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}