# Sistema de Gestión de Propiedades Inmobiliarias

API REST desarrollada con .NET 8 y C# para la gestión de propiedades inmobiliarias.

## 🛠️ Tecnologías Utilizadas

- .NET 8 / c#
- MongoDB
- Clean Architecture
- NUnit para pruebas
- Swagger para documentación API

## 📋 Requisitos Previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) instalado y ejecutándose en localhost:27017
- [MongoDB Compass](https://www.mongodb.com/try/download/compass) (opcional, para gestión visual de la base de datos)

## 🚀 Instalación

1. Clonar el repositorio:
```bash
git clone https://github.com/jrgeorge89/PropertyManagement.git
cd PropertyManagement
```

2. Restaurar paquetes NuGet:
```bash
dotnet restore
```

3. Configurar la base de datos:
   - La cadena de conexión está en `PropertyManagement.API/appsettings.json`
   - Por defecto: `mongodb://localhost:27017`
   - Base de datos: `PropertyManagementDB`

## ⚙️ Configuración de la Base de Datos

1. Ejecutar scripts de inicialización (usando MongoDB Compass):
   - Navegar a `mongodb-scripts/`
   - Seguir las instrucciones en `mongodb-scripts/README.md`
   - Ejecutar `seed-data.js` para poblar la base de datos
   - Ejecutar `create-indexes.js` para optimizar consultas

## 🏃‍♂️ Ejecución

1. Iniciar la API:
```bash
cd PropertyManagement.API
dotnet run
```

2. Acceder a Swagger UI:
   - Navegar a `http://localhost:5171`
   - La documentación completa de endpoints está disponible aquí

## 📡 Endpoints Disponibles

### Propiedades
- `GET /api/Properties` - Listar propiedades con filtros
  ```json
  {
    "Name": "Casa",
    "Address": "Carrera",
    "minPrice": 100000,
    "maxPrice": 500000
  }
  ```
- `GET /api/Properties/{id}` - Obtener detalle de propiedad
- `GET /api/Properties/{id}/images` - Obtener imágenes de propiedad

## 🏗️ Estructura del Proyecto

```
PropertyManagement/
├── PropertyManagement.API/         # Capa de presentación
├── PropertyManagement.Application/ # Lógica de aplicación
├── PropertyManagement.Domain/      # Entidades y contratos
├── PropertyManagement.Infrastructure/ # Implementaciones
├── PropertyManagement.Tests/       # Pruebas unitarias
└── mongodb-scripts/               # Scripts de base de datos
```

### Capas
- **Domain**: Entidades core y contratos
- **Application**: DTOs, interfaces y servicios
- **Infrastructure**: Implementación MongoDB
- **API**: Controladores y configuración
- **Tests**: Pruebas unitarias con NUnit

## 🧪 Pruebas

1. Ejecutar todas las pruebas:
```bash
dotnet test
```

2. Ejecutar pruebas con cobertura:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Tipos de Pruebas
- **Services**: Pruebas de lógica de negocio con mocks
- **Repositories**: Pruebas de acceso a datos con MongoDB

## 📝 Documentación Adicional

- [Scripts MongoDB](mongodb-scripts/README.md)
- [Configuración de Índices](mongodb-scripts/create-indexes.js)
- [Datos de Prueba](mongodb-scripts/seed-data.js)
