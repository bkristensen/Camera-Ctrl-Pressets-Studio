# 构建并发布Camera Control Studio

*为Camera Control Studio 1.0.1.*编写

从包含 `Camera-Control-Pressets-Studio.sln` 的目录运行命令。使用 Windows 和 PowerShell 进行安装程序和 Windows 测试。

## 1. 准备发布

1. 从为发布选择的提交开始。在编译或测试升级之前关闭任何正在运行的 Camera Control Studio 实例。
2. 安装`global.json`选择的.NET 8 SDK。如果您计划发布安装程序，请安装 Inno Setup 6 或 7。如果您计划使用 SignTool 签署文件或使用 MakeAppx 构建 MSIX，请安装 Windows SDK。
3. 使用 `./scripts/setup-dev.ps1 -CheckOnly` 检查先决条件。对于新工作站，`./scripts/setup-dev.ps1` 恢复固定包并运行便携式构建。
4. 在`Directory.Build.props`中设置发布版本并更新`CHANGELOG.md`。安装程序脚本从构建属性中读取默认版本。准备 MSIX 时，将由四部分组成的商店版本分开。

## 2. 构建GitHub包

选择一个命令。安装程序版本包括便携式版本。

| 建造什么 | 命令 | 输出 |
| --- | --- | --- |
| 便携式ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| 便携式 ZIP 和 Inno 安装程序 | `./scripts/build-installer.ps1` | 便携式 ZIP 加 `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` 检查翻译，构建具有锁定依赖项的版本，运行测试并创建 ZIP。在 `CameraControlStudio/bin/Release/net48` 中找到可执行文件，在 `artifacts/portable/Camera-Control-Studio` 中找到暂存文件。发布整个ZIP。该应用程序需要其配置、库和指南。

要使用当前用户的 Windows 证书存储中提供的个人或组织代码签名证书，请在构建时将其指纹和路径传递给 SignTool。安装程序命令在打包应用程序之前对应用程序进行签名，并使用时间戳对已完成的安装程序进行签名。

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

对于单独的 ZIP，给 `build.ps1` 提供相同的参数。对于赞助签名，请遵循批准的 [SignPath 工作流程](SIGNPATH.md)。清楚地标记未签名的下载。

## 3.检查Windows上的软件包

1. 将 ZIP 解压到新目录。从那里启动应用程序并打开 **?**，关闭并打开“设置”。安装 [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) 后，验证指南导航并打印到 PDF 以检查标题、打印日期和时间、安装版本、项目地址和页码。切换配置文件时验证应用程序版本、启动行为、预设、相机控制和语言选择。
2. 如果您计划发布安装程序，请将其安装在干净的 Windows 测试帐户上。验证“开始”菜单应用程序和用户指南快捷方式。检查所有 12 个指南是否都存在、升级是否保留现有配置文件以及卸载是否完成。
3. 完成 [ 发布清单 ](RELEASE-CHECKLIST.md)。对于签名版本，请对应用程序可执行文件和安装程序运行 SignTool 验证。仅在完成任何签名和重新打包后才生成哈希值。

## 4. 对 GitHub 下载进行暂存和哈希处理

替换本示例中的版本以供后续版本使用。对于仅 ZIP 的版本，省略安装程序行。

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

版本化文件夹包含最终的 ZIP、安装程序和 SHA-256 清单。 `bin` 和便携式暂存目录是此步骤的构建输入。

## 5. 发布到GitHub

1. 更新您为此版本查看的每份指南中的版本副标题。保持翻译同步。提交发布版本、变更日志、指南和源代码更改。在 [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases) 中，创建草稿并在测试的提交上使用 `v1.0.0` 等标签。
2. 将匹配的变更日志条目复制到发行说明中，说明下载是否已签名，并仅针对预览版本选择预发布选项。
3. 附上版本化的 ZIP、安装程序（如果已构建）以及 `artifacts/releases/<version>/` 中的 `SHA256SUMS.txt`。检查文件名，下载并验证完成的文件，然后发布。

## 可选的构建路径

| 目的 | 脚本 | 输出 |
| --- | --- | --- |
| 带有示例数据的文档屏幕截图 | `./scripts/capture-docs.ps1` | 所有 12 份用户指南的 PNG 格式为 `docs/images/screenshots/<version>/<language>/` |
| Docker 构建和逻辑测试 | `./scripts/build-docker.ps1` | `artifacts/docker/`中的便携式ZIP |
| Microsoft Store 提交候选者 | `./scripts/build-msix.ps1` 以及您的 Partner Center 身份、发布者和软件包版本 | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

商店构建需要真实的 Partner Center 身份，并遵循单独的 [MSIX 和商店清单 ](MSIX-AND-STORE.md)。输出是未签名的提交候选。 `Install.ps1` 是提取的便携式 ZIP 内的可选每用户安装程序。
