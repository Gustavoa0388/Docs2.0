# DEV_STATUS.md

## Estado geral
DV2-DEV-002 concluída — fundação executável Blazor entregue.

## Branch atual
feature/DV2-DEV-002-blazor-foundation (criada a partir de
claude/DV2-DEV-001-fundacao-tecnica — ver observação de base em "Riscos").

## Tarefa atual
DV2-DEV-002 — Fundação Executável do DocsViewer Omni (converter
DocsViewer.Web em Blazor Web App com Interactive Server, layout base,
dashboard mínimo, identidade visual sóbria).

## Requisitos/decisões relacionados
- CLAUDE.md — Arquitetura inicial; Tecnologias provisoriamente aprovadas
- docs/ARCHITECTURE.md — Dependências entre projetos
- docs/decisions/ADR-001-blazor-web-app-interactive-server.md — Interactive Server (aprovado na DV2-DEV-001)
- docs/decisions/ADR-002-web-core-com-clientes-shell-opcionais.md — núcleo Web centralizado (lido a partir da branch docs/ADR-002-web-core-shell-clients, ainda não mergeada em main; ver "Riscos")

## Arquitetura aplicada
DocsViewer.Web convertido em Blazor Web App (Interactive Server), sem
criar projeto paralelo. Estrutura adicionada:
- `Program.cs` — registra `AddRazorComponents().AddInteractiveServerComponents()` e mapeia `AddInteractiveServerRenderMode()`.
- `Components/App.razor`, `_Imports.razor`, `Routes.razor`, `Pages/Error.razor` — padrão do template Blazor Web App (.NET 8, gerado localmente com `dotnet new blazor --interactivity Server --empty` apenas como referência, depois integrado manualmente ao projeto existente).
- `Components/Layout/MainLayout.razor(.css)` — cabeçalho ("DocsViewer Omni" + "Versão em desenvolvimento" + placeholder de usuário) e composição do corpo (menu lateral + conteúdo).
- `Components/Layout/NavMenu.razor(.css)` — menu lateral (Início, Documentos, Favoritos, Solicitações, Administração).
- `Components/Pages/Home.razor` — dashboard com os 4 cards pedidos.
- `Components/Pages/NotImplemented.razor` — página compartilhada "Funcionalidade ainda não implementada", roteada em `/documentos`, `/favoritos`, `/solicitacoes`, `/administracao`.
- `wwwroot/app.css` — identidade visual (azul corporativo, fundo claro, sem framework CSS externo), com um breakpoint responsivo (~768px).
- `wwwroot/favicon.svg` — ícone simples com a marca "DV" (mesma identidade do cabeçalho).

DocsViewer.Domain, Application e Infrastructure não foram alterados —
nenhuma dependência nova foi introduzida em nenhum projeto (sem MediatR,
CQRS, AutoMapper, Redis, Docker, message bus).

## Migrations
Nenhuma.

## Banco
Não implementado — fora do escopo desta tarefa.

## Testes
Nenhum teste novo criado (não havia comportamento de negócio a testar).
Nenhum teste de template residual encontrado (UnitTest1.cs já havia sido
removido no fechamento da DV2-DEV-001). Confirmado que
DocsViewer.UnitTests e DocsViewer.IntegrationTests continuam compilando
normalmente dentro da solution.

## Resultado de restore
`dotnet restore` — "All projects are up-to-date for restore." Sem erros.

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0 Error(s).

## Resultado da execução
`dotnet run --project DocsViewer.Web` (perfil http, ASPNETCORE_ENVIRONMENT=Development):
- aplicação iniciou sem exceções (log revisado, sem stack trace/erro);
- `GET /` → 200, HTML renderizado no servidor com o layout completo;
- `GET /documentos`, `/favoritos`, `/solicitacoes`, `/administracao` → 200 (página "Funcionalidade ainda não implementada");
- `GET /app.css`, `/DocsViewer.Web.styles.css`, `/_framework/blazor.web.js` → 200 (arquivos estáticos e script do circuito Interactive Server carregam);
- validado também via navegador real (Chromium headless/Playwright): título da página, cabeçalho, item de menu ativo, navegação client-side (clique em "Documentos" troca de página sem reload completo, confirmando o circuito SignalR do Interactive Server funcional), captura de tela em largura desktop (1280px) e tablet (820px), sem erros de console/rede (após adicionar favicon.svg, que eliminou o único 404 encontrado na primeira validação).

