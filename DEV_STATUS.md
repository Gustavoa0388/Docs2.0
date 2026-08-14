# DEV_STATUS.md

## Estado geral
DV2-SPRINT-001 em andamento. Etapas 1-6 concluídas: merge DV2-DEV-002,
ADR-003/.NET 10, estrutura documental, rastreabilidade, e agora
DV2-DEV-003 (fundação de persistência) com seus testes.

## Branch atual
feature/DV2-DEV-003-persistencia (a partir de `main`, já com
DV2-DEV-002, ADR-003/.NET 10 e estrutura documental integradas).

## Tarefa atual
DV2-SPRINT-001 — Etapas 5 e 6: DV2-DEV-003 — Fundação de Persistência
(EF Core + DbContext) e seus testes.

## Requisitos/decisões relacionados
- CLAUDE.md — Armazenamento (banco: metadados/config/etc.; não BLOB sem ADR)
- CLAUDE.md — Proibições (sem segredo no código, sem senha em texto)
- docs/ARCHITECTURE.md — Dependências entre projetos (ver Q-006 sobre
  Web -> Infrastructure)
- docs/handoff/OPEN_QUESTIONS.md — Q-006 (nova, informativa/não bloqueante)

## O que foi feito

### Etapa 5 — DV2-DEV-003: Fundação de Persistência
1. Adicionados a `DocsViewer.Infrastructure`: `Microsoft.EntityFrameworkCore.SqlServer`
   e `Microsoft.EntityFrameworkCore.Design` (10.0.11, compatível com
   .NET 10; `Design` como `PrivateAssets=all`, ferramenta de dev-time).
2. Criado `DocsViewer.Infrastructure/Persistence/DocsViewerDbContext.cs`
   — `DbContext` sem nenhum `DbSet`/entidade de domínio, conforme
   escopo (ainda não implementar Documento, Revisão, usuários,
   permissões ou Audit Trail).
3. Criado `DocsViewer.Infrastructure/DependencyInjection/InfrastructureServiceCollectionExtensions.cs`
   — método `AddInfrastructure(IServiceCollection, IConfiguration)` que
   registra `DocsViewerDbContext` via `AddDbContext`, usando
   `UseSqlServer` somente se a connection string
   (`ConnectionStrings:DocsViewerDatabase`) estiver configurada. Sem
   Repository genérico, sem Unit of Work customizado, sem CQRS/MediatR.
4. `DocsViewer.Web/Program.cs` chama `builder.Services.AddInfrastructure(builder.Configuration)`.
   Isso exigiu adicionar a referência de projeto `Web -> Infrastructure`,
   que não está na lista de dependências de `docs/ARCHITECTURE.md` —
   registrado como **Q-006** (não bloqueante; justificado pelo próprio
   princípio "Web contém... composição" já em `ARCHITECTURE.md`).
5. Connection string de **exemplo**, sem credenciais reais, adicionada
   apenas a `appsettings.Development.json`
   (`Server=localhost,1433;Database=DocsViewerOmni;Trusted_Connection=True;TrustServerCertificate=True;`
   — sem usuário/senha). `UserSecretsId` configurado em
   `DocsViewer.Web.csproj` (via `dotnet user-secrets init`) como
   mecanismo padrão do ASP.NET Core para segredos locais reais; nenhum
   segredo foi armazenado no repositório.
6. **Migrations:** testado na prática (`dotnet ef migrations add`, com
   `dotnet-ef` instalado temporariamente) — o EF Core permite gerar uma
   migration mesmo sem nenhuma entidade, mas ela sai **completamente
   vazia** (`Up`/`Down` sem nenhuma operação), confirmando que não há
   migration útil a criar nesta etapa. A migration de sondagem foi
   removida manualmente (não commitada). Nenhuma tabela fictícia ou
   entidade `Test`/`Sample`/`Dummy` foi criada. A primeira migration
   real fica para a tarefa que introduzir as primeiras entidades de
   domínio.
7. **Startup/Health:** a aplicação não depende de banco configurado para
   mostrar a fundação visual — nada na UI (DV2-DEV-002) injeta
   `DocsViewerDbContext` ainda. Erro de configuração não é mascarado:
   sem connection string, `AddDbContext` registra o contexto mas sem
   provider; usá-lo de fato lança `InvalidOperationException` explícita
   (validado em teste).

