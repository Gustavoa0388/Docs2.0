# VALIDATION_AWARE_DEVELOPMENT.md

## Objetivo
Preservar evidências e rastreabilidade suficientes para apoiar o ciclo de validação.

Claude Code não aprova documentação regulatória.

## Toda implementação deve registrar
- ID URS;
- ID RN;
- ID funcional, se existir;
- componente;
- teste;
- resultado;
- migration;
- configuração;
- risco técnico;
- desvio.

## Regra
Não modificar requisito para combinar com o código. Se houver divergência, abrir questão.

## Testabilidade
Funcionalidades críticas devem favorecer teste unitário, integração, autorização, cenários negativos e verificação de auditoria.

## Mudanças
Não alterar comportamento já validado silenciosamente.
