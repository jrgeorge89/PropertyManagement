using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Domain.Entities;

public class PropertyTrace
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdPropertyTrace { get; set; }

    [BsonElement("dateSale")]
    public DateTime DateSale { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("value")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Value { get; set; }

    [BsonElement("tax")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Tax { get; set; }

    [Required(ErrorMessage = "El ID de la propiedad es requerido")]
    [BsonElement("idProperty")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdProperty { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    public PropertyTrace(string name, decimal value, decimal tax, ObjectId idProperty)
    {
        IdPropertyTrace = ObjectId.GenerateNewId();
        DateSale = DateTime.UtcNow;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value;
        Tax = tax;
        IdProperty = idProperty;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public PropertyTrace()
    {
        IdPropertyTrace = ObjectId.GenerateNewId();
        DateSale = DateTime.UtcNow;
        Name = string.Empty;
        Value = 0;
        Tax = 0;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}