# Descripción del proyecto

*Escrito para Camera Control Studio 1.1.1.*

Camera Control Studio guarda y recupera tomas de cámara en Windows. Úselo en un estudio, un aula o una reunión en vivo. Su aplicación de captura maneja el video.

## Comportamiento central

Utilice el anillo interior para movimientos normales y el anillo exterior para movimientos rápidos. Cada anillo tiene flechas diagonales. Haga doble clic en el botón central para elegir una cámara o arrástrela para dirigirla. Puede cambiar ambos gestos en Configuración → Panorámica. Los botones de zoom se mueven en el paso establecido. El control deslizante establece el zoom dentro del FOV actual. La aplicación no abre ninguna vista previa del vídeo. [Establecer movimiento y FOV](SETUP.md#establecer-el-campo-de-visi%C3%B3n)

Las posiciones permanentes almacenan un nombre, un ID de símbolo estable y el movimiento horizontal, vertical, zoom y FOV que informa el controlador. Los símbolos combinados muestran sujetos de izquierda a derecha. Los puestos permanentes aparecen primero en negrita. Las posiciones temporales no tienen ícono y duran hasta que cierras la aplicación. Utilice Reproducir o haga doble clic para recuperar cualquiera de los tipos.

Una interpolación suave requiere coordenadas intermedias. Una fila es amarilla durante la transición. Es verde sólo cuando la lectura coincide con el objetivo. Una acción manual, una cancelación o una transición fallida borra ese estado. El controlador determina la granularidad y latencia del movimiento real.

Los perfiles contienen selección de cámara, posiciones permanentes, valores de imagen, velocidades de movimiento manual, duración o velocidad de transición preestablecida, inversión de dirección, confirmación de cámara, diseño de ventana e idioma. Al cambiar los perfiles, se reinicia la aplicación con el idioma del perfil seleccionado. La confirmación de cámara opcional bloquea la enumeración y la conexión hasta que el usuario confirma que la transmisión ha comenzado.

## Arquitectura

`CameraControlStudio` es una aplicación .NET Framework 4.8 WinForms. Diseño de bodega `Forms/*.Designer.cs`. Manejo de eventos de retención `Forms/*.cs`. `AppForm` es la base común. `Controls` contiene renderizado vectorial. `Camera.cs` utiliza la cámara Windows DirectShow y las interfaces de procesamiento de video sin un gráfico de captura.

`Shared` contiene modelos, persistencia, traducción, aritmética de movimiento, posiciones de sesión y lógica de estado. `Tests` es un conjunto de regresión de consola .NET 8. Los perfiles utilizan reemplazo de archivos atómicos, copias de seguridad y comprobaciones de revisión. Se conserva el almacenamiento del perfil 0.6 existente; Las configuraciones obsoletas de inicio del programa se ignoran y se descartan al guardar.

## Alcance y compatibilidad

El propietario informó que la aplicación anterior funcionó en su computadora portátil y en la configuración de la cámara. Esta reescritura 0.7 se ha compilado y pasado pruebas lógicas, pero no se ha aplicado al hardware físico en este entorno de compilación. No se ha probado ninguna cámara o software de captura adicional.

Otras cámaras de Windows pueden funcionar cuando sus controladores implementan los controles estándar necesarios. El acceso simultáneo con software de captura, reunión o transmisión depende del controlador. Las cámaras virtuales no pueden exponer controles físicos. Este no es un controlador PTZ de red genérico. Linux y macOS pueden crear hosts a través del flujo de trabajo de Docker, pero no pueden ejecutar esta interfaz de usuario de Windows de forma nativa.

Informe las combinaciones exitosas, incluida la versión de Windows, el modelo de la cámara, la versión del controlador, la aplicación de captura y qué controles funcionaron. También informe las limitaciones para que la lista de compatibilidad pueda basarse en evidencia.

## Posible trabajo futuro

Automatización de regresión de la interfaz de usuario de Windows; mejor accesibilidad y descubrimiento del teclado; importación/exportación opcional de perfiles individuales; matriz de hardware verificada; comunicados firmados después de la aprobación; backends de cámara adicionales si preservan el flujo de trabajo principal simple. Estas son ideas, no funciones entregadas.
