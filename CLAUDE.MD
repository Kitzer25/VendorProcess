# Tarea: Auditoría de Repositorios y Generación de Servicios – Sistema de Vendibles

## Contexto del proyecto

Sistema de catálogo/pedidos ("vendibles"): los clientes consultan productos disponibles y los agregan a un pedido para que el proveedor los alistone y despache. El objetivo del sistema es sustituir un flujo manual/tardado por uno donde el cliente arma su pedido y el proveedor solo prepara y envía.

Arquitectura: Clean Architecture / Ports & Adapters (Domain, Infraestructure, Ports, Adapters).

Esta tarea tiene **dos fases secuenciales**. No iniciar la Fase 2 sin completar la Fase 1.

---

## Fase 1 – Auditoría de repositorios (Domain + Infraestructure)

### Alcance (leer y analizar, no asumir contenido)

- `Domain/Ports/Repositories`
- `Domain/Ports/Repositories/ERepositories`
- `Domain/Entities`
- `Domain/DTO's`
- `Infraestructure/Adapters/Repositories/*`

### Criterios de evaluación

**1. Coherencia de consultas**
- Los métodos de los repositorios (interfaz vs implementación) deben coincidir en firma, tipos de retorno y nombre semántico de lo que hacen.
- Detectar queries que traigan más datos de los necesarios (falta de `Select`/proyección a DTO, `Include` innecesarios, N+1).
- Verificar uso correcto de `async/await`, `CancellationToken` en todas las firmas, y `IQueryable` vs materialización (`ToListAsync`) en el lugar correcto (no exponer `IQueryable` fuera del repositorio).
- Confirmar filtros de negocio reales (ej. productos activos, stock disponible, soft-delete) estén aplicados de forma consistente entre repositorios.

**2. Principios SOLID (foco en SRP)**
- Cada repositorio debe limitarse a la persistencia/consulta de **una sola entidad o agregado**; señalar si hay lógica de negocio, validaciones o reglas de dominio filtradas dentro del repositorio (deben vivir en servicios de aplicación).
- Verificar que las interfaces en `ERepositories` no mezclen responsabilidades de más de una entidad.
- Señalar violaciones de ISP (interfaces con métodos que no todos los consumidores usan) si se detectan.

**3. Insights de valor (proponer, justificar y aplicar si es de bajo riesgo)**
- Oportunidades de paginación en listados de productos.
- Índices o filtros compuestos sugeridos según los `Where` más usados.
- Posible necesidad de un patrón Specification si los filtros se repiten entre repositorios.
- Manejo de concurrencia/stock si aplica (ej. `RowVersion`) al agregar productos a un pedido.

### Entregable de la Fase 1
- Lista corta de hallazgos por carpeta (qué se corrigió y por qué).
- Correcciones aplicadas directamente en el código cuando sean de bajo riesgo (nombres, firmas, proyecciones, filtros faltantes).
- Hallazgos que impliquen cambios estructurales grandes: reportarlos, no aplicarlos sin confirmación.

---

## Fase 2 – Generación de servicios de aplicación

Solo iniciar una vez cerrada la Fase 1.

### Rutas de salida
- Interfaces: `Infraestructure/Ports/Services/`
- Implementaciones: `Infraestructure/Adapters/Services/EServices`

### Reglas de diseño
- Un servicio por entidad/agregado relevante del dominio (respeta SRP igual que en Fase 1).
- Inyección vía constructor de `IUnitOfWork` y, cuando aplique, `IAuditService` u otras dependencias ya existentes en el proyecto (no inventar dependencias nuevas sin verificar que existan).
- CRUD básico implementado **a través del repositorio genérico** expuesto por `IUnitOfWork` (no acceder a `DbContext` directamente desde el servicio).
- `SaveChangesAsync` se invoca **una sola vez por operación de escritura**, garantizando atomicidad (no llamar múltiples veces dentro del mismo método).
- Todas las firmas async, con `CancellationToken`.
- Entrada/salida de los métodos en DTOs (usar los ya definidos en `Domain/DTO's`; crear nuevos solo si no existe uno adecuado).
- Registrar auditoría (`IAuditService.RegisterAsync`) en creación, actualización y eliminación cuando la entidad lo amerite (ej. productos, pedidos), no en simples consultas de lectura.
- Validaciones básicas de negocio (nulos, existencia del recurso, reglas simples) dentro del servicio, no en el repositorio.

### Estructura de referencia

```csharp
public class AdminProductService : IAdminProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;

    public AdminProductService(IUnitOfWork unitOfWork, IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
    }

    // Implementación de CRUD usando _unitOfWork.Repository<T>()
    // y _unitOfWork.SaveChangesAsync(ct) al final de cada operación de escritura.
}

public interface IAuditService
{
    Task RegisterAsync(string tableName, string recordId, string action, object? oldValues, object? newValues,
        int? userId, int? customerId, CancellationToken ct);

    Task<IEnumerable<AuditLogDto>> GetByRecordAsync(string tableName, string recordId, CancellationToken ct);
    Task<IEnumerable<AuditLogDto>> GetByUserAsync(int userId, DateTime from, DateTime to, CancellationToken ct);
    Task<IEnumerable<AuditLogDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct);
}
```

### Entregable de la Fase 2
- Una interfaz por servicio en `Infraestructure/Ports/Services/`.
- Una implementación por servicio en `Infraestructure/Adapters/Services/EServices`.
- CRUD mínimo cubierto: Create, GetById, GetAll (con proyección a DTO), Update, Delete/Deactivate.
- Uso confirmado de `IUnitOfWork` + `SaveChangesAsync` + registro de auditoría donde aplique.

---

## Checklist final antes de dar por terminada la tarea

- [ ] Repositorios revisados: firmas coherentes entre interfaz e implementación.
- [ ] Sin lógica de negocio dentro de repositorios (solo persistencia/consulta).
- [ ] Queries optimizadas (proyección, filtros, sin N+1).
- [ ] Insights propuestos documentados; los de bajo riesgo aplicados.
- [ ] Servicios generados siguen la estructura de referencia (constructor con `IUnitOfWork` + dependencias necesarias).
- [ ] CRUD genérico correctamente delegado al repositorio vía `IUnitOfWork`.
- [ ] `SaveChangesAsync` llamado una sola vez por operación de escritura.
- [ ] Auditoría registrada en create/update/delete cuando corresponde.
- [ ] Todo el código nuevo compila y sigue las convenciones existentes del proyecto.