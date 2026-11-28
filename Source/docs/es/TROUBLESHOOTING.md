# Solución de problemas

*Escrita para Camera Control Studio 1.1.1.*

## Índice

- [1. Aplicaciones de vídeo y campo de visión](#1-aplicaciones-de-v%C3%ADdeo-y-campo-de-visi%C3%B3n)
  - [Campo de visión limitado en Zoom Workplace](#campo-de-visi%C3%B3n-limitado-en-zoom-workplace)
- [2. Conexión y compatibilidad](#2-conexi%C3%B3n-y-compatibilidad)
  - [Cámara ausente o no disponible](#c%C3%A1mara-ausente-o-no-disponible)
  - [Controles ausentes o distintos entre aplicaciones](#controles-ausentes-o-distintos-entre-aplicaciones)
- [3. Movimiento y posiciones guardadas](#3-movimiento-y-posiciones-guardadas)
  - [El movimiento se detiene, va a saltos o se invierte](#el-movimiento-se-detiene-va-a-saltos-o-se-invierte)
  - [Campo de visión o posición guardada incorrectos](#campo-de-visi%C3%B3n-o-posici%C3%B3n-guardada-incorrectos)

## 1. Aplicaciones de vídeo y campo de visión

### Campo de visión limitado en Zoom Workplace

**Síntomas**\
Faltan los bordes de la escena. El zoom hacia fuera y el giro lateral se detienen antes de tiempo. Las posiciones guardadas cambian de encuadre. **Optimize for video sharing** está desactivado. La limitación también aparece en Logitech BRIO Control.

**Causa**\
**Confirmado en el caso registrado.** Zoom Workplace tenía activado **Efficiency Mode**. Al desactivarlo y reiniciar Zoom volvió el campo original. Camera Control Studio y las posiciones quedaron intactos. Se desconoce el mecanismo técnico.

**Solución**

1. En Zoom Workplace, abre tu foto de perfil → **Settings → Meeting & webinar → Advanced**.
2. En **Performance**, desactiva **Efficiency Mode**. Si está bloqueado, consulta al administrador de Zoom.
3. Cierra Zoom por completo y vuelve a abrirlo. Conserva tus posiciones mientras haces la prueba.

**Comprobación**\
Comprueba que vuelvan a verse los bordes. Abre las posiciones guardadas y compara el encuadre en directo.

**Software y dispositivos afectados**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control y Camera Control Studio 0.7.0-beta.8. No se registraron las versiones de Zoom, el controlador ni el firmware.

**Referencias**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Conexión y compatibilidad

### Cámara ausente o no disponible

**Síntomas**\
La cámara no aparece o deja de responder tras la suspensión.

**Causa**\
**Causas posibles.** Cable suelto, permisos de Windows, otra aplicación o confirmación de emisión pendiente.

**Solución**

1. Revisa el USB. Si la aplicación pide confirmación, inicia la emisión antes de elegir **Sí**.
2. Comprueba los permisos de cámara en Windows. Prueba la vista previa en Cámara de Windows y cierra esa aplicación.
3. Usa un control para reconectar tras la suspensión. Si falta la cámara guardada, elígela con el botón central. De forma predeterminada, haz doble clic en el botón central para abrir la lista de cámaras.

![Rueda de dirección con el selector de cámara en el centro](../images/screenshots/control%20wheel%20only.png)

**Comprobación**\
Confirma el nombre de la cámara abajo y prueba un control en la imagen en directo.

**Software y dispositivos afectados**\
Windows y cámaras integradas o USB. El acceso simultáneo depende del controlador.

**Referencias**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Guías de usuario](USER-GUIDE.md)

### Controles ausentes o distintos entre aplicaciones

**Síntomas**\
Hay imagen, pero faltan controles o los cambios solo aparecen en una aplicación.

**Causa**\
**Confirmado.** Camera Control Studio usa los controles que ofrece el controlador. **Posible.** El firmware, el controlador u otra aplicación pueden influir en la imagen o en los controles.

**Solución**

1. Selecciona la misma cámara física y compara las vistas previas de una en una.
2. Cierra las otras herramientas de cámara. Abre de nuevo la aplicación de vídeo y prueba un control disponible.
3. Consulta Windows Update y el soporte del modelo exacto. Sigue las instrucciones del fabricante para actualizar el controlador o firmware.

**Comprobación**\
Confirma qué control cambia la imagen en la aplicación que vas a usar. Anota las versiones si comunicas el problema.

**Software y dispositivos afectados**\
Windows, controlador, firmware y aplicaciones de cámara o vídeo.

**Referencias**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Movimiento y posiciones guardadas

### El movimiento se detiene, va a saltos o se invierte

**Síntomas**\
El giro, la inclinación o el zoom se detienen pronto. Las transiciones van a saltos o las flechas apuntan al lado contrario.

**Causa**\
**Confirmado.** El controlador fija los límites y los pasos. La aplicación puede invertir las flechas.

Si la dirección cambia al abrir Logitech Camera Settings o Logitech BRIO Control, es posible que otra aplicación haya afectado al control de la cámara. Según el informe, la imagen de Zoom seguía recta. El eje y la causa siguen sin confirmar.

**Solución**

1. Pulsa el botón cuadrado sobre las lupas para recuperar el campo más amplio y el zoom mínimo. Vuelve a probar.
2. En **Configuración → Paneo**, invierte solo el eje que va al revés.
3. Para movimientos finos prueba **Ctrl** con una flecha. Un cambio más lento no reduce el paso del controlador.
4. Fuera de la emisión, cierra la herramienta de Logitech y prueba ambos ejes en la misma aplicación y con el mismo zoom. Abre una sola herramienta de Logitech cada vez para ver si el cambio se repite.

**Comprobación**\
Comprueba la dirección y los límites con la imagen en directo.

**Software y dispositivos afectados**\
Cámaras con controles de giro, inclinación o zoom en Windows. Los límites dependen del modelo y del modo de vídeo.

**Referencias**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Guías de usuario](USER-GUIDE.md)

### Campo de visión o posición guardada incorrectos

**Síntomas**\
El plano queda cerrado, la posición muestra otro encuadre o no se pueden elegir ángulos.

**Causa**\
**Confirmado.** Las posiciones pertenecen a una cámara y un perfil. Una posición antigua sin ángulo guardado conserva el actual. Algunos controladores no ofrecen ángulos.

**Solución**

1. Verifica el perfil y la cámara. Recupera el campo más amplio y observa la imagen.
2. En **Vídeo → Campo de visión**, revisa el campo inicial. Si hay ángulos, permite el cambio manual y compáralos.
3. Abre la posición. Si el campo solo se reduce con Zoom abierto, sigue el apartado de Zoom. Conserva la posición mientras haces pruebas.

**Comprobación**\
Compara la imagen con el encuadre previsto. Prueba la posición otra vez al recuperar el campo original.

**Software y dispositivos afectados**\
Perfiles y posiciones de Camera Control Studio. Los ángulos dependen de la cámara y del controlador.

**Referencias**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Guías de usuario](USER-GUIDE.md)

---

¿Necesitas ayuda? Revisa los registros antes de compartirlos. Indica el modelo, las versiones y lo que probaste.

[Informar de un problema](../en/ISSUES.md) · [Guías de usuario](USER-GUIDES.md)
