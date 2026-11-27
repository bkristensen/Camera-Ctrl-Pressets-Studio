# Descrição do projeto

Camera Control Studio salva e recupera fotos da câmera no Windows. Use-o em um estúdio, sala de aula ou reunião ao vivo. Seu aplicativo de captura cuida do vídeo.

## Comportamento central

Use o anel interno para movimentos normais e o anel externo para movimentos rápidos. Cada anel possui setas diagonais. Clique duas vezes no botão central para escolher uma câmera ou arraste-a para dirigir. Você pode alterar ambos os gestos em Configurações → Panorâmica. Os botões de zoom se movem de acordo com a etapa definida. O controle deslizante define o zoom no FOV atual. O aplicativo não abre nenhuma visualização de vídeo. [Definir movimento e FOV](SETUP.md#defina-o-campo-de-vis%C3%A3o)

As posições permanentes armazenam um nome, um ID de símbolo estável e a panorâmica, inclinação, zoom e FOV que o driver relata. Os símbolos combinados mostram os assuntos da esquerda para a direita. As posições permanentes aparecem primeiro em negrito. As posições temporárias não têm ícone e duram até você fechar o aplicativo. Use Play ou clique duas vezes para recuperar qualquer tipo.

Uma interpolação suave solicita coordenadas intermediárias. Uma linha fica amarela durante a transição. Fica verde somente quando a leitura corresponde ao alvo. Uma ação manual, cancelamento ou transição com falha limpa esse estado. O driver determina a granularidade e a latência reais do movimento.

Os perfis contêm seleção de câmera, posições permanentes, valores de imagem, taxas de movimento manual, duração ou velocidade predefinida de transição, inversão de direção, confirmação de câmera, layout de janela e idioma. A alteração dos perfis reinicia o aplicativo com o idioma do perfil selecionado. A confirmação opcional da câmera bloqueia a enumeração e a conexão até que o usuário confirme que a transmissão foi iniciada.

## Arquitetura

`CameraControlStudio` é um aplicativo .NET Framework 4.8 WinForms. Layout de retenção `Forms/*.Designer.cs`. `Forms/*.cs` mantém manipulação de eventos. `AppForm` é a base comum. `Controls` contém renderização vetorial. `Camera.cs` usa câmera Windows DirectShow e interfaces de processamento de vídeo sem um gráfico de captura.

`Shared` contém modelos, persistência, tradução, aritmética de movimento, posições de sessão e lógica de estado. `Tests` é um conjunto de regressão de console .NET 8. Os perfis usam substituição atômica de arquivos, backups e verificações de revisão. O armazenamento de perfil 0,6 existente é mantido; configurações obsoletas de início do programa são ignoradas e descartadas ao salvar.

## Escopo e compatibilidade

O proprietário relatou que o aplicativo anterior funcionou na configuração do laptop e da câmera. Esta reescrita 0.7 compilou e passou nos testes lógicos, mas não foi exercida contra hardware físico neste ambiente de construção. Nenhuma câmera adicional ou software de captura foi testado.

Outras câmeras do Windows podem funcionar quando seus drivers implementam os controles padrão necessários. O acesso simultâneo com software de captura, reunião ou streaming depende do driver. As câmeras virtuais podem não expor controles físicos. Este não é um controlador PTZ de rede genérico. Linux e macOS podem ser hosts de compilação por meio do fluxo de trabalho do Docker, mas não podem executar esta UI do Windows nativamente.

Informe as combinações bem-sucedidas, incluindo versão do Windows, modelo da câmera, versão do driver, aplicativo de captura e quais controles funcionaram. Relate também as limitações para que a lista de compatibilidade possa ser baseada em evidências.

## Possível trabalho futuro

Automação de regressão de UI do Windows; melhor acessibilidade e descoberta de teclado; importação/exportação opcional de perfis individuais; matriz de hardware verificada; liberações assinadas após aprovação; back-ends de câmera adicionais se preservarem o fluxo de trabalho central simples. Estas são ideias, não recursos entregues.
