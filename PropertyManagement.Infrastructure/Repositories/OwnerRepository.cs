using MongoDB.Bson;
using MongoDB.Driver;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;

namespace PropertyManagement.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _owners;

    public OwnerRepository(MongoDbContext context)
    {
        _owners = context.Owners;
    }

    public async Task<Owner> GetByIdAsync(ObjectId id)
    {
        return await _owners
            .Find(o => o.IdOwner == id)
            .FirstOrDefaultAsync();
    }
}