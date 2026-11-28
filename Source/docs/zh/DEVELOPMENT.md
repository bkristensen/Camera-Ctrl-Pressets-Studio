[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# 编辑界面和翻译

*为Camera Control Studio 1.1.1.*编写

## 打开正确的文件

打开 `Camera-Control-Pressets-Studio.sln`。在解决方案资源管理器中，构建解决方案，然后打开 `CameraControlStudio → Forms → MainForm.cs`。新闻 **Shift+F7** 为 Windows Forms Designer。不要选择 `MainForm.Designer.cs` 作为设计表面。使用 F7 返回代码。

从 `.vsconfig` 安装 Visual Studio .NET 桌面工作负载。该应用程序是 .NET Framework 4.8 上的 WinForms。未使用 WPF 设计器。

## 更改布局

使用“属性”面板更改 `Dock`、`Margin`、`Padding`、行大小和列大小。使用文档大纲选择嵌套面板。主要布局使用表格和`SplitContainer`；避免硬编码屏幕坐标。

每个窗口都继承自`AppForm`，并且有一个不带参数的公共构造函数。将布局放在 `.Designer.cs` 中，将事件放在匹配的 `.cs` 文件中。在设计器中设置窗体的 `Icon` 属性。检查 Visual Studio 和正在运行的应用程序中的布局。

相机滚轮和蓝色按钮绘制在`Controls/BlueControls.cs`中。垂直分隔线和缩放控件位于 `Controls/MainViewControls.cs` 中。设计者可以移动这些控件并调整其大小。预设图标图像及其中性标题位于 `Assets/PressetIcons.resx` 中，`Controls/PresetIcons.cs` 加载它们。 “视频”选项卡包含设计师创作的滑块。正在运行的应用程序显示相机驱动程序支持的滑块。

## 设置默认值

在 `SavedData.Defaults` 中编辑 `Shared/Models.cs` 以获取新配置文件设置。编辑 `ProfileLayout` 作为初始布局。现有配置文件保留其保存的值。创建一个新的配置文件来测试新的默认值；不要删除用户配置文件来强制他们。

## 更新翻译

编辑`Shared/Locales`下相关语言文件中的匹配值。保持`key`不变。 XML 需要 `&amp;` 作为 & 符号。所有文件必须具有相同的密钥。保持存储的相机和图标 ID 不变。

运行 `scripts/check-translations.ps1`，然后构建。使用 `"Camera Control Studio.exe" --lang=de` 或设置中的语言选择器进行测试。更改保存的语言选择后重新启动。

## 添加语言

将 `en.xml` 复制到新的语言代码文件。翻译每个值并将代码和名称注册到 `Shared/UiText.cs` 中。翻译检查器和区域设置测试会自动选取新文件并注册。在 MSIX 清单中声明应用程序语言。

自动选择使用 Windows 语言前缀。不受支持的语言将回退为英语。测试窄窗口和长标签；翻译不得隐藏控件。
