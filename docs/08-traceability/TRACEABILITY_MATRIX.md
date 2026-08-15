# TRACEABILITY_MATRIX.md — DocsViewer Omni

## Status

Os documentos oficiais `DV2-URS-001 v0.3` (requisitos) e `DV2-BRN-001 v0.2 corrigido` (regras de negócio) foram incorporados ao repositório pela DV2-DOC-002 e usados nesta matriz. Ver `docs/handoff/OPEN_QUESTIONS.md` (Q-004, Q-007) para o histórico da pendência anterior.

**Ressalva de integridade documental (Q-007):** os arquivos `DV2-000_Product_Vision_v0.2_Draft.docx`, `DV2-001_Documento_de_Fundacao_v0.4.2_Draft.docx`, `DV2-PMP-001_Plano_Mestre_do_Projeto_v0.1_Draft.docx` e o `.docx` bruto de `DV2-URS-001` estão corrompidos no repositório (não são ZIP/OOXML válidos). Os IDs de requisito (`URS-*`) usados abaixo foram extraídos de `DV2-TRM-001.xlsx` (aba URS-BRN, íntegra, contendo o texto completo dos 232 requisitos e seu cross-reference com BRN), que atua como fonte substituta válida para `DV2-URS-001 v0.3`. Os IDs de regra (`BR-*`) vêm de `DV2-BRN-001.docx`, íntegro. Nenhum ID foi inventado.

Esta matriz cobre apenas os requisitos/regras genuinamente relacionados ao domínio implementado pela DV2-DEV-004 (`Document`, `DocumentRevision`, `OfficialFile`, `Category`, `DocumentType`) e ao cadastro/consulta funcional entregue pela DV2-DEV-005 (Application, persistência e UI Blazor para Category/DocumentType/Document/DocumentRevision). Requisitos de autenticação, permissões, visualizador, download, impressão, marca d'água, OCR/pesquisa, digitalização, auditoria e configuração organizacional pertencem a outras camadas/tarefas e não são listados individualmente aqui para não fingir cobertura — permanecem como pendência de tarefas futuras (DEV-006 em diante).

## Documento (Document) e Arquivo Oficial (OfficialFile)

