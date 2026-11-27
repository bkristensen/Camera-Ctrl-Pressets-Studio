# Traducir Camera Control Studio

*Escrito para Camera Control Studio 1.1.0.*

## Editar un idioma existente

1. Abra `Source/Shared/Locales/<code>.xml`. Utilice `en.xml` como referencia para contexto y claves.
2. Edite el texto entre las etiquetas `<text>`. Mantenga intactas las claves, los marcadores de posición como `{0}` y los ID almacenados. Guardar como UTF-8. En XML, escriba un signo comercial literal como `&amp;`.
3. Si también cambia la ayuda, edite la carpeta correspondiente en `Source/docs`. Pruebe enlaces a otras guías e imágenes en Guide Viewer.

## Agregar un idioma

1. Copie `en.xml` a un archivo con el nombre del nuevo código de idioma y traduzca todos los valores.
2. Registre el código y el nombre para mostrar en el mismo índice en `LanguageCodes` y `LanguageNames` en `Source/Shared/UiText.cs`. Actualice el manejo de derecha a izquierda cuando corresponda.
3. Agregue el idioma de la aplicación al manifiesto MSIX. Si agrega una guía de usuario, siga la estructura de una carpeta existente en `Source/docs`, agréguela a `USER-GUIDES.md` y verifique que Guide Viewer la abra.

## Verificar y enviar

Desde `Source`, ejecute `powershell -File scripts/check-translations.ps1` y compile. Inicie la aplicación con `--lang=<code>`. Verifique los cuadros de diálogo en el tamaño de ventana más pequeño y navegue con el teclado. Pruebe etiquetas largas, dirección del texto y mensajes que completen marcadores de posición.

Siga la guía del proyecto [GitHub](GITHUB.md) para abrir una solicitud de extracción. Nombre el idioma y el código, resuma los cambios e indique lo que probó. Incluya una captura de pantalla si ayuda a los revisores a evaluar el diseño. Agregue ediciones de seguimiento a la misma rama durante la revisión.
