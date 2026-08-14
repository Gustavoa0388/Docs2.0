# OPEN_QUESTIONS.md

## Modelo
### Q-000
**Data:**  
**Tarefa:**  
**Requisito:**  
**Pergunta:**  
**Impacto:**  
**Opções identificadas (sem escolher):**  
**Status:** Aberta

---

### Q-001
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001
**Requisito:** CLAUDE.md — Tecnologias provisoriamente aprovadas (Blazor Web App)
**Pergunta:** DocsViewer.Web foi criado como projeto ASP.NET Core mínimo (`dotnet new web`, sem páginas/componentes) porque a tarefa DV2-DEV-001 excluiu explicitamente "interface", "páginas" e "controllers" do escopo. O CLAUDE.md aprova "Blazor Web App" como tecnologia, mas não define o modelo de hospedagem/renderização (Server, WebAssembly ou Auto). Qual modelo deve ser adotado quando a UI entrar em escopo, e em qual tarefa isso deve ser decidido/formalizado (ADR)?
**Impacto:** Afeta a configuração de Program.cs, estrutura de componentes, App.razor e possivelmente a arquitetura de comunicação cliente-servidor do módulo Viewer.
**Opções identificadas (sem escolher):** Blazor Server; Blazor WebAssembly hospedado; Blazor Web App com render mode Auto (novidade do .NET 8, combina Server e WASM).
**Decisão:** Blazor Web App com **Interactive Server** como modelo principal de renderização da primeira versão. WebAssembly e Auto descartados nesta fase. Decisão do responsável do projeto, registrada em `docs/decisions/ADR-001-blazor-web-app-interactive-server.md`. Implementação do scaffolding Blazor em `DocsViewer.Web` fica para tarefa futura de UI/Viewer.
**Status:** Resolvida (2026-08-07 — ver ADR-001)

---

### Q-002
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001
**Requisito:** ARCHITECTURE.md — Dependências
**Pergunta:** ARCHITECTURE.md não define referências entre projetos para DocsViewer.UnitTests e DocsViewer.IntegrationTests. A tarefa DV2-DEV-001 pediu para adicionar "somente as referências entre projetos conforme ARCHITECTURE.md", então nenhuma referência foi adicionada de/para os projetos de teste. Quais projetos cada um deve referenciar quando os primeiros testes forem escritos?
**Impacto:** Necessário antes de qualquer tarefa que inclua testes unitários ou de integração reais.
**Opções identificadas (sem escolher):** UnitTests referenciando Domain e Application; IntegrationTests referenciando Infrastructure e/ou Web; definição caso a caso por tarefa.
**Decisão:** `DocsViewer.UnitTests` referencia `DocsViewer.Domain` e `DocsViewer.Application`. `DocsViewer.IntegrationTests` referencia `DocsViewer.Application` e `DocsViewer.Infrastructure`. Referência de `DocsViewer.IntegrationTests` para `DocsViewer.Web` só será adicionada quando um teste futuro precisar efetivamente subir/testar o host Web. Decisão do responsável do projeto, registrada no fechamento da DV2-DEV-001.
**Status:** Resolvida (2026-08-07)

---