| Requisito (URS) | Regra (BRN) | Componente | Arquivos de código | Teste | Status |
|---|---|---|---|---|---|
| URS-DOC-001 — Cadastrar novos documentos | — | `Document` (construtor), `DocumentService.CreateAsync`, página `Documentos/NovoDocumento.razor` | `Document.cs`, `DocumentService.cs`, `NovoDocumento.razor` | `DocumentTests.Documento_Sem_Revisao_E_Valido`, `DocumentServiceTests.CreateAsync_*` | Coberto — cadastro funcional entregue na DV2-DEV-005 |
| URS-DOC-002 — Identificação única no contexto da organização | BR-DOC-001 | `Document.Code` | `DocsViewer.Domain/Documents/Document.cs` | `DocumentTests.Document_Requer_Code_Nao_Vazio` | Parcialmente coberto — `Code` obrigatório e distinto de `Id`; unicidade não é imposta no banco (sem `OrganizationId`/escopo definido — ver Q-008), fica pendente para quando o escopo de unicidade for decidido |
| URS-DOC-003 — Padrões de codificação sem convenção imposta | — | `Document.Code` | `DocsViewer.Domain/Documents/Document.cs` | — | Coberto — `Code` é string livre, sem formato/tamanho imposto |
| URS-DOC-004 — Associar metadados de identificação/classificação | BR-DOC-006 | `Document` | `Document.cs` (Title, CategoryId, DocumentTypeId) | `DocumentTests.*` | Parcialmente coberto — metadados suportados atualmente: título, categoria, tipo; evolução de metadados (URS-DOC-005) não modelada |
| URS-DOC-006 — Classificar com categorias/subcategorias/tipos configurados | BR-CFG-001 | `Document`, `Category`, `DocumentType` | `Document.cs`, `Category.cs`, `DocumentType.cs` | `CategoryTests.*` | Coberto |
| URS-DOC-007 — Arquivo oficial vinculado a Documento e revisão | BR-DOC-003 | `OfficialFile` | `DocsViewer.Domain/Documents/OfficialFile.cs` | `OfficialFileTests.OfficialFile_Com_Revisao_De_Outro_Document_E_Invalido` | Coberto (estrutura de domínio) |
| URS-DOC-008 — PDF como formato principal | — | `OfficialFile.MimeType` | `OfficialFile.cs` | — | Não implementado — campo existe mas sem validação/restrição de formato; upload físico é fora de escopo da DV2-DEV-004/005 (ver CLAUDE.md — Futuro) |
| URS-DOC-011 — Detectar alteração indevida do arquivo oficial | BR-DOC-004 | `OfficialFile.HashValue/HashAlgorithm` | `OfficialFile.cs` | `OfficialFileTests.OfficialFile_Requer_Hash_Nao_Vazio` | Parcialmente coberto — campos de hash existem na estrutura; serviço de cálculo/reverificação de hash é fora de escopo (sem armazenamento físico de arquivo ainda) |
| URS-DOC-013 — Exclusão depende de autorização específica | BR-DOC-009, BR-DOC-010, BR-DOC-011, BR-DOC-012 | — | — | — | Não aplicável à DEV-004 — nenhum caso de uso de exclusão foi implementado; `DeleteBehavior.Restrict` apenas impede exclusão que deixaria FKs órfãs, não é controle de autorização |
| URS-DOC-014 — Retirada de uso não elimina histórico | BR-REV-004 | `DocumentRevision` (sem cascade) | `Configurations/*.cs` (`DeleteBehavior.Restrict`) | `DocsViewerDbContextModelTests.*` | Parcialmente coberto — modelo impede exclusão em cascata; funcionalidade de "retirada de uso" (disponibilização) não implementada (ver Q-009) |
| URS-DAT-001 — Preservar dados de identificação/controle/rastreabilidade | — | `DocsViewerDbContext` | `DocsViewer.Infrastructure/Persistence/**` | `DocsViewerDbContextModelTests.*` | Coberto (persistência estrutural) |
| URS-DAT-002 — Arquivo oficial inequivocamente associado a documento/revisão | BR-DOC-003, BR-DOC-002 | `OfficialFile` | `OfficialFile.cs` | `OfficialFileTests.*` | Coberto |
| URS-DAT-004 — Proteger registros contra estados incompatíveis com regras de negócio | BR-DOC-001, BR-DOC-002 | `Document`, `OfficialFile`, `Category` (construtores validantes) | `Document.cs`, `OfficialFile.cs`, `Category.cs` | `DocumentTests.*`, `OfficialFileTests.*`, `CategoryTests.*` | Coberto |
| URS-DAT-006 — Referência temporal consistente | — | `OfficialFile.IncorporatedAtUtc` | `OfficialFile.cs` | — | Parcialmente coberto — campo UTC existe; política de referência temporal da implantação não definida |
| URS-DAT-007 — Proteger contra sobrescrita concorrente não identificada | — | — | — | — | Não implementado — nenhum token de concorrência (`RowVersion`) na migration atual |
| URS-VWR-013 / URS-UX-002 — Código, título, revisão e condição documental identificáveis | — | `Document.Title`, catálogo `Documentos/Documentos.razor`, detalhe `Documentos/DocumentoDetalhe.razor` | `Document.cs`, `Documentos.razor`, `DocumentoDetalhe.razor` | `DocumentTests.Document_Requer_Title_Nao_Vazio` | Parcialmente coberto — código, título e revisões são exibidos no catálogo e no detalhe (DV2-DEV-005); "condição documental" (disponibilização) não é exibida, pois não existe ainda (ver Q-009, `docs/handoff/DV2-DSP-001-PROPOSAL.md`) |

## Revisão Documental (DocumentRevision)

| Requisito (URS) | Regra (BRN) | Componente | Arquivos de código | Teste | Status |
|---|---|---|---|---|---|
| URS-REV-001 — Documento e revisão como conceitos distintos, com vínculo | BR-DOC-001, BR-REV-001 | `Document`, `DocumentRevision` | `Document.cs`, `DocumentRevision.cs` | `DocumentTests.Documento_Pode_Possuir_Revisao` | Coberto |
| URS-REV-002 — Manter representações de múltiplas revisões históricas | BR-REV-004 | `Document.Revisions` (coleção), `DocumentoDetalhe.razor` (lista de revisões) | `Document.cs`, `DocumentoDetalhe.razor` | `DocumentTests.Documento_Pode_Possuir_Revisao`, `DocumentRevisionServiceTests.AddRevisionAsync_Vincula_Revisao_Ao_Documento_*` | Coberto — todas as revisões de um Document são listadas na tela de detalhe, sem marcar nenhuma como "vigente" |
| URS-REV-003 — Não criar/aprovar revisão autonomamente; identificador reflete processo da organização | BR-REV-002, BR-REV-003 | `DocumentRevision.RevisionIdentifier` (string livre), `DocumentRevisionService.AddRevisionAsync` | `DocumentRevision.cs`, `DocumentRevisionService.cs` | `DocumentRevisionTests.Identificador_De_Revisao_Nao_Precisa_Ser_Numerico`, `DocumentRevisionServiceTests.AddRevisionAsync_Aceita_Identificador_*` (`00`, `01`, `A`, `B`, `C1`) | Coberto — nenhuma lógica de geração/inferência automática de revisão existe; identificador sempre informado pelo usuário |
| URS-REV-004 — Identificar inequivocamente a revisão de uso corrente | BR-DSP-004, BR-DSP-007 | — | — | — | Não implementado — deliberadamente fora do escopo da DV2-DEV-004/005 (ver Q-009, `docs/handoff/DV2-DSP-001-PROPOSAL.md`) |
| URS-REV-011 — Arquivo oficial por revisão com controle individual de integridade | BR-DOC-003 | `OfficialFile.DocumentRevisionId` (opcional) | `OfficialFile.cs` | `OfficialFileTests.*` | Coberto (estrutura) |
| URS-REV-013 — Registrar informações temporais de vigência quando aplicável | BR-DOC-002, BR-DSP-004 | — | — | — | Não implementado — nenhuma noção de vigência modelada (decisão deliberada, ver BR-DSP-007, Q-009 e `docs/handoff/DV2-DSP-001-PROPOSAL.md`) |