### Etapa 6 — Testes da persistência
Adicionados a `DocsViewer.IntegrationTests/Persistence/` (projeto já
referencia `DocsViewer.Infrastructure`, conforme Q-002):
- `InfrastructureServiceCollectionExtensionsTests`: `AddInfrastructure`
  registra `DocsViewerDbContext` no DI; a connection string é lida
  corretamente da configuração; sem connection string, o contexto ainda
  é registrado mas usá-lo lança erro claro (não mascarado).
- `InfrastructureLayeringTests`: confirma via reflection que o assembly
  `DocsViewer.Infrastructure` não referencia `DocsViewer.Web` (separação
  de camadas).

Pacotes de teste adicionados a `DocsViewer.IntegrationTests`:
`Microsoft.Extensions.Configuration` (necessário para montar
`IConfiguration` em memória nos testes). `Microsoft.Extensions.Configuration.Binder`
foi adicionado e depois removido por não ter sido usado.

Nenhum teste artificial foi criado. `DocsViewer.UnitTests` permanece
vazio (nada de Domain/Application para testar ainda).

## Arquivos criados/alterados
- DocsViewer.Infrastructure/DocsViewer.Infrastructure.csproj (pacotes EF Core)
- DocsViewer.Infrastructure/Persistence/DocsViewerDbContext.cs (novo)
- DocsViewer.Infrastructure/DependencyInjection/InfrastructureServiceCollectionExtensions.cs (novo)
- DocsViewer.Web/DocsViewer.Web.csproj (referência a Infrastructure; pacote EF Core Design)
- DocsViewer.Web/Program.cs (AddInfrastructure)
- DocsViewer.Web/appsettings.Development.json (connection string de exemplo)
- DocsViewer.IntegrationTests/DocsViewer.IntegrationTests.csproj (pacote Configuration)
- DocsViewer.IntegrationTests/Persistence/InfrastructureServiceCollectionExtensionsTests.cs (novo)
- DocsViewer.IntegrationTests/Persistence/InfrastructureLayeringTests.cs (novo)
- docs/handoff/OPEN_QUESTIONS.md (Q-006 nova)
- DEV_STATUS.md (este arquivo)

## Banco / Migrations
Nenhuma migration criada (ver item 6 acima — nenhuma seria útil sem
entidades). Nenhum banco real foi provisionado ou acessado nesta tarefa.

## Testes
`dotnet test DocsViewer.sln`:
- DocsViewer.UnitTests: 0 testes (sem alteração; nada a testar ainda).
- DocsViewer.IntegrationTests: **4 testes, 4 aprovados, 0 falhas.**

## Resultado de restore
`dotnet restore DocsViewer.sln` — sem erros.

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, **0 Warning(s), 0
Error(s)**.

## Resultado da execução
`dotnet run --project DocsViewer.Web` — aplicação sobe sem exceções,
sem depender de banco configurado; `/` e `/documentos` retornam 200.
Nenhum erro de configuração mascarado (comportamento validado também em
teste automatizado).

## Decisões tomadas
- Referência `Web -> Infrastructure` adicionada apesar de não constar
  explicitamente em `docs/ARCHITECTURE.md` — necessária para "Configurar
  DI adequadamente" (item explícito da tarefa) e justificada pelo
  próprio princípio "Web contém... composição" já documentado. Registrada
  como Q-006 para confirmação/formalização, sem bloquear a tarefa.
- Nenhuma migration criada nesta etapa — confirmado experimentalmente
  que seria vazia/inútil sem entidades.
- Connection string real fica fora do repositório (User Secrets/variável
  de ambiente); apenas um exemplo sem credenciais em
  `appsettings.Development.json`.

## Assumptions
Nenhuma nova assumption além das decisões acima, já justificadas.

## Riscos
- Q-006 (nova, não bloqueante): grafo de dependências documentado em
  `docs/ARCHITECTURE.md` desatualizado em relação à referência
  `Web -> Infrastructure` necessária para composição de DI.
- Riscos herdados: Q-004 (documentos oficiais v0.2 ainda ausentes)
  segue válida e sem mudança nesta etapa.

## Pendências
- Q-004: aguardando documentos oficiais v0.2 no repositório.
- Q-006: confirmar/formalizar a referência Web -> Infrastructure em
  docs/ARCHITECTURE.md (edição simples) ou via ADR, conforme decisão do
  responsável do projeto.
- Primeira migration real: fica para a tarefa que introduzir as
  primeiras entidades de domínio (ex.: DV2-DEV-004).

## Próximo passo sugerido
Prosseguir com a Etapa 7 do DV2-SPRINT-001 (proposta DV2-DEV-004, sem
implementar), conforme planejado.
