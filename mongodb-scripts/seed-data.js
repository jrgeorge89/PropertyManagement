// =====================================================
// 1. LIMPIAR COLECCIONES EXISTENTES
// =====================================================
db.Owners.drop();
db.Properties.drop();
db.PropertyImages.drop();
db.PropertyTraces.drop();

// =====================================================
// 2. CREAR PROPIETARIOS
// =====================================================
const owners = [
  {
    name: "Ana María Gómez",
    address: "Calle 123 #45-67, Bogotá",
    photo: "https://i.pravatar.cc/150?img=1",
    birthDate: new Date("1985-06-15")
  },
  {
    name: "Carlos Alberto Ruiz",
    address: "Carrera 78 #90-12, Medellín",
    photo: "https://i.pravatar.cc/150?img=2",
    birthDate: new Date("1978-03-22")
  },
  {
    name: "Laura Patricia Valencia",
    address: "Avenida 5N #23-45, Cali",
    photo: "https://i.pravatar.cc/150?img=3",
    birthDate: new Date("1990-11-08")
  },
  {
    name: "Diego Alejandro Martínez",
    address: "Calle 45 #12-34, Barranquilla",
    photo: "https://i.pravatar.cc/150?img=4",
    birthDate: new Date("1982-09-30")
  },
  {
    name: "María José Sánchez",
    address: "Carrera 15 #67-89, Bucaramanga",
    photo: "https://i.pravatar.cc/150?img=5",
    birthDate: new Date("1988-12-25")
  }
];

db.Owners.insertMany(owners);
const insertedOwners = db.Owners.find().toArray();

// =====================================================
// 3. CREAR PROPIEDADES
// =====================================================
const properties = [];
const propertyTypes = ["Casa", "Apartamento", "Penthouse", "Lote"];
const zones = ["Norte", "Sur", "Este", "Oeste", "Centro"];
const cities = ["Bogotá", "Medellín", "Cali", "Barranquilla", "Bucaramanga"];

for (let i = 1; i <= 15; i++) {
  const type = propertyTypes[Math.floor(Math.random() * propertyTypes.length)];
  const zone = zones[Math.floor(Math.random() * zones.length)];
  const city = cities[Math.floor(Math.random() * cities.length)];
  const year = Math.floor(Math.random() * (2024 - 1990 + 1)) + 1990;
  const price = Math.floor(Math.random() * (1000000 - 100000 + 1)) + 100000;
  const owner = insertedOwners[Math.floor(Math.random() * insertedOwners.length)];

  properties.push({
      code: `PROP-${i.toString().padStart(3, '0')}`,
      name: `${type} moderna en zona ${zone} de ${city}`,
      address: `${type === 'Casa' ? 'Calle' : 'Carrera'} ${Math.floor(Math.random() * 150) + 1} #${Math.floor(Math.random() * 100) + 1}-${Math.floor(Math.random() * 100) + 1}, ${city}`,
      price: NumberDecimal(price.toString()),
      codeInternal: `INT-${i.toString().padStart(3, '0')}`,
      yearBuilt: year,
      idOwner: owner._id,
      createdAt: new Date(),
      updatedAt: new Date()
  });
}

db.Properties.insertMany(properties);
const insertedProperties = db.Properties.find().toArray();

// =====================================================
// 4. CREAR IMÁGENES DE PROPIEDADES
// =====================================================
const propertyImages = [];
insertedProperties.forEach(property => {
  const numImages = Math.floor(Math.random() * 3) + 2; // 2–4 imágenes
  for (let i = 0; i < numImages; i++) {
    propertyImages.push({
      idProperty: property._id,
      url: `https://picsum.photos/800/600?random=${Math.floor(Math.random() * 1000)}`,
      name: `Image ${i + 1}`,
      enabled: Math.random() > 0.3,
      createdAt: new Date(),
      updatedAt: new Date()
    });
  }
});
db.PropertyImages.insertMany(propertyImages);

// =====================================================
// 5. CREAR HISTORIALES DE VENTA (TRACES)
// =====================================================
const propertyTraces = [];
insertedProperties.forEach(property => {
  const numTraces = Math.floor(Math.random() * 3) + 1;
  let lastPrice = property.price * 0.8;

  for (let i = 1; i <= numTraces; i++) {
    const dateSale = new Date();
    dateSale.setMonth(dateSale.getMonth() - i * 6);
    const tax = lastPrice * 0.19;

    propertyTraces.push({
      idProperty: property._id,
      dateSale,
      name: `Venta histórica ${i}`,
      value: NumberDecimal(lastPrice.toString()),
      tax: NumberDecimal(tax.toString()),
      createdAt: new Date(),
      updatedAt: new Date()
    });
    lastPrice *= 0.9;
  }
});
db.PropertyTraces.insertMany(propertyTraces);

// =====================================================
// 6. RESULTADOS
// =====================================================
print(`✅ Owners insertados: ${db.Owners.countDocuments()}`);
print(`✅ Properties insertadas: ${db.Properties.countDocuments()}`);
print(`✅ PropertyImages insertadas: ${db.PropertyImages.countDocuments()}`);
print(`✅ PropertyTraces insertadas: ${db.PropertyTraces.countDocuments()}`);
