using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Domain.Entities;

public class Property
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdProperty { get; set; }

    [BsonElement("code")]
    [Required(ErrorMessage = "El código es requerido")]
    public string Code { get; set; }

    [BsonElement("type")]
    public string? Type { get; set; }

    [BsonElement("zone")]
    public string? Zone { get; set; }

    [BsonElement("city")]
    public string? City { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [BsonElement("name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La dirección es requerida")]
    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "El código interno es requerido")]
    [BsonElement("codeInternal")]
    public string CodeInternal { get; set; }

    [BsonElement("yearBuilt")]
    public int YearBuilt { get; set; }

    [Required(ErrorMessage = "El ID del propietario es requerido")]
    [BsonElement("idOwner")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdOwner { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    public Property(string code, string name, string address, decimal price, string codeInternal, int year, ObjectId idOwner,
        string? type = null, string? zone = null, string? city = null)
    {
        IdProperty = ObjectId.GenerateNewId();
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Price = price;
        CodeInternal = codeInternal ?? throw new ArgumentNullException(nameof(codeInternal));
        YearBuilt = year;
        IdOwner = idOwner;
        Type = type;
        Zone = zone;
        City = city;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Property()
    {
        IdProperty = ObjectId.GenerateNewId();
        Name = string.Empty;
        Address = string.Empty;
        Code = string.Empty;
        CodeInternal = string.Empty;
        Price = 0;
        YearBuilt = 0;
        Type = null;
        Zone = null;
        City = null;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}