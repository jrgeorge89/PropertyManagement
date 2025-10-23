using MongoDB.Driver;
using NUnit.Framework;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infrastructure.Configuration;
using PropertyManagement.Infrastructure.Data;
using PropertyManagement.Infrastructure.Repositories;
using PropertyManagement.Application.DTOs;

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
        // Configuracion de la base de datos de prueba
        var settings = new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "PropertyManagementTestDB"
        };

        _context = new MongoDbContext(settings);
        _repository = new PropertyRepository(_context);

        // Datos de prueba
        _testProperties = new List<Property>
        {
            new Property
            {
                Id = "1",
                Name = "Casa de Prueba 1",
                Address = "Dirección 1",
                Price = 200000,
                Code = "TEST-001",
                YearBuilt = 2020,
                IdOwner = "owner1"
            },
            new Property
            {
                Id = "2",
                Name = "Apartamento de Prueba",
                Address = "Dirección 2",
                Price = 150000,
                Code = "TEST-002",
                YearBuilt = 2019,
                IdOwner = "owner2"
            },
            new Property
            {
                Id = "3",
                Name = "Casa de Prueba 2",
                Address = "Dirección 3",
                Price = 300000,
                Code = "TEST-003",
                YearBuilt = 2021,
                IdOwner = "owner1"
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
        Assert.That(result, Has.Count.EqualTo(3));
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
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Price, Is.GreaterThanOrEqualTo(filter.MinPrice));
        Assert.That(result.First().Price, Is.LessThanOrEqualTo(filter.MaxPrice));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsProperty_WhenIdIsValid()
    {
        // Arrange
        var propertyId = "1";

        // Act
        var result = await _repository.GetByIdAsync(propertyId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(propertyId));
        Assert.That(result.Code, Is.EqualTo("TEST-001"));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsNull_WhenIdNotFound()
    {
        // Arrange
        var propertyId = "999";

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
        var result = await _repository.GetTotalCountAsync(filter);

        // Assert
        Assert.That(result, Is.EqualTo(1));
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