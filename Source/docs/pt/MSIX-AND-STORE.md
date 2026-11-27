# MSIX e distribuição na loja

*Escrito para Camera Control Studio 1.1.0.*

Use `scripts/build-msix.ps1` e o modelo de manifesto para construir um pacote MSIX. A Microsoft analisa o pacote antes que ele apareça na Loja. O executável portátil não está assinado.

## Escolha uma rota

| Rota | Assinatura |
| --- | --- |
| Microsoft Store com MSIX | A Microsoft assina o pacote da Loja aceito. Você não compra um certificado de assinatura para esta rota. |
| Microsoft Store com um instalador EXE/MSI | A assinatura do editor é necessária; a Loja não assina novamente esse instalador. |
| Download direto do MSIX | O pacote deve ter uma assinatura confiável no dispositivo do usuário. |
| EXE portátil direto | Uma compilação não assinada pode produzir avisos. A assinatura identifica o editor, mas não promete reputação imediata. |

## Lista de verificação

- [ ] Cadastre o produto e reserve seu nome em Partner Center. Copie o nome exato do pacote, Publisher e PublisherDisplayName.
- [ ] Instale as ferramentas do Windows SDK e execute o comando abaixo usando esses valores de identidade.
- [ ] Valide o manifesto e teste o acesso à câmera, instalação, atualização, desinstalação, armazenamento de perfil e idioma no Windows. O modelo declara operação de desktop/confiança total e acesso à webcam. Verifique se eles atendem aos requisitos de certificação atuais.
- [ ] Prepare a listagem, política de privacidade, link de suporte, informações de licença, classificação etária e capturas de tela reais. Explique os controles necessários do driver da câmera. Execute as verificações atuais de certificação de aplicativos do Windows.
- [ ] Faça upload do candidato para Partner Center, corrija problemas de validação, envie e aguarde a certificação. Não descreva o aplicativo como aprovado pela Loja antes da aprovação.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

A saída é um candidato não assinado ao envio da Loja, não um pacote de sideload público pronto para instalação. O script valida os campos de identidade obrigatórios e usa uma compilação x64 explícita. Escolha uma nova versão do pacote de quatro partes para cada lançamento, com a parte final zero para envio à Loja.

Referências oficiais, verificadas em 18 de novembro de 2026:

- [Opções de assinatura de código da Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Lista de verificação de prontidão do armazenamento](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- Guia de assinatura [MSIX](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Consulte também [SignPath](SIGNPATH.md). As regras atuais de elegibilidade e submissão devem ser verificadas novamente antes da publicação.
