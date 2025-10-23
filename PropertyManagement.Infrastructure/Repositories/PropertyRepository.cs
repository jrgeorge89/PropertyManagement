using MongoDB.Bson;
using MongoDB.Driver;
using PropertyManagement.Domain.Common;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;

namespace PropertyManagement.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _properties;
    private readonly IMongoCollection<PropertyImage> _propertyImages;

    public PropertyRepository(MongoDbContext context)
    {
        _properties = context.Properties;
        _propertyImages = context.PropertyImages;
    }

    public async Task<IEnumerable<Property>> GetAllAsync(IPropertyFilter filter)
    {
        var builder = Builders<Property>.Filter;
        var filterDefinition = builder.Empty;

        // Aplicar filtros si hay datos
        if (!string.IsNullOrEmpty(filter.Name))
        {
            filterDefinition = filterDefinition & builder.Regex(p => p.Name, new BsonRegularExpression(filter.Name, "i"));
        }

        if (!string.IsNullOrEmpty(filter.Address))
        {
            filterDefinition = filterDefinition & builder.Regex(p => p.Address, new BsonRegularExpression(filter.Address, "i"));
        }

        if (filter.MinPrice.HasValue)
        {
            filterDefinition = filterDefinition & builder.Gte(p => p.Price, filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            filterDefinition = filterDefinition & builder.Lte(p => p.Price, filter.MaxPrice.Value);
        }

        return await _properties
            .Find(filterDefinition)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Limit(filter.PageSize)
            .ToListAsync();
    }

    public async Task<Property> GetByIdAsync(ObjectId id)
    {
        return await _properties
            .Find(p => p.IdProperty == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PropertyImage>> GetPropertyImagesAsync(string propertyId)
    {
        return await _propertyImages
            .Find(pi => pi.IdProperty == propertyId)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(IPropertyFilter filter)
    {
        var builder = Builders<Property>.Filter;
        var filterDefinition = builder.Empty;

        // Aplicamos los mismos filtros que en GetAllAsync
        if (!string.IsNullOrEmpty(filter.Name))
        {
            filterDefinition = filterDefinition & builder.Regex(p => p.Name, new BsonRegularExpression(filter.Name, "i"));
        }

        if (!string.IsNullOrEmpty(filter.Address))
        {
            filterDefinition = filterDefinition & builder.Regex(p => p.Address, new BsonRegularExpression(filter.Address, "i"));
        }

        if (filter.MinPrice.HasValue)
        {
            filterDefinition = filterDefinition & builder.Gte(p => p.Price, filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            filterDefinition = filterDefinition & builder.Lte(p => p.Price, filter.MaxPrice.Value);
        }

        return (int)await _properties
            .CountDocumentsAsync(filterDefinition);
    }
}