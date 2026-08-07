# DEVELOPMENT_RULES.md

- Código e identificadores técnicos em inglês.
- UI preparada para localização.
- Documentação em pt-BR, salvo decisão.
- Preferir UTC em persistência temporal.
- Nullable reference types habilitado.
- Async em I/O relevante.
- Segredos fora do código e do Git.
- Nunca autorização apenas no frontend.
- Não expor filesystem path ao cliente.
- Validar uploads no servidor.
- Evitar IDOR autorizando cada recurso.
- Migrations versionadas e não destrutivas sem autorização.
- Mensagens de erro ao usuário sem stack trace.
- Detalhe técnico em SystemLog.
- AuditEvent em modelo próprio.
- UI responsiva e touch-friendly.
- Não criar abstrações “enterprise” sem necessidade.

## Definition of Done
Compila; testes passam; autorização revisada; auditoria considerada; `DEV_STATUS.md` atualizado; assumptions registradas.
