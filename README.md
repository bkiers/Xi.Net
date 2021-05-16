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

## Secrets

Enable secrets:

```
cd src/Xi.BlazorApp
dotnet user-secrets init
```

Add secrets (also insdie `src/Xi.BlazorApp`):

```
dotnet user-secrets set "Authentication:Google:ClientId" "..."
dotnet user-secrets set "Authentication:Google:ClientSecret" "..."
```