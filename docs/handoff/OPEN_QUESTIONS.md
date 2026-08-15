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
**Opções identificadas (sem escolher):** Blazor Server; Blazor WebAssembly hospedado; Blazor Web App com render mode Auto.
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
**Tarefa:** DV2-DEV-003
**Requisito:** docs/ARCHITECTURE.md — Dependências
**Pergunta:** Formalizar `Web -> Infrastructure` para composição/DI ou exigir ADR específico?
**Impacto:** Afeta o grafo formal de dependências da solução.
**Opções identificadas (sem escolher):** atualizar `ARCHITECTURE.md`; criar ADR; manter como detalhe não documentado.
**Decisão:** `Web -> Infrastructure` formalizado em `docs/ARCHITECTURE.md` exclusivamente como Composition Root (registro de implementações de infraestrutura na inicialização, ex.: `Program.cs`). As demais regras de dependência do monólito modular permanecem inalteradas e proibidas (`Domain -> Infrastructure`, `Domain -> Web`, `Application -> Infrastructure`, `Application -> Web`, `Infrastructure -> Web`).
**Status:** Resolvida (2026-08-15 — DV2-DEV-004, ver docs/ARCHITECTURE.md)

---

### Q-007
**Data:** 2026-08-15
**Tarefa:** DV2-SPRINT-002
**Requisito:** Documentação oficial do produto (Q-004)
**Pergunta:** Com os 6 documentos oficiais incorporados ao repositório pela DV2-DOC-002 (`DV2-000`, `DV2-001`, `DV2-PMP-001`, `DV2-URS-001 v0.3`, `DV2-BRN-001 v0.2`, `DV2-TRM-001 v0.1`), esses arquivos estão de fato utilizáveis como fonte de verdade para revisão documental de tarefas de domínio?
**Impacto:** Determina se a revisão documental da DV2-DEV-004 (e tarefas futuras) pode se basear nos arquivos `.docx`/`.xlsx` reais ou depende de fontes alternativas.
**Achado (não presumir corrigido sem verificação):** Verificação técnica (comando `file`, dump hexadecimal e `python zipfile.ZipFile`) confirmou que **4 dos 6 arquivos estão corrompidos/ilegíveis** no commit atual de `main`: `DV2-000_Product_Vision_v0.2_Draft.docx`, `DV2-001_Documento_de_Fundacao_v0.4.2_Draft.docx`, `DV2-PMP-001_Plano_Mestre_do_Projeto_v0.1_Draft.docx` e `DV2-URS-001_Especificacao_de_Requisitos_do_Usuario_v0.3_Draft.docx` não são arquivos ZIP/OOXML válidos (`BadZipFile: File is not a zip file`) — não abrem no Word nem em ferramentas de extração. Apenas `DV2-BRN-001...docx` (regras de negócio) e `DV2-TRM-001...xlsx` (matriz URS × BRN, com o texto integral dos 232 requisitos URS e das 92 regras BRN) são arquivos válidos e íntegros.
**Mitigação aplicada nesta tarefa:** A revisão documental da DV2-DEV-004 (Etapa 2 da DV2-SPRINT-002) foi realizada com base no conteúdo íntegro de `DV2-BRN-001` e `DV2-TRM-001`, que juntos cobrem o texto completo dos requisitos URS (via a aba URS-BRN de `DV2-TRM-001`) e das regras de negócio — substituindo, na prática, o `DV2-URS-001.docx` corrompido. `DV2-000`, `DV2-001` e `DV2-PMP-001` permanecem sem substituto disponível nesta tarefa.
**Status:** Resolvida quanto à presença dos documentos no repositório (encerrando a pendência original da Q-004/Q-007 — os arquivos existem em `main` desde a DV2-DOC-002). **Permanece pendência distinta e não encerrada:** os 4 arquivos `.docx` citados acima precisam ser re-upload/re-gerados como OOXML válido antes de servirem como fonte de verdade documental; até lá, qualquer tarefa que precise de `DV2-000`, `DV2-001`, `DV2-PMP-001` ou do texto integral formatado de `DV2-URS-001` deve tratar esse conteúdo como indisponível e não deve presumir ou reconstruir seu conteúdo. Recomenda-se abrir uma tarefa documental dedicada (ex.: `DV2-DOC-003`) para corrigir os arquivos corrompidos.

