using MongoDB.Driver;
using MongoDB.Bson;
using NUnit.Framework;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infrastructure.Configuration;
using PropertyManagement.Infrastructure.Data;
using PropertyManagement.Infrastructure.Repositories;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace PropertyManagement.Tests.Repositories;

[TestFixture]
public class PropertyRepositoryTests
{
    private MongoDbContext _context;
    private PropertyRepository _repository;
    private List<Property> _testProperties;

    [SetUp]
    public async Task Setup()
    {
        // Configuración de la base de datos de prueba
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                {"MongoDB:DatabaseName", "PropertyManagementTestDB"}
            })
            .Build();

        _context = new MongoDbContext(configuration);
        _repository = new PropertyRepository(_context);

        // Datos de prueba
        _testProperties = new List<Property>
        {
            new Property("TEST-001", "Casa de Prueba 1", "Dirección 1", 200000, "INT-001", 2020, ObjectId.GenerateNewId())
            {
                Type = "Casa",
                Zone = "Norte",
                City = "Bogotá"
            },
            new Property("TEST-002", "Apartamento de Prueba", "Dirección 2", 150000, "INT-002", 2019, ObjectId.GenerateNewId())
            {
                Type = "Apartamento",
                Zone = "Centro",
                City = "Bogotá"
            },
            new Property("TEST-003", "Casa de Prueba 2", "Dirección 3", 300000, "INT-003", 2021, ObjectId.GenerateNewId())
            {
                Type = "Casa",
                Zone = "Sur",
                City = "Bogotá"
            }
        };

        // Limpiar y poblar la colección de prueba
        await _context.Properties.DeleteManyAsync(Builders<Property>.Filter.Empty);
        await _context.Properties.InsertManyAsync(_testProperties);
    }

    [Test]
    public async Task GetAllAsync_ReturnsAllProperties_WhenNoFilter()
    {
        // Arrange
        var filter = new PropertyFilterDto();

        // Act
        var result = await _repository.GetAllAsync(filter);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetAllAsync_ReturnsFilteredProperties_WhenFilterApplied()
    {
        // Arrange
        var filter = new PropertyFilterDto
        {
            MinPrice = 250000,
            MaxPrice = 350000
        };

        // Act
        var result = await _repository.GetAllAsync(filter);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Price, Is.GreaterThanOrEqualTo(filter.MinPrice));
        Assert.That(result.First().Price, Is.LessThanOrEqualTo(filter.MaxPrice));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsProperty_WhenIdIsValid()
    {
        // Arrange
        var propertyId = _testProperties[0].IdProperty;

        // Act
        var result = await _repository.GetByIdAsync(propertyId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IdProperty, Is.EqualTo(propertyId));
        Assert.That(result.Code, Is.EqualTo("TEST-001"));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsNull_WhenIdNotFound()
    {
        // Arrange
        var propertyId = ObjectId.GenerateNewId();

        // Act
        var result = await _repository.GetByIdAsync(propertyId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetTotalCountAsync_ReturnsCorrectCount_WhenFilterApplied()
    {
        // Arrange
        var filter = new PropertyFilterDto
        {
            MinPrice = 100000,
            MaxPrice = 200000
        };

        // Act
        var filteredProperties = await _repository.GetAllAsync(filter);
        var result = await _repository.GetTotalCountAsync(filter);

        // Assert
        Assert.That(result, Is.EqualTo(filteredProperties.Count()));
    }

    [TearDown]
    public async Task TearDown()
    {
        // Limpiar la base de datos de prueba
        if (_context != null)
        {
            await _context.Properties.DeleteManyAsync(Builders<Property>.Filter.Empty);
        }
    }
}