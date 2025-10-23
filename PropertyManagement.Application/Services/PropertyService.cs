using MongoDB.Bson;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Interfaces;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Domain.Exceptions;

namespace PropertyManagement.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IOwnerRepository _ownerRepository;

    public PropertyService(IPropertyRepository propertyRepository, IOwnerRepository ownerRepository)
    {
        _propertyRepository = propertyRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<PropertyListDto>> GetPropertiesAsync(PropertyFilterDto filter)
    {
        var properties = await _propertyRepository.GetAllAsync(filter);
        var propertyDtos = new List<PropertyListDto>();

        foreach (var property in properties)
        {
            var owner = await _ownerRepository.GetByIdAsync(property.IdOwner);
            var images = await _propertyRepository.GetPropertyImagesAsync(property.IdProperty.ToString());
            var firstImage = images.FirstOrDefault();

            propertyDtos.Add(new PropertyListDto(
                property.IdProperty.ToString(),
                property.Name,
                property.Address,
                property.Price,
                firstImage?.Url,
                owner?.Name ?? "Sin propietario"
            ));
        }

        return propertyDtos;
    }

    public async Task<PropertyDetailDto> GetPropertyByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId propertyId))
        {
            throw new ArgumentException("ID de propiedad inválido", nameof(id));
        }

        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property == null)
        {
            throw new PropertyNotFoundException(id);
        }

        var owner = await _ownerRepository.GetByIdAsync(property.IdOwner);
        var images = await _propertyRepository.GetPropertyImagesAsync(property.IdProperty.ToString());

        var ownerDto = new OwnerDto(
            owner?.IdOwner.ToString() ?? string.Empty,
            owner?.Name ?? string.Empty,
            owner?.Address ?? string.Empty,
            owner?.Photo,
            owner?.BirthDate ?? DateTime.UtcNow
        );

        var imageDtos = images.Select(img => new ImageDto(
            img.IdPropertyImage.ToString(),
            img.Url
        )).ToList();

        return new PropertyDetailDto(
            property.IdProperty.ToString(),
            property.Name,
            property.Address,
            property.Price,
            property.CodeInternal,
            property.YearBuilt,
            ownerDto,
            imageDtos,
            property.Type,
            property.Zone,
            property.City
        );
    }

    public async Task<int> GetTotalPropertiesCountAsync(PropertyFilterDto filter)
    {
        return await _propertyRepository.GetTotalCountAsync(filter);
    }
}