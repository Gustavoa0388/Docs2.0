# DEV_STATUS.md

## Estado geral
DV2-SPRINT-002 concluída: (1) DV2-DEV-004 consolidada contra a documentação
oficial real (`DV2-BRN-001 v0.2`, `DV2-TRM-001 v0.1`) e integrada em `main`
via PR #9; (2) DV2-DEV-005 entrega o primeiro cadastro documental funcional
(Category, DocumentType, Document, DocumentRevision) com Application,
persistência EF Core e UI Blazor.

## Branch atual
`feature/DV2-DEV-005-document-registration` (a partir de `main` pós-merge
do PR #9).

## Tarefa atual
DV2-SPRINT-002 — Consolidação do Domínio e Cadastro Documental Inicial.

## Parte 1 — Fechamento da DV2-DEV-004

### Documentos oficiais — presentes, com ressalva de integridade
Os 6 documentos oficiais estão fisicamente presentes em `main` desde a
DV2-DOC-002. Verificação técnica (comando `file`, hexdump, `python
zipfile.ZipFile`) confirmou que **4 dos 6 arquivos estão corrompidos**:
`DV2-000_Product_Vision_v0.2_Draft.docx`,
`DV2-001_Documento_de_Fundacao_v0.4.2_Draft.docx`,
`DV2-PMP-001_Plano_Mestre_do_Projeto_v0.1_Draft.docx` e o `.docx` bruto de
`DV2-URS-001` não são ZIP/OOXML válidos. Apenas `DV2-BRN-001.docx` e
`DV2-TRM-001.xlsx` são íntegros — usados como fonte real para a revisão
documental (`DV2-TRM-001` contém o texto completo dos 232 requisitos URS).
Registrado em detalhe na **Q-007**.

### Revisão documental (Etapa 2 da DV2-SPRINT-002)
Revisão de `Document`, `DocumentRevision`, `OfficialFile`, `Category`,
`DocumentType` contra `DV2-BRN-001`/`DV2-TRM-001` encontrou uma lacuna real:
`Document` não possuía `Title`, exigido por `URS-UX-002`/`URS-VWR-013`.
Corrigido: `Title` adicionado à entidade, ao mapeamento EF
(`DocumentConfiguration`), aos testes, e a migration `InitialDocumentDomain`
foi regenerada. Nenhuma outra divergência de domínio foi encontrada — o
restante do modelo (separação Document/Revision, `OfficialFile` com revisão
opcional, `RevisionIdentifier` livre, ausência de vigência,
`DeleteBehavior.Restrict` uniforme) está alinhado com `BR-DOC-*`, `BR-REV-*`
e `BR-DSP-*`.

### Questões encerradas
- **Q-006:** `Web -> Infrastructure` formalizado como Composition Root em
  `docs/ARCHITECTURE.md`. Resolvida.
- **Q-007:** documentos presentes no repositório desde a DV2-DOC-002 —
  resolvida quanto à presença. Ressalva de corrupção de 4 arquivos
  permanece como pendência distinta (recomendada tarefa `DV2-DOC-003`).
- **Q-008:** `OrganizationId` não introduzido nesta fase — configurabilidade
  por organização não é multi-tenancy; tenancy será decidida futuramente.
  Nenhuma migration de Organization criada. Resolvida.
- **Q-009:** disponibilização não modelada na DEV-004; modelagem funcional
  transferida formalmente à DEV-005 (que também não a implementou — ver
  `docs/handoff/DV2-DSP-001-PROPOSAL.md`). Resolvida para o escopo da
  DEV-004.

Ver `docs/handoff/OPEN_QUESTIONS.md` para o texto completo de cada decisão
(histórico de Q-001 a Q-005 preservado, nada apagado).

### Rebase e integração
`feature/DV2-DEV-004-document-domain` rebaseada sobre a `main`
pós-DV2-DOC-002. Conflitos em 7 arquivos de documentação resolvidos
tomando a versão de `main` como autoritativa (instrução explícita da
tarefa); nenhuma alteração de código de domínio foi perdida. PR #9
atualizado e **mergeado em `main`** (commit `840333d`).

### Rastreabilidade real
`docs/08-traceability/TRACEABILITY_MATRIX.md` reescrita com vínculos reais
`URS-*` ↔ `BR-*` ↔ componente ↔ código ↔ teste, classificando cada item
como Coberto / Parcialmente coberto / Não implementado / Não aplicável —
substituindo a tabela anterior baseada apenas em `DEC-DOM-*`.

### Build/test/run pós-rebase
`dotnet build` → 0 erros, 0 warnings. `dotnet test` → 31/31 (21 unit + 10
integration). `dotnet run --project DocsViewer.Web` validado via curl:
`/`, `/documentos`, `/administracao` → HTTP 200, sem exceções no log.

## Parte 2 — DV2-DEV-005 (cadastro documental inicial)

### Domínio (ajustes mínimos para suportar os casos de uso)
Adicionados a `Document` (sem alterar schema): `AddRevision(DocumentRevision)`
(reforça que a revisão pertence ao mesmo `Document`, mesmo padrão de
`OfficialFile`), `UpdateTitle(string)`, `SetCategory(Guid?)`,
`SetDocumentType(Guid?)`. `Category`/`DocumentType` já possuíam `Rename`/
`SetParent` da DEV-004 — reaproveitados sem alteração.

### Application (`DocsViewer.Application`)
Um repositório específico por agregado (sem Repository genérico universal):
`ICategoryRepository`, `IDocumentTypeRepository`, `IDocumentRepository`.
Serviços de caso de uso: `CategoryService` (criar, listar, renomear,
definir/alterar pai — autorreferência barrada pelo domínio, pai inexistente
barrado pelo serviço), `DocumentTypeService` (criar, listar, renomear),
`DocumentService` (criar, listar, consultar, editar título/categoria/tipo —
valida que categoria/tipo informados existem), `DocumentRevisionService`
(adicionar revisão a um Document existente). Documento sem nenhuma revisão
é estado válido em todos os fluxos — nenhuma "Rev.00" é criada
automaticamente, nenhuma revisão é inferida como "atual".
`ApplicationServiceCollectionExtensions.AddApplication()` registra os 4
serviços como `Scoped`.

### Infrastructure (persistência)
`CategoryRepository`, `DocumentTypeRepository`, `DocumentRepository` em
`DocsViewer.Infrastructure/Persistence/Repositories/`, implementando as
interfaces de Application via `DocsViewerDbContext` (EF Core já existente
da DEV-004, sem alteração de schema/migration). Registrados em
`InfrastructureServiceCollectionExtensions.AddInfrastructure(...)`.
`DbContext` nunca é referenciado diretamente por componentes Razor — Web
depende apenas de Application (serviços) e de Infrastructure exclusivamente
como Composition Root (`Program.cs`), preservando a regra formalizada na
Q-006.

### Banco de desenvolvimento
Nenhum SQL Server disponível neste ambiente: não há instância local, e o
daemon Docker (`dockerd`) não está em execução — não foi iniciado nem
instalado nada para contornar isso, por instrução explícita da tarefa.
`appsettings.Development.json` mantém a connection string já existente
(`Server=localhost,1433;...`), sem credenciais reais versionadas. Quando
houver ambiente com SQL Server: `dotnet ef database update --project
DocsViewer.Infrastructure --startup-project DocsViewer.Web` aplica a
migration `InitialDocumentDomain` (a única existente, sem alteração nesta
tarefa) e a aplicação passa a funcionar normalmente.

### UI Blazor (Interactive Server, layout/identidade existentes)
- `Administracao/AdministracaoIndex.razor` (`/administracao`) — substitui a
  página "não implementada" por um índice com links para Categorias e
  Tipos documentais.
- `Administracao/Categorias.razor` (`/administracao/categorias`) — lista,
  formulário "Nova categoria" (nome + categoria pai opcional), edição
  inline (nome + pai).
- `Administracao/TiposDocumentais.razor` (`/administracao/tipos-documentais`)
  — lista, criar, editar (nome).
- `Documentos/Documentos.razor` (`/documentos`) — substitui a página "não
  implementada" por catálogo funcional: Código, Título, Categoria, Tipo,
  revisões (lista de identificadores ou "Sem revisão").
- `Documentos/NovoDocumento.razor` (`/documentos/novo`) — formulário com
  código, título, categoria opcional, tipo documental opcional.
- `Documentos/DocumentoDetalhe.razor` (`/documentos/{Id:guid}`) — edição de
  metadados (título/categoria/tipo), lista de revisões, formulário
  "Adicionar revisão" (identificador livre).

Nenhum campo específico de implantação (Ortobio/Viman) foi criado. CSS
adicionado a `wwwroot/app.css` reaproveita as variáveis de cor já
existentes (`--color-primary`, etc.) e o padrão visual dos cards do
dashboard — sem copiar a interface do repositório Demo.

### Disponibilização (Etapa 18)
**Não implementada nesta tarefa**, por instrução explícita. `DV2-BRN-001`
(seção 6, BR-DSP-001 a 009) descreve o comportamento esperado mas não fixa
nomes de estado, transições nem cardinalidade — por isso, em vez de um
enum improvisado, foi registrada uma proposta:
`docs/handoff/DV2-DSP-001-PROPOSAL.md`, com terminologia derivada do BRN
real (Incorporado / Em conferência / Disponibilizado para uso corrente /
Retirado do uso operacional / Histórico) e os pontos que permanecem em
aberto.

### Validações
Apenas regras conhecidas: campos obrigatórios já validados pelo domínio
(`Code`, `Title`, `Name`), e checagem de existência de `Category`/
`DocumentType`/`Document` referenciados (integridade referencial, não regra
de negócio inventada). Nenhum tamanho máximo, padrão alfanumérico,
obrigatoriedade de revisão ou unicidade global foi inventado — todos
permanecem deliberadamente ausentes por falta de fonte documental (mesma
decisão já registrada na DEV-004).

### Testes
31 testes herdados da DEV-004 (após rebase/merge) + testes novos desta
tarefa:
- **Domain:** `AddRevision_Vincula_Revisao_Do_Mesmo_Document`,
  `AddRevision_De_Outro_Document_E_Invalido`, `UpdateTitle_*`,
  `SetCategory_E_SetDocumentType_Aceitam_Nulo` em `DocumentTests.cs`.
- **Application** (`DocsViewer.UnitTests/Application/**`, usando
  repositórios em memória em `TestDoubles/Fake*Repository.cs` — não usa EF
  Core InMemory/SQLite, preservando a mesma cautela da DEV-004 sobre não
  substituir o provider real do SQL Server nos testes que o exigem):
  `CategoryServiceTests` (criar raiz, criar com pai, pai inexistente,
  renomear, autorreferência rejeitada, alterar pai, listar vazio),
  `DocumentTypeServiceTests` (criar, listar, renomear, renomear inexistente),
  `DocumentServiceTests` (criar sem/com categoria e tipo, categoria/tipo
  inexistentes, atualizar metadados, atualizar documento inexistente,
  listar), `DocumentRevisionServiceTests` (identificadores `00`, `01`, `A`,
  `B`, `C1` como texto livre, revisão vincula ao documento, documento
  inexistente, **documento sem nenhuma revisão adicionada permanece
  válido**).

**Total: 52 testes unitários + 10 testes de integração, 62 aprovados, 0
falhas.** Nenhum teste artificial de getter/setter.

### Build/test/run
`dotnet build DocsViewer.sln` → 0 erros, 0 warnings. `dotnet test
DocsViewer.sln` → 62/62 aprovados. `dotnet run --project DocsViewer.Web`:
`/` e `/administracao` (sem consulta a banco) → HTTP 200; `/documentos`,
`/documentos/novo`, `/administracao/categorias`,
`/administracao/tipos-documentais`, `/documentos/{id}` → HTTP 500 nesta
execução, com `SqlException` de conectividade (SQL Server indisponível
neste ambiente) — confirmado no log da aplicação que a causa é
exclusivamente de infraestrutura, não um defeito de código. CRUD real fim
a fim não foi validado contra banco real nesta tarefa (ver "Riscos"
abaixo).

### Rastreabilidade
`docs/08-traceability/TRACEABILITY_MATRIX.md` atualizada: linhas
anteriormente "Parcialmente coberto — UI é escopo da DEV-005" promovidas
para "Coberto" onde a funcionalidade foi de fato entregue (cadastro de
Document, administração de Category/DocumentType, listagem/adição de
Revision), nova seção "DV2-DEV-005 — Application, persistência e UI"
detalhando caso de uso → componente → repositório → página → teste.

## Decisões tomadas (DV2-SPRINT-002)
- Um repositório específico por agregado (`ICategoryRepository`,
  `IDocumentTypeRepository`, `IDocumentRepository`), sem Repository
  genérico universal — decisão técnica de escopo, não regra de negócio.
- Resolução de nomes de Category/DocumentType para exibição no catálogo de
  Documentos feita na página Blazor (dicionário `Guid -> string` montado a
  partir de `CategoryService`/`DocumentTypeService`), não por navegação EF
  adicional em `Document` — evita reabrir o modelo de domínio já revisado
  e mergeado na DEV-004.
- Testes de Application usam repositórios em memória (fakes), não EF Core
  InMemory/SQLite — mantém a mesma cautela da DEV-004 de não substituir o
  provider real do SQL Server nos testes que dependem dele.
- Disponibilização registrada como proposta (`DV2-DSP-001-PROPOSAL.md`),
  não implementada — ver Etapa 18.

## Riscos
- **Corrupção documental (Q-007):** `DV2-000`, `DV2-001`, `DV2-PMP-001` e o
  `.docx` de `DV2-URS-001` continuam corrompidos — qualquer requisito que
  dependa exclusivamente deles permanece inacessível. Recomendada tarefa
  `DV2-DOC-003` dedicada a corrigir os arquivos.
- **Ausência de SQL Server neste ambiente:** CRUD real fim a fim
  (criar/listar/editar Category, DocumentType, Document, Revision contra
  banco real) não foi validado nesta tarefa — apenas via testes com
  repositórios em memória/EF Core model tests e smoke test HTTP. Deve ser
  validado manualmente assim que houver ambiente com SQL Server disponível
  (aplicar `InitialDocumentDomain` e repetir os fluxos da Etapa 23).
- **Disponibilização (Q-009 → DV2-DSP-001-PROPOSAL):** ainda sem estado
  interno modelado; funcionalidades que dependam de "revisão vigente"
  continuam bloqueadas até decisão humana sobre a proposta.
- **`Document.Code` sem unicidade:** permanece sem imposição de unicidade
  (mesma razão da DEV-004 — sem escopo de organização definido, Q-008).

## Itens deliberadamente não implementados (fora de escopo da DEV-005)
Armazenamento físico/upload de PDF; autenticação; usuários; perfis;
permissões reais; Audit Trail; PDF Viewer; OCR; marca d'água; impressão;
download; scanner; clientes-shell; APK; integração Viman; estados de
disponibilização/vigência (ver proposta).

## Pendências
- Q-007 (corrupção documental): abrir `DV2-DOC-003` para corrigir os 4
  arquivos `.docx` inválidos.
- Validar CRUD real da DEV-005 contra SQL Server assim que disponível.
- Decisão humana sobre `DV2-DSP-001-PROPOSAL.md` antes de modelar
  disponibilização em produção.
- Migration `InitialDocumentDomain` segue nunca aplicada a um banco real.

## Próximo passo sugerido (DEV-006)
1. Resolver a pendência de infraestrutura (SQL Server) e validar o CRUD
   completo da DEV-005 ponta a ponta.
2. Levar `DV2-DSP-001-PROPOSAL.md` para decisão humana e, se aprovada,
   modelar disponibilização (estado, transições, permissões associadas).
3. Iniciar armazenamento físico real de `OfficialFile` (upload, hash,
   repositório/NAS) — ainda fora de escopo em todas as tarefas até aqui.
4. Considerar corrigir os documentos oficiais corrompidos (`DV2-DOC-003`)
   antes de expandir ainda mais regras de negócio sobre fontes parciais.
