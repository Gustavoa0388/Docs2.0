# docs/ — Índice da documentação do DocsViewer Omni

Este README organiza a documentação do repositório. Ele **não substitui** nenhum documento formal controlado; é apenas um índice técnico para navegação.

## Estrutura

```text
docs/
├── 00-product/          — visão de produto
├── 01-project/          — fundação e planejamento
├── 02-validation/       — estratégia e evidências de validação
├── 03-requirements/     — especificação de requisitos do usuário (URS)
├── 04-business-rules/   — regras de negócio (BRN)
├── 05-risk/             — análise de riscos
├── 06-design/           — arquitetura e design técnico
├── 07-tests/            — planos e protocolos de teste
├── 08-traceability/     — matriz de rastreabilidade
├── decisions/           — Architecture Decision Records (ADRs)
└── handoff/             — perguntas abertas e protocolo de desenvolvimento
```

## Documentos oficiais vigentes em Draft

| Documento | Versão | Arquivo |
|---|---|---|
| DV2-000 — Product Vision | v0.2 Draft | `docs/00-product/DV2-000_Product_Vision_v0.2_Draft.docx` |
| DV2-001 — Documento de Fundação | v0.4.2 Draft | `docs/01-project/DV2-001_Documento_de_Fundacao_v0.4.2_Draft.docx` |
| DV2-PMP-001 — Plano Mestre do Projeto | v0.1 Draft | `docs/01-project/DV2-PMP-001_Plano_Mestre_do_Projeto_v0.1_Draft.docx` |
| DV2-URS-001 — Especificação de Requisitos do Usuário | v0.3 Draft | `docs/03-requirements/DV2-URS-001_Especificacao_de_Requisitos_do_Usuario_v0.3_Draft.docx` |
| DV2-BRN-001 — Especificação de Regras de Negócio | v0.2 Draft corrigido | `docs/04-business-rules/DV2-BRN-001_Especificacao_de_Regras_de_Negocio_v0.2_Draft_CORRIGIDO.docx` |
| DV2-TRM-001 — Matriz preliminar URS ↔ BRN | v0.1 | `docs/08-traceability/DV2-TRM-001_Matriz_Preliminar_URS-BRN_v0.1.xlsx` |

Os seis artefatos acima constituem as referências documentais ativas do projeto enquanto não houver baseline ou revisão formal posterior. Versões Draft anteriores permanecem apenas como histórico e não devem orientar novas implementações quando houver conflito com a versão ativa.

## Documentos de metodologia existentes

Permanecem nos caminhos atuais para não quebrar referências já utilizadas pelo desenvolvimento:

- `docs/PROJECT_CONTEXT.md`
- `docs/ARCHITECTURE.md`
- `docs/DEVELOPMENT_RULES.md`
- `docs/VALIDATION_AWARE_DEVELOPMENT.md`
- `docs/PERMISSION_MODEL.md`
- `docs/ROADMAP.md`

## ADRs

A pasta `decisions/` contém as decisões arquiteturais formais vigentes, incluindo ADR-001, ADR-002 e ADR-003.
