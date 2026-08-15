# DEV_STATUS.md

## Estado geral
DV2-DEV-004 concluída — primeira base do domínio documental implementada.
Documentos oficiais (URS v0.3, BRN v0.2, etc.) **não** foram incorporados
por não terem sido localizados — Q-004/Q-007 seguem abertas.

## Branch atual
feature/DV2-DEV-004-document-domain (a partir de `main`, com
DV2-SPRINT-001 integrado: DV2-DEV-002, ADR-003/.NET 10, estrutura
documental/rastreabilidade, DV2-DEV-003).

## Tarefa atual
DV2-DEV-004 — Domínio Documental Inicial do DocsViewer Omni.

## Pré-condições (confirmadas antes de iniciar)
- `main` atualizada (`git pull`), DV2-SPRINT-001 integrado.
- `net10.0` confirmado em todos os 6 projetos.
- ADR-001, ADR-002, ADR-003 presentes em `docs/decisions/`.
- `dotnet restore` e `dotnet build DocsViewer.sln` limpos antes de iniciar.

## Documentos oficiais — não incorporados
Busca exaustiva realizada (sistema de arquivos local completo + Google
Drive) pelos 6 arquivos oficiais (`DV2-000` v0.2, `DV2-001` v0.4.2,
`DV2-PMP-001` v0.1, `DV2-URS-001` v0.3, `DV2-BRN-001` v0.2 corrigido,
`DV2-TRM-001` v0.1) — **nenhum foi encontrado**. A branch
`docs/DV2-DOC-002-official-drafts`, aparentemente destinada a recebê-los,
está idêntica a `main` (nenhum arquivo enviado). Nenhum documento foi
recriado de memória. Registrado como **Q-007** (nova). **Q-004
permanece aberta.**

Como consequência, a seção de incorporação de documentos da tarefa ficou
bloqueada, mas a implementação do domínio **não** ficou bloqueada, pois a
própria tarefa forneceu as decisões de domínio aprovadas (DEC-DOM-001 a
004) diretamente no texto — tratadas como fonte de verdade válida para
esta tarefa específica, sem substituir o BRN formal.

READMEs de `docs/00-product/`, `01-project/`, `03-requirements/`,
`04-business-rules/`, `08-traceability/` e `docs/README.md` atualizados
apenas com as versões corretas agora conhecidas (ex.: URS é v0.3, não
v0.2) e com o resultado da busca — sem declarar nenhum arquivo como
presente.

## Q-006 — Composition Root
**Encerrada.** `docs/ARCHITECTURE.md` atualizado: `Web -> Infrastructure`
formalizado na lista de dependências, com nota explícita de que essa
referência serve exclusivamente ao papel de Composition Root, e que
continuam proibidas `Domain -> Infrastructure`, `Domain -> Web`,
`Application -> Infrastructure`, `Application -> Web` e
`Infrastructure -> Web`. Nenhum ADR adicional criado (não exigido pela
estrutura documental existente).

## Entidades de domínio criadas
Em `DocsViewer.Domain`:
- `Documents/Document.cs` — identidade lógica; `Code` (negócio) distinto
  de `Id` (técnico); pode existir sem `Revisions`.
- `Documents/DocumentRevision.cs` — `RevisionIdentifier` como `string`
  livre (não numérico obrigatório); não possui campo de vigência; nunca
  inferida por `MAX()`.
- `Documents/OfficialFile.cs` — hash (`HashValue`/`HashAlgorithm`) e
  metadados de arquivo; `DocumentRevisionId` opcional; construtor valida
  que a `DocumentRevision` informada pertence ao mesmo `Document`.
- `Categories/Category.cs` — hierárquica, `ParentCategoryId` opcional,
  sem limite de profundidade; impede auto-referência na criação e ao
  reatribuir pai.
- `DocumentTypes/DocumentType.cs` — classificação simples, independente
  de `Category`, sem seed de valores fixos.

Nenhum campo específico de cliente (Ortobio/Viman) foi criado.

## Invariantes implementadas (seção 15 da tarefa)
- `Document` possui identidade técnica (`Id` `Guid`, não vazio) e código
  obrigatório (`Code`).
- `DocumentRevision` sempre pertence a um `Document` (`DocumentId`
  obrigatório).
- `OfficialFile` sempre pertence a um `Document` (`DocumentId`
  obrigatório).
