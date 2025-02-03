## :books: Book Store API
A simple Book Store API built with ASP.NET Core and SQL Server running in Docker.

## :rocket: Getting Started

### :wrench: Prerequisites
Before you start, make sure you have:
- Docker & Docker Compose installed

Or if running locally without Docker
- .NET 8+ SDK installed
- SQL Server 2022 Express installed

## :package: Running the Project with Docker

Clone the repository:

```sh
git clone https://github.com/thiseasm/BookStoreAPI.git
```

Build and start the containers:

```sh
docker-compose up --build
```

The API should be running at:

```sh
http://localhost:8080
```

## :hammer_and_wrench: Configuration
The API uses environment variables for configuration. You can find them in:
- `appsettings.json` (local development)
- `docker-compose.yml` (Docker setup)

### :link: Database Connection String

In Docker, the API connects using:

```yaml
ConnectionStrings__BookStoreDB=Server=db;Database=BookStoreDB;User Id=sa;Password=Passw0rd;TrustServerCertificate=True;
```

For local development, update `appsettings.json`:

```json
"ConnectionStrings": {
  "BookStoreDB": "Server=localhost\\SQLEXPRESS;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
}
```

## :hammer_and_wrench: Database Migrations

Running locally (not Docker) or inside Docker, migrations are automatically applied on first startup.

## :book: API Endpoints

### :pushpin: Users

| Method | Endpoint           | Description       |
|--------|-------------------|-------------------|
| GET    | `/api/users`      | Get all users    |
| GET    | `/api/users/{id}` | Get user by ID   |
| POST   | `/api/users`      | Create new user  |
| POST    | `/api/users/{id}/roles` | Update user roles   |

### :pushpin: Roles

| Method | Endpoint      | Description      |
|--------|--------------|------------------|
| GET    | `/api/roles` | Get all roles    |

### :pushpin: Books

| Method | Endpoint      | Description      |
|--------|--------------|------------------|
| GET    | `/api/books` | Get all books    |
| GET    | `/api/books/{id}` | Get book by ID   |
| POST   | `/api/books`      | Create new book  |

### :pushpin: Categories

| Method | Endpoint      | Description      |
|--------|--------------|------------------|
| GET    | `/api/categories` | Get all categories    |
| DELETE   | `/api/categories/{id}`      | Delete category  |

### :pushpin: Audit

| Method | Endpoint      | Description      |
|--------|--------------|------------------|
| GET    | `/api/audit/users` | Get all audit logs for user actions   |

More endpoints can be found in Swagger UI:

```sh
http://localhost:8080/swagger
```

## :memo: Logging & Cleanup
- **Audit Logs:** Stored in `UserLogs` table
- **Cleanup Job:** Deletes logs older than 20 days (or otherwise configured in appsettings.json), runs as a background service

## :flashlight: Troubleshooting

### Database Connection Issues?

:white_check_mark: Ensure Docker containers are running:

```sh
docker ps
```

:white_check_mark: Restart containers if needed:

```sh
docker-compose down && docker-compose up --build
```

## :handshake: Contributing
Feel free to submit issues or pull requests!

## :scroll: License
This project is licensed under the MIT License.
