using System;

namespace PropertyManagement.Domain.Exceptions;

public class PropertyNotFoundException : Exception
{
    public PropertyNotFoundException(string propertyId)
        : base($"No se encontró la propiedad con ID: {propertyId}")
    {
        StatusCode = 400;
    }

    public int StatusCode { get; }
}