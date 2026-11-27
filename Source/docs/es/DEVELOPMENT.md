[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Editar la interfaz y las traducciones.

*Escrito para Camera Control Studio 1.0.1.*

## Abra el archivo correcto

Abra `Camera-Control-Pressets-Studio.sln`. En el Explorador de soluciones, cree la solución y luego abra `CameraControlStudio → Forms → MainForm.cs`. Presione **Shift+F7** para el Windows Forms Designer. No seleccione `MainForm.Designer.cs` como superficie de diseño. Utilice F7 para volver al código.

Instale la carga de trabajo de escritorio Visual Studio .NET desde `.vsconfig`. La aplicación es WinForms en .NET Framework 4.8. No se utiliza el diseñador WPF.

## Cambiar diseño

Utilice el panel Propiedades para cambiar `Dock`, `Margin`, `Padding`, tamaños de fila y tamaño de columna. Utilice el esquema del documento para seleccionar paneles anidados. El diseño principal utiliza tablas y un `SplitContainer`; Evite las coordenadas de pantalla codificadas.

Cada ventana hereda de `AppForm` y tiene un constructor público sin parámetros. Coloque el diseño en `.Designer.cs` y los eventos en el archivo `.cs` correspondiente. Establezca la propiedad `Icon` del formulario en el Diseñador. Verifique el diseño en Visual Studio y en la aplicación en ejecución.

La rueda de la cámara y los botones azules están dibujados en `Controls/BlueControls.cs`. El divisor vertical y los controles de zoom están en `Controls/MainViewControls.cs`. El diseñador puede mover y dimensionar estos controles. Las imágenes de íconos preestablecidos y sus títulos neutrales están en `Assets/PressetIcons.resx` y `Controls/PresetIcons.cs` los carga. La pestaña Vídeo contiene controles deslizantes creados por diseñadores. La aplicación en ejecución muestra los controles deslizantes compatibles con el controlador de la cámara.

## Establecer valores predeterminados

Edite `Shared/Models.cs`, en `SavedData.Defaults`, para configurar el nuevo perfil. Edite `ProfileLayout` para el diseño inicial. Los perfiles existentes conservan sus valores guardados. Cree un nuevo perfil para probar nuevos valores predeterminados; No elimine perfiles de usuario para forzarlos.

## Actualizar una traducción

Edite el valor coincidente en el archivo de idioma correspondiente en `Shared/Locales`. Mantenga el `key` sin cambios. XML requiere `&amp;` para un signo comercial. Todos los archivos deben tener las mismas claves. Mantenga sin cambios los ID de cámara e íconos almacenados.

Ejecute `scripts/check-translations.ps1` y luego construya. Prueba con `"Camera Control Studio.exe" --lang=de` o el selector de idioma en ajustes. Reinicie después de cambiar la opción de idioma guardada.

## Agregar un idioma

Copie `en.xml` a un nuevo archivo de código de idioma. Traduce cada valor y registra el código y el nombre en `Shared/UiText.cs`. El verificador de traducción y las pruebas locales seleccionan el nuevo archivo y lo registran automáticamente. Declare el idioma de la aplicación en el manifiesto MSIX.

La selección automática utiliza el prefijo de idioma de Windows. Los idiomas no admitidos vuelven al inglés. Pruebe ventanas estrechas y etiquetas largas; La traducción no debe ocultar controles.
