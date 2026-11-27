# Lista de verificação de lançamento

Use o guia de construção e versão [](BUILD-AND-RELEASE.md) para comandos e caminhos de saída.

## Verificações automatizadas

- [ ] Execute `scripts/build.ps1` em um checkout limpo com dependências bloqueadas.
- [ ] Confirme se cada localidade possui as mesmas chaves exclusivas e não vazias. A compilação executa a verificação de tradução e os testes de localidade.
- [ ] Revise a versão fonte e binária, a identidade do pacote e as notas de lançamento.

## Verificações de janelas e câmeras

- [ ] Lance em 900 × 500 e verifique o mínimo de 670 × 450. Redimensione, maximize e restaure em escala de exibição de 100%, 125%, 150% e 200%. Verifique o rodapé, as entradas, os ícones combinados e os glifos de zoom.
- [ ] Abra as configurações à direita. Arraste o divisor. Fechar e reabrir. Teste um layout salvo após desconectar um monitor.
- [ ] Recuperar uma posição: amarelo durante o movimento, verde somente após a leitura correspondente. Pan/tilt/zoom limpa a cor ativa. Desconecte durante uma transição: sem verde falso. Os ícones nunca desaparecem após a confirmação.
- [ ] Teste cada ícone combinado, incluindo cada ordem invertida. Teste os botões de zoom, o controle deslizante de zoom e ambas as inversões de direção.
- [ ] Em um Brio compatível, teste o FOV automático em ambas as direções de zoom e nas bordas panorâmicas esquerda e direita. Siga as etapas do teste [Brio](VALIDATION.md#campo-de-vis%C3%A3o-e-zoom-brio).
- [ ] Ative a confirmação da câmera. Reiniciar. Verifique se não há enumeração ou conexão de câmera antes de Sim. Teste Não e tente novamente a partir de um botão panorâmico/zoom. Teste a alteração do perfil e a seleção da fonte.

## Dados e edição

- [ ] Faça backup do arquivo de perfil 0.6 antes da migração. Confirme se as predefinições e os IDs dos ícones sobreviveram. Confirme se as configurações obsoletas do programa externo estão ausentes após salvar.
- [ ] Teste novos/copie/renomeie perfis, restaure o layout e mantenha o comportamento do layout atual. Verifique se as posições temporárias permanecem apenas na sessão.
- [ ] Teste as configurações e padrões automáticos/manuais da imagem com a câmera real.
- [ ] Abra cada formulário com Shift+F7 em Visual Studio. Ajuste uma propriedade de layout, salve e reconstrua. O desenho personalizado permanece nas classes de controle.
- [ ] Abra o visualizador de guia do ZIP extraído em um processo do Windows de 64 bits. Abra-o novamente e imprima um guia para PDF. Confirme se o instalador e o ZIP incluem o carregador nativo WebView2 correspondente em `runtimes` para cada arquitetura suportada.
- [ ] Verifique o índice do guia do usuário [](USER-GUIDES.md) e confirme se todos os guias de idioma estão presentes em ambos os pacotes. Verifique as capturas de tela do guia [](SCREENSHOTS.md) para cobertura completa de guias e subguias, corte e sobreposição.

## Distribuição

- [ ] Para uma versão portátil, use `scripts/build.ps1`. Para um instalador Inno Setup, use `scripts/build-installer.ps1`, que também executa a construção portátil e os testes. Verifique a versão mostrada pelo instalador e pelo aplicativo.
- [ ] Para uma versão Store, siga a lista de verificação [Store](MSIX-AND-STORE.md). Teste os tipos de pacote que você planeja publicar no Windows.
- [ ] Se estiver assinando, verifique as assinaturas Authenticode no executável e no instalador do aplicativo, quando aplicável. Rotule claramente os downloads não assinados. Gere hashes SHA-256 após assinatura e empacotamento final.
- [ ] Link para a política de assinatura de código [](SIGNPATH.md) nas notas de versão do GitHub. Para versões SignPath, verifique a procedência da construção e a aprovação manual e credite SignPath pelos arquivos assinados.
- [ ] Publique fontes, licenças, notas de lançamento, hashes e binários juntos. Não reivindique testes de hardware não realizados ou aprovação da Loja.
