# 📝 Changelog

[2.3.0.0] - 2025-09-30

🚀 Features

- 添加检测流水线并发设置
- 添加32\64位内核切换
- 添加高并发内核/原版切换
- 支持流水线分阶段自适应并发检测
- 支持流水线分段高并发模式，增强位置标签，大幅提高性能
- 添加AutoUpdater，优化GUI自动更新，修复部分进度显示

🐛 Bug Fixes

- 修复saveMethod group的初始化位置
- 修复版本架构切换逻辑,避免下载进程冲突
- 修复R2和webdav的默认载入位置
- 修复cron输入框显示
- 修复listen-port参数写入错误
- 修复检查更新窗体错误引用导致重复创建实例的错误
- 修复生成update.xml使用了错误的version

⚡ Performance

- 优化控件提示，提升使用体验
- 优化保留之前成功节点的逻辑
- 优化开始检测\结束检测的按钮事件逻辑,修复bug
- 优化启动检测按钮和日志标签显示
- 优化检查更新逻辑和UI

🎨 Styling

- 语义化控件名称

⚙️ Miscellaneous Tasks

- *(UI)* 调整界面尺寸，以适配旧设备
- Upgrade .NET Framework from 4.7.2 to 4.8
