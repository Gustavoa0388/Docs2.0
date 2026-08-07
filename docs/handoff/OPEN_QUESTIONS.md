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
**Status:** Aberta — decisão humana pendente. Nenhuma alteração de `TargetFramework` foi feita.
