# DEV_STATUS.md

## Estado geral
DV2-REPO-001 concluída — linha principal consolidada. DV2-DEV-002 segue
concluída, agora com base efetiva em `main`.

## Branch atual
feature/DV2-DEV-002-blazor-foundation — rebaseada sobre `main` (após a
consolidação DV2-REPO-001). PR #4 aberto contra `main`.

## Tarefa atual
DV2-REPO-001 — Consolidação da linha principal do repositório.

## Requisitos/decisões relacionados
- CLAUDE.md — Git ("nunca trabalhar diretamente em main"), Documentação
- docs/decisions/ADR-001-blazor-web-app-interactive-server.md
- docs/decisions/ADR-002-web-core-com-clientes-shell-opcionais.md

## O que foi feito (DV2-REPO-001)
1. Inspecionados os PRs #1, #2, #3 e #4 e seus commits (`mergeable_state`
   de #1 e #3 confirmado como `clean` contra `main` antes de qualquer
   ação).
2. Confirmado que o PR #1 contém a fundação técnica esperada da
   DV2-DEV-001 (DocsViewer.sln + 6 projetos + ADR-001 + fechamento
   formal).
3. **PR #1 mergeado em `main`** via merge commit (`1570610`), preservando
   os commits originais `7cf0f2d` e `5c69b93` sem reescrevê-los.
4. **PR #3 (ADR-002) mergeado em `main`** via merge commit (`263d431`),
   preservando o commit original `0a07d12`. ADR-001 preservado (arquivos
   distintos, sem conflito).
5. **PR #2 (URS v0.1) não foi mergeado.** Comentário registrado no PR
   (https://github.com/Gustavoa0388/Docs2.0/pull/2) explicando que está
   superado pelas versões vigentes mais recentes (DV2-URS-001 v0.2 etc.,
   ainda fora do repositório) e deve ser substituído por atualização
   documental posterior. A branch `docs/DV2-URS-001-v0.1` foi preservada
   intacta para referência histórica — nenhum commit foi perdido.
6. `feature/DV2-DEV-002-blazor-foundation` **rebaseada sobre a `main`**
   consolidada (`git rebase main`). Rebase limpo, sem conflitos: como os
   commits da DV2-DEV-001 foram preservados via merge commit (não
   reescritos), o merge-base entre a branch e `main` já era `5c69b93`, e
   apenas o commit da DEV-002 (`4fb95cc` → `7eb1485` após rebase) precisou
   ser reaplicado.
7. Confirmado que o diff `main...feature/DV2-DEV-002-blazor-foundation`
   contém somente arquivos da DEV-002 (`DocsViewer.Web/Components/**`,
   `Program.cs`, `wwwroot/**`, além das atualizações de `DEV_STATUS.md` e
   `docs/handoff/OPEN_QUESTIONS.md`).
8. `dotnet restore` + `dotnet build DocsViewer.sln` executados na branch
   rebaseada — sem erros.
9. Aplicação executada (`dotnet run --project DocsViewer.Web`) e
   validada via HTTP — funcionamento básico confirmado, sem exceções.

Nenhuma alteração funcional nova foi feita (sem banco, entidades,
autenticação ou qualquer nova funcionalidade) — esta tarefa foi
exclusivamente de organização de repositório/Git.

## Migrations
Nenhuma.

## Banco
Não implementado.

## Testes
Nenhum teste novo. Build da solution (incluindo DocsViewer.UnitTests e
DocsViewer.IntegrationTests) confirmado sem erros após o rebase.

## Resultado de restore
`dotnet restore DocsViewer.sln` — sem erros, todos os 6 projetos
restaurados.

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0 Error(s).

## Resultado da execução
`dotnet run --project DocsViewer.Web` (perfil http, ASPNETCORE_ENVIRONMENT=Development):
aplicação iniciou sem exceções; `GET /`, `/documentos`, `/favoritos`,
`/solicitacoes`, `/administracao`, `/app.css`, `/favicon.svg`,
`/_framework/blazor.web.js` → todos 200.

## Decisões tomadas
- Merge de PR #1 e PR #3 em `main` usando método **merge commit** (não
  squash/rebase), para preservar os SHAs originais dos commits — em linha
  com "não perder commits existentes".
- PR #2 deliberadamente não mergeado; tratado como superado, registrado
  via comentário no próprio PR e em Q-004, sem fechar/excluir o PR ou a
  branch (decisão de fechar fica para o responsável do projeto).
- `feature/DV2-DEV-002-blazor-foundation` rebaseada (não recriada) sobre
  `main`, por ser uma operação seura neste caso (sem conflitos, histórico
  compartilhado preservado) e por deixar o PR #4 com diff limpo.
- Nenhum documento (URS v0.2, Product Vision v0.2, Documento de Fundação
  v0.4.2, PMP-001, BRN-001 v0.2) foi recriado de memória — permanecem
  fora do repositório, a serem adicionados em tarefa documental separada.

## Assumptions
Nenhuma nova assumption técnica.

## Riscos
- PR #2 permanece aberto e desatualizado (URS v0.1) — risco de alguém
  reabrir/mergear por engano no futuro sem checar o comentário de
  "superado". Mitigado por comentário explícito no PR e por Q-004.
- Documentos oficiais v0.2 (Product Vision, Documento de Fundação, PMP,
  URS, BRN) ainda não estão no repositório — bloqueia tarefas futuras de
  domínio/negócio (Q-004, ainda aberta).
- Risco já conhecido: .NET 8 LTS encerra suporte em 10/11/2026 — Q-003
  ainda aberta, sem decisão.

## Pendências
- Q-003: decisão humana sobre migrar (ou não) para .NET 10 LTS.
- Q-004: aguardando os documentos oficiais v0.2 serem adicionados ao
  repositório (tarefa documental separada).
- Q-005: **resolvida** nesta tarefa (main consolidada).
- Implementação real dos clientes-shell Windows/Android (ADR-002) — fora
  de escopo até tarefa formal.

## Próximo passo sugerido
Revisar o PR #4 (agora com base efetiva em `main` e diff restrito à
DEV-002) e decidir sobre o merge. Em paralelo, priorizar a tarefa
documental que trará DV2-000 v0.2, DV2-001 v0.4.2, DV2-PMP-001,
DV2-URS-001 v0.2 e DV2-BRN-001 v0.2 ao repositório (resolve Q-004), e a
decisão humana sobre Q-003 (.NET 8 vs .NET 10). Só então iniciar a
próxima tarefa de produto (ex.: banco/EF Core ou primeiras entidades de
Domain, conforme ROADMAP.md).
