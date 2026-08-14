# DEV_STATUS.md

## Estado geral
DV2-SPRINT-001 concluído (Etapas 1 a 7). Ver relatório final da tarefa
para o resumo consolidado de todo o sprint.

## Branch atual
feature/DV2-SPRINT-001-etapa7-dev004-proposal (a partir de `main`, já
com DV2-DEV-002, ADR-003/.NET 10, estrutura documental/rastreabilidade e
DV2-DEV-003 integradas).

## Tarefa atual
DV2-SPRINT-001 — Etapa 7: preparar proposta da DV2-DEV-004 (domínio
documental), **sem implementar**.

## Requisitos/decisões relacionados
- CLAUDE.md — Documentos, Armazenamento, Integridade
- docs/PERMISSION_MODEL.md — catálogo `DOCUMENT_*`
- docs/decisions/ADR-002-web-core-com-clientes-shell-opcionais.md
- docs/handoff/OPEN_QUESTIONS.md — Q-004 (bloqueia modelagem real)

## O que foi feito
Criado `docs/handoff/DV2-DEV-004-PROPOSAL.md`, analisando (sem
implementar) o domínio documental a partir apenas de documentação já
presente no repositório (`CLAUDE.md`, `PERMISSION_MODEL.md`, ADR-002,
`PROJECT_CONTEXT.md`, `ROADMAP.md`):
- Documento (Documento Lógico) e Revisão Documental como entidades
  distintas (já estabelecido em `CLAUDE.md`).
- Representação/Arquivo Oficial (hash SHA-256, separação banco/repositório).
- Relação Documento ↔ Revisões (proposta conceitual 1:N, não decidida).
- Estados internos de disponibilização — **explicitamente distintos**
  do ciclo `Rascunho → Vigente → Obsoleto` do processo de controle
  documental da organização, conforme alertado pela própria tarefa.
  Candidatos inferidos do catálogo real de permissões (`DOCUMENT_PUBLISH`,
  `DOCUMENT_OBSOLETE`, `DOCUMENT_VIEW_OBSOLETE`).
- Histórico, invariantes candidatas (não aprovadas), riscos conhecidos.
- Requisitos URS e regras BRN relacionados: **bloqueados** — `DV2-URS-001
  v0.2` e `DV2-BRN-001 v0.2` não estão no repositório (Q-004); nenhum ID
  foi inventado ou herdado da URS v0.1 superada.
- 5 questões que precisam de decisão humana, listadas na própria
  proposta (candidatas a virar entradas de `OPEN_QUESTIONS.md` quando a
  tarefa DV2-DEV-004 for formalmente aberta).

Nenhuma entidade, `DbSet`, migration, endpoint, página ou regra de
autorização foi criada. `DocsViewerDbContext` permanece sem entidades.

## Arquivos criados
- docs/handoff/DV2-DEV-004-PROPOSAL.md (novo)
- DEV_STATUS.md (este arquivo)

## Migrations
Nenhuma.

## Banco
Não implementado.

## Testes
Nenhum (tarefa puramente de análise/documentação; nenhum código alterado).

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0
Error(s) (inalterado, nenhum código tocado nesta etapa).

## Decisões tomadas
- Nenhuma questão nova adicionada a `docs/handoff/OPEN_QUESTIONS.md`
  nesta etapa — as questões pendentes identificadas ficam registradas na
  seção 11 da própria proposta (`DV2-DEV-004-PROPOSAL.md`), para não
  tratar `OPEN_QUESTIONS.md` como diário de planejamento especulativo,
  conforme instruído.
- Nenhum ciclo `Rascunho → Vigente → Obsoleto` foi assumido como sendo
  do DocsViewer — tratado explicitamente como responsabilidade do
  processo documental da organização.

## Assumptions
Nenhuma.

## Riscos
- Modelagem de Documento/Revisão antes da chegada de URS/BRN v0.2 tem
  risco real de precisar ser refeita — registrado na proposta como
  recomendação de não iniciar DV2-DEV-004 antes de Q-004 ser resolvida,
  ou de limitar o escopo inicial ao que já é inequívoco em `CLAUDE.md`.

## Pendências
- Q-004: aguardando documentos oficiais v0.2.
- Q-006: aguardando confirmação/formalização da referência
  Web -> Infrastructure em docs/ARCHITECTURE.md.
- As 5 questões da seção 11 de `DV2-DEV-004-PROPOSAL.md` precisam de
  decisão antes (ou no início) da tarefa DV2-DEV-004.

## Próximo passo sugerido
Ver seção "Próximo passo" do relatório final do DV2-SPRINT-001.