## Categoria (Category) e Tipo Documental (DocumentType)

| Requisito (URS) | Regra (BRN) | Componente | Arquivos de código | Teste | Status |
|---|---|---|---|---|---|
| URS-DOC-006 / URS-ORG-005 — Criar e administrar categorias/subcategorias documentais | BR-CFG-001 | `Category` (auto-relacionamento `ParentCategoryId`), `CategoryService`, página `Administracao/Categorias.razor` | `Category.cs`, `CategoryService.cs`, `Categorias.razor` | `CategoryTests.*` (raiz, com pai, autorreferência rejeitada), `CategoryServiceTests.*` (criar, listar, renomear, alterar pai, rejeitar pai inexistente) | Coberto — administração funcional (criar/listar/editar nome/definir e alterar pai) entregue na DV2-DEV-005 |
| URS-ORG-006 — Configurar tipos documentais de acordo com a organização | BR-CFG-001, BR-CFG-003 | `DocumentType`, `DocumentTypeService`, página `Administracao/TiposDocumentais.razor` | `DocumentType.cs`, `DocumentTypeService.cs`, `TiposDocumentais.razor` | `DocumentTypeServiceTests.*` (criar, listar, renomear, rejeitar tipo inexistente) | Coberto — administração funcional entregue na DV2-DEV-005; nenhum tipo documental é hardcoded no core (atende BR-CFG-001) |
| URS-GEN-002 / URS-GEN-003 — Configurável por organização, sem regra exclusiva no núcleo | BR-CFG-001, BR-CFG-003 | `Category`, `DocumentType` | `Category.cs`, `DocumentType.cs` | — | Coberto — nenhuma categoria, tipo ou nome de organização hardcoded no domínio |

## Decisões de escopo formalizadas nesta tarefa (DV2-SPRINT-002)

| Decisão | Referência | Status |
|---|---|---|
| Não introduzir `OrganizationId`/multi-tenancy na DV2-DEV-004 | Q-008 | Resolvida — nenhuma migration de Organization criada |
| Não modelar estados de disponibilização (vigência/uso corrente) na DV2-DEV-004 | Q-009, BR-DSP-001 a BR-DSP-009 | Resolvida — modelagem funcional transferida à DV2-DEV-005 |
| `Web -> Infrastructure` exclusivamente como Composition Root | Q-006 | Resolvida — ver `docs/ARCHITECTURE.md` |

## Mapeamento EF Core / Integridade estrutural

| Regra (BRN) | Componente | Arquivos de código | Teste | Status |
|---|---|---|---|---|
| BR-DOC-001, BR-DOC-002, BR-DOC-003 — Chaves, FKs e cardinalidade do domínio documental | `DocsViewerDbContext` + `Configurations/*` | `DocsViewer.Infrastructure/Persistence/**` | `DocsViewerDbContextModelTests.*` (todas as FKs `DeleteBehavior.Restrict`; nenhuma exclusão em cascata) | Coberto |
| BR-DOC-004 — Arquivo oficial não modificado silenciosamente | `OfficialFile` (imutável após construção — sem setters públicos) | `OfficialFile.cs` | `OfficialFileTests.*` | Coberto (estrutura — sem serviço de armazenamento físico ainda) |

## DV2-DEV-005 — Application, persistência e UI

