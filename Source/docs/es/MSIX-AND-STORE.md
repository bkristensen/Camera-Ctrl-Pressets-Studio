# MSIX y distribución en tienda

*Escrito para Camera Control Studio 1.1.1.*

Utilice `scripts/build-msix.ps1` y la plantilla de manifiesto para crear un paquete MSIX. Microsoft revisa el paquete antes de que pueda aparecer en la Tienda. El ejecutable portátil no está firmado.

## Elige una ruta

| Ruta | firma |
| --- | --- |
| Tienda Microsoft con MSIX | Microsoft firma el paquete de la Tienda aceptado. No compra un certificado de firma para esta ruta. |
| Microsoft Store con un instalador EXE/MSI | Se requiere la firma del editor; la Tienda no vuelve a firmar ese instalador. |
| Descarga directa MSIX | El paquete debe tener una firma confiable en el dispositivo del usuario. |
| EXE portátil directo | Una compilación sin firmar puede generar advertencias. La firma identifica al editor pero no promete reputación inmediata. |

## Lista de verificación

- [ ] Registre el producto y reserve su nombre en Partner Center. Copie el nombre exacto del paquete, el editor y el nombre de visualización del editor.
- [ ] Instale las herramientas del SDK de Windows y ejecute el siguiente comando utilizando esos valores de identidad.
- [ ] Valide el manifiesto y pruebe el acceso a la cámara, la instalación, la actualización, la desinstalación, el almacenamiento del perfil y el idioma en Windows. La plantilla declara funcionamiento de escritorio/plena confianza y acceso a la cámara web. Verifique que coincidan con los requisitos de certificación actuales.
- [ ] Prepare el listado, la política de privacidad, el enlace de soporte, la información de la licencia, la clasificación por edades y las capturas de pantalla reales. Explique los controles requeridos del controlador de cámara. Ejecute las comprobaciones de certificación de aplicaciones de Windows actuales.
- [ ] Cargue el candidato a Partner Center, solucione los problemas de validación, envíelo y espere la certificación. No describa la aplicación como aprobada por la Tienda antes de su aprobación.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

El resultado es un candidato de envío a la tienda sin firmar, no un paquete de descarga pública listo para instalar. El script valida los campos de identidad requeridos y utiliza una compilación x64 explícita. Elija una nueva versión del paquete de cuatro partes para cada versión, con la parte final cero para el envío a la Tienda.

Referencias oficiales, consultadas el 18 de noviembre de 2026:

- [Opciones de firma de código de Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Lista de verificación de preparación para la tienda](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX guía de firma](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Véase también [SignPath](SIGNPATH.md). Las reglas actuales de elegibilidad y presentación deben verificarse nuevamente antes de su publicación.
