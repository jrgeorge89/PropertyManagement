using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Domain.Entities;

public class PropertyImage
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId IdPropertyImage { get; set; }

    [Required(ErrorMessage = "El ID de la propiedad es requerido")]
    [BsonElement("idProperty")]
    public string IdProperty { get; set; }

    [Required(ErrorMessage = "La URL de la imagen es requerida")]
    [BsonElement("url")]
    public string Url { get; set; }

    [BsonElement("enabled")]
    public bool Enabled { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    public PropertyImage(string idProperty, string file, bool enabled = true)
    {
        IdPropertyImage = ObjectId.GenerateNewId();
        IdProperty = idProperty;
        Url = file ?? throw new ArgumentNullException(nameof(file));
        Enabled = enabled;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public PropertyImage()
    {
        IdPropertyImage = ObjectId.GenerateNewId();
        IdProperty = string.Empty;
        Enabled = true;
        Url = string.Empty;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}