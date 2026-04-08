Autorizador Centralizado
Este proyecto es una solución integral para la gestión centralizada de autenticación y autorización. Permite administrar el acceso a múltiples aplicaciones desde un solo punto, garantizando seguridad y consistencia en los permisos de usuario.

🚀 Características
Autenticación Centralizada: Inicio de sesión único (SSO) para diversas plataformas.

Gestión de Roles y Permisos: Control detallado de qué puede hacer cada usuario en cada sistema.

Arquitectura Robusta: Desarrollado con tecnologías modernas siguiendo patrones de diseño escalables.

Backend: .NET / C# utilizando Entity Framework Core.

Base de Datos: SQL Server.

📋 Requisitos Previos
Antes de comenzar, asegúrate de tener instalado:

.NET SDK (v8.0 o superior)

SQL Server Management Studio

🔧 Configuración e Instalación
1. Clonar el repositorio
Bash
git clone https://github.com/CarlosAguilar97/authenticator-centralizado.git
cd authenticator-centralizado
2. Configuración del Backend
Navega a la carpeta del servidor.

Actualiza la cadena de conexión en el archivo appsettings.json para que apunte a tu instancia de SQL Server.


📁 Estructura del Proyecto
Plaintext
├── Backend/              # Lógica de servidor, API REST y acceso a datos (EF Core)
├── Database/             # Scripts SQL y esquemas de base de datos
└── Docs/                 # Documentación adicional del proyecto
🤝 Contribuciones
Haz un Fork del proyecto.

Crea una nueva rama (git checkout -b feature/NuevaFuncionalidad).

Realiza tus cambios y haz un Commit (git commit -m 'Añade NuevaFuncionalidad').

Sube tus cambios a tu rama (git push origin feature/NuevaFuncionalidad).

Abre un Pull Request.

📄 Licencia
Este proyecto está bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.

Desarrollado por Carlos Aguilar
