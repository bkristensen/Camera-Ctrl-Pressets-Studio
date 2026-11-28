# Compilación y lanzamiento de Camera Control Studio

*Escrito para Camera Control Studio 1.1.1.*

Ejecute los comandos desde el directorio que contiene `Camera-Control-Pressets-Studio.sln`. Utilice Windows y PowerShell para el instalador y las pruebas de Windows.

## 1. Preparar el lanzamiento

1. Comience desde la confirmación seleccionada para el lanzamiento. Cierre cualquier instancia de Camera Control Studio en ejecución antes de compilar o probar una actualización.
2. Instale el SDK de .NET 8 seleccionado por `global.json`. Instale Inno Setup 6 o 7 si planea publicar el instalador. Instale el SDK de Windows si planea firmar archivos con SignTool o crear un MSIX con MakeAppx.
3. Verifique los requisitos previos con `./scripts/setup-dev.ps1 -CheckOnly`. Para una nueva estación de trabajo, `./scripts/setup-dev.ps1` restaura los paquetes anclados y ejecuta la compilación portátil.
4. Configure la versión de lanzamiento en `Directory.Build.props` y actualice `CHANGELOG.md`. El script del instalador lee la versión predeterminada de las propiedades de compilación. Mantenga separada la versión Store de cuatro partes cuando prepare un MSIX.

## 2. Construya los paquetes GitHub

Elija un comando. La versión del instalador incluye la versión portátil.

| que construir | Comando | Salida |
| --- | --- | --- |
| Portátil ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Instalador portátil ZIP e Inno | `./scripts/build-installer.ps1` | Portátil ZIP más `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` verifica las traducciones, crea una versión con dependencias bloqueadas, ejecuta pruebas y crea el ZIP. Busque el ejecutable en `CameraControlStudio/bin/Release/net48` y los archivos preparados en `artifacts/portable/Camera-Control-Studio`. Publicar el ZIP completo. La aplicación necesita su configuración, bibliotecas y guías.

Para utilizar un certificado de firma de código individual u organizacional disponible en el almacén de certificados de Windows del usuario actual, pase su huella digital y la ruta a SignTool al compilar. El comando del instalador firma la aplicación antes de empaquetarla y firma el instalador completo con una marca de tiempo.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

Solo para el ZIP, proporcione los mismos parámetros al `build.ps1`. Para la firma patrocinada, siga el flujo de trabajo aprobado [SignPath](SIGNPATH.md). Marque claramente las descargas sin firmar.

## 3. Verifique los paquetes en Windows

1. Extraiga el ZIP a un nuevo directorio. Inicie la aplicación desde allí y abra **?** con la Configuración cerrada y abierta. Con [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) instalado, verifique la navegación de la guía e imprima en PDF para verificar el título, la fecha y hora de impresión, la versión instalada, la dirección del proyecto y los números de página. Verifique la versión de la aplicación, el comportamiento de inicio, los ajustes preestablecidos, los controles de la cámara y la selección de idioma al cambiar de perfil.
2. Si planea publicar el instalador, instálelo en una cuenta de prueba limpia de Windows. Verifique la aplicación del menú Inicio y los accesos directos de la guía del usuario. Verifique que las 12 guías estén presentes, que una actualización conserve los perfiles existentes y que se complete la desinstalación.
3. Trabaje con la lista de verificación de lanzamiento de [](RELEASE-CHECKLIST.md). Para compilaciones firmadas, ejecute la verificación SignTool en el ejecutable y el instalador de la aplicación. Genere hashes solo después de finalizar la firma y el reempaquetado.

## 4. Prepare y haga hash de las descargas de GitHub

Reemplace la versión en este ejemplo para versiones posteriores. Omita las líneas del instalador para una versión exclusiva de ZIP.

```powershell
$version = '1.0.0'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

La carpeta versionada contiene el ZIP final, el instalador y el manifiesto SHA-256. El `bin` y los directorios de preparación portátiles son entradas de compilación para este paso.

## 5. Publicar en GitHub

1. Actualice el subtítulo de la versión en cada guía que revise para esta versión. Mantenga sus traducciones sincronizadas. Confirme la versión de lanzamiento, el registro de cambios, las guías y los cambios de origen. En [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases), cree un borrador y use una etiqueta como `v1.0.0` en la confirmación probada.
2. Copie la entrada del registro de cambios coincidente en las notas de la versión, indique si las descargas están firmadas y seleccione la opción de versión preliminar solo para versiones preliminares.
3. Adjunte el ZIP versionado, el instalador si está integrado y el `SHA256SUMS.txt` de `artifacts/releases/<version>/`. Revise los nombres de los archivos, descargue y verifique los archivos terminados y luego publíquelos.

## Rutas de construcción opcionales

| Propósito | Guión | Salida |
| --- | --- | --- |
| Capturas de pantalla de la documentación con datos de muestra. | `./scripts/capture-docs.ps1` | PNG en `docs/images/screenshots/<version>/<language>/` para las 12 guías de usuario |
| Pruebas lógicas y de compilación de Docker | `./scripts/build-docker.ps1` | ZIP portátil en `artifacts/docker/` |
| Candidato de presentación de Microsoft Store | `./scripts/build-msix.ps1` con su identidad Partner Center, editor y versión del paquete | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

La compilación de la Tienda requiere la identidad Partner Center real y sigue la lista de verificación de la Tienda [MSIX y ](MSIX-AND-STORE.md) por separado. El resultado es un candidato de envío sin firmar. `Install.ps1` es el instalador opcional por usuario dentro de un ZIP portátil extraído.
