# Requisitos — DocsViewer 2.0

Esta pasta concentra a documentação de requisitos do produto DocsViewer 2.0.

## Documento vigente em elaboração

- **DV2-URS-001 — Especificação de Requisitos do Usuário**
- Versão: **0.1**
- Status: **Draft — Em elaboração**
- Data da consolidação: **09/08/2026**
- Documento formal correspondente: `DV2-URS-001_Especificacao_de_Requisitos_do_Usuario_v0.1_Draft.docx`

> Este README é apenas um índice técnico do repositório e não substitui o documento formal controlado em DOCX.

## Decisões consolidadas nesta versão

- O DocsViewer 2.0 é um produto configurável; particularidades da Ortobio pertencem à implantação.
- Desktop e Mobile pertencem ao mesmo produto; a disponibilidade de funcionalidades pode ser configurada por organização, respeitando permissões, limitações técnicas e homologação.
- Windows é a plataforma Desktop prioritária e Android a plataforma Mobile prioritária da primeira homologação.
- Cada organização poderá cadastrar e configurar seus próprios perfis-base; perfis são templates de permissões, não autorização absoluta.
- Permissões são granulares por funcionalidade e podem possuir escopo e ajustes individuais.
- Documento e revisão são conceitos distintos; revisões históricas devem ser preservadas e a revisão vigente deve ser inequivocamente identificável.
- Audit Trail é separado de log técnico e deve registrar contexto suficiente para rastreabilidade, incluindo usuário, data/hora e, quando disponível, dispositivo registrado e/ou endereço IP.
- Download pode ser permitido em Desktop ou Mobile conforme política e permissão, com identificação/marca d'água configurável e auditoria.
- Solicitação de impressão pode ser iniciada em Mobile; a execução da impressão ocorre em contexto Desktop homologado na primeira versão.
- Favoritos entram no escopo inicial e devem estar acessíveis na área inicial/dashboard; o limite é configurável por organização.
- Visualização lado a lado de dois documentos entra no escopo inicial para interfaces com área útil adequada.
- OCR básico de PDFs digitalizados entra no núcleo inicial para permitir pesquisa textual sem disponibilização de arquivos de autoria.
- OCR inteligente/extração semântica permanece no roadmap.
- Digitalização integrada permanece como `Should` e será direcionada a Windows Desktop; NAPS2 SDK é alternativa técnica a avaliar, não decisão de URS.
- Visualização 3D básica permanece como `Should`; conversão automática de arquivos nativos de Engenharia permanece futura.
- Política de credencial inicial é configurável por organização, incluindo regras de composição.

## Questões Q-URS-001 a Q-URS-006

Todas foram encerradas na versão 0.1 Draft:

1. Q-URS-001 — Download em Mobile: **Resolvida**.
2. Q-URS-002 — Solicitação de impressão em Mobile: **Resolvida**.
3. Q-URS-003 — Favoritos: **Resolvida**.
4. Q-URS-004 — Visualizador duplo: **Resolvida**.
5. Q-URS-005 — Digitalização integrada: **Resolvida**.
6. Q-URS-006 — Política de senha inicial: **Resolvida**.

## Próximos documentos derivados

A URS deverá alimentar, conforme o Plano Mestre do Projeto:

- `DV2-BRN-001` — Regras de Negócio;
- `DV2-RA-001` — Análise de Riscos;
- `DV2-NFR-001` — Requisitos Não Funcionais;
- `DV2-FRS-001` — Especificação Funcional;
- `DV2-SDS-001` — Arquitetura e Design;
- futura Matriz de Rastreabilidade.

## Regra de governança

A versão mais recente do documento controlado deve ser tratada como referência ativa. Versões substituídas permanecem recuperáveis no histórico Git e não devem ser mantidas como fonte concorrente de verdade.
