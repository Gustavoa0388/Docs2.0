# TRACEABILITY_MATRIX.md — DocsViewer Omni

## Status
**Ainda aguardando incorporação real de `DV2-URS-001 v0.3` e `DV2-BRN-001 v0.2 corrigido` ao repositório.**

A tarefa DV2-DEV-004 previu que esses documentos estariam disponíveis nesta data, mas uma busca exaustiva (sistema de arquivos local e Google Drive) não os localizou — ver `docs/handoff/OPEN_QUESTIONS.md` (Q-004, Q-007). Por isso, **nenhum ID de requisito (URS-RU-XXX) ou regra de negócio (RN-XXX) real existe ainda** para vincular. Nenhum ID foi inventado, reconstruído de memória ou herdado de versões antigas (a v0.1 Draft da URS, PR #2, foi marcada como superada e seus IDs não foram trazidos para esta matriz).

O que a DV2-DEV-004 pôde vincular de forma real, nesta data, são as **decisões de domínio aprovadas diretamente na própria tarefa** (`DEC-DOM-001` a `DEC-DOM-004`, fornecidas no corpo da tarefa DV2-DEV-004) — que não substituem BRN formal, mas são a única fonte de regra disponível até a chegada dos documentos oficiais.

## Estrutura prevista (requisito/regra formais — ainda vazia)

| Requisito (URS) | Regra de Negócio (BRN) | ADR relacionado | Componente/Módulo | Arquivos de código | Teste | Status |
|---|---|---|---|---|---|---|
| _(aguardando URS v0.3)_ | _(aguardando BRN v0.2)_ | | | | | |

## Cobertura real da DV2-DEV-004 (decisões de domínio da própria tarefa, não URS/BRN formais)

| Decisão | Componente/Módulo | Arquivos de código | Teste | Status |
|---|---|---|---|---|
| DEC-DOM-001 — Documento pode não possuir Revisão | `Document`, `DocumentRevision` | `DocsViewer.Domain/Documents/Document.cs`, `DocumentRevision.cs` | `DocumentTests.Documento_Sem_Revisao_E_Valido`, `Documento_Pode_Possuir_Revisao` | Coberto |
| DEC-DOM-002 — Hash pertence ao Arquivo Oficial | `OfficialFile` | `DocsViewer.Domain/Documents/OfficialFile.cs` | `OfficialFileTests.OfficialFile_Requer_Hash_Nao_Vazio` | Coberto (estrutura de domínio apenas — sem serviço de arquivos/reverificação, fora de escopo) |
| DEC-DOM-003 — Categorias hierárquicas, pai opcional | `Category` | `DocsViewer.Domain/Categories/Category.cs` | `CategoryTests.*` | Coberto |
| DEC-DOM-004 — Tipo documental configurável, independente de Category | `DocumentType` | `DocsViewer.Domain/DocumentTypes/DocumentType.cs` | (sem teste dedicado — entidade simples, sem invariante além de nome obrigatório, já validada indiretamente pelo build/mapeamento EF) | Parcialmente coberto |
| Identificador de Revisão não numérico | `DocumentRevision` | `DocsViewer.Domain/Documents/DocumentRevision.cs` | `DocumentRevisionTests.Identificador_De_Revisao_Nao_Precisa_Ser_Numerico` | Coberto |
| OfficialFile ↔ mesmo Document da Revisão | `OfficialFile` | `DocsViewer.Domain/Documents/OfficialFile.cs` | `OfficialFileTests.OfficialFile_Com_Revisao_De_Outro_Document_E_Invalido` | Coberto |
| Mapeamento EF Core (chaves, FKs, delete restrict) | `DocsViewerDbContext` + `Configurations/*` | `DocsViewer.Infrastructure/Persistence/**` | `DocsViewerDbContextModelTests.*` (Infrastructure) | Coberto |
| Disponibilização (estados de disponibilização do DocsViewer) | — | — | — | **Ainda não implementado** — deliberadamente não modelado nesta tarefa (ver seção 10 da DV2-DEV-004: não inventar enum definitivo sem documentação fechando o assunto) |
| Organização/multi-tenancy (`OrganizationId`) | — | — | — | **Ainda não implementado** — ver Q-008 |

## Vínculos já existentes (decisões arquiteturais, sem requisito formal ainda)

| ADR | Título | Requisitos relacionados citados no próprio ADR |
|---|---|---|
| ADR-001 | Modelo de renderização do Blazor Web App: Interactive Server | CLAUDE.md — Tecnologias provisoriamente aprovadas; Segurança |
| ADR-002 | Núcleo Web com clientes-shell opcionais para Windows e Android | (nenhum requisito formal — impacto documental previsto sobre URS futura) |
| ADR-003 | Adoção do .NET 10 LTS como framework-base | CLAUDE.md — Tecnologias provisoriamente aprovadas; Q-003 |

## Regra de atualização

Esta matriz deve ser preenchida somente com IDs que existam de fato em documentos oficiais presentes no repositório, ou com decisões explicitamente aprovadas e registradas (como as `DEC-DOM-XXX` acima). Não preencher linhas por antecipação. Quando `DV2-URS-001 v0.3` e `DV2-BRN-001 v0.2` chegarem ao repositório, a seção "Cobertura real da DV2-DEV-004" acima deve ser revisada e, se os IDs formais correspondentes existirem, migrada para a tabela de "Estrutura prevista".
