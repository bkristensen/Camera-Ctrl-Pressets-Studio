# Política de firma de código

*Escrito para Camera Control Studio 1.1.0.*

La Fundación SignPath ofrece firma gratuita para proyectos de código abierto elegibles. Se requiere aceptación. El certificado identifica a la Fundación SignPath como editor; el servicio vincula las compilaciones verificadas al repositorio de origen. El proyecto ha solicitado la firma de la Fundación SignPath. La solicitud está pendiente y las descargas de GitHub permanecen sin firmar.

Antes de presentar la solicitud, publique la fuente bajo su licencia. Mantenga el proyecto actualizado. Publique una versión, documente la aplicación y haga que la compilación sea verificable. Firme sólo sus propios archivos de proyecto. Mantenga las credenciales de firma fuera del repositorio.

La política de privacidad [](../../PRIVACY.md) cubre la aplicación, su verificación de actualización GitHub y el tiempo de ejecución WebView2 de GuideViewer.

## Mientras la solicitud esté pendiente

- Verifique que el repositorio público tenga una compilación de Windows exitosa en **Actions**. Utilice ese flujo de trabajo como base de la integración SignPath.
- Confirme las personas responsables de los compromisos, revisiones y firmas de aprobaciones. Active la autenticación multifactor para todos los miembros del equipo en GitHub y SignPath.
- Pídale a SignPath que confirme cómo se deben manejar los binarios del SDK de WebView2 y el instalador de Inno Setup. Los paquetes incluyen los archivos binarios y cargadores del SDK de Microsoft. La aplicación utiliza el tiempo de ejecución WebView2 instalado en la PC del usuario. Enumere estas dependencias en la aplicación y conserve sus avisos.
- Agregue un enlace de la política de firma de código **** a las notas de la versión GitHub para la página de descarga proporcionada en la aplicación. Indique el estado de firma real de esas descargas.

## Después de que SignPath acepte el proyecto.

- Agregue la atribución SignPath aprobada y los nombres confirmados o grupos de permisos para confirmadores, revisores y aprobadores a esta página.
- Configure SignPath para aceptar artefactos de la compilación GitHub Actions confiable. Restringir la firma a los archivos binarios del proyecto. Establezca y aplique el nombre del producto y la versión coincidente del producto en todos los archivos enviados juntos.
- Para ZIP, firme el ejecutable de la aplicación antes de empaquetarlo. Para el instalador, coloque el ejecutable de la aplicación firmada en el instalador antes de firmar el instalador terminado. Obtenga aprobación manual para cada solicitud de firma de versión.
- Verifique las firmas y marcas de tiempo de Authenticode, luego genere sumas de verificación SHA-256. Incluya un enlace de la política de firma de código **** y el estado de la firma en las notas de la versión. Verifique los archivos y hashes cargados antes de publicar.

Para descargas directas de GitHub, una firma válida identifica al editor y protege la integridad del binario. Una nueva descarga aún puede activar SmartScreen mientras se construye la reputación. Para la distribución de primera ejecución a través de la Tienda, Microsoft firma paquetes MSIX aceptados.

Para obtener un certificado de firma de código de confianza pública disponible a través del almacén de certificados de Windows del usuario actual, utilice `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` para el ZIP portátil. Utilice los mismos parámetros con `scripts/build-installer.ps1` para firmar tanto el ejecutable de la aplicación como el instalador generado. La aprobación de SignPath requiere su propio flujo de trabajo de firma.

Lea los términos oficiales de [, ](https://signpath.org/terms.html), el sitio web de la Fundación [, ](https://signpath.org/) y la guía de pantalla inteligente [ de Microsoft, ](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Consultado el 24 de noviembre de 2026. Para la ruta Tienda, consulte [MSIX y Store](MSIX-AND-STORE.md).
