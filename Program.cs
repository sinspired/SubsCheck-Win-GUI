using System;
using System.Threading;
using System.Windows.Forms;

namespace subs_check.win.gui
{
    static class Program
    {
        // 定义一个全局唯一的标识符，使用项目名称作为互斥体的名称
        private static string appMutexName = "cmliu/SubsCheck-Win-GUI";
        private static Mutex mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 尝试创建一个命名互斥体，如果已存在，则获取它
            bool createdNew;
            mutex = new Mutex(true, appMutexName, out createdNew);

            if (!createdNew)
            {
                // 如果互斥体已存在（即程序已在运行），则终止当前实例
                MessageBox.Show("应用程序已经在运行中。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainGui());
            }
            finally
            {
                // 确保释放互斥体
                mutex.ReleaseMutex();
            }
        }
    }
}
