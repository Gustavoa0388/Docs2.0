# DEV_STATUS.md

## Estado geral
DV2-SPRINT-001 em andamento. Etapas 1 (merge DV2-DEV-002), 2 (ADR-003 —
.NET 10 LTS), 3 (estrutura documental) e 4 (rastreabilidade) concluídas.

## Branch atual
feature/DV2-SPRINT-001-etapa3-estrutura-documental (a partir de `main`,
já com DV2-DEV-002 e ADR-003/.NET 10 integradas).

## Tarefa atual
DV2-SPRINT-001 — Etapas 3 e 4: consolidação da estrutura documental e
preparação da rastreabilidade.

## Requisitos/decisões relacionados
- docs/handoff/OPEN_QUESTIONS.md — Q-004 (documentos oficiais v0.2 ainda
  ausentes; permanece aberta)
- docs/decisions/ADR-002-web-core-com-clientes-shell-opcionais.md —
  cita DV2-SDS-001, DV2-000, DV2-001, DV2-URS-001 como impacto documental

## O que foi feito

### Etapa 3 — Estrutura documental
1. Criadas as pastas `docs/00-product`, `01-project`, `02-validation`,
   `03-requirements`, `04-business-rules`, `05-risk`, `06-design`,
   `07-tests`, `08-traceability`, cada uma com `README.md` explicando o
   documento oficial esperado, sua versão vigente e status (todas
   ausentes nesta data).
2. Criado `docs/README.md` como índice geral da documentação, mapeando a
   estrutura completa e listando explicitamente os 5 documentos oficiais
   ainda pendentes (DV2-000 v0.2, DV2-001 v0.4.2, DV2-PMP-001 vigente,
   DV2-URS-001 v0.2, DV2-BRN-001 v0.2 corrigido).
3. **Nenhum arquivo existente foi movido.** `docs/PROJECT_CONTEXT.md`,
   `ARCHITECTURE.md`, `DEVELOPMENT_RULES.md`,
   `VALIDATION_AWARE_DEVELOPMENT.md`, `PERMISSION_MODEL.md` e
   `ROADMAP.md` permanecem na raiz de `docs/`, pois `CLAUDE.md` e
   `CLAUDE_MASTER_PROMPT.md` referenciam esses caminhos diretamente e
   `CLAUDE.md` não está entre os arquivos que esta tarefa pode alterar.
   `docs/decisions/` e `docs/handoff/` já tinham a organização correta e
   não foram tocadas.
4. Nenhum DOCX foi recriado de memória; nenhum documento oficial foi
   inventado.

### Etapa 4 — Rastreabilidade
1. Criado `docs/08-traceability/README.md`, explicando o propósito da
   matriz e o que já pode/não pode ser rastreado hoje.
2. Criado `docs/08-traceability/TRACEABILITY_MATRIX.md`, com a estrutura
   de colunas (Requisito URS / Regra BRN / ADR / Componente / Arquivos /
   Teste / Status) sem nenhuma linha de requisito ou regra — porque URS
   v0.2 e BRN v0.2 ainda não estão no repositório. A única seção
   preenchida lista as 3 ADRs já aprovadas (ADR-001, ADR-002, ADR-003),
   que têm IDs reais.
3. Nenhum ID de requisito/regra foi inventado ou herdado da URS v0.1
   superada.

## Arquivos criados nesta etapa
- docs/README.md
- docs/00-product/README.md
- docs/01-project/README.md
- docs/02-validation/README.md
- docs/03-requirements/README.md
- docs/04-business-rules/README.md
- docs/05-risk/README.md
- docs/06-design/README.md
- docs/07-tests/README.md
- docs/08-traceability/README.md
- docs/08-traceability/TRACEABILITY_MATRIX.md
- DEV_STATUS.md (este arquivo)

## Migrations
Nenhuma.

## Banco
Não implementado.

## Testes
Nenhum (tarefa puramente documental).

## Resultado de restore / build / execução
Não aplicável a esta etapa (nenhuma alteração de código). O último
resultado válido de build/execução (Etapa 2, .NET 10) segue vigente:
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0
Error(s).

## Decisões tomadas
- Não mover documentos de metodologia existentes (`PROJECT_CONTEXT.md`
  etc.) para dentro da nova estrutura numerada, para não quebrar
  referências de `CLAUDE.md`/`CLAUDE_MASTER_PROMPT.md` — registrado
  explicitamente em `docs/README.md`.
- Matriz de rastreabilidade criada apenas com estrutura de colunas,
  vazia de requisitos/regras, até que URS v0.2 e BRN v0.2 cheguem ao
  repositório.

## Assumptions
Nenhuma nova assumption técnica.

## Riscos
- Nenhum risco técnico novo. Risco documental já conhecido (Q-004)
  segue registrado e sem mudança de status.

## Pendências
- Q-004: aguardando os documentos oficiais v0.2 (Product Vision,
  Documento de Fundação, PMP-001, URS, BRN) serem adicionados ao
  repositório, em tarefa documental dedicada — isso desbloqueará o
  preenchimento real de `docs/08-traceability/TRACEABILITY_MATRIX.md`.

## Próximo passo sugerido
Prosseguir com a Etapa 5 do DV2-SPRINT-001 (DV2-DEV-003 — fundação de
persistência com EF Core), já que suas pré-condições (DV2-DEV-002
integrada, migração para .NET 10 concluída, build limpo) estão
satisfeitas. Q-004 segue como pendência paralela, a ser resolvida por
tarefa documental separada quando os arquivos oficiais estiverem
disponíveis.
