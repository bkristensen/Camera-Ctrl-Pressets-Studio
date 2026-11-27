# Lista de verificación de liberación

*Escrito para Camera Control Studio 1.1.0.*

Utilice la guía de compilación y lanzamiento [](BUILD-AND-RELEASE.md) para comandos y rutas de salida.

## Controles automatizados

- [ ] Ejecute `scripts/build.ps1` en una caja limpia con dependencias bloqueadas.
- [ ] Confirme que cada configuración regional tenga las mismas claves únicas y no vacías. La compilación ejecuta la verificación de traducción y las pruebas locales.
- [ ] Revise la versión fuente y binaria, la identidad del paquete y las notas de la versión.

## Comprobaciones de Windows y cámara.

- [ ] Inicie a 900 × 500 y verifique el mínimo de 670 × 450. Cambie el tamaño, maximice y restaure al 100%, 125%, 150% y 200% de escala de visualización. Verifique el pie de página, las entradas, los íconos combinados y los glifos de zoom.
- [ ] Abra la configuración a la derecha. Arrastra el divisor. Cerrar y volver a abrir. Pruebe un diseño guardado después de desconectar un monitor.
- [ ] Recuperar una posición: amarillo durante el movimiento, verde solo después de coincidir con la lectura. Pan/tilt/zoom borra el color activo. Desconectar durante una transición: no hay falso verde. Los iconos nunca desaparecen después de la confirmación.
- [ ] Pruebe cada ícono combinado, incluido cada orden inverso. Pruebe los botones de zoom, el control deslizante de zoom y las inversiones de ambas direcciones.
- [ ] En un Brio compatible, pruebe el FOV automático en ambas direcciones del zoom y en los bordes de panorámica izquierdo y derecho. Siga los pasos de prueba [Brio](VALIDATION.md#campo-de-visi%C3%B3n-brio-y-zoom).
- [ ] Habilite la confirmación de la cámara. Reanudar. Verifique que no haya ninguna enumeración o conexión de cámaras antes de Sí. Pruebe No, luego vuelva a intentarlo desde un botón de panorámica/zoom. Prueba de cambio de perfil y selección de fuente.

## Datos y edición

- [ ] Haga una copia de seguridad del archivo de perfil 0.6 antes de la migración. Confirme que los ajustes preestablecidos y los ID de iconos sobrevivan. Confirme que no haya configuraciones obsoletas del programa externo después de guardarlas.
- [ ] Pruebe perfiles nuevos, copie o cambie el nombre, restaure el diseño y mantenga el comportamiento del diseño actual. Verifique que los puestos temporales sigan siendo solo de sesión.
- [ ] Pruebe la configuración automática/manual de la imagen y los valores predeterminados con la cámara real.
- [ ] Abra cada formulario con Shift+F7 en Visual Studio. Ajuste una propiedad de diseño, guárdela y reconstruya. El dibujo personalizado permanece en las clases de control.
- [ ] Abra el visor de guías del ZIP extraído en un proceso de Windows de 64 bits. Ábralo nuevamente e imprima una guía para PDF. Confirme que el instalador y ZIP incluyan el cargador nativo WebView2 correspondiente en `runtimes` para cada arquitectura compatible.
- [ ] Consulte el índice de la guía del usuario [](USER-GUIDES.md) y confirme que todas las guías de idiomas estén presentes en ambos paquetes. Consulte las capturas de pantalla de la guía [](SCREENSHOTS.md) para ver la cobertura, el recorte y la superposición completos de pestañas y subpestañas.

## Distribución

- [ ] Para una versión portátil, utilice `scripts/build.ps1`. Para un instalador Inno Setup, utilice `scripts/build-installer.ps1`, que también ejecuta la compilación portátil y las pruebas. Verifique la versión que muestra el instalador y la aplicación.
- [ ] Para una versión de la Tienda, siga la lista de verificación de la Tienda [](MSIX-AND-STORE.md). Pruebe los tipos de paquetes que planea publicar en Windows.
- [ ] Si firma, verifique las firmas de Authenticode en el ejecutable de la aplicación y en el instalador, cuando corresponda. Etiquete claramente las descargas sin firmar. Genere hashes SHA-256 después de la firma y el empaquetado finales.
- [ ] Enlace al [Política de firma de código](SIGNPATH.md) en el GitHub notas de lanzamiento. Para SignPath lanzamientos, verificar la procedencia de la construcción y la aprobación y el crédito manuales SignPath para los archivos que firmó.
- [ ] Publique fuente, licencia, notas de versión, hashes y binarios juntos. No reclame pruebas de hardware o aprobación de la tienda no realizadas.
