# DV2-DSP-001-PROPOSAL — Estados internos de Disponibilização (proposta, não implementada)

## Status
**Proposta de análise, registrada na DV2-DEV-005 (Etapa 18 da DV2-SPRINT-002). Nenhum enum, coluna ou migration foi criado a partir deste documento.** A DV2-DEV-005 implementa apenas cadastro de Category, DocumentType, Document e DocumentRevision — nenhum estado de disponibilização é persistido nesta tarefa (decisão explícita da tarefa, reiterando Q-009).

## Por que esta proposta existe
A Q-009 (`docs/handoff/OPEN_QUESTIONS.md`) encerrou a DV2-DEV-004 sem modelar disponibilização, transferindo a modelagem funcional formalmente para a DV2-DEV-005. A própria DV2-SPRINT-002 instruiu, na Etapa 18, a **não implementar** vigência documental nesta tarefa, e a só iniciar modelagem conceitual **se os documentos oficiais permitirem sem ambiguidade** — caso contrário, registrar esta proposta. Após revisar `DV2-BRN-001 v0.2` (seções 3 e 6, únicas fontes íntegras disponíveis — ver Q-007), a conclusão é que **ainda falta especificação suficiente** para um enum de produção: o BRN define o comportamento esperado (regras BR-DSP-001 a BR-DSP-009) mas não fixa nomes de estados, transições permitidas nem cardinalidade (ex.: pode haver mais de uma revisão "em conferência" simultaneamente? uma retirada é reversível?). Por isso, este documento propõe terminologia derivada da fonte real, sem decidir a modelagem final.

## Fontes utilizadas (documentos íntegros e reais)
- `DV2-BRN-001 v0.2`, seção 3 "Terminologia operacional" — define oficialmente os termos **Disponibilizado para Uso Corrente** e **Histórico**.
- `DV2-BRN-001 v0.2`, seção 6 "Incorporação, Conferência e Disponibilização" — regras BR-DSP-001 a BR-DSP-009.
- `DV2-TRM-001 v0.1` — requisitos URS-REV-004, URS-REV-008, URS-REV-009, URS-DOC-014 (ver `docs/08-traceability/TRACEABILITY_MATRIX.md`).

Nenhum termo foi inventado; todos vêm diretamente do BRN real (não do BRN corrompido/ausente — ver ressalva de integridade em Q-007).

## Comportamento já estabelecido pelo BRN (não é decisão desta proposta, é regra aprovada)
- BR-DSP-001: upload/digitalização não disponibiliza automaticamente.
- BR-DSP-002: deve haver conferência do arquivo/metadados antes da disponibilização.
- BR-DSP-003: disponibilização para uso corrente é ação controlada, executada por usuário autorizado.
- BR-DSP-004: o sistema deve identificar inequivocamente qual representação está disponibilizada para uso corrente por Documento.
- BR-DSP-005: ao disponibilizar uma nova revisão, a anterior deixa de aparecer nos fluxos operacionais normais (preservando histórico).
- BR-DSP-006: retirada do uso corrente é controlada por permissão e gera Audit Trail.
- BR-DSP-007: o sistema **não infere vigência pela ordem numérica/textual das revisões** — reforça por que `DocumentRevision.RevisionIdentifier` permanece texto livre e por que a DV2-DEV-004/005 não tentam determinar "a revisão atual" automaticamente.
- BR-DSP-008/009: aprovação em duas etapas é **opcional**, configurável por organização — não deve ser assumida como universal.

## Estados candidatos (proposta, terminologia derivada do BRN — não decidida)
Sugestão de vocabulário para uma futura tarefa de modelagem, alinhado aos termos já usados pelo BRN e ao exemplo citado na tarefa:

1. **Incorporado** — arquivo recebido pelo DocsViewer (upload/digitalização), ainda não conferido nem disponível a usuários operacionais (BR-DSP-001).
2. **Em conferência** — arquivo/metadados em validação antes de decidir disponibilizar (BR-DSP-002).
3. **Disponibilizado para uso corrente** — termo oficial do BRN (seção 3); representação identificada inequivocamente como a aplicável nos fluxos operacionais normais (BR-DSP-003/004).
4. **Retirado do uso operacional** — representação removida do fluxo operacional normal por ação controlada (BR-DSP-005/006), sem necessariamente ser destruída.
5. **Histórico** — termo oficial do BRN (seção 3); representação preservada, sujeita a autorização específica para consulta (BR-REV-004 a BR-REV-013).

## Pontos que permanecem em aberto (motivo de não implementar ainda)
- O BRN não define se as transições entre estados são lineares (1→2→3→4→5) ou se alguns estados podem ser pulados (ex.: uma revisão incorporada já conferida previamente por outro processo).
- Não há definição de cardinalidade: pode um Documento ter mais de uma revisão simultaneamente "em conferência"? A leitura mais conservadora do BRN sugere que não, mas isso não está explícito.
- BR-DSP-008/009 tornam a aprovação em duas etapas configurável por organização — um enum de estados fixo no core poderia não comportar essa configurabilidade sem uma modelagem adicional (ex.: estados x etapas de aprovação como conceitos separados).
- Não há indicação de reversibilidade de "Retirado do uso operacional" (pode retornar a "Disponibilizado"?).

## Recomendação
Tratar a modelagem definitiva de disponibilização como tarefa própria (ex.: `DV2-DEV-006` ou dedicada), com os pontos em aberto acima resolvidos por decisão humana antes de qualquer enum/migration de produção. Até lá, `Document`/`DocumentRevision` permanecem sem qualquer campo de estado, e a apresentação de revisões nas telas da DV2-DEV-005 lista todas as revisões existentes sem marcar nenhuma como "vigente" ou "atual" (consistente com BR-DSP-007).
