# Solução de problemas

*Guia escrito para Camera Control Studio 1.0.1.*

## Sumário

- [1. Aplicativos de vídeo e campo de visão](#1-aplicativos-de-vídeo-e-campo-de-visão)
  - [Campo de visão limitado no Zoom Workplace](#campo-de-visão-limitado-no-zoom-workplace)
- [2. Conexão e compatibilidade](#2-conexão-e-compatibilidade)
  - [Câmera ausente ou indisponível](#câmera-ausente-ou-indisponível)
  - [Controles ausentes ou diferentes entre aplicativos](#controles-ausentes-ou-diferentes-entre-aplicativos)
- [3. Movimento e posições salvas](#3-movimento-e-posições-salvas)
  - [Movimento para, dá saltos ou segue no sentido errado](#movimento-para-dá-saltos-ou-segue-no-sentido-errado)
  - [Campo de visão ou posição salva incorretos](#campo-de-visão-ou-posição-salva-incorretos)

## 1. Aplicativos de vídeo e campo de visão

### Campo de visão limitado no Zoom Workplace

**Sintomas**\
As bordas da cena somem. A câmera não abre todo o zoom nem gira tanto para os lados. As posições salvas mudam de enquadramento. **Optimize for video sharing** fica desativado. O limite também aparece no Logitech BRIO Control.

**Causa**\
**Confirmado no caso relatado.** O **Efficiency Mode** estava ativo no Zoom Workplace. Desativá-lo e reiniciar o Zoom trouxe de volta o campo original. O Camera Control Studio e as posições ficaram intactos. O mecanismo técnico é desconhecido.

**Solução**

1. No Zoom Workplace, abra sua foto de perfil → **Settings → Meeting & webinar → Advanced**.
2. Em **Performance**, desative **Efficiency Mode**. Se estiver bloqueado, consulte o administrador do Zoom.
3. Feche o Zoom por completo e abra-o novamente. Mantenha as posições salvas durante o teste.

**Verificação**\
Veja se as bordas voltaram. Abra as posições e compare o enquadramento ao vivo.

**Programas e dispositivos afetados**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control e Camera Control Studio 0.7.0-beta.8. As versões do Zoom, do driver e do firmware não foram registradas.

**Referências**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Conexão e compatibilidade

### Câmera ausente ou indisponível

**Sintomas**\
A câmera não aparece ou para de responder depois da suspensão.

**Causa**\
**Causas possíveis.** Conexão USB, permissão do Windows, outro aplicativo ou confirmação de transmissão pendente.

**Solução**

1. Confira o cabo USB. Se houver pedido de confirmação, inicie a transmissão antes de escolher **Sim**.
2. Revise as permissões da câmera no Windows. Abra e feche a prévia no aplicativo Câmera do Windows.
3. Use um controle para reconectar após a suspensão. Se a câmera salva não aparecer, selecione-a pelo botão central. Por padrão, clique duas vezes no botão central para abrir a lista de câmeras.

![Seletor de direção com o botão da câmera no centro](../images/screenshots/control%20wheel%20only.png)

**Verificação**\
Confira o nome da câmera no rodapé e teste um controle na imagem ao vivo.

**Programas e dispositivos afetados**\
Windows e câmeras integradas ou USB. O acesso simultâneo depende do driver.

**Referências**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Guias do usuário](USER-GUIDE.md)

### Controles ausentes ou diferentes entre aplicativos

**Sintomas**\
A imagem aparece, mas alguns controles faltam ou só alteram a imagem em um aplicativo.

**Causa**\
**Confirmado.** A aplicação usa os controles que o driver oferece. **Causas possíveis.** O driver, o firmware ou outro aplicativo podem afetar os controles ou a imagem.

**Solução**

1. Selecione a mesma câmera física. Compare a prévia em um aplicativo por vez.
2. Feche as outras ferramentas de câmera. Reabra seu aplicativo de vídeo e teste um controle disponível.
3. Consulte o Windows Update e o suporte do modelo exato. Siga o fabricante ao atualizar driver ou firmware.

**Verificação**\
Confirme o efeito no aplicativo que você vai usar. Anote as versões se for relatar o problema.

**Programas e dispositivos afetados**\
Windows, driver, firmware e aplicativos de vídeo ou câmera.

**Referências**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Movimento e posições salvas

### Movimento para, dá saltos ou segue no sentido errado

**Sintomas**\
Giro, inclinação ou zoom param cedo. A transição ocorre em saltos ou as setas movem a imagem ao contrário.

**Causa**\
**Confirmado.** O driver determina alcance e tamanho do passo. O aplicativo pode inverter as setas.

Se a direção mudar ao abrir Logitech Camera Settings ou Logitech BRIO Control, outro aplicativo pode ter afetado os controles. Segundo o relato, a imagem no Zoom continuou na posição correta. O eixo afetado e a causa ainda não foram confirmados.

**Solução**

1. Use o botão quadrado acima das lupas para restaurar o campo mais amplo e o zoom mínimo. Teste novamente.
2. Em **Configurações → Movimento**, inverta apenas o eixo afetado.
3. Tente **Ctrl** com uma seta para movimentos menores. Uma transição mais longa não diminui os passos do driver.
4. Fora da transmissão, feche a ferramenta da Logitech. Teste os dois eixos no mesmo aplicativo de vídeo e com o mesmo zoom. Abra uma ferramenta da Logitech por vez para ver se a mudança volta.

**Verificação**\
Confira o sentido e os limites na imagem ao vivo.

**Programas e dispositivos afetados**\
Câmeras com controles de movimento ou zoom no Windows. Os limites dependem do modelo e do modo de vídeo.

**Referências**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Guias do usuário](USER-GUIDE.md)

### Campo de visão ou posição salva incorretos

**Sintomas**\
O quadro fica fechado, uma posição muda de enquadramento ou as opções de ângulo não aparecem.

**Causa**\
**Confirmado.** As posições pertencem a uma câmera e a um perfil. Posições antigas sem ângulo salvo mantêm o atual. Alguns drivers não oferecem ângulos.

**Solução**

1. Confira o perfil e a câmera. Restaure o quadro completo e observe a imagem.
2. Em **Configurações → Movimento**, confira o campo inicial. Se o driver oferecer ângulos, permita a troca manual e compare.
3. Abra a posição. Se o campo ficar menor apenas com o Zoom aberto, siga a seção sobre o Zoom. Preserve a posição durante o teste.

**Verificação**\
Compare a imagem com o enquadramento esperado. Abra a posição de novo depois de recuperar o campo original.

**Programas e dispositivos afetados**\
Perfis e posições do Camera Control Studio. Os ângulos dependem da câmera e do driver.

**Referências**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Guias do usuário](USER-GUIDE.md)

---

Precisa de ajuda? Confira os registros antes de compartilhá-los. Informe modelo, versões, testes e resultados.

[Relatar um problema](../en/ISSUES.md) · [Guias do usuário](../USER-GUIDES.md)
