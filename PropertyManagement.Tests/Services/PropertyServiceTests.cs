using Moq;
using MongoDB.Bson;
using NUnit.Framework;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Domain.Common;
using PropertyManagement.Domain.Exceptions;
using System.Collections.Generic;

namespace PropertyManagement.Tests.Services;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IOwnerRepository> _ownerRepositoryMock;
    private PropertyService _propertyService;
    private List<Property> _properties;
    private List<Owner> _owners;

    [SetUp]
    public void Setup()
    {
        // Configurar mocks
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();

        // Inicializar servicio
        _propertyService = new PropertyService(
            _propertyRepositoryMock.Object,
            _ownerRepositoryMock.Object
        );

        // Datos de prueba
        var ownerId = ObjectId.GenerateNewId();
        _owners = new List<Owner>
        {
            new Owner("Juan Pérez", "Calle 123", "https://example.com/photo1.jpg")
            {
                IdOwner = ownerId,
                BirthDate = new DateTime(1980, 1, 1)
            }
        };

        var propertyId1 = ObjectId.GenerateNewId();
        var propertyId2 = ObjectId.GenerateNewId();
        
        _properties = new List<Property>
        {
            new Property("PROP-001", "Casa Moderna", "Calle Principal 123", 250000, "INT-001", 2020, ownerId)
            {
                IdProperty = propertyId1,
                Type = "Casa",
                Zone = "Norte",
                City = "Bogotá"
            },
            new Property("PROP-002", "Apartamento Centro", "Avenida Central 456", 150000, "INT-002", 2018, ownerId)
            {
                IdProperty = propertyId2,
                Type = "Apartamento",
                Zone = "Centro",
                City = "Bogotá"
            }
        };
    }

    [Test]
    public async Task GetPropertiesAsync_ReturnsListDto_WhenPropertiesExist()
    {
        // Arrange
        var filter = new PropertyFilterDto
        {
            MinPrice = 100000,
            MaxPrice = 300000
        };

        _propertyRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<PropertyFilterDto>()))
            .ReturnsAsync(_properties);
        
        _propertyRepositoryMock.Setup(x => x.GetTotalCountAsync(It.IsAny<PropertyFilterDto>()))
            .ReturnsAsync(_properties.Count);

        // Act
        var result = await _propertyService.GetPropertiesAsync(filter);

        // Assert
        Assert.That(result, Is.Not.Null);
        var propertyList = result.ToList();
        Assert.That(propertyList, Has.Count.EqualTo(2));
        Assert.That(propertyList[0].Name, Is.EqualTo("Casa Moderna"));
        Assert.That(propertyList[1].Name, Is.EqualTo("Apartamento Centro"));
    }

    [Test]
    public async Task GetPropertyByIdAsync_ReturnsDetailDto_WhenPropertyExists()
    {
        // Arrange
        var propertyId = _properties[0].IdProperty;
        var property = _properties.First();
        var owner = _owners.First();

        _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId))
            .ReturnsAsync(property);
        
        _ownerRepositoryMock.Setup(x => x.GetByIdAsync(property.IdOwner))
            .ReturnsAsync(owner);


        // Act
        var result = await _propertyService.GetPropertyByIdAsync(propertyId.ToString());

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(property.Name));
        Assert.That(result.Owner.Name, Is.EqualTo(owner.Name));
    }

    [Test]
    public void GetPropertyByIdAsync_ThrowsException_WhenPropertyNotFound()
    {
        // Arrange
        var propertyIdString = "507f1f77bcf86cd799439011";
        var propertyId = ObjectId.Parse(propertyIdString);
        
        _propertyRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<ObjectId>(id => id == propertyId)))
            .ReturnsAsync((Property)null);

        // Act & Assert
        var exception = Assert.ThrowsAsync<PropertyNotFoundException>(async () =>
            await _propertyService.GetPropertyByIdAsync(propertyIdString)
        );

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.Contain(propertyIdString));
        Assert.That(exception.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task GetTotalPropertiesCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        var filter = new PropertyFilterDto();
        var expectedCount = 2;

        _propertyRepositoryMock.Setup(x => x.GetTotalCountAsync(It.IsAny<PropertyFilterDto>()))
            .ReturnsAsync(expectedCount);

        // Act
        var result = await _propertyService.GetTotalPropertiesCountAsync(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedCount));
    }

    [TearDown]
    public void TearDown()
    {
        // No es necesario limpiar las variables en TearDown para pruebas unitarias
    }
}