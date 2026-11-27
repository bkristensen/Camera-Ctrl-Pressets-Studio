# Traduzir Camera Control Studio

*Escrito para Camera Control Studio 1.0.1.*

## Editar um idioma existente

1. Abra `Source/Shared/Locales/<code>.xml`. Use `en.xml` como referência para contexto e chaves.
2. Edite o texto entre as tags `<text>`. Mantenha as chaves, os espaços reservados como `{0}` e os IDs armazenados intactos. Salve como UTF-8. Em XML, escreva um E comercial literal como `&amp;`.
3. Se você também alterar a ajuda, edite a pasta correspondente em `Source/docs`. Teste links para outros guias e imagens no Guide Viewer.

## Adicionar um idioma

1. Copie `en.xml` para um arquivo nomeado para o novo código de idioma e traduza todos os valores.
2. Registre o código e o nome de exibição no mesmo índice em `LanguageCodes` e `LanguageNames` em `Source/Shared/UiText.cs`. Atualize o tratamento da direita para a esquerda quando aplicável.
3. Adicione o idioma do aplicativo ao manifesto MSIX. Se você adicionar um guia do usuário, siga a estrutura de uma pasta existente em `Source/docs`, adicione-o a `USER-GUIDES.md` e verifique se o Guide Viewer o abre.

## Verifique e envie

Em `Source`, execute `powershell -File scripts/check-translations.ps1` e construa. Inicie o aplicativo com `--lang=<code>`. Verifique as caixas de diálogo no menor tamanho de janela e navegue com o teclado. Teste rótulos longos, direção do texto e mensagens que preenchem espaços reservados.

Siga o guia do projeto [GitHub](GITHUB.md) para abrir uma solicitação pull. Nomeie a linguagem e o código, resuma as alterações e indique o que você testou. Inclua uma captura de tela se isso ajudar os revisores a avaliar o layout. Adicione edições de acompanhamento ao mesmo branch durante a revisão.
