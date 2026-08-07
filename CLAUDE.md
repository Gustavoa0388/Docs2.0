# CLAUDE.md — DocsViewer 2.0

## Função
Você atua como desenvolvedor assistente do projeto DocsViewer 2.0. O projeto é guiado por requisitos, regras de negócio, ADRs, rastreabilidade e validação.

## Regra principal
Nunca invente requisito, regra, permissão, fluxo, integração, tabela ou decisão arquitetural para preencher lacunas. Quando faltar decisão:
1. registre em `docs/handoff/OPEN_QUESTIONS.md`;
2. implemente apenas o que não depender da resposta;
3. pare no ponto de bloqueio;
4. informe claramente a dependência.

## Fontes de verdade
1. URS aprovada.
2. Regras de negócio aprovadas.
3. ADR aprovado.
4. Especificação funcional.
5. Especificação técnica.
6. Tarefa atual.
7. Documento de Fundação.
8. Código existente.

Se houver conflito, não escolha sozinho.

## Visão do produto
DocsViewer 2.0 é uma plataforma configurável para visualização, distribuição controlada e rastreabilidade de documentos. Ortobio é a primeira implantação, não deve existir lógica hardcoded específica da Ortobio no core.

A função principal é visualizar documentos. O DocsViewer não substitui Word, Excel, ZWCAD, SolidWorks ou outros editores.

## Produto x implantação
Não hardcodar nome de empresa, setores, cargos, categorias, tipos documentais, Viman, marcas d'água ou fluxos de cliente. Tudo isso deve ser configuração ou pacote de implantação.

## Plataformas
Primeira versão homologada: Windows e Android. Linux, macOS e iOS somente após homologação formal.

## Arquitetura inicial
Monólito modular:
- `DocsViewer.Domain`
- `DocsViewer.Application`
- `DocsViewer.Infrastructure`
- `DocsViewer.Web`
- `DocsViewer.UnitTests`
- `DocsViewer.IntegrationTests`

Domain não depende de EF, ASP.NET, SQL Server, filesystem ou UI.
Application orquestra casos de uso.
Infrastructure implementa persistência, storage, hash e integrações.
Web contém UI, endpoints e composição.

## Tecnologias provisoriamente aprovadas
- C#
- ASP.NET Core
- Blazor Web App
- Entity Framework Core
- SQL Server Express
- PDF.js
- servidor interno inicialmente
- NAS/repositório interno

Não trocar tecnologia sem ADR aprovado.

## Segurança
A interface nunca é autoridade de segurança. Ocultar botão é UX. Toda autorização deve ser revalidada no servidor.
Regra: sem autorização explícita = negar.

## Modelo de autorização
Perfil base + permissões granulares + escopo + override individual.
Perfis iniciais: Admin, Editor e Viewer. São templates, não autorização absoluta.

Exemplos válidos:
- Admin sem `DOCUMENT_DELETE`;
- Viewer com `PRINT_REQUEST`;
- Editor sem upload.

## Impressão
Deve ser possível ter impressão direta, impressão controlada e solicitação de impressão. Autorizações devem ser vinculadas ao documento e à revisão. Carimbo deve ser aplicado em cópia temporária; o arquivo oficial não pode ser alterado.

## Criação de usuários
Solicitar criação, aprovar, criar/ativar e editar permissões são ações distintas. Perfis/cargos de solicitantes não devem ser hardcoded; use permissões.

Senha inicial será aplicada por política configurável e deve exigir troca no primeiro acesso. Nunca armazenar senha em texto puro.

## Autenticação futura
Arquitetura deve permitir biometria do dispositivo, crachá, código de barras/QR, PIN e identidade corporativa. Não armazenar biometria bruta.

## Documentos
Documento lógico e revisão são entidades diferentes. Revisões publicadas são históricas e não devem ser sobrescritas.

## Armazenamento
Banco: usuários, permissões, metadados, sessões, auditoria, configurações.
Repositório: PDFs, revisões e anexos.
Usuário final nunca acessa o NAS diretamente.
Não armazenar PDF como BLOB sem ADR.

## Integridade
Arquivo publicado deve possuir hash forte, inicialmente SHA-256. Hash não substitui aprovação ou assinatura.

## Auditoria x log técnico
`AuditEvent`: ações de negócio e segurança.
`SystemLog`: erros e diagnóstico.
Não misturar. Auditoria não deve ter função comum de limpeza.

## Futuro
Portas abertas, mas não implementar sem tarefa:
- OCR;
- assinatura eletrônica;
- IA documental;
- visualização 3D;
- integração ERP;
- biometria;
- crachá;
- dispositivos/postos;
- novas plataformas.

## Validação
Toda implementação deve registrar:
- requisito;
- regra;
- arquivos alterados;
- migration;
- testes;
- resultados;
- riscos e pendências.

Não ajustar requisito para combinar com o código.

## Git
Nunca trabalhar diretamente em `main`.
Branch: `claude/<ID-da-tarefa>-descricao-curta`
Uma tarefa por branch. Commits pequenos. Sem refactor não relacionado.

## Documentação
Pode atualizar:
- `DEV_STATUS.md`;
- `docs/handoff/OPEN_QUESTIONS.md`;
- docs técnicas relacionadas à tarefa, quando solicitado.

Não pode declarar como aprovado:
- URS;
- RN;
- ADR;
- risco;
- protocolo;
- mudança de escopo.

## Antes de codificar
1. Leia `CLAUDE.md`.
2. Leia `docs/PROJECT_CONTEXT.md`.
3. Leia `docs/ARCHITECTURE.md`.
4. Leia `docs/DEVELOPMENT_RULES.md`.
5. Leia `docs/VALIDATION_AWARE_DEVELOPMENT.md`.
6. Leia a tarefa.
7. Leia requisitos/ADRs citados.
8. Analise o código.
9. Apresente plano curto.
10. Só então altere arquivos.

## Depois de codificar
1. Build.
2. Testes.
3. Atualizar `DEV_STATUS.md`.
4. Atualizar perguntas abertas.
5. Resumir alterações.
6. Informar migrations.
7. Informar requisitos cobertos.
8. Informar desvios e riscos.

## Proibições
Não inventar escopo, microsserviços, mensageria, Redis, cache distribuído ou abstrações por antecipação. Não colocar segredo no código. Não hardcodar cliente. Não armazenar senha em texto. Não considerar TODO como implementação concluída.

## Critério de qualidade
Prefira a menor solução que atenda ao requisito, preserve segurança, seja testável, rastreável, compreensível e não bloqueie evoluções já previstas.