## Decisões tomadas
- Base da branch: `feature/DV2-DEV-002-blazor-foundation` criada a partir
  de `claude/DV2-DEV-001-fundacao-tecnica` (não de `main`), pois `main`
  ainda não contém a solução .NET da DV2-DEV-001 (PR #1 aberto, não
  mergeado). Ver Q-005 em OPEN_QUESTIONS.md.
- Conversão do projeto Web feita manualmente, copiando/adaptando a
  estrutura gerada por `dotnet new blazor --interactivity Server --empty`
  em diretório temporário fora do repositório (apenas como referência de
  boilerplate correto), em vez de rodar o template diretamente sobre
  DocsViewer.Web — para não arriscar sobrescrever o `.csproj`/referência
  já existente.
- Identidade visual construída com CSS próprio (sem Bootstrap ou outro
  framework CSS), variáveis de cor centralizadas (`:root`), para manter a
  solução simples e sem dependências novas.
- Itens de menu não implementados apontam para uma única página
  compartilhada "Funcionalidade ainda não implementada" (em vez de
  desabilitados), para comprovar navegação real funcionando.
- Favicon simples (SVG com a marca "DV") adicionado após validação no
  navegador revelar 404 em `/favicon.ico` — ajuste de scaffolding, não
  decisão de identidade visual definitiva.

## Assumptions
- Nenhuma nova assumption técnica além das já registradas na DV2-DEV-001
  (xUnit como framework de teste; net8.0 como TargetFramework, mantido
  sem alteração nesta tarefa).

## Riscos
- **Base de branch:** `main` ainda não contém nem o código da DV2-DEV-001
  nem os documentos das PRs #2/#3 (URS v0.1, ADR-002). Enquanto esses PRs
  não forem revisados/mergeados, novas tarefas de código continuarão
  precisando partir de `claude/DV2-DEV-001-fundacao-tecnica` (ou desta
  branch) em vez de `main`. Não bloqueou esta tarefa, mas é uma pendência
  de governança do repositório — ver Q-005.
- **Documentos oficiais ausentes:** DV2-000, DV2-001, DV2-PMP-001 e
  DV2-BRN-001, citados na tarefa, não existem em nenhuma branch do
  repositório — ver Q-004. Sem impacto nesta tarefa (escopo puramente
  visual), mas bloqueante para tarefas futuras de domínio/negócio.
- Risco já conhecido: .NET 8 LTS encerra suporte em 10/11/2026 — ver Q-003
  (ainda aberta, sem decisão).

## Pendências
- Q-003: decisão humana sobre migrar (ou não) para .NET 10 LTS.
- Q-004: origem/existência de DV2-000, DV2-001, DV2-PMP-001, DV2-BRN-001.
- Q-005: ordem de integração dos PRs #1, #2, #3 e desta tarefa em `main`.
- Referência de DocsViewer.IntegrationTests para DocsViewer.Web, quando
  houver teste real que precise subir o host Web (Q-002, já decidida
  quanto ao critério, ainda não aplicável).
- Implementação real dos clientes-shell Windows/Android (ADR-002) —
  explicitamente fora do escopo desta tarefa.

## Próximo passo sugerido
Revisar e decidir sobre a integração dos PRs pendentes (#1, #2, #3) em
`main` antes de abrir novas tarefas de código, para que a base deixe de
divergir. Em paralelo, decisão humana sobre Q-003 (.NET 8 vs .NET 10) e
esclarecimento de Q-004 (documentos ausentes). Tecnicamente, a próxima
tarefa de produto seria a primeira funcionalidade real de domínio
(Documento/Revisão) ou banco/EF Core, conforme ROADMAP.md — Fase 1/2,
porém isso depende das definições acima.
