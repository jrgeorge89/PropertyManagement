using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infrastructure.Configuration;

namespace PropertyManagement.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var settings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
        
        if (settings == null)
            throw new ArgumentNullException(nameof(settings), "Configuración de MongoDB no encontrada en la configuración");

        _mongoClient = new MongoClient(settings.ConnectionString);
        _database = _mongoClient.GetDatabase(settings.DatabaseName);

        // Registrar los mapeos de las clases
        RegisterClassMaps();
    }

    private void RegisterClassMaps()
    {
        
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");

    public IMongoDatabase GetDatabase() => _database;
}