
![Logo](logo.png)
# Lince - API em .NET

O **Lince** é um sistema completo para **monitoramento, análise e gestão
de EPIs** (Equipamentos de Proteção Individual), integrando dados em
tempo real de sensores instalados em estações e ambientes operacionais.\
A plataforma permite acompanhar uso, conformidade, alertas de segurança
e comportamento operacional, garantindo maior proteção para
colaboradores e maior controle para equipes de supervisão e segurança.

Nosso objetivo é oferecer uma solução moderna e confiável para
**monitoramento inteligente de EPIs**, reduzindo riscos, prevenindo
acidentes e centralizando informações essenciais para os times de
segurança corporativa.

## Problemas que a aplicação resolve
-   Falta de visibilidade sobre **uso correto** de EPIs.
-   Dificuldade em monitorar automaticamente **violação de áreas
    restritas**.
-   Baixa eficiência em auditorias e checklists de segurança.
-   Falta de relatórios centralizados para tomada de decisão.
-   Integração limitada entre sensores físicos e aplicações
    administrativas.

## Sobre o time

- **Arthur Algate RM:560109**: Responsável pelo banco de dados e Compliance QA.  
- **Carlos Clementino RM:561187**: Responsável pelo desenvolvimento da API em .NET e Java Spring Boot, infraestrutura e práticas de DevOps, e pela integração com dispositivos IoT.  
- **Eder Silva RM:559647**: Responsável pela criação do APP mobile.

## Como rodar a aplicação

### Pré-requisitos
- .NET 9 SDK ou superior  
- IDE recomendada: **Rider**  
- Oracle Database

### Passos para executar

1. Clone o repositório:  
```bash
git clone https://github.com/ARC-ceo/Lince-Dotnet.git
```

2. Abra o projeto no **Rider**.  


3. Execute a aplicação:  
```bash
dotnet run
```

4. A API estará disponível em: `http://localhost:5281`.

### Testando a API
A documentação dos endpoints está disponível via **Swagger UI**:  
`http://localhost:5281/`

Além disso, disponibilizamos no repositório uma **collection do Insomnia** contendo todas as requisições da API prontas para uso, facilitando os testes e a integração durante o desenvolvimento.

## Endpoints da API

A API foi documentada com **Swagger / OpenAPI**, oferecendo exemplos completos de requisição e resposta.

### Endpoints principais

| Método | Endpoint       | Descrição                                    |
|--------|----------------|---------------------------------------------|
| GET    | /supervisor       | Listar todos supervisores cadastrados       |
| PUT    | /supervisor       | Atualizar cadastro do supervisor            |
| POST   | /supervisor       | Criar cadastro de supervisor                |
| GET    | /supervisor/{id}  | Buscar cadastro do supervisor               |
| DELETE | /supervisor/{id}  | Deletar cadastro do supervisor              |

> Para todos os endpoints, exemplos detalhados de request e response estão disponíveis no **Swagger UI** e **Collection para o Insomnia** presente aqui no repositório.

## Tecnologias utilizadas
- .NET 9 / C#  
- ASP.NET Core Web API  
- Entity Framework Core  
- Oracle Database  
- Swagger / OpenAPI  

---

**Lince** — Visão total. Risco mínimo. 🦁
