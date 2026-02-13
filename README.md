# eVote360 - Sistema de Votación Electrónica

eVote360 es una plataforma web integral diseñada para la gestión y ejecución de procesos electorales de manera transparente y eficiente. El sistema permite administrar candidatos, partidos políticos y puestos electivos, facilitando el ejercicio del voto para los ciudadanos y el monitoreo para los administradores.

## Tecnologías Utilizadas

* **Lenguaje:** C# (.NET 8 o 9)
* **Framework Web:** ASP.NET Core MVC
* **Persistencia:** Entity Framework Core (Enfoque Code First)
* **Base de Datos:** SQL Server
* **Arquitectura:** Onion Architecture (Arquitectura de Cebolla)
* **Seguridad:** ASP.NET Identity para la gestión de usuarios y roles
* **Mapeo:** AutoMapper para la transformación de objetos
* **Diseño:** Bootstrap para una interfaz limpia y responsiva
* **Servicios Compartidos:** Capa Shared para servicios de correo electrónico

## Arquitectura del Proyecto

La solución está implementada bajo la Arquitectura Onion, garantizando un bajo acoplamiento y alta mantenibilidad:

1. **Core (Domain):** Definición de las entidades fundamentales (Ciudadanos, Partidos, Puestos, Candidatos, Votos).
2. **Application:** Lógica de negocio, interfaces, servicios genéricos, DTOs y ViewModels con sus respectivas validaciones.
3. **Infrastructure:** Implementación de persistencia (Contexto de base de datos y repositorios genéricos), servicios de Identity y lógica compartida (envío de correos).
4. **Presentation (Web):** Controladores y Vistas Razor que gestionan el flujo de trabajo de electores, dirigentes y administradores.

## Funcionalidades Principales

### Funcionalidades del Elector
* **Validación de Identidad:** Acceso al sistema mediante el número de cédula.
* **Proceso de Votación:** Selección de candidatos por puesto electivo (Presidente, Alcalde, Senador, Diputado).
* **Confirmación de Voto:** Resumen de la selección antes de finalizar y confirmación de que el voto ha sido procesado.

### Funcionalidades del Administrador
* **Gestión de Usuarios:** Mantenimiento de cuentas para administradores y dirigentes.
* **Mantenimiento de Puestos y Partidos:** Creación y edición de las entidades que participan en el proceso.
* **Mantenimiento de Ciudadanos:** Gestión del padrón electoral.
* **Control de Elecciones:** Apertura y cierre de procesos electorales y visualización de resultados en tiempo real.

### Funcionalidades del Dirigente
* **Gestión de Candidatos:** Registro de aspirantes asociados a un partido y un puesto electivo.
* **Alianzas Políticas:** Sistema de solicitudes de alianza entre partidos para candidaturas compartidas.

## Requerimientos Técnicos Implementados

* **Validaciones:** Uso estricto de ViewModels con Data Annotations para validaciones de formularios.
* **Transferencia de Datos:** Implementación de DTOs para la comunicación entre servicios y controladores.
* **Patrones:** Uso de Repositorios Genéricos y el patrón de Servicio.
* **Seguridad de Datos:** Lógica para inactivar entidades sin eliminarlas físicamente, manteniendo la integridad referencial y el historial del sistema.

## Instrucciones de Instalación

1. **Clonación del Proyecto:**
   Clone el repositorio en su estación de trabajo local.

2. **Configuración de la Base de Datos:**
   Actualice la cadena de conexión en el archivo `appsettings.json` del proyecto Web.

3. **Migraciones:**
   Ejecute el comando para generar la estructura de la base de datos:
   `dotnet ef database update`

4. **Ejecución:**
   Inicie la aplicación mediante su IDE o a través de la consola con `dotnet run`.
