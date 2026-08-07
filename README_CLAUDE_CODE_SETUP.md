# Como usar com Claude Code

1. Copie `CLAUDE.md` para a raiz do repositório.
2. Copie `docs/`.
3. Copie `DEV_STATUS.md`.
4. Versione no Git.
5. Crie cada tarefa usando `docs/handoff/TASK_TEMPLATE.md`.
6. Quando quiser reforçar contexto, use `CLAUDE_MASTER_PROMPT.md`.
7. Nunca peça apenas “continue o DocsViewer”.
8. Revise o diff antes de merge.
9. Para sincronizar com a outra frente, traga `DEV_STATUS.md`, resumo, commits e perguntas abertas.

## Primeira tarefa recomendada
**ID:** DV2-DEV-001  
**Título:** Criar estrutura inicial da solução.

**Incluído:** solution, Domain, Application, Infrastructure, Web, UnitTests, IntegrationTests, referências e build verde.

**Fora do escopo:** banco, autenticação, entidades, UI final e migrations.

**Aceite:** `dotnet build` sem erro e dependências respeitando `ARCHITECTURE.md`.
