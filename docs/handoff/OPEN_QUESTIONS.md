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
**Status:** Aberta

---

### Q-002
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001
**Requisito:** ARCHITECTURE.md — Dependências
**Pergunta:** ARCHITECTURE.md não define referências entre projetos para DocsViewer.UnitTests e DocsViewer.IntegrationTests. A tarefa DV2-DEV-001 pediu para adicionar "somente as referências entre projetos conforme ARCHITECTURE.md", então nenhuma referência foi adicionada de/para os projetos de teste. Quais projetos cada um deve referenciar quando os primeiros testes forem escritos?
**Impacto:** Necessário antes de qualquer tarefa que inclua testes unitários ou de integração reais.
**Opções identificadas (sem escolher):** UnitTests referenciando Domain e Application; IntegrationTests referenciando Infrastructure e/ou Web; definição caso a caso por tarefa.
**Status:** Aberta