**Atualização (2026-08-15 — DV2-DOC-003):** os 4 arquivos foram substituídos manualmente por versões íntegras em `main` (commit `507174a`). Reverificação técnica nesta tarefa (mesmos métodos: `file`, `python zipfile.ZipFile`, abertura via `python-docx`/`openpyxl`, confirmação de `word/document.xml`/`xl/workbook.xml` presentes) confirmou que **os 6 documentos oficiais são ZIP/OOXML válidos, íntegros e legíveis**: `DV2-000` (94 parágrafos, 3 tabelas), `DV2-001` (239 parágrafos, 3 tabelas), `DV2-PMP-001` (247 parágrafos, 5 tabelas), `DV2-URS-001 v0.3` (107 parágrafos, 31 tabelas — texto extraído é idêntico ao já usado via `DV2-TRM-001` na DV2-DEV-004, confirmando que a mitigação anterior não introduziu divergência), `DV2-BRN-001` e `DV2-TRM-001` (já íntegros). A pendência de corrupção documental está **encerrada**.
**Status (atualizado):** Resolvida integralmente — presença E integridade confirmadas para os 6 documentos oficiais (2026-08-15 — DV2-DOC-003, validado na tarefa de fechamento).

---

### Q-008
**Data:** 2026-08-15
**Tarefa:** DV2-SPRINT-002
**Requisito:** Modelo de domínio documental (Document/DocumentRevision/OfficialFile)
**Pergunta:** O domínio documental da DV2-DEV-004 deveria introduzir `OrganizationId` (ou equivalente) para suportar múltiplas organizações/implantações?
**Impacto:** Afetaria o modelo de dados, as migrations e potencialmente toda a arquitetura de persistência.
**Opções identificadas (sem escolher):** introduzir `OrganizationId` nas entidades já nesta fase; adiar a decisão de multi-tenancy para uma tarefa/ADR específico.
**Decisão:** Não introduzir `OrganizationId` nesta fase. Configurabilidade por organização (CLAUDE.md — "Produto x implantação") não equivale a multi-tenancy; a arquitetura de tenancy será decidida posteriormente, em tarefa/ADR dedicado, quando houver necessidade concreta. Nenhuma migration relacionada a Organization foi criada na DV2-DEV-004.
**Status:** Resolvida (2026-08-15 — decisão humana, DV2-SPRINT-002)

---

### Q-009
**Data:** 2026-08-15
**Tarefa:** DV2-SPRINT-002
**Requisito:** Modelo de domínio documental — estados de disponibilização/vigência
**Pergunta:** A ausência de modelagem de estados de disponibilização (vigência/aprovação/obsolescência) na DV2-DEV-004 é uma lacuna a ser corrigida nesta tarefa, ou pode ser tratada em tarefa futura?
**Impacto:** Determina se `Document`/`DocumentRevision` precisam de um campo/estado adicional já na DV2-DEV-004, ou se o escopo atual (documento e revisão sem noção de vigência) permanece válido.
**Opções identificadas (sem escolher):** modelar estados de disponibilização já na DV2-DEV-004; criar um enum provisório apenas para não deixar a pergunta em aberto; transferir a modelagem funcional formalmente para uma tarefa futura dedicada.
**Decisão:** Estados de disponibilização não devem ser confundidos com vigência/aprovação/obsolescência do processo de controle documental da organização. A ausência de modelagem de disponibilização na DV2-DEV-004 é aceitável. Nenhum enum improvisado foi criado apenas para encerrar esta questão.
**Status:** Resolvida para o escopo da DEV-004; modelagem funcional transferida formalmente para DV2-DEV-005.
