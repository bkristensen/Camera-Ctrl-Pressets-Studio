# 故障排查

*本指南适用于 Camera Control Studio 1.0.1。*

## 目录

- [1. 视频应用与视野](#1-%E8%A7%86%E9%A2%91%E5%BA%94%E7%94%A8%E4%B8%8E%E8%A7%86%E9%87%8E)
  - [Zoom Workplace 中视野受限](#zoom-workplace-%E4%B8%AD%E8%A7%86%E9%87%8E%E5%8F%97%E9%99%90)
- [2. 连接与兼容性](#2-%E8%BF%9E%E6%8E%A5%E4%B8%8E%E5%85%BC%E5%AE%B9%E6%80%A7)
  - [找不到摄像头或无法连接](#%E6%89%BE%E4%B8%8D%E5%88%B0%E6%91%84%E5%83%8F%E5%A4%B4%E6%88%96%E6%97%A0%E6%B3%95%E8%BF%9E%E6%8E%A5)
  - [控制项缺失或在不同应用中表现不同](#%E6%8E%A7%E5%88%B6%E9%A1%B9%E7%BC%BA%E5%A4%B1%E6%88%96%E5%9C%A8%E4%B8%8D%E5%90%8C%E5%BA%94%E7%94%A8%E4%B8%AD%E8%A1%A8%E7%8E%B0%E4%B8%8D%E5%90%8C)
- [3. 移动与已保存位置](#3-%E7%A7%BB%E5%8A%A8%E4%B8%8E%E5%B7%B2%E4%BF%9D%E5%AD%98%E4%BD%8D%E7%BD%AE)
  - [移动停止、跳动或方向相反](#%E7%A7%BB%E5%8A%A8%E5%81%9C%E6%AD%A2%E8%B7%B3%E5%8A%A8%E6%88%96%E6%96%B9%E5%90%91%E7%9B%B8%E5%8F%8D)
  - [视野或已保存位置不符](#%E8%A7%86%E9%87%8E%E6%88%96%E5%B7%B2%E4%BF%9D%E5%AD%98%E4%BD%8D%E7%BD%AE%E4%B8%8D%E7%AC%A6)

## 1. 视频应用与视野

### Zoom Workplace 中视野受限

**现象**\
画面边缘缺失。无法继续缩小或横向转动。已保存位置的构图改变。Zoom 中的 **Optimize for video sharing** 无法使用。原版 Logitech BRIO Control 也出现同样的限制。

**原因**\
**在报告的案例中已确认。** Zoom Workplace 启用了 **Efficiency Mode**。关闭该模式并重启 Zoom 后，原有视野恢复。Camera Control Studio 和已保存位置均未改动。视野变窄的技术机制尚不清楚。

**解决方法**

1. 在 Zoom Workplace 中打开头像菜单，进入 **Settings → Meeting & webinar → Advanced**。
2. 在 **Performance** 下关闭 **Efficiency Mode**。如果设置被锁定，请联系 Zoom 管理员。
3. 完全退出并重新打开 Zoom Workplace。测试期间保留现有位置。

**验证**\
确认画面边缘恢复。调用已保存的位置，对比实时构图。

**涉及的软件和设备**\
Windows、Zoom Workplace、Logitech BRIO、Logitech BRIO Control 和 Camera Control Studio 0.7.0-beta.8。Zoom、驱动和固件的版本未记录。

**参考资料**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. 连接与兼容性

### 找不到摄像头或无法连接

**现象**\
摄像头未列出，或电脑从睡眠恢复后无法控制。

**原因**\
**可能原因。** USB 连接、Windows 权限、其他应用占用摄像头，或尚未确认直播已开始。

**解决方法**

1. 检查 USB 连接。若应用要求确认，请先开始直播，再选 **是**。
2. 检查 Windows 摄像头权限。在 Windows 相机中试看片刻，然后关闭它。
3. 从睡眠恢复后，用任一摄像头控制重连。如果已保存的摄像头不在列表中，请用中央按钮重新选择。 默认情况下，双击中央按钮即可打开摄像头列表。

![方向轮和中央的摄像头选择按钮](../images/screenshots/control%20wheel%20only.png)

**验证**\
确认窗口底部的摄像头名称正确，并测试一个支持的控制能否改变实时画面。

**涉及的软件和设备**\
Windows 和内置或 USB 摄像头。能否同时访问取决于驱动。

**参考资料**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [用户指南](USER-GUIDE.md)

### 控制项缺失或在不同应用中表现不同

**现象**\
有预览，但部分控制项缺失，或画面只在某个应用中随控制改变。

**原因**\
**已确认。** 本应用使用 Windows 驱动提供的控制。**可能原因。** 驱动、固件或其他应用可能影响控制或画面。

**解决方法**

1. 在各应用中选同一个实体摄像头。一次只用一个应用对比预览。
2. 关闭其他摄像头工具，重开要用的视频应用，试用一个可用的控制。
3. 查阅 Windows 更新及该型号的厂商支持页。更新驱动或固件时遵循厂商说明。

**验证**\
确认在实际使用的应用中哪些控制会改变画面。报告问题时记下型号和版本。

**涉及的软件和设备**\
Windows、摄像头驱动、固件及所用的视频或摄像头应用。

**参考资料**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. 移动与已保存位置

### 移动停止、跳动或方向相反

**现象**\
转动、倾斜或变焦提前停止。切换位置时出现跳动，或方向键的效果相反。

**原因**\
**已确认。** 驱动决定范围和步长。本应用可以反转方向。

如果打开 Logitech Camera Settings 或 Logitech BRIO Control 后方向改变，其他应用可能影响了摄像头控制。据报告，Zoom 中的图像仍是正的。受影响的轴和原因尚未确认。

**解决方法**

1. 按放大镜按钮上方的方框按钮，恢复最广视野和最小变焦，再测试。
2. 打开 **设置 → 平移**，仅反转方向有误的轴。
3. 需要精细移动时，试用 **Ctrl** 加方向键。延长切换时间无法缩小驱动步长。
4. 在停止直播后关闭 Logitech 工具。在同一视频应用中以相同的变焦级别测试两个轴。每次只打开一个 Logitech 工具，观察方向是否再次改变。

**验证**\
在实时画面中检查方向和移动范围。

**涉及的软件和设备**\
驱动支持转动、倾斜或变焦的 Windows 摄像头。范围取决于型号和视频模式。

**参考资料**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [用户指南](USER-GUIDE.md)

### 视野或已保存位置不符

**现象**\
画面过窄，已保存位置的构图不符，或没有视角选项。

**原因**\
**已确认。** 位置属于特定摄像头和配置。旧位置未保存视角时会保留当前视角。有些驱动不提供可选视角。

**解决方法**

1. 确认当前配置和摄像头。恢复完整画面并检查实时预览。
2. 在 **视频 → 视野** 中检查启动视野。驱动提供视角时，允许手动调整并比较。
3. 调用已保存位置。若仅在 Zoom 打开时变窄，请参阅上方的 Zoom 条目。查找原因时保留原位置。

**验证**\
将实时画面与预期构图比较。恢复原有视野后再次调用位置。

**涉及的软件和设备**\
Camera Control Studio 配置和已保存位置。视角依摄像头和驱动而异。

**参考资料**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [用户指南](USER-GUIDE.md)

---

还需要帮助？分享日志前先检查内容。请说明型号、版本、操作和结果。

[报告问题](../en/ISSUES.md) · [用户指南](USER-GUIDES.md)
