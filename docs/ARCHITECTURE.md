# ARCHITECTURE.md

## Estilo
Monólito modular.

## Dependências
`Web -> Application`
`Web -> Infrastructure` (exclusivamente como Composition Root — ver nota abaixo)
`Infrastructure -> Application`
`Infrastructure -> Domain`
`Application -> Domain`
`Domain -> nenhum projeto`

### Nota sobre `Web -> Infrastructure` (Composition Root)
`DocsViewer.Web` pode depender de `DocsViewer.Infrastructure` **exclusivamente** para registrar e configurar implementações de infraestrutura na inicialização da aplicação (Composition Root — ex.: `Program.cs` chamando `AddInfrastructure(...)`). Essa referência não amplia as demais regras de dependência do monólito modular. Continuam **proibidas**:
- `Domain -> Infrastructure`
- `Domain -> Web`
- `Application -> Infrastructure`
- `Application -> Web`
- `Infrastructure -> Web`

Decisão registrada originalmente como Q-006 (`docs/handoff/OPEN_QUESTIONS.md`) durante a DV2-DEV-003, formalizada aqui na DV2-DEV-004.

## Módulos previstos
Identity, Authorization, Organizations, Documents, Revisions, Categories, Search, Viewer, Printing, Audit, Administration, Reporting, Configuration e Integration.

## Princípios
- UI sem regra crítica;
- Domain sem EF Core;
- autorização no servidor;
- browser sem acesso direto ao NAS;
- Document separado de DocumentRevision;
- auditoria separada de log técnico;
- configuração do cliente fora do core.

## Multi-organização
Não implementar SaaS/multitenancy complexo por antecipação. Porém, não assumir globalmente que sempre existirá uma única empresa quando isso gerar retrabalho inevitável. A estratégia final de tenancy exige ADR.