### Q-003
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001 (fechamento)
**Requisito:** CLAUDE.md — Tecnologias provisoriamente aprovadas (C#, ASP.NET Core); não trocar tecnologia sem ADR aprovado
**Pergunta:** O projeto foi iniciado em agosto de 2026 com `TargetFramework net8.0`. Deve permanecer em .NET 8 LTS ou migrar para .NET 10 LTS antes de avançar a fundação técnica?
**Impacto:** Afeta o ciclo de suporte de segurança/patches do projeto, a janela de compatibilidade de pacotes (EF Core, ASP.NET Core, Blazor) e o custo de uma futura migração (hoje é praticamente zero, pois não há código de domínio, EF Core configurado nem UI implementada; tende a crescer conforme o projeto avança).

**Análise objetiva (dados verificados em pesquisa nesta data):**
| | .NET 8 (LTS) | .NET 10 (LTS) |
|---|---|---|
| Lançamento | Novembro/2023 | Novembro/2025 |
| Fim de suporte | **10/11/2026** (~3 meses após o início do projeto) | 11/2028 |
| Maturidade em 2026-08-07 | ~2 anos e 9 meses em produção | ~9 meses em produção (GA, já usado em produção por outros projetos) |
| EF Core / ASP.NET Core / Blazor | Versões correspondentes ao .NET 8 | EF Core 10 (JSON nativo, LINQ ampliado), Blazor com melhorias de persistência de estado e resiliência, ASP.NET Core com OpenAPI 3.1 e melhorias de segurança/diagnóstico |
| Custo de migração agora | — | Baixo: solution ainda não tem entidades, EF Core, UI ou integrações configuradas (apenas `TargetFramework` e pacotes de teste a ajustar) |

**Recomendação (não implementada — aguardando decisão humana):** Migrar para .NET 10 LTS antes de aprofundar a fundação técnica. Justificativa: o .NET 8 encerra suporte em ~3 meses (11/2026), antes mesmo de o projeto provavelmente sair da fase de fundação; permanecer nele significa iniciar um projeto novo já com prazo curto até ficar sem patches de segurança. O .NET 10 já é LTS estável (GA há ~9 meses) e o custo de troca é mínimo agora, tendendo a aumentar a cada tarefa futura que adicionar código.

**Opções identificadas (sem escolher pela IA — decisão humana):** (1) Migrar para .NET 10 LTS agora; (2) Permanecer em .NET 8 LTS e planejar migração antes de 11/2026; (3) Permanecer em .NET 8 LTS e reavaliar em tarefa futura específica.
**Decisão:** Migrar para .NET 10 LTS agora (opção 1). Decisão fornecida pelo responsável do projeto na tarefa DV2-SPRINT-001 (Etapa 2) e registrada formalmente em `docs/decisions/ADR-003-dotnet-10-lts.md`. Todos os 6 projetos da solution migrados para `net10.0`; `dotnet restore` e `dotnet build DocsViewer.sln` sem erros/warnings; aplicação executada e Interactive Server confirmado funcional em .NET 10.
**Status:** Resolvida (2026-08-14 — ver ADR-003)

---

### Q-004
**Data:** 2026-08-10
**Tarefa:** DV2-DEV-002
**Requisito:** Item 3 da tarefa DV2-DEV-002 ("Documentação oficial")
**Pergunta:** A tarefa DV2-DEV-002 lista como documentação oficial a ser lida antes da implementação: `DV2-000 — Product Vision`, `DV2-001 — Documento de Fundação`, `DV2-PMP-001 — Plano Mestre do Projeto` e `DV2-BRN-001 — Regras de Negócio`. Nenhum desses quatro documentos existe no repositório — nem em `main`, nem em nenhuma branch (verificado via listagem de árvore em `main`, `docs/ADR-002-web-core-shell-clients` e `docs/DV2-URS-001-v0.1`). Esses documentos ainda não foram criados/versionados, ou existem em outro local fora deste repositório?
**Impacto:** Nesta tarefa (DV2-DEV-002) o impacto foi nulo, pois o escopo é puramente shell/layout visual, sem regra de negócio. Passa a ser bloqueante para qualquer tarefa futura que dependa de Product Vision, Documento de Fundação, Plano Mestre ou Regras de Negócio formalizados (ex.: modelagem de Documento/Revisão, permissões, Audit Trail).
**Opções identificadas (sem escolher):** os documentos ainda não foram redigidos e precisam ser criados/versionados; existem em ferramenta externa (Google Drive, Word, etc.) e ainda não foram trazidos ao repositório; ou a lista da tarefa antecipou nomes de documentos previstos no Plano Mestre que ainda serão elaborados.
**Atualização (2026-08-10, DV2-REPO-001):** a tarefa de consolidação da linha principal informou que as versões vigentes mais recentes são `DV2-000 Product Vision v0.2`, `DV2-001 Documento de Fundação v0.4.2`, `DV2-PMP-001 vigente`, `DV2-URS-001 v0.2` e `DV2-BRN-001 v0.2 corrigido`, mas confirmou que **nenhum desses arquivos foi trazido ao repositório nesta tarefa** (por instrução explícita — serão adicionados em tarefa documental separada). Nenhum conteúdo desses documentos foi reconstruído de memória. O PR #2 (baseado na URS v0.1) foi marcado como superado por essas versões e não foi mergeado — ver comentário em https://github.com/Gustavoa0388/Docs2.0/pull/2.
**Status:** Aberta — permanece aberta até os documentos v0.2 acima serem efetivamente adicionados ao repositório.

---

### Q-005
**Data:** 2026-08-10
**Tarefa:** DV2-DEV-002
**Requisito:** Item 4 da tarefa DV2-DEV-002 ("Branch") — "garantir que nenhuma alteração documental pendente seja perdida"
**Pergunta:** Existem 3 Pull Requests abertos como draft, nenhum mergeado em `main`: PR #1 (`claude/DV2-DEV-001-fundacao-tecnica` — fundação técnica/código), PR #2 (`docs/DV2-URS-001-v0.1` — URS v0.1 Draft) e PR #3 (`docs/ADR-002-web-core-shell-clients` — ADR-002, núcleo Web + clientes-shell). Como `main` ainda não contém nem o código da DV2-DEV-001 nem esses documentos, a branch `feature/DV2-DEV-002-blazor-foundation` desta tarefa foi criada a partir de `claude/DV2-DEV-001-fundacao-tecnica` (única base com a solução .NET presente), e não de `main`. Qual a ordem correta de integração desses PRs (#1, #2, #3 e agora o desta tarefa) em `main`?
**Impacto:** Enquanto os PRs não forem revisados/mergeados, cada nova tarefa de código precisa continuar partindo de `claude/DV2-DEV-001-fundacao-tecnica` (ou de uma branch equivalente) em vez de `main`, e a árvore de branches tende a divergir mais. Não é um impedimento técnico para o trabalho em si, mas é uma decisão de governança do repositório.
**Opções identificadas (sem escolher):** revisar e mergear PR #1 antes de iniciar novas tarefas de código; manter o encadeamento atual (branches de código a partir de `claude/DV2-DEV-001-fundacao-tecnica`) até uma revisão consolidada; mergear os PRs documentais (#2, #3) independentemente do código, já que não conflitam com ele.
**Decisão:** Resolvida pela tarefa DV2-REPO-001 (consolidação da linha principal). PR #1 e PR #3 foram mergeados em `main` via merge commit, preservando os commits originais (`7cf0f2d`, `5c69b93`, `0a07d12`) sem reescrevê-los. PR #2 foi deliberadamente **não** mergeado (ver Q-004) e permanece registrado como superado, com a branch preservada para referência histórica. A branch `feature/DV2-DEV-002-blazor-foundation` foi rebaseada sobre a `main` consolidada, ficando com diff restrito apenas aos arquivos da DEV-002.
**Status:** Resolvida (2026-08-10 — main consolidada com DV2-DEV-001 + ADR-002; fragmentação de PRs de código/ADR-001 resolvida)
