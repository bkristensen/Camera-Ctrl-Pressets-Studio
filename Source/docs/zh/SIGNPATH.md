# 代码签名政策

*为Camera Control Studio 1.1.1.*编写

SignPath 基金会为符合条件的开源项目提供免费签名。需要接受。该证书标识SignPath基金会为发布者；该服务将经过验证的版本链接到源存储库。该项目已申请SignPath基金会签约。该申请正在等待处理，GitHub 下载仍未签名。

在申请之前，请根据其许可证发布源。保持项目最新。发布版本，记录应用程序并使构建可验证。仅签署您自己的项目文件。保持在存储库之外签署凭据。

[隐私政策](../../PRIVACY.md)涵盖该应用程序、其GitHub更新检查和GuideViewer的WebView2运行时。

## 当申请正在等待处理时

- 检查公共存储库在 **Actions** 下是否已成功构建 Windows。使用该工作流程作为 SignPath 集成的基础。
- 确认负责提交、审查和签署批准的人员。在 GitHub 和 SignPath 上为所有团队成员开启多重身份验证。
- 请求 SignPath 确认应如何处理 WebView2 SDK 二进制文件和 Inno Setup 安装程序。这些软件包包括 Microsoft 的 SDK 二进制文件和加载程序。该应用程序使用安装在用户 PC 上的 WebView2 运行时。在应用程序中列出这些依赖项并保留它们的通知。
- 将 ** 代码签名策略 ** 链接添加到应用程序中提供的下载页面的 GitHub 发行说明。说明这些下载的实际签名状态。

## SignPath验收项目后

- 将批准的 SignPath 属性以及提交者、审阅者和批准者的确认名称或权限组添加到此页面。
- 配置 SignPath 以接受来自可信 GitHub Actions 构建的工件。限制对项目二进制文件的签名。在一起提交的文件中设置并强制执行产品名称和匹配的产品版本。
- 对于 ZIP，在打包之前对应用程序可执行文件进行签名。对于安装程序，在对完成的安装程序进行签名之前，将签名的应用程序可执行文件放入安装程序中。获得每个发布签名请求的手动批准。
- 验证 Authenticode 签名和时间戳，然后生成 SHA-256 校验和。在发行说明中包含 ** 代码签名策略 ** 链接和签名状态。在发布之前验证上传的文件和哈希值。

对于直接 GitHub 下载，有效签名可识别发布者并保护二进制文件的完整性。在建立声誉的同时，新的下载仍然可以触发 SmartScreen。对于通过应用商店的首次运行分发，Microsoft 签署了接受的 MSIX 软件包。

对于可通过当前用户的 Windows 证书存储区获得的公开信任的代码签名证书，请使用 `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` 作为便携式 ZIP。使用与 `scripts/build-installer.ps1` 相同的参数对应用程序可执行文件和生成的安装程序进行签名。 SignPath 批准需要自己的签名工作流程。

阅读[官方条款](https://signpath.org/terms.html)、[基金会网站](https://signpath.org/)和Microsoft的[SmartScreen指南](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation)。检查日期：2026 年 11 月 24 日。有关 Store 路线，请参阅 [MSIX 和 Store](MSIX-AND-STORE.md)。
