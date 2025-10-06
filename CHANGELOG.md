# 📝 Changelog

[2.4.0.0] - 2025-10-06

🚀 Features

- 自动检测设置系统代理与界面优化

🐛 Bug Fixes

- *(build)* 修复32 位进程无法访问 64 位进程模块的错误
- 修复原版内核保留上次成功节点设置

⚡ Performance

- 优化覆写配置和内核下载逻辑，自动选择系统代理或github代理
- 优化下载策略、避免不必要的检测
- 统一覆写文件和内核下载更新逻辑，提高稳定性
- 优化检查更新界面和更多参数页面

⚙️ Miscellaneous Tasks

- *(doc)* 修改readme，更新GUI界面截图
- *(ui)* 修改启用WebUI API密钥按钮文本
