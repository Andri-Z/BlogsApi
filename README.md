# 📰 Blogging Platform API

Este es un proyecto basico de **API RESTful** construido con **ASP.NET Core 8** y **MongoDB** que permite a los usuarios **crear, leer, actualizar y eliminar** publicaciones de blog. También incluye funcionalidades de **búsqueda** por titulo, contenido y categoria.

Esta idea de proyecto la obtuve desde aqui: https://roadmap.sh/projects/blogging-platform-api

## ✨ Características

- CRUD completo para publicaciones de blog.
- Búsqueda por término en los campos de (titulo, contenido o categoría).
- Arquitectura limpia y escalable.
- Documentación automática con Swagger.
- Soporte para MongoDB usando `MongoDB.Driver`.

## 🧱 Tecnologías usadas

- ASP.NET Core 8
- MongoDB
- C#
- Swagger / OpenAPI

## 🚀 Endpoints principales

| Método | Ruta                     | Descripción                                |
|--------|--------------------------|--------------------------------------------|
| GET    | `/api/v1/blogs`          | Obtener todas las publicaciones            |
| GET    | `/api/v1/blogs/{id}`     | Obtener una publicación por su ID          |
| GET    | `/api/v1/blogs/search?term=` | Buscar por término en título, categoría o contenido |
| POST   | `/api/v1/blogs`          | Crear una nueva publicación                |
| PUT    | `/api/v1/blogs/{id}`     | Actualizar una publicación existente       |
| DELETE | `/api/v1/blogs/{id}`     | Eliminar una publicación                   |


## ⚙️ Importante

Debes modificar el archivo `appsettings.json`, este ya tiene la estructura, solo hay que cambiar los valores:

```json
{
  "ConnectionStrings": {
    "BlogsApi": "Cadena de conexion",
    "DatabaseName": "NombreDeTuBase",
    "CollectionName": "NombreDeTuColeccion"
  }
}
```

## 🚴 Correr el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Andri-Z/BlogsApi.git
   cd BlogsApi
   ```

2. Modifica el archivo `appsettings.json` como se muestra arriba.

3. Restaura paquetes y ejecuta el proyecto:
   ```bash
   dotnet restore
   dotnet run
   ```
## 🙌 Contribuciones

¡Las contribuciones son bienvenidas! Siempre y cuando esto no afecte la funcionalidad del proyecto.
