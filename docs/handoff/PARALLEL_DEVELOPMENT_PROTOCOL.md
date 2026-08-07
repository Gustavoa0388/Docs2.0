# PARALLEL_DEVELOPMENT_PROTOCOL.md

## Regra de ouro
Claude Code não define produto. Claude Code implementa tarefas suficientemente definidas.

## Fluxo
1. requisito/regra documentado;
2. criar tarefa DV2-DEV-XXX;
3. definir critérios;
4. Claude cria branch;
5. implementa só o escopo;
6. testa;
7. atualiza DEV_STATUS;
8. humano revisa diff;
9. atualizar rastreabilidade;
10. merge após revisão.

## Evitar duplicidade
Consultar DEV_STATUS e branches antes de iniciar. Definir propriedade do módulo.

## Bom uso do Claude
Scaffolding, entidades aprovadas, mappings EF, migrations não destrutivas, testes, componentes UI conforme protótipo, endpoints/casos de uso especificados e refactors explicitamente solicitados.

## Decisão humana
Produto, requisito, regra, risco, arquitetura, segurança crítica, validação e comportamento ambíguo.