| Caso de uso | Componente Application | Repositório (Infrastructure) | Página (Web) | Teste |
|---|---|---|---|---|
| Criar/listar/renomear/definir-alterar pai de Category | `CategoryService` | `CategoryRepository` | `Administracao/Categorias.razor` | `CategoryServiceTests.*` |
| Criar/listar/renomear DocumentType | `DocumentTypeService` | `DocumentTypeRepository` | `Administracao/TiposDocumentais.razor` | `DocumentTypeServiceTests.*` |
| Criar/listar/consultar/editar metadados de Document | `DocumentService` | `DocumentRepository` | `Documentos/Documentos.razor`, `Documentos/NovoDocumento.razor`, `Documentos/DocumentoDetalhe.razor` | `DocumentServiceTests.*` |
| Adicionar revisão a um Document existente | `DocumentRevisionService` | `DocumentRepository` | `Documentos/DocumentoDetalhe.razor` | `DocumentRevisionServiceTests.*` |

**Ressalva de ambiente (Etapa 12):** nenhum SQL Server está disponível neste ambiente (verificado — não há instância local nem `dockerd` em execução para subir um container). Por instrução da tarefa, nenhuma infraestrutura de sistema foi instalada para contornar essa limitação. Isso significa que:
- Os testes acima (Application) usam repositórios em memória (`DocsViewer.UnitTests/TestDoubles/Fake*Repository.cs`) — cobrem a lógica de orquestração dos casos de uso, não o SQL gerado.
- Os testes de integração (`DocsViewerDbContextModelTests`, herdados da DEV-004, sem alteração de schema nesta tarefa) continuam validando o modelo EF Core contra o provider real do SQL Server (sem abrir conexão).
- As páginas Blazor foram smoke-testadas via HTTP: `/` e `/administracao` (sem consulta a banco) respondem HTTP 200; `/documentos`, `/documentos/novo`, `/administracao/categorias`, `/administracao/tipos-documentais` e `/documentos/{id}` retornam HTTP 500 nesta execução, com `Microsoft.Data.SqlClient.SqlException` de conectividade — comportamento esperado e não um defeito de código, confirmado inspecionando o stack trace do log da aplicação.
- CRUD real fim a fim (criar categoria → aparecer na lista → editar → refletir no formulário de Documento) não foi validado contra um banco real nesta tarefa. Fica documentado como pendência de validação (ver `DEV_STATUS.md` — Riscos) para quando houver SQL Server disponível.

## Fora de escopo da DV2-DEV-004 (não enumerado individualmente para não fingir cobertura)

Requisitos e regras relacionados a autenticação/usuários (URS-USR-\*, BR-USR-\*), perfis/permissões (URS-ACL-\*, BR-ACL-\*), visualizador (URS-VWR-\* exceto VWR-013, BR-VWR-\*), pesquisa/OCR (URS-SRC-\*/URS-OCR-\*, BR-SRC-\*), download (URS-DOC-017 a 021, BR-DWL-\*), impressão (BR-PRN-\*), marca d'água (BR-WMK-\*), digitalização (BR-SCN-\*), auditoria (BR-AUD-\*), plataformas/clientes-shell (BR-PLT-\*), cadastro de organização (URS-ORG-001 a 004, 007, 008) e disponibilização/vigência (URS-REV-004, URS-REV-013, BR-DSP-\*) **não foram implementados** nem pela DV2-DEV-004 nem pela DV2-DEV-005, e não são reivindicados como cobertos. Serão tratados em tarefas futuras conforme surgirem na sprint.

## Vínculos arquiteturais (ADRs)

| ADR | Título | Requisitos/decisões relacionados |
|---|---|---|
| ADR-001 | Modelo de renderização do Blazor Web App: Interactive Server | CLAUDE.md — Tecnologias provisoriamente aprovadas; Q-001 |
| ADR-002 | Núcleo Web com clientes-shell opcionais para Windows e Android | BR-PLT-001 a BR-PLT-005 |
| ADR-003 | Adoção do .NET 10 LTS como framework-base | CLAUDE.md — Tecnologias provisoriamente aprovadas; Q-003 |

## Regra de atualização

Esta matriz deve ser preenchida somente com IDs que existam de fato nos documentos oficiais íntegros presentes no repositório (`DV2-BRN-001.docx`, `DV2-TRM-001.xlsx`) ou em decisões explicitamente aprovadas e registradas em `docs/handoff/OPEN_QUESTIONS.md`. Não preencher linhas por antecipação nem declarar cobertura sem teste/código correspondente. Quando os arquivos `DV2-000`, `DV2-001`, `DV2-PMP-001` e o `.docx` de `DV2-URS-001` forem corrigidos (ver Q-007), esta matriz deve ser revisada para incorporar eventuais requisitos adicionais hoje inacessíveis. Quando uma tarefa futura implementar disponibilização (ver `docs/handoff/DV2-DSP-001-PROPOSAL.md`), as linhas "Não implementado"/"Parcialmente coberto" relacionadas (URS-REV-004, URS-REV-013, BR-DSP-\*) devem ser reavaliadas.
