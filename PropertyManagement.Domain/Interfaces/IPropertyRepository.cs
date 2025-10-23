using MongoDB.Bson;
using PropertyManagement.Domain.Common;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IPropertyRepository
{
    /// <summary>
    /// Obtiene todas las propiedades aplicando los filtros especificados
    /// </summary>
    Task<IEnumerable<Property>> GetAllAsync(IPropertyFilter filter);

    /// <summary>
    /// Obtiene una propiedad por su ID
    /// </summary>
    Task<Property> GetByIdAsync(ObjectId id);

    /// <summary>
    /// Obtiene las imágenes asociadas a una propiedad
    /// </summary>
    Task<IEnumerable<PropertyImage>> GetPropertyImagesAsync(string propertyId);

    /// <summary>
    /// Obtiene el número total de propiedades que coinciden con los filtros
    /// </summary>
    Task<int> GetTotalCountAsync(IPropertyFilter filter);
}