# Validação para 0.7.0-beta.3

*Escrito para Camera Control Studio 1.1.1.*

A versão do Windows Release usa dependências bloqueadas. Paridade de tradução e testes automatizados aprovados. Os testes abrangem movimento, predefinições, idioma do perfil, persistência, registro e o HRESULT de câmera não pronta relatado após a hibernação.

O ZIP portátil extraído foi testado em processos x86 e x64 do Windows. Ambos abriram e reabriram os guias de uso diário e configuração, exibiram suas capturas de tela e imprimiram em PDF. O x64 PDF tinha título, versão, data e contagem correta de páginas em todas as cinco páginas. O botão **-** estava presente durante a inicialização em tempo de design do WinForms. O carregador ARM64 foi verificado quanto à arquitetura binária correta.

O perfil do English Standard foi capturado com uma câmera de demonstração. As 12 capturas de tela da janela inteira cobrem os estados da janela principal, cada guia de configurações e cada subguia. Os links locais nos guias e READMEs foram verificados em relação aos caminhos dos documentos empacotados.

## Campo de visão e zoom Brio

Use um Brio físico que ofereça 65°, 78° e 90°. Observe o modelo, firmware, driver, versões do aplicativo e qual modo de vídeo o Zoom usa.

1. Inicie o Zoom e use uma cena fixa com pontos de referência visíveis próximos às bordas. Confirme o enquadramento mais amplo em 90° e zoom mínimo usando Logitech Brio Control.
2. Inicie Camera Control Studio com **Manter como está** e confirme se o dispositivo mantém seu FOV. Reinicie com **Mais amplo disponível** e repita com um FOV especificado e com a confirmação de streaming habilitada.
3. Com todos os três aplicativos em execução, altere panorâmica, inclinação, zoom e FOV no Logitech Brio Control. Confirme as leituras atualizadas em Camera Control Studio com FOV manual desabilitado e habilitado.
4. Altere FOV em Camera Control Studio. Observe quando o Logitech Brio Control atualiza seu próprio display. Salve e recupere predefinições para cada FOV, incluindo uma predefinição mais antiga sem FOV.
5. A partir de uma foto estreita e ampliada, escolha **Quadro completo** e compare a imagem com a referência da etapa 1. Repita durante uma transição predefinida e com o Logitech Brio Control já aberto.
6. Feche o Camera Control Studio e verifique se o Logitech Brio Control ainda pode trocar a câmera. Repita na ordem inversa de inicialização do aplicativo. Anexe o log de diagnóstico e anote qualquer discrepância entre a imagem ao vivo e os valores relatados.
7. Em 65° e zoom mínimo, pressione Menos zoom uma vez. Verifique se FOV muda para 78° e a próxima etapa diminui o zoom. Repita a 78°. Verifique se o Zoom out para apenas em 90° e zoom mínimo. Teste o caminho inverso para Zoom in.
8. Em uma borda panorâmica horizontal, pressione a mesma seta novamente. Verifique se uma FOV mais ampla e um ajuste de zoom permitem movimento sem perder a foto. Se nenhuma visualização ajudar, verifique se o enquadramento original retorna. Desligue o recurso e confirme se o FOV permanece no lugar.

Inno Setup 7 compilou o instalador com os guias para todos os 12 idiomas e os carregadores x86, x64 e ARM64. Ambos os pacotes não são assinados, com hashes SHA-256 fornecidos. Uma instalação e desinstalação de conta limpa, um ciclo de hibernação de câmera física e um teste de tempo de execução ARM64 estão pendentes. Consulte a lista de verificação de versão [](RELEASE-CHECKLIST.md) e [guia de construção e versão](BUILD-AND-RELEASE.md).
