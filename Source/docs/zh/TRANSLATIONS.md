# 翻译 Camera Control Studio

*为Camera Control Studio 1.1.0.*编写

## 编辑现有语言

1. 打开`Source/Shared/Locales/<code>.xml`。使用 `en.xml` 作为上下文和键的参考。
2. 编辑 `<text>` 标签之间的文本。保持密钥、占位符（例如 `{0}`）和存储的 ID 完好无损。另存为 UTF-8。在 XML 中，将文字与符号写为 `&amp;`。
3. 如果您还更改了帮助，请编辑 `Source/docs` 中的匹配文件夹。在 Guide Viewer 中测试指向其他指南和图像的链接。

## 添加语言

1. 将 `en.xml` 复制到以新语言代码命名的文件并翻译所有值。
2. 在 `LanguageCodes` 中的相同索引处注册代码和显示名称，在 `Source/Shared/UiText.cs` 中注册 `LanguageNames` 中的代码和显示名称。如果适用，更新从右到左的处理。
3. 将应用程序语言添加到 MSIX 清单中。如果添加用户指南，请遵循 `Source/docs` 下现有文件夹的结构，将其添加到 `USER-GUIDES.md`，并验证 Guide Viewer 是否打开它。

## 验证并提交

从 `Source` 开始，运行 `powershell -File scripts/check-translations.ps1` 并构建。使用 `--lang=<code>` 启动应用程序。检查最小窗口大小的对话框并使用键盘导航。测试长标签、文本方向和填充占位符的消息。

按照 [GitHub 项目指南](GITHUB.md) 打开拉取请求。命名语言和代码，总结更改，并说明您测试的内容。如果可以帮助审阅者评估布局，请包含屏幕截图。在审核期间将后续编辑添加到同一分支。
