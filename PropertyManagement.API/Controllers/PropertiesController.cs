using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Interfaces;

namespace PropertyManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly ILogger<PropertiesController> _logger;

    public PropertiesController(IPropertyService propertyService, ILogger<PropertiesController> logger)
    {
        _propertyService = propertyService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyListDto>>> GetProperties([FromQuery] PropertyFilterDto filter)
    {
        try
        {
            var properties = await _propertyService.GetPropertiesAsync(filter);
            var totalCount = await _propertyService.GetTotalPropertiesCountAsync(filter);

            Response.Headers["X-Total-Count"] = totalCount.ToString();
            Response.Headers["Access-Control-Expose-Headers"] = "X-Total-Count";

            return Ok(properties);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el listado de propiedades");
            return StatusCode(500, "Error interno del servidor al obtener las propiedades");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDetailDto>> GetProperty(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("ID de propiedad inválido");
            }

            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound($"No se encontró la propiedad con ID: {id}");
            }

            return Ok(property);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la propiedad con ID: {Id}", id);
            return StatusCode(500, "Error interno del servidor al obtener la propiedad");
        }
    }

    [HttpGet("{id}/images")]
    public async Task<ActionResult<IEnumerable<ImageDto>>> GetPropertyImages(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("ID de propiedad inválido");
            }

            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound($"No se encontró la propiedad con ID: {id}");
            }

            // Las imágenes ya vienen filtradas (solo activas) desde el servicio
            return Ok(property.Images);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las imágenes de la propiedad con ID: {Id}", id);
            return StatusCode(500, "Error interno del servidor al obtener las imágenes");
        }
    }
}