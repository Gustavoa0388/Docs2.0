# 08-traceability — Rastreabilidade

Esta pasta concentra a matriz de rastreabilidade do DocsViewer Omni, ligando requisitos (URS), regras de negócio (BRN), decisões arquiteturais (ADRs), componentes de código e testes.

## Status

**Estrutura preparada, sem vínculos reais ainda.**

A rastreabilidade de requisito/regra de negócio depende de `DV2-URS-001 v0.2` e `DV2-BRN-001 v0.2 corrigido`, que ainda não estão neste repositório (ver `docs/handoff/OPEN_QUESTIONS.md` — Q-004). Por isso, `TRACEABILITY_MATRIX.md` nesta pasta contém apenas a estrutura de colunas, sem linhas de requisito/regra — nenhum ID de requisito ou regra foi inventado.

## O que já pode ser rastreado hoje

As três ADRs aprovadas (`docs/decisions/ADR-001`, `ADR-002`, `ADR-003`) já têm IDs reais e podem ser referenciadas por tarefas futuras. Tarefas de desenvolvimento (`DV2-DEV-XXX`) e suas alterações de arquivos também são registradas em `DEV_STATUS.md` a cada execução, mas isso ainda não constitui uma matriz de rastreabilidade formal ligando requisito → regra → código → teste.

## Quando a matriz poderá ser preenchida

Assim que `DV2-URS-001 v0.2` e `DV2-BRN-001 v0.2` forem incorporados ao repositório (pastas `03-requirements/` e `04-business-rules/`), uma tarefa formal deve popular `TRACEABILITY_MATRIX.md` com os vínculos reais, usando exclusivamente os IDs definidos nesses documentos.
