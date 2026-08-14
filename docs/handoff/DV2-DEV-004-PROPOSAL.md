# DV2-DEV-004-PROPOSAL — Domínio Documental (proposta, não implementada)

## Status
**Proposta de análise. Nenhum código foi criado a partir deste documento.** Nenhuma entidade, banco, migration ou funcionalidade foi implementada na tarefa que gerou esta proposta (DV2-SPRINT-001, Etapa 7). Esta proposta serve para preparar a tarefa formal `DV2-DEV-004`, que ainda precisa ser aberta e ter seu escopo definido/aprovado.

## Fontes utilizadas
Somente documentação já presente no repositório nesta data:
- `CLAUDE.md` — seções "Documentos", "Armazenamento", "Integridade", "Impressão", "Modelo de autorização".
- `docs/PERMISSION_MODEL.md` — catálogo provisório de permissões (`DOCUMENT_*`).
- `docs/decisions/ADR-002-web-core-com-clientes-shell-opcionais.md` — menciona "documentos, revisões, pesquisa" como responsabilidades do núcleo Web.
- `docs/PROJECT_CONTEXT.md` — acervo típico de exemplo (não hardcodável) e estratégia de produto configurável.
- `docs/ROADMAP.md` — Fase 2 ("Categorias, documentos, revisões, upload, hash e repositório").

**`DV2-URS-001 v0.2` e `DV2-BRN-001 v0.2` ainda não estão no repositório (Q-004, aberta).** Por isso, nenhum ID de requisito (`URS-RU-XXX`) ou regra de negócio (`RN-XXX`) real pôde ser citado nesta proposta — nenhum foi inventado.

## Aviso importante — ciclo de vida documental x representação digital
`CLAUDE.md` não define um ciclo de vida `Rascunho → Vigente → Obsoleto` para as entidades do DocsViewer, e esta proposta **não o inventa**. Vigência e obsolescência documental são decisões do processo de controle documental da própria organização (ex.: aprovação, revisão, substituição formal de um documento controlado). O DocsViewer não é a autoridade sobre esse processo — ele **representa digitalmente** o resultado desse processo e controla a **disponibilização** dessa representação (o que pode ser visualizado, buscado, baixado, impresso, conforme permissão).

Essa distinção é a base de toda a seção 7 (Estados internos) abaixo: qualquer estado interno proposto aqui é sobre a representação digital no DocsViewer, não sobre o processo de controle documental da organização.

## 1. Documento (Documento Lógico)

Conceito: uma entidade lógica estável que representa "um documento controlado" ao longo do tempo (ex.: uma Norma, uma Ordem de Fabricação — nomes de exemplo do acervo típico, não hardcodáveis no core, conforme `docs/PROJECT_CONTEXT.md`).

Já estabelecido em `CLAUDE.md`: "Documento lógico e revisão são entidades diferentes." Isso é uma restrição, não uma sugestão — qualquer modelagem futura deve manter Documento e Revisão como conceitos separados.

Atributos prováveis (a confirmar em URS/BRN v0.2, não decididos aqui): identificador estável, categoria/tipo documental (configurável por implantação — `DOCUMENT_SEARCH`/`DOCUMENT_VIEW` já preveem busca e categorização), organização/escopo proprietário, metadados de localização/pasta lógica.

## 2. Revisão Documental

Conceito: cada versão publicada de um Documento. Já estabelecido em `CLAUDE.md`: "Revisões publicadas são históricas e não devem ser sobrescritas."

Isso implica, no mínimo (sem decidir modelagem concreta): uma Revisão é imutável após publicada; cada Documento pode ter múltiplas Revisões ao longo do tempo; deve existir alguma forma de identificar a Revisão vigente sem depender de sobrescrever revisões antigas.

## 3. Representação / Arquivo Oficial

`CLAUDE.md` já estabelece: arquivo publicado deve ter hash forte (SHA-256 inicialmente) — "hash não substitui aprovação ou assinatura". Armazenamento: PDFs/revisões/anexos ficam no repositório (NAS), não no banco como BLOB sem ADR; usuário final nunca acessa o NAS diretamente; banco guarda metadados.

Isso implica que a "Revisão" (registro lógico) e o "arquivo físico" da revisão são conceitos relacionados mas fisicamente separados (metadado no banco, arquivo no repositório), e que qualquer acesso do usuário final ao arquivo precisa passar por uma camada intermediária do DocsViewer (nunca acesso direto ao NAS).

## 4. Relação Documento ↔ Revisões

Proposta conceitual (não decidida): um Documento possui zero ou mais Revisões, tipicamente 1:N. Não foi encontrada, na documentação disponível, nenhuma indicação de que um Documento possa existir sem nenhuma Revisão publicada versus poder existir "em rascunho" antes da primeira publicação — este é um ponto a decidir (ver seção 9).

## 5. Estados internos necessários à disponibilização

Distintos do ciclo de vida documental da organização (ver aviso acima). Candidatos, baseados no que já existe no catálogo de permissões (`docs/PERMISSION_MODEL.md`, real, não inventado):

