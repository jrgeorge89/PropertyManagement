namespace PropertyManagement.Domain.Common;

public interface IPropertyFilter
{
    string? Name { get; }
    string? Address { get; }
    decimal? MinPrice { get; }
    decimal? MaxPrice { get; }
    int PageNumber { get; }
    int PageSize { get; }
}