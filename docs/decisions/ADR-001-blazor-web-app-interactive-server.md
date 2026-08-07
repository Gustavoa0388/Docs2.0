# ADR-001 — Modelo de renderização do Blazor Web App: Interactive Server

**Status:** Aprovado
**Data:** 2026-08-07

## Contexto
O CLAUDE.md aprova "Blazor Web App" como tecnologia de UI (`docs/PROJECT_CONTEXT.md`, `CLAUDE.md` — Tecnologias provisoriamente aprovadas), mas não define o modelo de hospedagem/renderização. O Blazor Web App (introduzido no .NET 8) suporta múltiplos modelos: Interactive Server, Interactive WebAssembly e Interactive Auto (combina os dois, com fallback).

Durante a tarefa DV2-DEV-001 essa lacuna foi registrada como questão aberta (Q-001 em `docs/handoff/OPEN_QUESTIONS.md`), e `DocsViewer.Web` foi criado como projeto ASP.NET Core mínimo, sem nenhuma configuração de Blazor, para não antecipar essa decisão.

## Decisão
A primeira versão do DocsViewer 2.0 usará **Blazor Web App com Interactive Server** como modelo principal de renderização.

WebAssembly e Auto não serão usados nesta fase.

Esta é uma decisão de fechamento formal da DV2-DEV-001, feita pelo responsável do projeto. Esta ADR registra a decisão; a implementação do template/scaffolding do Blazor (conversão de `DocsViewer.Web`, `App.razor`, layout, etc.) está fora do escopo da tarefa que originou este ADR e será feita em tarefa futura dedicada à fundação de UI/Viewer.

## Alternativas
- **Interactive WebAssembly**: exigiria baixar o runtime .NET para o navegador (maior payload inicial), execução do código no cliente. Não escolhida nesta fase.
- **Interactive Auto**: combina Server e WebAssembly, com maior complexidade operacional e de build (dois modos de execução a manter). Não escolhida nesta fase.
- **Interactive Server** (escolhida): estado e execução no servidor via SignalR, menor payload inicial, mais simples de operar e depurar em primeira versão; adequado ao contexto de PCs/tablets em rede interna (Ortobio) descrito em `docs/PROJECT_CONTEXT.md`.

## Consequências positivas
- Ciclo de execução mais simples (uma única forma de renderização/estado).
- Menor superfície de ataque no cliente (lógica permanece no servidor), alinhado ao princípio de `CLAUDE.md` — Segurança: "a interface nunca é autoridade de segurança".
- Facilita reautorização no servidor a cada interação (já que a interação já passa pelo servidor via SignalR).

## Consequências negativas
- Dependência de conexão persistente (SignalR) com o servidor; degradação de rede afeta a interatividade.
- Escalabilidade horizontal exige atenção a afinidade de sessão/estado no servidor (não avaliado nesta ADR; a ser tratado quando houver tarefa de infraestrutura/deploy).
- Migração futura para WebAssembly ou Auto, se necessária, exigirá revisão desta ADR.

## Impacto em validação
Nenhum impacto imediato — nenhuma UI foi implementada nesta tarefa. Tarefas futuras que implementarem UI deverão registrar rastreabilidade normalmente (URS/RN/ID funcional) e citar esta ADR.

## Requisitos relacionados
- CLAUDE.md — Tecnologias provisoriamente aprovadas
- CLAUDE.md — Segurança (autorização revalidada no servidor)
- docs/PROJECT_CONTEXT.md — Prioridade (visualização rápida, organizada, segura e rastreável)

## Aprovação
Aprovado pelo responsável do projeto em 2026-08-07, no fechamento formal da tarefa DV2-DEV-001.
