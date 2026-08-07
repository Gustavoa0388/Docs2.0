# DEV_STATUS.md

## Estado geral
Em desenvolvimento — DV2-DEV-001 concluída.

## Branch atual
claude/DV2-DEV-001-fundacao-tecnica

## Tarefa atual
DV2-DEV-001 — Criar a fundação técnica da solução.

## Requisitos relacionados
- CLAUDE.md — Arquitetura inicial (monólito modular, projetos)
- README_CLAUDE_CODE_SETUP.md — Primeira tarefa recomendada

## Regras relacionadas
- docs/ARCHITECTURE.md — Dependências entre projetos

## Alterações realizadas
Criada a solution `DocsViewer.sln` com os 6 projetos previstos no CLAUDE.md
(Domain, Application, Infrastructure, Web, UnitTests, IntegrationTests),
todos vazios (sem entidades, sem controllers, sem páginas, sem
autenticação/autorização, sem banco). Adicionadas somente as referências
entre projetos definidas em docs/ARCHITECTURE.md. Removido o endpoint de
exemplo do template padrão do projeto Web (`app.MapGet("/", ...)`) e os
arquivos `Class1.cs` de exemplo dos class libraries, para manter os
projetos efetivamente vazios. Adicionado `.gitignore` padrão do .NET
(bin/obj), sem relação com funcionalidade.

## Arquivos alterados
- DocsViewer.sln (novo)
- DocsViewer.Domain/DocsViewer.Domain.csproj (novo)
- DocsViewer.Application/DocsViewer.Application.csproj (novo, referencia Domain)
- DocsViewer.Infrastructure/DocsViewer.Infrastructure.csproj (novo, referencia Application e Domain)
- DocsViewer.Web/DocsViewer.Web.csproj (novo, referencia Application)
- DocsViewer.Web/Program.cs (novo, host mínimo sem endpoints)
- DocsViewer.UnitTests/DocsViewer.UnitTests.csproj (novo, sem referências)
- DocsViewer.IntegrationTests/DocsViewer.IntegrationTests.csproj (novo, sem referências)
- .gitignore (novo)

## Banco / migrations
Nenhuma — fora do escopo desta tarefa.

## Testes criados
Nenhum teste real — fora do escopo desta tarefa (apenas scaffolding dos
projetos DocsViewer.UnitTests e DocsViewer.IntegrationTests com os stubs
padrão do template xUnit).

## Resultado dos testes
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0 Error(s).
Critério de aceite da tarefa atendido.

## Decisões/assumptions
- Framework de teste adotado nos projetos de teste: xUnit. Não havia
  definição nos documentos aprovados; é o padrão atual do ecossistema
  .NET, sem impacto arquitetural. Não bloqueia a tarefa.
- Target framework: .NET 8 (LTS). Não havia versão fixada no CLAUDE.md.
- DocsViewer.Web foi criado como projeto ASP.NET Core mínimo (`dotnet new
  web`), não como Blazor Web App, porque a tarefa excluiu explicitamente
  "interface", "páginas" e "controllers" do escopo. Ver Q-001 em
  docs/handoff/OPEN_QUESTIONS.md.
- Nenhuma referência de projeto foi adicionada de/para DocsViewer.UnitTests
  e DocsViewer.IntegrationTests, pois docs/ARCHITECTURE.md não define essas
  referências e a tarefa pediu para seguir apenas o que está lá. Ver Q-002
  em docs/handoff/OPEN_QUESTIONS.md.
- Projetos organizados em pastas na raiz do repositório (sem `src/`/`tests/`),
  cada uma com o nome exato do projeto, por ser a estrutura mínima que
  atende ao pedido sem introduzir organização não solicitada.

## Riscos
Nenhum risco técnico identificado nesta tarefa. Ambiente de execução não
tinha o SDK do .NET instalado; foi instalado .NET SDK 8.0 via apt para
viabilizar o build (ferramenta de desenvolvimento, não faz parte do
código do produto).

## Pendências
- Q-001: definir modelo de hospedagem/renderização do Blazor Web App
  (Server / WebAssembly / Auto) antes de iniciar tarefas de UI.
- Q-002: definir quais projetos DocsViewer.UnitTests e
  DocsViewer.IntegrationTests devem referenciar, antes da primeira tarefa
  que inclua testes reais.

## Próximo passo
Aguardar próxima tarefa formal (ex.: fundação de banco/EF Core ou
primeiras entidades de Domain, conforme ROADMAP.md — Fase 1).