- `DOCUMENT_PUBLISH` já existe como permissão — sugere que existe uma ação de "publicar" uma revisão, tornando-a disponível.
- `DOCUMENT_OBSOLETE` e `DOCUMENT_VIEW_OBSOLETE` já existem como permissões — sugerem que existe um estado de "obsoleto"/"retirado de disponibilização padrão" no próprio DocsViewer, com visualização restrita por permissão específica.

Isso sugere (sem decidir) que o DocsViewer já prevê, pelo próprio catálogo de permissões, pelo menos dois estados de disponibilização de uma Revisão: disponível (padrão) e obsoleta (visível apenas com `DOCUMENT_VIEW_OBSOLETE`). Se isso corresponde ou não ao conceito de "vigente/obsoleto" do processo documental da organização, ou se é um estado técnico independente, é uma decisão a confirmar com URS/BRN v0.2 — não presumida aqui.

## 6. Histórico

Decorre diretamente de "Revisões publicadas são históricas e não devem ser sobrescritas" (`CLAUDE.md`) e da separação `AuditEvent` x `SystemLog` (`CLAUDE.md` — Auditoria x log técnico): o histórico de um Documento (suas Revisões) é dado de domínio persistente; ações sobre ele (quem publicou, quem tornou obsoleto, quando) são candidatas a `AuditEvent`, não a `SystemLog`.

## 7. Possíveis invariantes (candidatas, não aprovadas)

- Uma Revisão publicada nunca é alterada nem removida (apenas novas Revisões são criadas).
- Um Documento não pode ter duas Revisões simultaneamente marcadas como "vigente"/"disponível padrão" (a definir o nome exato do estado).
- Toda Revisão publicada possui hash SHA-256 registrado no momento da publicação.
- O arquivo de uma Revisão publicada, uma vez gravado no repositório, não é sobrescrito no mesmo caminho (nova Revisão = novo arquivo).

Estas são inferências a partir de regras já escritas em `CLAUDE.md`, não uma modelagem aprovada — precisam confirmação formal via BRN v0.2.

## 8. Requisitos URS relacionados
**Bloqueado.** `DV2-URS-001 v0.2` não está no repositório (Q-004). Nenhum ID foi inventado ou herdado da v0.1 superada.

## 9. Regras BRN relacionadas
**Bloqueado.** `DV2-BRN-001 v0.2 corrigido` não está no repositório (Q-004). Nenhuma regra foi inventada.

## 10. Riscos conhecidos
- Sem URS/BRN v0.2, qualquer modelagem de Documento/Revisão feita antes da chegada desses documentos corre risco real de precisar ser refeita — reforça a recomendação de **não iniciar implementação de DV2-DEV-004 antes de Q-004 ser resolvida**, ou de ao menos limitar o escopo inicial ao que já está inequivocamente estabelecido em `CLAUDE.md` (Documento ≠ Revisão; Revisão imutável; hash SHA-256; separação banco/repositório).
- Risco de acoplar "estado de disponibilização" do DocsViewer ao processo de vigência/obsolescência da organização por engano, misturando responsabilidades — mitigado ao tratar isso como questão explícita a decidir (seção 11), não como suposição.

## 11. Questões que precisam de decisão humana (candidatas — não registradas em OPEN_QUESTIONS.md nesta etapa)

Ficam registradas aqui, no escopo da proposta, para serem levadas a `docs/handoff/OPEN_QUESTIONS.md` quando a tarefa `DV2-DEV-004` for formalmente aberta (ou antes, se o responsável do projeto preferir decidir já):

1. Os estados `DOCUMENT_PUBLISH`/`DOCUMENT_OBSOLETE`/`DOCUMENT_VIEW_OBSOLETE` do catálogo de permissões correspondem a um estado técnico interno do DocsViewer (disponibilização) ou já pressupõem replicar o conceito de vigência/obsolescência do processo documental da organização? Precisa ser esclarecido antes de modelar o(s) estado(s) da Revisão.
2. Um Documento pode existir sem nenhuma Revisão publicada (ex.: "em criação")? Ou um Documento só passa a existir no DocsViewer no momento em que sua primeira Revisão é publicada?
3. Como é definida a "Revisão vigente" de um Documento — por um campo explícito na Revisão, por ordenação/data, ou outro mecanismo? Isso é regra de negócio (BRN) ou decisão técnica (SDS)?
4. O hash SHA-256 é calculado e armazenado apenas no momento da publicação, ou também recalculado/validado em cada acesso? Tem impacto de performance e de auditoria.
5. Quais categorias/tipos documentais mínimos o core precisa suportar de forma configurável (sem hardcode), e qual a estrutura de configuração por implantação (`docs/PROJECT_CONTEXT.md` já estabelece que os nomes não são hardcodáveis, mas não define a estrutura de configuração)?

## 12. Não implementado nesta proposta
Confirmação explícita: nenhuma entidade `Document`/`DocumentRevision` (ou similar), nenhum `DbSet`, nenhuma migration, nenhum endpoint, nenhuma página, nenhuma regra de autorização foi criada como parte desta proposta. `DocsViewerDbContext` (DV2-DEV-003) permanece sem entidades.
