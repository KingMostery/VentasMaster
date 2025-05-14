
README - VentasMaster
Descripción del Proyecto
VentasMaster es un sistema de gestión de ventas desarrollado en C# con WPF y Material Design. El sistema permite administrar productos, usuarios y categorías, con una interfaz moderna y funcional.

Características Principales
Gestión de Usuarios: Creación y edición de usuarios con roles (Administrador/Cajero)
Gestión de Productos: Creación, visualización y exportación de productos
Generación de Códigos de Barras: Creación automática de códigos de barras para productos
Exportación a PDF: Generación de reportes de productos en formato PDF
Interfaz Moderna: Diseño basado en Material Design para una experiencia de usuario mejorada
Arquitectura
El proyecto sigue una arquitectura de capas:

Capa de Datos

Capa de Controladores

Capa de Presentación

Vistas XAML

Controladores

MongoDB Data Access

MongoDB Database

Tecnologías Utilizadas
Lenguaje: C# (.NET Framework 4.8.1)
Interfaz de Usuario: WPF con Material Design
Base de Datos: MongoDB
Generación de PDF: MigraDoc/PDFsharp
Generación de Códigos de Barras: ZXing.NET
Módulos Principales
Módulo de Usuarios
Permite la creación y gestión de usuarios del sistema con diferentes niveles de acceso:

Administradores: Acceso completo al sistema
Cajeros: Acceso limitado a funciones específicas
Módulo de Productos
Gestiona el inventario de productos, permitiendo:

Crear nuevos productos
Visualizar listados de productos
Filtrar productos por nombre, categoría y precio
Exportar listados a PDF con códigos de barras
Módulo de Categorías
Permite la creación y gestión de categorías para clasificar los productos del sistema.

Estructura del Proyecto
El proyecto está organizado en las siguientes carpetas principales:

Controllers: Contiene la lógica de negocio (UsuarioController, ProductosController, CategoriasController)
Views: Interfaces de usuario (XAML)
Model: Clases de modelo de datos (Usuario, Producto, Categorias)
Connection: Configuración de conexión a MongoDB
Utils: Utilidades como generación de códigos de barras (CodeBarGenerador)
Requisitos del Sistema
Windows 7 o superior
.NET Framework 4.8.1
MongoDB
Instalación
Clonar el repositorio
Restaurar los paquetes NuGet
Configurar la conexión a MongoDB en Connection/MongoDbConfig.cs
Compilar y ejecutar la aplicación
Dependencias Principales
MaterialDesign: Interfaz gráfica moderna
MongoDB.Driver: Conexión con la base de datos
PDFsharp-MigraDoc-GDI: Generación de documentos PDF
ZXing.Net: Generación de códigos de barras
Contribuciones
Este proyecto está siendo desarrollado por el equipo de KingMostery. Para contribuir, por favor crear un pull request con sus cambios propuestos.

Licencia
Derechos reservados © 2025 KingMostery
