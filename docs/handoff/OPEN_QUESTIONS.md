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
**Pergunta:** DocsViewer.Web foi criado como projeto ASP.NET Core mínimo (`dotnet new web`, sem páginas/componentes) porque a tarefa DV2-DEV-001 excluiu explicitamente "interface", "páginas" e "controllers" do escopo. O CLAUDE.md aprova "Blazor Web App" como tecnologia, mas não define o modelo de hospedagem/renderização (Server, WebAssembly ou Auto).
**Decisão:** Blazor Web App com **Interactive Server** como modelo principal de renderização da primeira versão. WebAssembly e Auto descartados nesta fase. Decisão registrada em `docs/decisions/ADR-001-blazor-web-app-interactive-server.md`.
**Status:** Resolvida (2026-08-07 — ver ADR-001)

---

### Q-002
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001
**Requisito:** ARCHITECTURE.md — Dependências
**Pergunta:** Quais projetos os projetos de teste devem referenciar?
**Decisão:** `DocsViewer.UnitTests` referencia `DocsViewer.Domain` e `DocsViewer.Application`. `DocsViewer.IntegrationTests` referencia `DocsViewer.Application` e `DocsViewer.Infrastructure`. Referência para `DocsViewer.Web` somente quando um teste futuro efetivamente necessitar do host Web.
**Status:** Resolvida (2026-08-07)

---

### Q-003
**Data:** 2026-08-07
**Tarefa:** DV2-DEV-001 / DV2-SPRINT-001
**Requisito:** Framework-base do produto
**Pergunta:** Permanecer em .NET 8 LTS ou migrar para .NET 10 LTS?
**Decisão:** Adotar .NET 10 LTS. Decisão registrada em `docs/decisions/ADR-003-dotnet-10-lts.md`; todos os projetos migrados para `net10.0`.
**Status:** Resolvida (2026-08-14 — ver ADR-003)

---

### Q-004
**Data:** 2026-08-10
**Tarefa:** DV2-DEV-002 / DV2-DOC-002
**Requisito:** Documentação oficial do produto
**Pergunta:** Os documentos oficiais vigentes existiam fora do repositório e ainda não haviam sido incorporados ao GitHub.
**Impacto:** Impedia que tarefas de domínio e rastreabilidade utilizassem diretamente as fontes documentais controladas.
**Decisão:** A tarefa documental `DV2-DOC-002` incorporou ao repositório as referências ativas: `DV2-000 Product Vision v0.2 Draft`, `DV2-001 Documento de Fundação v0.4.2 Draft`, `DV2-PMP-001 v0.1 Draft`, `DV2-URS-001 v0.3 Draft`, `DV2-BRN-001 v0.2 Draft corrigido` e `DV2-TRM-001 v0.1`.
**Status:** Resolvida (2026-08-15 — DV2-DOC-002)

---

### Q-005
**Data:** 2026-08-10
**Tarefa:** DV2-REPO-001
**Requisito:** Governança de branches/PRs
**Pergunta:** Qual a ordem de integração dos PRs iniciais e qual base utilizar para desenvolvimento?
**Decisão:** PR #1 e PR #3 foram mergeados em `main`; PR #2 foi deliberadamente não mergeado por estar superado; a DEV-002 foi rebaseada sobre `main` consolidada.
**Status:** Resolvida (2026-08-10)

---

### Q-006
**Data:** 2026-08-14
**Tarefa:** DV2-DEV-003 / DV2-DEV-004
**Requisito:** docs/ARCHITECTURE.md — Dependências
**Pergunta:** Formalizar `Web -> Infrastructure` para composição/DI ou exigir ADR específico?
**Decisão:** `DocsViewer.Web` pode depender de `DocsViewer.Infrastructure` exclusivamente como Composition Root, para registro e configuração de implementações de infraestrutura na inicialização. Permanecem proibidas as dependências `Domain -> Infrastructure`, `Domain -> Web`, `Application -> Infrastructure`, `Application -> Web` e `Infrastructure -> Web`. A relação foi formalizada em `docs/ARCHITECTURE.md` pela DV2-DEV-004.
**Status:** Resolvida (2026-08-15 — DV2-DEV-004)

---

### Q-007
**Data:** 2026-08-15
**Tarefa:** DV2-DEV-004 / DV2-DOC-002
**Requisito:** Documentação oficial do produto
**Pergunta:** O pacote documental oficial citado pela DV2-DEV-004 não foi localizado no ambiente do Claude Code.
**Decisão:** Os seis artefatos oficiais em Draft foram incorporados à branch documental `docs/DV2-DOC-002-official-drafts` e verificados diretamente no GitHub: Product Vision v0.2, Fundação v0.4.2, PMP v0.1, URS v0.3, BRN v0.2 corrigido e TRM v0.1.
**Status:** Resolvida (2026-08-15 — DV2-DOC-002)

---

### Q-008
**Data:** 2026-08-15
**Tarefa:** DV2-DEV-004
**Requisito:** Configuração organizacional / domínio documental
**Pergunta:** Deve ser introduzido `OrganizationId` nas entidades de domínio nesta fase?
**Decisão:** Não modelar `OrganizationId` nesta fase. A capacidade de configuração por organização não implica, por si só, arquitetura multi-tenant. O modelo de tenancy deverá ser definido em decisão arquitetural específica antes de introduzir chave organizacional nas entidades.
**Status:** Resolvida (2026-08-15 — decisão do responsável do produto)

---

### Q-009
**Data:** 2026-08-15
**Tarefa:** DV2-DEV-004
**Requisito:** Disponibilização de representações documentais
**Pergunta:** Quais estados internos de disponibilização devem ser modelados no domínio?
**Decisão:** A ausência de estados definitivos não bloqueia o domínio estrutural da DV2-DEV-004. A modelagem de disponibilização será tratada na DV2-DEV-005/FRS correspondente, distinguindo explicitamente estado interno de disponibilização do DocsViewer da vigência/obsolescência documental definida externamente. Nomes antigos de permissões não devem ser tratados como estados de domínio.
**Status:** Resolvida para a DV2-DEV-004; modelagem funcional pendente da DV2-DEV-005 (2026-08-15)
