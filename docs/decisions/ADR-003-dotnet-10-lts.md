# ADR-003 — Adoção do .NET 10 LTS como framework-base do DocsViewer Omni

**Status:** Aprovado (Accepted)
**Data:** 2026-08-14
**Projeto:** DocsViewer Omni (DocsViewer 2.0)
**Categoria:** Arquitetura / Plataforma / Tecnologia

## 1. Contexto

A fundação técnica do DocsViewer Omni (DV2-DEV-001) foi criada em `net8.0`, por ser o LTS disponível no início do projeto. Na ocasião, a tarefa registrou a questão Q-003 em `docs/handoff/OPEN_QUESTIONS.md`, comparando objetivamente .NET 8 LTS e .NET 10 LTS, sem decidir a migração — decisão explicitamente reservada ao responsável do projeto.

Dados que fundamentaram a análise (verificados por pesquisa em 2026-08-07 e reconfirmados nesta data):

- .NET 8 é LTS, lançado em novembro/2023, com fim de suporte em **10/11/2026**.
- .NET 10 é LTS, lançado em novembro/2025, com fim de suporte em **11/2028**, e já está estável (GA há cerca de 9 meses nesta data).
- O projeto está em fase de fundação: até esta data (DV2-DEV-001, DV2-DEV-002, DV2-REPO-001) não existe persistência real, EF Core configurado, migrations ou entidades de domínio.

Neste sprint (DV2-SPRINT-001), o responsável do projeto forneceu a decisão humana explicitamente pendente em Q-003.

## 2. Decisão

O **DocsViewer Omni adotará .NET 10 LTS como framework-base da nova geração**.

Todos os projetos da solution (`DocsViewer.Domain`, `DocsViewer.Application`, `DocsViewer.Infrastructure`, `DocsViewer.Web`, `DocsViewer.UnitTests`, `DocsViewer.IntegrationTests`) passam a ter `TargetFramework` igual a `net10.0`.

Nenhuma biblioteca ou framework adicional é introduzido por esta decisão — ela se limita à versão do .NET.

## 3. Justificativa

- O projeto ainda está no início: nenhuma persistência real foi criada, nenhuma migration existe, e a única UI existente (DV2-DEV-002) não depende de nenhuma API específica de versão do .NET.
- Migrar agora evita uma migração estrutural posterior, quando já existirem entidades, EF Core, migrations e mais código dependente da plataforma — o custo de troca tende a crescer a cada tarefa futura.
- .NET 8 encerra suporte em 10/11/2026 — um horizonte curto demais para um produto que ainda está em fundação e cujo ciclo de vida esperado é de médio/longo prazo.
- .NET 10 LTS oferece horizonte de suporte mais adequado ao ciclo de vida esperado do produto (suporte até 11/2028) e já é uma versão estável (GA, não mais recém-lançada).

## 4. Alternativas consideradas

### A. Permanecer em .NET 8 LTS até o fim do suporte (11/2026)
**Rejeitada.** Adiar a migração aumentaria o custo técnico (mais código dependente da versão) e criaria pressão de prazo, já que o suporte terminaria antes mesmo de o produto provavelmente sair da fase de fundação.

### B. Permanecer em .NET 8 LTS e reavaliar em tarefa futura específica
**Rejeitada nesta decisão.** Adiar a decisão sem prazo definido mantém a mesma pressão de prazo da opção A, sem benefício adicional, já que o custo de migração é mínimo agora e tende a aumentar.

### C. Migrar para .NET 10 LTS agora
**Aprovada.** Custo de migração mínimo nesta fase (sem persistência, sem entidades, sem migrations), com horizonte de suporte mais longo (11/2028) alinhado ao ciclo de vida esperado do produto.

## 5. Consequências

### Positivas
- Horizonte de suporte de segurança/patches estendido até 11/2028.
- Acesso a melhorias já disponíveis em .NET 10 relevantes à stack aprovada: EF Core 10 (JSON nativo, LINQ ampliado), Blazor com melhorias de persistência de estado e resiliência, ASP.NET Core com OpenAPI 3.1 e melhorias de segurança/diagnóstico.
- Evita uma migração estrutural mais custosa em fase posterior do projeto, quando já existir código de domínio e persistência.

### Negativas / riscos
- .NET 10 é uma versão mais nova (GA há ~9 meses nesta data); pacotes de terceiros específicos podem ter cobertura/maturidade menor do que para .NET 8, embora nenhuma dependência de terceiros crítica tenha sido identificada na solution até o momento.
- Ambientes de desenvolvimento/CI/produção precisarão ter o SDK/runtime do .NET 10 disponível (neste ambiente de desenvolvimento, o SDK 10.0.110 foi instalado adicionalmente ao 8.0.129 já existente, sem removê-lo).
- Qualquer decisão de infraestrutura (servidor interno, homologação Windows/Android) precisará considerar a versão de runtime .NET 10 quando essas tarefas forem abertas — não avaliado nesta ADR.

## 6. Impacto sobre desenvolvimento

- `TargetFramework` alterado para `net10.0` em todos os 6 projetos da solution.
- Nenhuma mudança de arquitetura, dependências entre projetos ou bibliotecas adicionais — a decisão é estritamente sobre a versão do .NET.
- Build e execução revalidados após a migração (`dotnet restore`, `dotnet build DocsViewer.sln`, execução do `DocsViewer.Web`).

## 7. Impacto sobre validação

- Nenhuma funcionalidade validada anteriormente é afetada, pois a solution ainda está na fase de fundação (sem comportamento de negócio implementado).
- Tarefas futuras que envolvam validação formal devem registrar esta ADR como referência da versão de plataforma vigente.
- Não há impacto em dados persistidos, pois nenhuma persistência real existe até esta data.

## 8. Requisitos relacionados

- CLAUDE.md — Tecnologias provisoriamente aprovadas ("Não trocar tecnologia sem ADR aprovado")
- docs/handoff/OPEN_QUESTIONS.md — Q-003 (encerrada por esta ADR)

## 9. Aprovação

Decisão fornecida pelo responsável do projeto na tarefa DV2-SPRINT-001 (Etapa 2), em 2026-08-14, encerrando a questão Q-003.
