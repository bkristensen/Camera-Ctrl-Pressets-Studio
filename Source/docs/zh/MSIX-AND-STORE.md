# MSIX及商店分布

*为Camera Control Studio 1.1.1.*编写

使用 `scripts/build-msix.ps1` 和清单模板构建 MSIX 包。 Microsoft 会先对程序包进行审核，然后再将其显示在应用商店中。可移植可执行文件未签名。

## 选择路线

| 路线 | 签约 |
| --- | --- |
| 带有 MSIX 的 Microsoft Store | Microsoft 签署接受的商店包。您无需为此路由购买签名证书。 |
| 带有 EXE/MSI 安装程序的 Microsoft Store | 需要发布者签名；商店不会重新签署该安装程序。 |
| 直接MSIX下载 | 该包必须具有用户设备上信任的签名。 |
| 直接可移植的EXE | 未签名的构建可能会产生警告。签名可以识别发布者的身份，但并不保证立即享有声誉。 |

## 清单

- [ ] 在 Partner Center 中注册产品并保留其名称。复制准确的包名称、Publisher 和 PublisherDisplayName。
- [ ] 安装 Windows SDK 工具并使用这些标识值运行以下命令。
- [ ] 验证清单并测试 Windows 上的相机访问、安装、升级、卸载、配置文件存储和语言。该模板声明桌面/完全信任操作和网络摄像头访问。验证这些是否符合当前的认证要求。
- [ ] 准备列表、隐私政策、支持链接、许可证信息、年龄评级和真实屏幕截图。解释所需的相机驱动程序控制。运行当前的 Windows 应用程序认证检查。
- [ ] 将候选人上传到Partner Center，修复验证问题，提交并等待认证。请勿在批准之前将应用程序描述为“商店已批准”。

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

输出是未签名的存储提交候选者，而不是准备安装的公共旁加载包。该脚本验证所需的身份字段并使用显式 x64 构建。为每个版本选择一个新的四部分包版本，最后一部分为零用于商店提交。

官方参考资料，2026 年 11 月 18 日检查：

- [Microsoft 代码签名选项](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [商店准备检查表](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX签约指南](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

另请参见 [SignPath](SIGNPATH.md)。在发布之前应再次检查当前的资格和提交规则。
