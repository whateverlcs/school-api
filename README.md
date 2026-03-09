# 🏫 School API

<p align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/whateverlcs/school-api?color=black">
  
  <img alt="GitHub Size Repository" src="https://img.shields.io/github/repo-size/whateverlcs/school-api?color=black">
    
  <img alt="GitHub Last Commit" src="https://img.shields.io/github/last-commit/whateverlcs/school-api?color=black">
  
  <img alt="GitHub Stars Repository" src="https://img.shields.io/github/stars/whateverlcs/school-api?style=social">
</p>

API desenvolvida com o objetivo de praticar e aprimorar conceitos de arquitetura em camadas, validações, autenticação via JWT, migrações, mapeamento de entidades e boas práticas no desenvolvimento backend utilizando .NET.

A aplicação realiza o gerenciamento de **Escolas**, **Usuários** e **Estudantes**, incluindo autenticação, associação de estudantes à escola e consultas filtradas.

## 📌 Índice

- Arquitetura
- Tecnologias e Pacotes Utilizados
- Como Executar o Projeto
- Migrations & Banco de Dados
- Autenticação
- Coleção de Endpoints
- Health Check
- Boas Práticas Utilizadas
- Licença

## 🧩 Arquitetura

O projeto segue uma abordagem organizada em camadas:

/src
- School.API — Camada de apresentação (Controllers, Swagger, Middlewares)
- School.Application — Casos de uso, DTOs, Validations, Services
- School.Communication — Enums, Requests, Responses
- School.Domain — Entidades, Interfaces, Regras de negócio
- School.Exceptions — Exceções e mensagens de erro
- School.Infrastructure — EF Core, Repositórios, Migrations, Dapper

Outros pontos da arquitetura:

- Uso de FluentValidation para validações
- AutoMapper para mapeamento de objetos
- Sqids para geração de IDs encodados
- JWT Authentication
- Dapper + EF Core + FluentMigrator
- Health Check com EF Core
- BCrypt para hashing de senha

## 🛠 Tecnologias e Pacotes Utilizados

### Plataforma
- .NET 8
- ASP.NET Core Web API
- SQL Server

### Infraestrutura e Persistência
- Entity Framework Core
- Dapper
- FluentMigrator / FluentMigrator.Runner

### Segurança e Utilidades
- System.IdentityModel.Tokens.Jwt
- BCrypt.Net-Next
- Sqids

### Mapeamento e Validação
- AutoMapper
- FluentValidation

### Observabilidade
- HealthChecks
- HealthChecks.EntityFrameworkCore

### API & Documentação
- Swashbuckle.AspNetCore (Swagger)

## 🚀 Como Executar o Projeto

1. Clonar o repositório
2. Configurar infraestrutura
3. Aplicar migrations
4. Rodar a API
5. Acessar o Swagger

## 🔐 Autenticação

A API utiliza JWT Bearer Token.

Fluxo:
1. Criar usuário (POST /user)
2. Realizar login (POST /login)
3. Receber token e refresh token
4. Revalidar quando expirar: POST /token/refresh-token

Enviar token no header Authorization: Bearer <TOKEN>

## 📚 Endpoints

🏫 Academy
- POST /academy
- GET /academy
- GET /academy/{id}
- PUT /academy/{id}
- DELETE /academy/{id}
- POST /academy/name
- POST /academy/state
- POST /academy/city

👤 Student
- POST /student
- GET /student
- GET /student/{id}
- PUT /student/{id}
- DELETE /student/{id}
- GET /student/academy/{id}

🎓 User
- POST /user
- GET /user
- PUT /user
- PUT /user/change-password

🔐 Login & Token
- POST /login
- POST /token/refresh-token

❤️ Health Check
- GET /health

## 🧱 Boas Práticas

- Clean Architecture
- DDD-light
- DTOs
- Repository Pattern
- FluentValidation
- AutoMapper
- BCrypt
- Logs e tratamento de erros

## 📄 Licença

Projeto livre para estudos e uso educacional.
