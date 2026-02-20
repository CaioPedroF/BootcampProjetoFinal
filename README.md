# Monitoramento de Equipamentos Pesados - API REST

API para gerenciar equipamentos de mina, como caminhões fora-de-estrada, escavadeiras, perfuratrizes, etc.  
Implementada em **.NET 7**, **PostgreSQL** e testável com **Insomnia**.


## Tecnologias Utilizadas
-.NET
- ASP .NET ore
- Entity Framework Core
- PosteGreSQL(Dbeaver)
- Insomia(Para Testes)


## Como Executar o Projto

### Pré-requesitos


- .NET SDK instalados
- Banco de dados Configurado


### Executar

'''Powershell
dotnet restore
dotnet run

---

## 🚀 Como rodar via Docker Compose

1. Certifique-se de ter o **Docker** e **Docker Compose** instalados.  
2. Na raiz do projeto, crie um arquivo `docker-compose.yml` com o seguinte conteúdo:

```yaml
version: '3.8'

services:
  db:
    image: postgres:16
    container_name: monitoramento-db
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: monitoramentoequipamentospesados
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD", "pg_isready", "-U", "postgres"]
      interval: 5s
      retries: 5

  api:
    build: .
    container_name: monitoramentoequipamentospesados
    depends_on:
      db:
        condition: service_healthy
    ports:
      - "5187:5187"
    environment:
      ConnectionStrings__DefaultConnection: "Host=db;Port=5432;Database=monitoramentoequipamentospesados;Username=postgres;Password=postgres"

volumes:
  pgdata:
