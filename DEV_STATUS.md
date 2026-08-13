# DEV_STATUS.md

## Estado geral
DV2-DEV-001 fechada formalmente.

## Branch atual
claude/DV2-DEV-001-fundacao-tecnica

## Tarefa atual
DV2-DEV-001 — Criar a fundação técnica da solução (fechamento formal:
decisões humanas para Q-001 e Q-002 aplicadas; Q-003 registrada).

## Requisitos relacionados
- CLAUDE.md — Arquitetura inicial (monólito modular, projetos)
- CLAUDE.md — Tecnologias provisoriamente aprovadas (Blazor Web App)
- README_CLAUDE_CODE_SETUP.md — Primeira tarefa recomendada

## Regras relacionadas
- docs/ARCHITECTURE.md — Dependências entre projetos

## Alterações realizadas
**Parte 1 (scaffolding):** Criada a solution `DocsViewer.sln` com os 6
projetos previstos no CLAUDE.md (Domain, Application, Infrastructure, Web,
UnitTests, IntegrationTests), todos vazios (sem entidades, sem
controllers, sem páginas, sem autenticação/autorização, sem banco).
Adicionadas as referências entre projetos principais definidas em
docs/ARCHITECTURE.md. Removido o endpoint de exemplo do template padrão
do projeto Web e os `Class1.cs` de exemplo dos class libraries. Adicionado
`.gitignore` padrão do .NET.

**Parte 2 (fechamento formal):**
- Criado `docs/decisions/ADR-001-blazor-web-app-interactive-server.md`,
  registrando a decisão aprovada pelo responsável do projeto: Blazor Web
  App com **Interactive Server** como modelo de renderização da primeira
  versão (WebAssembly e Auto descartados nesta fase). Nenhuma alteração
  de código foi feita em DocsViewer.Web — o ADR registra a decisão para
  quando a tarefa de UI/Viewer for aberta.
- Q-001 e Q-002 marcadas como Resolvidas em
  docs/handoff/OPEN_QUESTIONS.md, com a decisão registrada em cada uma.
- Adicionadas as referências de teste decididas pelo responsável do
  projeto: `DocsViewer.UnitTests` → `DocsViewer.Domain` e
  `DocsViewer.Application`; `DocsViewer.IntegrationTests` →
  `DocsViewer.Application` e `DocsViewer.Infrastructure` (referência a
  `DocsViewer.Web` fica para quando um teste real precisar do host Web).
- Removidos os `UnitTest1.cs` de exemplo de ambos os projetos de teste,
  que permanecem vazios (mantido apenas `GlobalUsings.cs`).
- Registrada Q-003 em docs/handoff/OPEN_QUESTIONS.md: análise objetiva
  .NET 8 LTS vs .NET 10 LTS (datas de fim de suporte, maturidade, custo de
  migração) com recomendação de migrar para .NET 10 LTS, para decisão
  humana. **Nenhuma alteração de TargetFramework foi feita** — segue
  net8.0 em todos os projetos.

## Arquivos alterados
- docs/decisions/ADR-001-blazor-web-app-interactive-server.md (novo)
- docs/handoff/OPEN_QUESTIONS.md (Q-001 e Q-002 resolvidas; Q-003 criada)
- DocsViewer.UnitTests/DocsViewer.UnitTests.csproj (referências a Domain e Application adicionadas)
- DocsViewer.IntegrationTests/DocsViewer.IntegrationTests.csproj (referências a Application e Infrastructure adicionadas)
- DocsViewer.UnitTests/UnitTest1.cs (removido)
- DocsViewer.IntegrationTests/UnitTest1.cs (removido)
- DEV_STATUS.md (este arquivo)

## Banco / migrations
Nenhuma — fora do escopo desta tarefa.

## Testes criados
Nenhum teste real — fora do escopo. Os projetos DocsViewer.UnitTests e
DocsViewer.IntegrationTests permanecem vazios (sem nenhum arquivo de
teste), apenas com as referências de projeto corretas já configuradas.

## Resultado dos testes
`dotnet build DocsViewer.sln` — Build succeeded, 0 Warning(s), 0 Error(s).
Critério de aceite da tarefa atendido após as alterações de fechamento.

## Decisões/assumptions
- **Q-001 (resolvida):** Blazor Web App com Interactive Server — decisão
  do responsável do projeto, registrada em ADR-001. Implementação do
  scaffolding Blazor fica para tarefa futura de UI/Viewer.
- **Q-002 (resolvida):** referências de teste definidas pelo responsável
  do projeto (ver acima).
- **Q-003 (aberta):** recomendação registrada de migrar para .NET 10 LTS
  (fundamentada em datas de fim de suporte: .NET 8 termina em 10/11/2026,
  .NET 10 é suportado até 11/2028), mas decisão e execução dependem de
  aprovação humana explícita. TargetFramework não foi alterado.
- Framework de teste adotado: xUnit (assumption já registrada na criação
  da tarefa, sem impacto arquitetural).

## Riscos
Nenhum risco técnico novo nesta etapa. Risco já conhecido: .NET 8 LTS
(versão atual do projeto) encerra suporte em 10/11/2026 — ver Q-003.

## Pendências
- Q-003: decisão humana sobre migrar (ou não) para .NET 10 LTS antes de
  a fundação técnica avançar mais.
- Implementação do scaffolding Blazor Web App (Interactive Server) em
  DocsViewer.Web, quando houver tarefa formal de UI/Viewer.
- Referência de DocsViewer.IntegrationTests para DocsViewer.Web, quando
  houver teste real que precise subir o host Web.

## Próximo passo
Aguardar decisão humana sobre Q-003 e a próxima tarefa formal (ex.:
fundação de banco/EF Core ou primeiras entidades de Domain, conforme
ROADMAP.md — Fase 1).
