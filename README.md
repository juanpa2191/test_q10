# EstudiantesApp

## Descripción

EstudiantesApp es una aplicación web ASP.NET Core que gestiona la información de estudiantes y sus inscripciones en materias académicas.

## Características Principales

- **Gestión de Estudiantes**:
  - CRUD completo de estudiantes (Crear, Leer, Actualizar, Eliminar)
  - Validación de datos
  - Interfaz de usuario amigable

- **Gestión de Materias**:
  - Administración de materias académicas
  - Visualización de información detallada
  - Asignación de créditos

- **Sistema de Inscripciones**:
  - Inscripción de estudiantes en materias
  - Lógica de negocio para limitar inscripciones
  - Reglas de negocio para materias con créditos mayores o iguales a 4
  - Verificación de cupos disponibles

## Arquitectura

El proyecto está estructurado en capas:

- **EstudiantesApp**: Proyecto principal con la aplicación web
- **EstudiantesApp.Application**: Lógica de negocio y servicios
- **EstudiantesApp.Domain**: Entidades del dominio
- **EstudiantesApp.Infrastructure**: Implementación de repositorios y configuración
- **EstudiantesApp.Tests**: Pruebas unitarias

## Tecnologías Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- AutoMapper
- Bootstrap

## Configuración

La aplicación utiliza una base de datos SQL Server configurada en el archivo `appsettings.json`. La cadena de conexión predeterminada es:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=Server;Database=EstudiantesDb;Trusted_Connection=True;Encrypt=False;"
  }
}
```

## Requisitos

- .NET 7.0 o superior
- SQL Server
- Visual Studio 2022 o superior

## Instalación

1. Clonar el repositorio
2. Restaurar paquetes: `dotnet restore`
3. Ejecutar migraciones: `dotnet ef database update`
4. Ejecutar la aplicación: `dotnet run`


## Seguridad

La aplicación implementa:
- HTTPS por defecto
- Validación de modelos
- Manejo de excepciones

## Contribución

Las contribuciones son bienvenidas. Por favor, crea un issue para discutir cambios importantes antes de enviar un pull request.
