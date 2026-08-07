# ARCHITECTURE.md

## Estilo
Monólito modular.

## Dependências
`Web -> Application`
`Infrastructure -> Application`
`Infrastructure -> Domain`
`Application -> Domain`
`Domain -> nenhum projeto`

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
