# Construir e lançar Camera Control Studio

*Escrito para Camera Control Studio 1.1.0.*

Execute os comandos do diretório que contém `Camera-Control-Pressets-Studio.sln`. Use Windows e PowerShell para o instalador e testes do Windows.

## 1. Prepare o lançamento

1. Comece a partir do commit selecionado para o lançamento. Feche qualquer instância Camera Control Studio em execução antes de compilar ou testar uma atualização.
2. Instale o SDK do .NET 8 selecionado por `global.json`. Instale Inno Setup 6 ou 7 se planeja publicar o instalador. Instale o SDK do Windows se você planeja assinar arquivos com SignTool ou criar um MSIX com MakeAppx.
3. Verifique os pré-requisitos com `./scripts/setup-dev.ps1 -CheckOnly`. Para uma nova estação de trabalho, `./scripts/setup-dev.ps1` restaura os pacotes fixados e executa a compilação portátil.
4. Defina a versão de lançamento em `Directory.Build.props` e atualize `CHANGELOG.md`. O script do instalador lê a versão padrão das propriedades de compilação. Mantenha a versão Store de quatro partes separada ao preparar um MSIX.

## 2. Construa os pacotes GitHub

Escolha um comando. A compilação do instalador inclui a compilação portátil.

| O que construir | Comando | Saída |
| --- | --- | --- |
| ZIP portátil | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Instalador portátil ZIP e Inno | `./scripts/build-installer.ps1` | ZIP portátil mais `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` verifica traduções, cria Release com dependências bloqueadas, executa testes e cria o ZIP. Encontre o executável em `CameraControlStudio/bin/Release/net48` e os arquivos preparados em `artifacts/portable/Camera-Control-Studio`. Publique o ZIP completo. O aplicativo precisa de configuração, bibliotecas e guias.

Para usar um certificado de assinatura de código individual ou organizacional disponível no armazenamento de certificados do Windows do usuário atual, passe sua impressão digital e o caminho para SignTool ao compilar. O comando do instalador assina o aplicativo antes de empacotá-lo e assina o instalador concluído com um carimbo de data/hora.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

Somente para ZIP, forneça os mesmos parâmetros para `build.ps1`. Para assinatura patrocinada, siga o fluxo de trabalho [SignPath aprovado](SIGNPATH.md). Marque claramente os downloads não assinados.

## 3. Verifique os pacotes no Windows

1. Extraia o ZIP para um novo diretório. Inicie o aplicativo a partir daí e abra **-** com Configurações fechadas e abertas. Com [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) instalado, verifique a navegação do guia e imprima em PDF para verificar o título, data e hora de impressão, versão instalada, endereço do projeto e números de página. Verifique a versão do aplicativo, comportamento de inicialização, predefinições, controles de câmera e seleção de idioma ao trocar de perfil.
2. Se você planeja publicar o instalador, instale-o em uma conta de teste limpa do Windows. Verifique o aplicativo do menu Iniciar e os atalhos do guia do usuário. Verifique se todos os 12 guias estão presentes, se uma atualização mantém os perfis existentes e se a desinstalação foi concluída.
3. Trabalhe na lista de verificação de lançamento [](RELEASE-CHECKLIST.md). Para compilações assinadas, execute a verificação SignTool no executável e no instalador do aplicativo. Gere hashes somente após a conclusão de qualquer assinatura e reempacotamento.

## 4. Preparar e fazer hash dos downloads GitHub

Substitua a versão neste exemplo nas versões subsequentes. Omita as linhas do instalador para uma versão somente ZIP.

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

A pasta versionada contém o ZIP final, o instalador e o manifesto SHA-256. O `bin` e os diretórios temporários portáteis são entradas de construção para esta etapa.

## 5. Publique em GitHub

1. Atualize o subtítulo da versão em cada guia revisado para esta versão. Mantenha suas traduções sincronizadas. Confirme a versão de lançamento, changelog, guias e alterações de origem. Em [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases), crie um rascunho e use uma tag como `v1.0.0` no commit testado.
2. Copie a entrada correspondente do changelog nas notas de lançamento, indique se os downloads estão assinados e selecione a opção de pré-lançamento apenas para compilações de visualização.
3. Anexe o ZIP com versão, o instalador, se integrado, e o `SHA256SUMS.txt` do `artifacts/releases/<version>/`. Revise os nomes dos arquivos, baixe e verifique os arquivos finalizados e publique.

## Caminhos de construção opcionais

| Objetivo | Roteiro | Saída |
| --- | --- | --- |
| Capturas de tela da documentação com dados de amostra | `./scripts/capture-docs.ps1` | PNGs em `docs/images/screenshots/<version>/<language>/` para todos os 12 guias de usuário |
| Construção do Docker e testes lógicos | `./scripts/build-docker.ps1` | ZIP portátil em `artifacts/docker/` |
| Candidato a envio da Microsoft Store | `./scripts/build-msix.ps1` com sua identidade Partner Center, editor e versão do pacote | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

A construção da Loja requer a identidade Partner Center real e segue o [MSIX separado e a lista de verificação da Loja](MSIX-AND-STORE.md). A saída é um candidato a envio não assinado. `Install.ps1` é o instalador opcional por usuário dentro de um ZIP portátil extraído.
