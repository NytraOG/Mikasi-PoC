# Blackshield Deployment

Postgres läuft in Docker — identische Konfiguration lokal und auf dem Hetzner-Server.

## Dateien

| Datei | Zweck |
|---|---|
| `docker-compose.yml` | Basis-Definition (Postgres-Image, Healthcheck, Volume) — überall identisch |
| `docker-compose.override.yml` | Lokale Overrides (Port 5432 öffnen, Verbose-Logs) — wird automatisch geladen |
| `docker-compose.prod.yml` | Produktion (kein offener Port, Backup-Sidecar) — explizit aktivieren |
| `.env.example` | Template für DB-Credentials |
| `.env` | Tatsächliche Credentials (gitignored) |
| `backups/` | Lokales Backup-Verzeichnis (gitignored) |

## Lokal hochfahren

```powershell
cd Blackshield/deploy
docker compose up -d
docker compose ps             # Status
docker compose logs -f postgres
```

Postgres ist danach unter `localhost:5432` erreichbar (User & Passwort aus `.env`).

## Lokal stoppen

```powershell
docker compose stop           # Stoppen, Daten bleiben erhalten
docker compose down           # Stoppen + Container entfernen, Daten bleiben (Volume)
docker compose down -v        # Stoppen + Container + Volume LÖSCHEN — frische DB!
```

## Verbinden mit DBeaver / Rider / pgAdmin

| Feld | Wert |
|---|---|
| Host | `localhost` |
| Port | `5432` |
| Database | `blackshield` |
| User | `blackshield` |
| Password | aus `.env` |

## Auf dem Hetzner-Server hochfahren

```bash
# Auf dem Server
cd ~/blackshield/deploy
docker compose -f docker-compose.yml -f docker-compose.prod.yml --env-file .env up -d
```

Wichtig: `.env` wird hier separat auf dem Server erstellt — nicht aus dem Repo.

## Backup manuell ziehen

```powershell
# Lokal:
docker compose exec postgres pg_dump -U blackshield -Fc blackshield > local-backup.dump
```

```bash
# Vom Server:
ssh bs@<server-ip> 'docker compose -f /home/bs/blackshield/deploy/docker-compose.yml exec -T postgres pg_dump -U blackshield -Fc blackshield' > prod-backup.dump
```

## Backup wieder einspielen

```powershell
docker compose exec -T postgres pg_restore -U blackshield -d blackshield --clean --if-exists < local-backup.dump
```

## Datenbank komplett resetten (lokal)

```powershell
docker compose down -v
docker compose up -d
cd ../Client
dotnet ef database update
```
