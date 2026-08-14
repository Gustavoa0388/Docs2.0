# docs/ — Índice da documentação do DocsViewer Omni

Este README organiza a documentação do repositório. Ele **não substitui** nenhum documento formal controlado (DOCX/PDF); é apenas um índice técnico para navegação.

## Estrutura

```
docs/
├── 00-product/          — visão de produto (Product Vision)
├── 01-project/           — documentos de projeto (Documento de Fundação, Plano Mestre)
├── 02-validation/        — estratégia e evidências de validação
├── 03-requirements/      — especificação de requisitos do usuário (URS)
├── 04-business-rules/    — regras de negócio (BRN)
├── 05-risk/              — análise de riscos
├── 06-design/            — arquitetura e design técnico (SDS)
├── 07-tests/             — planos e protocolos de teste formais
├── 08-traceability/       — matriz de rastreabilidade
├── decisions/            — Architecture Decision Records (ADRs)
└── handoff/              — perguntas abertas, templates de tarefa, protocolo de desenvolvimento paralelo
```

Cada pasta `NN-*` contém um `README.md` próprio explicando o que é esperado ali e o que ainda está pendente.

## Documentos de metodologia existentes (não movidos)

Os arquivos abaixo já existiam na raiz de `docs/` antes desta reorganização e **permanecem onde estão**, porque `CLAUDE.md` e `CLAUDE_MASTER_PROMPT.md` referenciam seus caminhos diretamente (`docs/PROJECT_CONTEXT.md`, `docs/ARCHITECTURE.md`, etc.). Movê-los quebraria essas referências, e `CLAUDE.md` não está na lista de arquivos que esta tarefa está autorizada a alterar.

- `docs/PROJECT_CONTEXT.md`
- `docs/ARCHITECTURE.md`
- `docs/DEVELOPMENT_RULES.md`
- `docs/VALIDATION_AWARE_DEVELOPMENT.md`
- `docs/PERMISSION_MODEL.md`
- `docs/ROADMAP.md`

Se, no futuro, uma tarefa formal decidir mover algum desses arquivos para dentro da estrutura numerada (por exemplo, `PROJECT_CONTEXT.md` para `00-product/`), essa tarefa deve também atualizar `CLAUDE.md` e `CLAUDE_MASTER_PROMPT.md` na mesma mudança.

## Documentação oficial vigente ainda ausente do repositório

Os seguintes documentos oficiais (fora do GitHub, em versões mais recentes que qualquer conteúdo já visto neste repositório) ainda **não foram incorporados**:

| Documento | Versão vigente | Pasta prevista |
|---|---|---|
| DV2-000 — Product Vision | v0.2 | `00-product/` |
| DV2-001 — Documento de Fundação | v0.4.2 | `01-project/` |
| DV2-PMP-001 — Plano Mestre do Projeto | vigente | `01-project/` |
| DV2-URS-001 — Especificação de Requisitos do Usuário | v0.2 | `03-requirements/` |
| DV2-BRN-001 — Regras de Negócio | v0.2 corrigido | `04-business-rules/` |

Nenhum desses documentos foi recriado a partir de memória, conversas ou versões antigas. Este repositório já teve uma versão anterior da URS (v0.1 Draft) registrada em PR (`#2`, branch `docs/DV2-URS-001-v0.1`), mas ela foi marcada como **superada** e não foi mergeada — ver `docs/handoff/OPEN_QUESTIONS.md` (Q-004).

**Q-004 permanece aberta até que os arquivos reais das versões acima sejam efetivamente adicionados ao repositório**, em tarefa documental dedicada.

## decisions/ e handoff/

Essas duas pastas já existiam com a estrutura correta antes desta tarefa e não foram alteradas em sua organização:
- `decisions/` — ADRs (`ADR_TEMPLATE.md`, `ADR-001`, `ADR-002`, `ADR-003`).
- `handoff/` — `OPEN_QUESTIONS.md`, `TASK_TEMPLATE.md`, `PARALLEL_DEVELOPMENT_PROTOCOL.md`.
