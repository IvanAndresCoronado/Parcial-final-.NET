# Parcial Final — Inventario de Videojuegos (C# .NET 8, Avalonia UI y ADO.NET)

## 6.1. Información general
- **Nombre del proyecto:** `ParcialFinal` — Inventario de Videojuegos.
- **Estudiante:** Iván Andrés Coronado Hamburger — Estudiante de Ingeniería de Sistemas (4.º semestre), Corporación Universitaria Americana.
- **Descripción del software:** Aplicación de escritorio desarrollada en C# sobre .NET 8 con interfaz gráfica en Avalonia UI y persistencia relacional en SQLite mediante ADO.NET (`Microsoft.Data.Sqlite`), sin uso de ORM.
- **Problema o necesidad que resuelve:** Permite llevar un control organizado y persistente de un catálogo o inventario de videojuegos (título, plataforma, género, precio y stock) desde una interfaz gráfica intuitiva sin depender de comandos en terminal ni hojas dispersas.
- **Objetivo principal de la aplicación:** Diseñar, implementar, probar y documentar una aplicación de escritorio que integre Programación Orientada a Objetos (POO), eventos en Avalonia UI y operaciones CRUD parametrizadas con ADO.NET y SQLite.

## 6.2. Funcionalidades
- **Registrar videojuego (`Create`):** Captura los datos del formulario y guarda un nuevo registro en SQLite previa validación.
- **Consultar y listar videojuegos (`Read`):** Carga y muestra los registros almacenados en la base de datos dentro de la lista principal de la interfaz.
- **Actualizar videojuego (`Update`):** Permite seleccionar un registro existente, modificar sus campos y guardar los cambios en SQLite.
- **Eliminar videojuego (`Delete`):** Elimina el registro seleccionado de la base de datos y actualiza la vista.
- **Limpiar formulario:** Restablece las cajas de texto y deselecciona el elemento activo.
- **Validación y notificaciones en pantalla:** Verifica campos obligatorios y tipos/rangos numéricos (precio y cantidad >= 0) mostrando mensajes claros de éxito o error sin cerrar la aplicación.

## 6.3. Tecnologías utilizadas
- **Framework / SDK:** .NET 8 (`net8.0`).
- **Lenguaje:** C#.
- **Interfaz gráfica:** Avalonia UI (`11.2.1`, incluyendo `Avalonia.Desktop`, `Avalonia.Themes.Fluent` y `Avalonia.Fonts.Inter`) con vistas declarativas en XAML (`.axaml`) y lógica de eventos en *code-behind* (`.axaml.cs`).
- **Acceso a datos:** ADO.NET mediante el proveedor `Microsoft.Data.Sqlite` (`8.0.10`), sin Entity Framework Core ni otro ORM.
- **Base de datos:** SQLite (`inventario_videojuegos.db`).
- **Herramientas de desarrollo y control de versiones:** Visual Studio Code, .NET CLI, Git y GitHub (`IvanAndresCoronado`).

## 6.4. Estructura del proyecto

```text
ParcialFinal/
├── Data/
│   └── VideojuegoDao.cs
├── evidencias/
│   ├── 01_registro.png
│   ├── 02_consulta.png
│   ├── 03_actualizacion.png
│   ├── 04_eliminacion.png
│   ├── 05_validacion.png
│   └── 06_persistencia.png
├── Models/
│   └── Videojuego.cs
├── Services/
│   └── VideojuegoService.cs
├── App.axaml
├── App.axaml.cs
├── app.manifest
├── MainWindow.axaml
├── MainWindow.axaml.cs
├── ParcialFinal.csproj
├── Program.cs
├── .gitignore
└── README.md