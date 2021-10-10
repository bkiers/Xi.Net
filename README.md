# Xi.Net

A Blazor app to play correspondence Chinese Chess. The frontend is built using 
the amazing [MudBlazor](https://mudblazor.com/) framework.

![screen dark mode](https://github.com/bkiers/Xi.Net/blob/master/screen-dm.png?raw=true)

![screen light mode](https://github.com/bkiers/Xi.Net/blob/master/screen-lm.png?raw=true)

## Database

Login your PostgreSQL server (`psql postgres`) and create the database and user:

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

Initialize the secrets:
```
dotnet user-secrets init
```

And add (at least) the Google secret and ID:

```
dotnet user-secrets set "Google:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "Google:ClientSecret" "YOUR_CLIENT_SECRET"
dotnet user-secrets set "XiConfig:SendGridApiKey" "YOUR_API_KEY"
```

The SendGrid API key is optional.