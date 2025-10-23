# Scripts de MongoDB para Property Management

Este directorio contiene scripts para inicializar y configurar la base de datos MongoDB del sistema de gestión de propiedades.

## Prerrequisitos

- MongoDB instalado y ejecutándose en localhost:27017
- MongoDB Compass instalado

## Scripts Disponibles

### 1. seed-data.js
Llena la base de datos con datos de prueba:
- 5 propietarios
- 15 propiedades
- 2-4 imágenes por propiedad
- Historiales de venta por propiedad

### 2. create-indexes.js
Crea los índices necesarios para optimizar las consultas:

Properties:
- Índice de texto en name y address
- Índice en price
- Índice en idOwner

PropertyImages:
- Índice en idProperty
- Índice en enabled
- Índice compuesto (idProperty + enabled)

PropertyTraces:
- Índice en idProperty
- Índice en dateSale

## Instrucciones de Uso con MongoDB Compass

1. Abrir MongoDB Compass y conectar a localhost:27017

2. Para ejecutar seed-data.js:
   - Ir a la base de datos "PropertyManagementDB"
   - Clic en "Aggregations"
   - Seleccionar "New Pipeline"
   - Clic en "Import"
   - Navegar y seleccionar el archivo "seed-data.js"
   - Clic en "Run"

3. Para ejecutar create-indexes.js:
   - Ir a la base de datos "PropertyManagementDB"
   - Para cada colección mencionada en el script:
     - Seleccionar la colección
     - Ir a la pestaña "Indexes"
     - Clic en "Create Index"
     - Copiar y pegar la configuración del índice desde create-indexes.js
     - Clic en "Create"

## Verificación

Para verificar que los datos se han insertado correctamente en MongoDB Compass:

1. Navegar a cada colección y verificar el conteo:
   - Owners: 5 documentos
   - Properties: 15 documentos
   - PropertyImages: entre 30-60 documentos
   - PropertyTraces: entre 15-45 documentos

2. Verificar los índices:
   - En cada colección, ir a la pestaña "Indexes"
   - Confirmar que los índices listados en create-indexes.js están presentes

## Notas Importantes

- Ejecutar primero seed-data.js para crear y poblar las colecciones
- Luego crear los índices siguiendo las instrucciones de create-indexes.js
- Si necesitas reiniciar, puedes eliminar la base de datos y volver a ejecutar los scripts