- `OfficialFile.DocumentRevision`, quando informada, deve pertencer ao
  mesmo `Document` do `OfficialFile` (`InvalidOperationException` caso
  contrário).
- `Category` não pode ter a si própria como pai (na criação e ao
  reatribuir pai).

**Deliberadamente não implementado:** unicidade de `Document.Code`
(nenhuma fonte documental confirma se deve ser único globalmente ou por
organização — ver Q-008); qualquer limite de tamanho/comprimento de
string (nenhuma fonte documental os define); estados de disponibilização
(ver seção "Disponibilização" abaixo).

## Configurações EF Core
Em `DocsViewer.Infrastructure/Persistence/Configurations/` (uma classe
`IEntityTypeConfiguration<T>` por entidade, aplicadas via
`ApplyConfigurationsFromAssembly`):
- Todas as chaves primárias `Guid` com `ValueGeneratedNever()` (geradas
  em código, não pelo banco).
- `Document -> Category` e `Document -> DocumentType`: FK opcional,
  `DeleteBehavior.Restrict`.
- `DocumentRevision -> Document`: FK obrigatória, `Restrict`.
- `OfficialFile -> Document`: FK obrigatória, `Restrict`.
- `OfficialFile -> DocumentRevision`: FK opcional, `Restrict`.
- `Category -> Category` (pai): FK opcional, `Restrict`.
- **Nenhum cascade delete em nenhuma relação** — comportamento
  conservador de exclusão em toda a árvore Document → Revision →
  OfficialFile e nas demais relações, não só na cadeia citada
  explicitamente na tarefa.
- Domain permanece sem qualquer dependência de EF Core; toda configuração
  fica em Infrastructure.

## Migration
`InitialDocumentDomain`
(`DocsViewer.Infrastructure/Persistence/Migrations/`), criada com
`dotnet ef migrations add` e **inspecionada antes de aceitar**:
- 5 tabelas: `Categories`, `DocumentTypes`, `Documents`,
  `DocumentRevisions`, `OfficialFiles`.
- Todas as FKs geradas como `ON DELETE NO ACTION` (script SQL gerado e
  revisado com `dotnet ef migrations script`).
- 6 índices, um por coluna de FK.
- Nenhuma tabela fictícia. Nenhuma migration foi aplicada contra um banco
  real (ambiente sem SQL Server disponível) — apenas gerada e
  inspecionada (arquivos de migration + script SQL).

## Testes unitários (`DocsViewer.UnitTests`)
20 testes, 20 aprovados, cobrindo (entre outros) exatamente os cenários
pedidos na seção 18: Documento sem Revisão é válido; Documento pode
possuir Revisão; identificador de Revisão não numérico (`00`, `01`, `A`,
`B`, `C1`); OfficialFile sem Revisão; OfficialFile com Revisão de outro
Document é inválido; Category raiz; Category com pai; Category não pode
ser pai de si mesma (na criação e ao reatribuir). Nenhum teste de
getter/setter trivial.

## Testes de integração (`DocsViewer.IntegrationTests`)
10 testes, 10 aprovados (4 já existentes da DV2-DEV-003 + 6 novos). Os
novos testam o **modelo EF Core construído pelo provider real do SQL
Server** (chaves, FKs, obrigatoriedade, `DeleteBehavior.Restrict`, ausência
de cascade), sem abrir nenhuma conexão de banco — não foi usado um
provider diferente (InMemory/SQLite) para não dar falsa confiança sobre
comportamento específico do SQL Server. Nenhum teste exigiu banco real
não executável nesta tarefa.

## Resultado de restore
`dotnet restore DocsViewer.sln` — sem erros.

## Resultado de build
`dotnet build DocsViewer.sln` — Build succeeded, **0 Warning(s), 0
Error(s)**.

## Resultado de test
`dotnet test DocsViewer.sln` — **30 testes, 30 aprovados, 0 falhas**
(20 UnitTests + 10 IntegrationTests).

## Resultado da execução
`dotnet run --project DocsViewer.Web` — aplicação sobe sem exceções após
a introdução do domínio e do EF Core; Interactive Server e navegação
revalidados via navegador headless (Chromium/Playwright), sem erros de
console; `/`, `/documentos`, `/favoritos`, `/solicitacoes`,
`/administracao` → 200. Dashboard (DV2-DEV-002) inalterado. Nenhum CRUD
do domínio foi exposto na UI (fora de escopo desta tarefa).

