// =====================================================
// Índices para Properties
// =====================================================
db.Properties.createIndex(
  { name: "text", address: "text" },
  { name: "properties_text_search", default_language: "spanish" }
);

db.Properties.createIndex(
  { price: 1 },
  { name: "properties_price" }
);

db.Properties.createIndex(
  { idOwner: 1 },
  { name: "properties_owner" }
);

print("✅ Índices de Properties creados");

// =====================================================
// Índices para PropertyImages
// =====================================================
db.PropertyImages.createIndex(
  { idProperty: 1 },
  { name: "property_images_property" }
);

db.PropertyImages.createIndex(
  { enabled: 1 },
  { name: "property_images_enabled" }
);

db.PropertyImages.createIndex(
  { idProperty: 1, enabled: 1 },
  { name: "property_images_property_enabled" }
);

print("✅ Índices de PropertyImages creados");

// =====================================================
// Índices para PropertyTraces
// =====================================================
db.PropertyTraces.createIndex(
  { idProperty: 1 },
  { name: "property_traces_property" }
);

db.PropertyTraces.createIndex(
  { dateSale: -1 },
  { name: "property_traces_date" }
);

print("✅ Índices de PropertyTraces creados");

// =====================================================
// Mostrar resumen
// =====================================================
print("\n📘 Índices en Properties:");
db.Properties.getIndexes().forEach(printjson);

print("\n📘 Índices en PropertyImages:");
db.PropertyImages.getIndexes().forEach(printjson);

print("\n📘 Índices en PropertyTraces:");
db.PropertyTraces.getIndexes().forEach(printjson);

print("\n🎉 Script finalizado correctamente.");
