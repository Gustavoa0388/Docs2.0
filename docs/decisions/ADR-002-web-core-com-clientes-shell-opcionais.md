# ADR-002 — Núcleo Web com clientes-shell opcionais para Windows e Android

**Status:** Aprovado  
**Data:** 2026-08-10  
**Projeto:** DocsViewer 2.0  
**Categoria:** Arquitetura / Distribuição / Experiência de Plataforma

## 1. Contexto

O DocsViewer 2.0 é uma aplicação Web centralizada, planejada para uso prioritário em ambientes Windows e Android, incluindo estações administrativas e pontos operacionais no chão de fábrica.

Embora o acesso por navegador homologado continue sendo válido, existe uma necessidade de produto para que determinados usuários possam abrir o DocsViewer com aparência e experiência de software/aplicativo instalado, sem depender visualmente da interface de um navegador de uso geral, como barra de endereço, abas, favoritos ou demais elementos típicos de navegação Web.

Essa necessidade é particularmente relevante para estações de produção e tablets dedicados, mas não deve obrigar todas as organizações ou todos os usuários a utilizar um cliente instalado.

O controle de acesso à Internet, políticas de navegador, firewall, proxy, modo quiosque e demais restrições de rede ou estação permanecem responsabilidades da infraestrutura/TI da organização e não serão assumidos pelo DocsViewer como mecanismo de segurança de rede.

## 2. Decisão

O DocsViewer 2.0 adotará a seguinte estratégia arquitetural:

> **Núcleo Web centralizado + clientes-shell opcionais para Windows e Android.**

O núcleo Web continuará sendo a fonte principal de comportamento funcional do produto, concentrando regras de negócio, autenticação, autorização, gestão documental, revisões, pesquisa, Audit Trail, solicitações e demais funcionalidades do sistema.

Os clientes-shell serão camadas finas de apresentação/acesso destinadas a oferecer experiência semelhante a aplicativo instalado, sem constituírem sistemas funcionais independentes.

### 2.1 Formas de acesso previstas

O produto poderá ser acessado por:

1. navegador Web homologado;
2. cliente-shell Windows;
3. cliente-shell Android.

A organização poderá utilizar uma ou mais dessas formas conforme sua implantação, políticas internas e dispositivos homologados.

## 3. Cliente-shell Windows

Será previsto um cliente instalável para Windows, provisoriamente denominado `DocsViewer.Desktop`.

Esse cliente deverá:

- apresentar o DocsViewer em janela própria, com identidade visual do produto;
- ocultar elementos típicos de navegador de uso geral, como barra de endereço e abas;
- carregar o núcleo Web configurado para a implantação;
- permitir criação de atalho no Desktop e/ou Menu Iniciar;
- poder ser distribuído por instalador Windows;
- manter o núcleo funcional centralizado no servidor Web;
- poder, futuramente, atuar como ponte para recursos locais quando houver justificativa arquitetural aprovada.

WebView2 é uma alternativa técnica natural para esse cliente, porém a tecnologia de implementação detalhada deverá ser confirmada na especificação de arquitetura/SDS e não é transformada por este ADR em dependência imutável.

## 4. Cliente-shell Android

Será previsto um cliente instalável para Android, distribuível como APK, provisoriamente denominado `DocsViewer Mobile`.

Esse cliente deverá:

- criar ícone próprio na tela do dispositivo;
- abrir o DocsViewer em experiência fullscreen/aplicativa;
- não depender visualmente da interface normal de um navegador de uso geral;
- carregar o núcleo Web configurado para a implantação;
- manter o núcleo funcional centralizado no servidor Web;
- permitir futura integração com recursos locais do dispositivo quando formalmente especificado.

WebView, Trusted Web Activity/PWA empacotada ou outra alternativa equivalente poderão ser avaliadas tecnicamente. A escolha final deverá ser registrada na arquitetura correspondente.

## 5. Distribuição

A estratégia de distribuição deverá permitir que uma implantação disponibilize instaladores/clientes por endereço Web controlado, por exemplo:

- `/install/windows`
- `/install/android`