## Rastreabilidade
`docs/08-traceability/TRACEABILITY_MATRIX.md` atualizada:
- Tabela de requisito/regra formais (URS/BRN) permanece vazia — ainda
  sem documentos oficiais.
- Nova tabela "Cobertura real da DV2-DEV-004" vinculando cada
  `DEC-DOM-XXX` (e demais invariantes) a componente, arquivo de código e
  teste, com status Coberto/Parcialmente coberto/Ainda não implementado
  — sem fingir cobertura formal de URS/BRN.
- Disponibilização e Organização/multi-tenancy marcados explicitamente
  como "Ainda não implementado", com referência às questões abertas.

## Decisões tomadas
- IDs técnicos: `Guid`, gerados em código (`ValueGeneratedNever()`),
  distintos do código documental (`Document.Code`) — não há ADR/BRN
  definindo estratégia de ID; escolha justificada como decisão técnica
  de scaffolding, não regra de negócio.
- Nenhum limite de tamanho/formato de string foi definido em nenhuma
  entidade — ausência de fonte documental para tais limites.
- `Document.Code` não foi tornado único (nem globalmente nem por escopo)
  — sem fonte documental que defina o escopo de unicidade, e criar essa
  restrição agora poderia colidir com uma futura modelagem de
  Organização (Q-008).
- `DeleteBehavior.Restrict` aplicado uniformemente em todas as FKs do
  domínio documental (não só na cadeia Document→Revision→OfficialFile
  citada na tarefa), pelo mesmo princípio de exclusão conservadora.
- `Web -> Infrastructure` formalizado em `docs/ARCHITECTURE.md` (Q-006
  encerrada), sem ADR adicional.

## Assumptions
- Estratégia de identificador técnico (`Guid`) — ver "Decisões tomadas".
- Nenhuma outra assumption além das já documentadas.

## Riscos
- **Q-004/Q-007:** documentos oficiais (URS v0.3, BRN v0.2, Product
  Vision, Documento de Fundação, PMP, TRM) seguem ausentes do
  repositório — qualquer regra de negócio real implementada antes deles
  chegarem corre risco de precisar ser revista.
- **Q-008 (nova):** `OrganizationId` não modelado (Organization não
  especificada formalmente) — modelo atual assume implicitamente escopo
  único; migration adicional será necessária quando isso for resolvido.
- **Q-009 (nova):** nenhum estado de disponibilização foi modelado
  (deliberadamente, conforme instruído) — funcionalidades futuras que
  dependam disso (ex.: tela de Documentos) ficam bloqueadas até decisão.
- Risco já conhecido: Q-003 (.NET 8 vs 10) já resolvida; nenhum risco
  novo relacionado a framework nesta tarefa.

## Itens deliberadamente não implementados (fora de escopo, seção 25)
CRUD completo de interface; autenticação; usuários; perfis; permissões;
Audit Trail funcional; upload físico; armazenamento real de PDF (sem
BLOB); PDF Viewer; OCR; impressão; download; marca d'água; scanner;
shells; APK Android; integração Viman. Também não implementados por
decisão explícita da própria tarefa: estados de disponibilização (Q-009);
`OrganizationId`/multi-tenancy (Q-008); unicidade de `Document.Code`.

## Pendências
- Q-004, Q-007: localizar e incorporar os 6 documentos oficiais.
- Q-008: especificação formal de `Organization` e `OrganizationId`.
- Q-009: estados de disponibilização do DocsViewer.
- Migration `InitialDocumentDomain` nunca foi aplicada a um banco real
  (sem SQL Server disponível neste ambiente) — aplicar e validar quando
  houver ambiente com banco disponível.

## Próximo passo sugerido
1. Localizar e incorporar o pacote documental oficial (Q-004/Q-007) —
   bloqueia rastreabilidade formal e qualquer regra de negócio adicional
   confiável.
2. Decidir Q-008 (Organization/multi-tenancy) e Q-009 (disponibilização)
   antes de expandir o domínio documental além desta fundação.
3. Revisar/mergear o PR desta tarefa.
4. Só então avançar para funcionalidade que dependa de CRUD real do
   domínio (ex.: cadastro de Category/DocumentType, primeira tela de
   Documentos), conforme ROADMAP.md — Fase 2.
