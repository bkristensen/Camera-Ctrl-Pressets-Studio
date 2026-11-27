# Política de assinatura de código

A Fundação SignPath oferece assinatura gratuita para projetos de código aberto elegíveis. A aceitação é necessária. O certificado identifica a Fundação SignPath como editora; os links de serviço compilações verificadas para o repositório de origem. O projeto solicitou assinatura da Fundação SignPath. O aplicativo está pendente e os downloads do GitHub permanecem sem assinatura.

Antes de se inscrever, publique a fonte sob sua licença. Mantenha o projeto atualizado. Publique uma versão, documente o aplicativo e torne a compilação verificável. Assine apenas seus próprios arquivos de projeto. Continue assinando credenciais fora do repositório.

A política de privacidade [](../../PRIVACY.md) cobre o aplicativo, sua verificação de atualização GitHub e o tempo de execução WebView2 do GuideViewer.

## Enquanto o pedido estiver pendente

- Verifique se o repositório público teve uma compilação bem-sucedida do Windows em **Actions**. Use esse fluxo de trabalho como base da integração SignPath.
- Confirme as pessoas responsáveis pelos commits, revisões e assinaturas de aprovações. Ative a autenticação multifator para todos os membros da equipe em GitHub e SignPath.
- Peça ao SignPath para confirmar como os binários do SDK WebView2 e o instalador Inno Setup devem ser tratados. Os pacotes incluem binários e carregadores SDK da Microsoft. O aplicativo usa o Runtime WebView2 instalado no PC do usuário. Liste essas dependências no aplicativo e preserve seus avisos.
- Adicione um link de política de assinatura de código **** às notas de versão GitHub para a página de download fornecida no aplicativo. Indique o status real de assinatura desses downloads.

## Após SignPath aceitar o projeto

- Adicione a atribuição SignPath aprovada e os nomes confirmados ou grupos de permissão para committers, revisores e aprovadores a esta página.
- Configure SignPath para aceitar artefatos da construção GitHub Actions confiável. Restrinja a assinatura aos binários do projeto. Defina e aplique o nome do produto e a versão correspondente do produto nos arquivos enviados juntos.
- Para ZIP, assine o executável do aplicativo antes de empacotar. Para o instalador, coloque o executável do aplicativo assinado no instalador antes de assinar o instalador finalizado. Obtenha aprovação manual para cada solicitação de assinatura de liberação.
- Verifique as assinaturas e carimbos de data/hora do Authenticode e, em seguida, gere somas de verificação SHA-256. Inclua um link da política de assinatura do código **** e o status da assinatura nas notas de versão. Verifique os arquivos carregados e hashes antes de publicar.

Para downloads diretos de GitHub, uma assinatura válida identifica o editor e protege a integridade do binário. Um novo download ainda pode acionar o SmartScreen enquanto a reputação aumenta. Para distribuição inicial através da Loja, a Microsoft assina pacotes MSIX aceitos.

Para obter um certificado de assinatura de código publicamente confiável disponível por meio do armazenamento de certificados do Windows do usuário atual, use `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` para o ZIP portátil. Use os mesmos parâmetros com `scripts/build-installer.ps1` para assinar o executável do aplicativo e o instalador gerado. A aprovação SignPath requer seu próprio fluxo de trabalho de assinatura.

Leia os termos oficiais do [](https://signpath.org/terms.html), o site da [Foundation](https://signpath.org/) e o guia [SmartScreen da Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Verificado em 24 de novembro de 2026. Para a rota Store, consulte [MSIX e Store](MSIX-AND-STORE.md).
