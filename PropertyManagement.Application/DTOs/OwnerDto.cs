using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class OwnerDto
{
    public string IdOwner { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La dirección es requerida")]
    public string Address { get; set; }

    public string? Photo { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public OwnerDto()
    {
        IdOwner = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        Photo = null;
        BirthDate = DateTime.UtcNow;
    }

    public OwnerDto(string idOwner, string name, string address, string? photo, DateTime birthDate)
    {
        IdOwner = idOwner;
        Name = name;
        Address = address;
        Photo = photo;
        BirthDate = birthDate;
    }
}