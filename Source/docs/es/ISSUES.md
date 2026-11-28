# Informar o seguir un problema

*Escrito para Camera Control Studio 1.1.1.*

[Solución de problemas](TROUBLESHOOTING.md)

## Buscar antes de informar

**Informar un error o sugerir una característica**\
<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue>\
Código ![QR para los problemas GitHub del proyecto](../images/qr/issues.png)

1. Abra la pestaña [Issues del proyecto](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue). Busque términos clave del síntoma o solicitud, como `is:issue Brio zoom` o `is:issue label:enhancement preset`.
2. Consultar resultados abiertos y cerrados. Lea informes similares para detectar síntomas y soluciones alternativas.
3. En caso de un problema de coincidencia, seleccione **Subscribe** en la barra lateral para recibir actualizaciones o reaccione con 👍 para demostrar que le afecta. Añade un comentario si puedes aportar detalles como una cámara diferente, el resultado de una prueba o una reproducción fiable.

![Emitir búsqueda encima de las pestañas Abierto y Cerrado. El nuevo problema está en la esquina superior derecha.](../images/reporting/github-issues.png)

## Abrir un informe

Abre el panel de configuración. Elige **Contacto y acerca de → Contacto**. Elige **Informar de un problema** o **Solicitar una función nueva** para abrir el borrador adecuado en GitHub. Para un fallo, la aplicación ofrece guardar un ZIP de soporte. Revísalo y adjúntalo tú mismo al borrador. [Informar de un problema](Report%20a%20bug.md) · [Solicitar una función nueva](Request%20a%20feature.md)

En caso de error, incluya la versión de Camera Control Studio, la versión de Windows, el modelo de cámara, la cámara o software de captura relevante y el orden en el que inició las aplicaciones. Describe los pasos exactos, lo que esperabas, qué sucedió y con qué frecuencia. Para una solicitud, describa la tarea que desea realizar y cómo utilizaría la función.

![Editor de incidencias de GitHub con título, descripción, carga de archivos y botón Crear](../images/reporting/github-new-issue.png)

Si **Type** está disponible, use **Bug** para un comportamiento inesperado, **Feature** para una solicitud o **Task** para un trabajo definido. Si puedes aplicar etiquetas, utiliza las más relevantes, como `bug`, `enhancement`, `documentation` o `accessibility`. Los mantenedores pueden agregar metadatos después del envío.

![Menú de tipo de problema de GitHub con tarea, error y característica](../images/reporting/github-issue-type.png)

![Selector de etiquetas GitHub](../images/reporting/github-issue-labels.png)

![Ejemplos de etiquetas y sus significados](../images/reporting/github-label-reference.png)

## Adjuntar evidencia

Para guardar el paquete, elige **Contacto y acerca de → Contacto → Guardar ZIP de soporte** y selecciona una ubicación. La aplicación muestra la ruta del archivo.

El ZIP incluye una captura de la aplicación, el archivo guardado `profiles.json`, la disposición de la ventana en `window.json` y los registros disponibles en `Logs`.

Revisa el ZIP para retirar datos privados, como nombres de perfiles, ID de cámaras y rutas. Arrastra tú mismo el ZIP o los archivos pertinentes a la descripción del borrador.

Revisa la descripción y los archivos en **Preview**. Después elige **Create**.

[Referencia de búsqueda de problemas de GitHub](https://docs.github.com/en/search-github/searching-on-github/searching-issues-and-pull-requests)