ou página equivalente de downloads da própria implantação.

A URL efetiva, domínio, certificados e mecanismo de distribuição serão definidos por implantação e infraestrutura.

O cliente-shell deverá poder ser configurado para apontar ao endereço do servidor DocsViewer correspondente à organização, evitando a necessidade de manter código funcional diferente por cliente.

## 6. Regra arquitetural principal

Funcionalidades de negócio não deverão ser implementadas exclusivamente nos clientes-shell quando puderem permanecer no núcleo Web.

O shell somente deverá assumir responsabilidades locais quando existir necessidade justificada, como:

- integração com scanner ou outro hardware local;
- identificação confiável do dispositivo;
- integração com funcionalidades específicas do sistema operacional;
- impressão local controlada;
- autenticação biométrica ou recursos nativos futuros;
- outras capacidades aprovadas por decisão arquitetural/requisito correspondente.

## 7. Consequências positivas

A decisão proporciona:

- um único núcleo funcional do DocsViewer 2.0;
- atualização centralizada do produto;
- redução de duplicação de regras entre plataformas;
- experiência de software instalado no Windows;
- experiência de aplicativo instalado no Android;
- manutenção do acesso por navegador quando desejado;
- flexibilidade por organização e implantação;
- caminho controlado para integração futura com hardware e recursos locais.

## 8. Consequências e riscos

A decisão introduz responsabilidades adicionais, incluindo:

- manutenção dos clientes-shell e respectivos instaladores/pacotes;
- homologação de versões do runtime/componente Web utilizado pelos shells;
- definição segura do endereço do servidor da implantação;
- tratamento de indisponibilidade do servidor/rede;
- atualização dos shells quando houver alteração de funcionalidade nativa;
- necessidade de evitar divergência funcional entre navegador e clientes-shell.

Esses riscos deverão ser tratados nos documentos de arquitetura, requisitos não funcionais, análise de riscos e testes correspondentes.

## 9. Itens que não fazem parte desta decisão

Este ADR não define:

- política de acesso à Internet;
- firewall, proxy ou filtros Web da organização;
- modo quiosque obrigatório;
- tecnologia definitiva do shell Windows;
- tecnologia definitiva do shell Android;
- mecanismo definitivo de atualização automática dos clientes;
- formato final do instalador Windows;
- mecanismo final de distribuição do APK;
- política de MDM/gerenciamento de dispositivos.

Esses itens serão definidos somente quando houver necessidade formal e documentação correspondente.

## 10. Impacto documental

Esta decisão deve ser refletida nas próximas revisões de:

- `DV2-000 — Product Vision`;
- `DV2-001 — Documento de Fundação`;
- `DV2-URS-001 — Especificação de Requisitos do Usuário`;
- `DV2-SDS-001 — Arquitetura e Design`, quando elaborado;
- documentação de instalação, distribuição e implantação;
- matriz de rastreabilidade futura.

Requisitos sugeridos para a próxima revisão da URS:

- o produto deve permitir acesso por clientes-shell instaláveis nas plataformas homologadas;
- os clientes-shell devem oferecer experiência de aplicativo sem exigir interface de navegador de uso geral;
- o uso de clientes-shell não deve criar núcleos funcionais independentes do produto Web;
- a forma de acesso disponível pode ser definida por organização/implantação.

## 11. Alternativas consideradas

### A. Navegador como única forma de acesso

**Não adotada como única estratégia.** Mantém simplicidade técnica, porém não atende plenamente à necessidade de experiência de aplicativo em estações e tablets dedicados.

### B. Aplicações Windows e Android completas e independentes

**Rejeitada.** Introduziria duplicação de lógica, maior custo de manutenção, risco de divergência funcional e aumento significativo da superfície de validação.

### C. Núcleo Web com clientes-shell opcionais

**Aprovada.** Preserva centralização e multiplataforma, adicionando experiência nativa onde ela agrega valor.

## 12. Aprovação

A decisão foi aprovada pelo responsável do projeto em 2026-08-10 e deve ser tratada como decisão arquitetural vigente até que seja formalmente substituída por novo ADR ou revisão aprovada.
