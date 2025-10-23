using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Interfaces;

public interface IPropertyService
{
    /// <summary>
    /// Obtiene un listado paginado de propiedades con información básica
    /// </summary>
    Task<IEnumerable<PropertyListDto>> GetPropertiesAsync(PropertyFilterDto filter);

    /// <summary>
    /// Obtiene el detalle completo de una propiedad por su ID
    /// </summary>
    Task<PropertyDetailDto> GetPropertyByIdAsync(string id);

    /// <summary>
    /// Obtiene el número total de propiedades que coinciden con los filtros
    /// </summary>
    Task<int> GetTotalPropertiesCountAsync(PropertyFilterDto filter);
}