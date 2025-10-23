using Moq;
using NUnit.Framework;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

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
        _owners = new List<Owner>
        {
            new Owner
            {
                Id = "1",
                Name = "Juan Pérez",
                Address = "Calle 123",
                Photo = "https://example.com/photo1.jpg",
                BirthDate = new DateTime(1980, 1, 1)
            }
        };

        _properties = new List<Property>
        {
            new Property
            {
                Id = "1",
                Name = "Casa Moderna",
                Address = "Calle Principal 123",
                Price = 250000,
                Code = "PROP-001",
                YearBuilt = 2020,
                IdOwner = "1",
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = "1",
                        Url = "https://example.com/image1.jpg",
                        Enabled = true
                    }
                }
            },
            new Property
            {
                Id = "2",
                Name = "Apartamento Centro",
                Address = "Avenida Central 456",
                Price = 150000,
                Code = "PROP-002",
                YearBuilt = 2018,
                IdOwner = "1"
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

        _propertyRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<IPropertyFilter>()))
            .ReturnsAsync(_properties);
        
        _propertyRepositoryMock.Setup(x => x.GetTotalCountAsync(It.IsAny<IPropertyFilter>()))
            .ReturnsAsync(_properties.Count);

        // Act
        var result = await _propertyService.GetPropertiesAsync(filter);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Casa Moderna"));
        Assert.That(result[1].Name, Is.EqualTo("Apartamento Centro"));
    }

    [Test]
    public async Task GetPropertyByIdAsync_ReturnsDetailDto_WhenPropertyExists()
    {
        // Arrange
        var propertyId = "1";
        var property = _properties.First();
        var owner = _owners.First();

        _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId))
            .ReturnsAsync(property);
        
        _ownerRepositoryMock.Setup(x => x.GetByIdAsync(property.IdOwner))
            .ReturnsAsync(owner);

        // Act
        var result = await _propertyService.GetPropertyByIdAsync(propertyId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(propertyId));
        Assert.That(result.Name, Is.EqualTo(property.Name));
        Assert.That(result.Owner.Name, Is.EqualTo(owner.Name));
        Assert.That(result.Images, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetPropertyByIdAsync_ThrowsException_WhenPropertyNotFound()
    {
        // Arrange
        var propertyId = "999";
        _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId))
            .ReturnsAsync((Property)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _propertyService.GetPropertyByIdAsync(propertyId)
        );
        Assert.That(ex.Message, Does.Contain(propertyId));
    }

    [Test]
    public async Task GetTotalPropertiesCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        var filter = new PropertyFilterDto();
        var expectedCount = 2;

        _propertyRepositoryMock.Setup(x => x.GetTotalCountAsync(It.IsAny<IPropertyFilter>()))
            .ReturnsAsync(expectedCount);

        // Act
        var result = await _propertyService.GetTotalPropertiesCountAsync(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedCount));
    }

    [TearDown]
    public void TearDown()
    {
        _propertyRepositoryMock = null;
        _ownerRepositoryMock = null;
        _propertyService = null;
        _properties = null;
        _owners = null;
    }
}