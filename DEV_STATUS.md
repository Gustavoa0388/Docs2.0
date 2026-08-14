# DEV_STATUS.md

## Estado geral
DV2-SPRINT-001 em andamento. Etapa 1 (merge da DV2-DEV-002) e Etapa 2
(ADR-003 — migração para .NET 10 LTS) concluídas.

## Branch atual
feature/DV2-ADR-003-dotnet10-lts (a partir de `main`, já com DV2-DEV-002
integrada).

## Tarefa atual
DV2-SPRINT-001 — Etapa 2: DV2-ADR-003 — adoção do .NET 10 LTS.

## Requisitos/decisões relacionados
- CLAUDE.md — Tecnologias provisoriamente aprovadas ("não trocar
  tecnologia sem ADR aprovado")
- docs/decisions/ADR-003-dotnet-10-lts.md (novo)
- docs/handoff/OPEN_QUESTIONS.md — Q-003 (encerrada por esta etapa)

## O que foi feito

### Etapa 1 — Merge da DV2-DEV-002
PR #4 revisado (diff restrito à DEV-002, build 0 erros/0 warnings,
execução validada via HTTP) e mergeado em `main` (merge commit
`ce68914`). `main` atualizada localmente; `dotnet restore` + `dotnet
build DocsViewer.sln` + execução revalidados diretamente em `main`, sem
erros.

### Etapa 2 — ADR-003 e migração para .NET 10 LTS
1. Instalado .NET SDK 10.0.110 no ambiente (adicional ao 8.0.129 já
   existente, sem removê-lo).
2. Criado `docs/decisions/ADR-003-dotnet-10-lts.md`, registrando a
   decisão do responsável do projeto: adotar .NET 10 LTS como
   framework-base, com contexto, justificativa, alternativas
   consideradas, consequências, riscos, impacto sobre desenvolvimento e
   validação. Status: Aprovado (Accepted).
3. `TargetFramework` alterado de `net8.0` para `net10.0` nos 6 projetos
   da solution (`DocsViewer.Domain`, `Application`, `Infrastructure`,
   `Web`, `UnitTests`, `IntegrationTests`). Nenhuma outra alteração de
   dependências/bibliotecas.
4. `dotnet restore` e `dotnet build DocsViewer.sln` — sem erros e **sem
   warnings** decorrentes da migração (nada a corrigir).
5. Aplicação executada em .NET 10 (`dotnet run --project DocsViewer.Web`)
   — todas as rotas retornaram 200, sem exceções no log.
6. Interactive Server revalidado via navegador headless (Chromium/
   Playwright): navegação client-side funcional, sem erros de console.
7. Q-003 encerrada em `docs/handoff/OPEN_QUESTIONS.md`, referenciando
   ADR-003.

## Arquivos alterados/criados nesta etapa
- docs/decisions/ADR-003-dotnet-10-lts.md (novo)
- DocsViewer.Domain/DocsViewer.Domain.csproj (TargetFramework net10.0)
- DocsViewer.Application/DocsViewer.Application.csproj (TargetFramework net10.0)
- DocsViewer.Infrastructure/DocsViewer.Infrastructure.csproj (TargetFramework net10.0)
- DocsViewer.Web/DocsViewer.Web.csproj (TargetFramework net10.0)
- DocsViewer.UnitTests/DocsViewer.UnitTests.csproj (TargetFramework net10.0)
- DocsViewer.IntegrationTests/DocsViewer.IntegrationTests.csproj (TargetFramework net10.0)
- docs/handoff/OPEN_QUESTIONS.md (Q-003 resolvida)
- DEV_STATUS.md (este arquivo)

## Migrations
Nenhuma.

## Banco
Não implementado.

## Testes
Nenhum teste novo. Build da solution completo (incluindo
DocsViewer.UnitTests e DocsViewer.IntegrationTests) sem erros após a
migração.

## Resultado de restore
`dotnet restore DocsViewer.sln` — sem erros (net10.0).

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, **0 Warning(s), 0
Error(s)** (net10.0).

## Resultado da execução
`dotnet run --project DocsViewer.Web` (net10.0) — aplicação iniciou sem
exceções; todas as rotas (`/`, `/documentos`, `/favoritos`,
`/solicitacoes`, `/administracao`, `/app.css`, `/favicon.svg`,
`/_framework/blazor.web.js`) retornaram 200; Interactive Server
confirmado funcional via navegador headless.

## Decisões tomadas
- Migração para .NET 10 LTS decidida pelo responsável do projeto (item 2
  da tarefa DV2-SPRINT-001), registrada em ADR-003 e aplicada
  integralmente — nenhuma decisão adicional foi inventada.
- SDK .NET 10 instalado ao lado do .NET 8 no ambiente de
  desenvolvimento, sem remover o 8.0 (mantém compatibilidade com
  ferramentas que ainda o exijam).

## Assumptions
Nenhuma nova assumption técnica além da decisão explícita fornecida.

## Riscos
- .NET 10 é uma versão mais nova; pacotes de terceiros específicos podem
  ter cobertura/maturidade menor do que para .NET 8 — nenhuma dependência
  crítica de terceiros identificada até o momento na solution.
- Ambientes de CI/produção precisarão ter o SDK/runtime .NET 10
  disponível quando essas tarefas forem abertas — não avaliado nesta
  etapa.
- Riscos herdados (Q-004: documentos oficiais v0.2 ainda ausentes do
  repositório) seguem válidos.

## Pendências
- Q-003: **resolvida** nesta etapa (ver ADR-003).
- Q-004: aguardando documentos oficiais v0.2 no repositório.
- Próximas etapas do DV2-SPRINT-001: consolidação documental,
  rastreabilidade, fundação de persistência (DV2-DEV-003), testes e
  proposta DV2-DEV-004.

## Próximo passo sugerido
Prosseguir com a Etapa 3 do DV2-SPRINT-001 (estrutura documental) e
demais etapas planejadas, conforme instrução do sprint.
