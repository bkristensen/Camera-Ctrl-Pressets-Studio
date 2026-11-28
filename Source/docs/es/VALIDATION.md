# Validación para 0.7.0-beta.3

*Escrito para Camera Control Studio 1.1.1.*

La compilación de Windows Release utiliza dependencias bloqueadas. Se pasaron la paridad de traducción y las pruebas automatizadas. Las pruebas cubren movimiento, ajustes preestablecidos, idioma del perfil, persistencia, registro y el HRESULT de cámara no lista informado después de la hibernación.

El ZIP portátil extraído se probó en procesos de Windows x86 y x64. Ambos abrieron y volvieron a abrir las guías de configuración y uso diario, mostraron sus capturas de pantalla y las imprimieron en PDF. El x64 PDF tenía el título, la versión, la fecha y el recuento de páginas correcto en las cinco páginas. El botón **?** estuvo presente durante la inicialización en tiempo de diseño de WinForms. Se verificó que el cargador ARM64 tuviera la arquitectura binaria correcta.

El perfil de English Standard fue capturado con una cámara de demostración. Las 12 capturas de pantalla de ventana completa cubren ambos estados de la ventana principal, cada pestaña de configuración y cada subpestaña. Los enlaces locales en las guías y archivos README se compararon con las rutas de los documentos empaquetados.

## Campo de visión Brio y Zoom

Utilice un Brio físico que ofrezca 65°, 78° y 90°. Tenga en cuenta el modelo, el firmware, el controlador, las versiones de la aplicación y el modo de vídeo que utiliza Zoom.

1. Inicie Zoom y use una escena fija con puntos de referencia visibles cerca de los bordes. Confirme el encuadre más amplio a 90° y el zoom mínimo con Logitech Brio Control.
2. Inicie Camera Control Studio con **No cambiar** y confirme que el dispositivo conserva su FOV. Reinicie con **Más amplio disponible**, luego repita con un FOV especificado y con la confirmación de transmisión habilitada.
3. Con las tres aplicaciones en ejecución, cambie la panorámica, la inclinación, el zoom y FOV en Logitech Brio Control. Confirme las lecturas actualizadas en Camera Control Studio con el manual FOV deshabilitado y habilitado.
4. Cambie FOV en Camera Control Studio. Observe cuando Logitech Brio Control actualiza su propia pantalla. Guarde y recupere ajustes preestablecidos para cada FOV, incluido un ajuste preestablecido anterior sin FOV.
5. Desde una toma estrecha ampliada, elija **Encuadre completo** y compare la imagen con la referencia del paso 1. Repita durante una transición preestablecida y con Logitech Brio Control ya abierto.
6. Cierre Camera Control Studio y compruebe si Logitech Brio Control aún puede cambiar la cámara. Repita en el orden inverso de inicio de la aplicación. Adjunte el registro de diagnóstico y observe cualquier discrepancia entre la imagen en vivo y los valores informados.
7. A 65° y zoom mínimo, presione Alejar una vez. Verifique que FOV cambie a 78° y que el siguiente paso se aleje. Repetir a 78°. Verifique que el alejamiento se detenga solo en 90° y el zoom mínimo. Pruebe la ruta inversa para acercar.
8. En un borde horizontal de la bandeja, presione la misma flecha nuevamente. Compruebe si una FOV más amplia y un ajuste de zoom permiten el movimiento sin perder la toma. Si ninguna vista ayuda, verifique que regrese el encuadre original. Apague la función y confirme que el FOV permanezca en su lugar.

Inno Setup 7 compiló el instalador con las guías para los 12 idiomas y los cargadores x86, x64 y ARM64. Ambos paquetes no están firmados y se suministran hashes SHA-256. Están pendientes una instalación y desinstalación limpia de la cuenta, un ciclo de hibernación de la cámara física y una prueba de tiempo de ejecución ARM64. Consulte la lista de verificación de lanzamiento de [, la guía de compilación y lanzamiento de ](RELEASE-CHECKLIST.md) y [](BUILD-AND-RELEASE.md).
