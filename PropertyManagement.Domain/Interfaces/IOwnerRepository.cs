using MongoDB.Bson;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IOwnerRepository
{
    /// <summary>
    /// Obtiene un propietario por su ID
    /// </summary>
    Task<Owner> GetByIdAsync(ObjectId id);
}