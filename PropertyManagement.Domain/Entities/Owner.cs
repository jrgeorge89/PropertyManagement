using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Domain.Entities;

public class Owner
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdOwner { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [BsonElement("name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La dirección es requerida")]
    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("photo")]
    public string? Photo { get; set; }

    [BsonElement("birthDate")]
    public DateTime BirthDate { get; set; }

    public Owner(string name, string address, string? photo = null)
    {
        IdOwner = ObjectId.GenerateNewId();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Photo = photo;
        BirthDate = DateTime.UtcNow;
    }

    public Owner()
    {
        IdOwner = ObjectId.GenerateNewId();
        Name = string.Empty;
        Address = string.Empty;
        Photo = null;
        BirthDate = DateTime.UtcNow;
    }
}