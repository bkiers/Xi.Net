# Xi.Net

TODO

## Database

Setup the database:

```sql
CREATE DATABASE xi;
CREATE USER xi_usr WITH PASSWORD 's3cr3t';
GRANT ALL PRIVILEGES ON DATABASE xi TO xi_usr;
```

## Migrations

From `./src/Xi.Database`, run:

```bash
dotnet ef --startup-project ../Xi.BlazorApp Migrations add MigrationName
```