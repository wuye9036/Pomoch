using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pomoch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		static string IconPath = "./Resources/Pomoch.ico";

		static public ImageSource IconSource()
		{
			Uri u = new Uri(IconPath, UriKind.RelativeOrAbsolute);
			return new BitmapImage(u);
		}

		static public System.Drawing.Icon DrawingIcon()
		{
			return System.Drawing.Icon.ExtractAssociatedIcon(IconPath);
		}

		private NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenu trayIconContextMenu;

		private System.Windows.Forms.MenuItem menuItemDisplay;
		private System.Windows.Forms.MenuItem menuItemStart;
		private System.Windows.Forms.MenuItem menuItemPause;
		private System.Windows.Forms.MenuItem menuItemResume;
		private System.Windows.Forms.MenuItem menuItemCancel;
		private System.Windows.Forms.MenuItem menuItemExit;

        public MainWindow()
        {
            InitializeComponent();

			trayIconContextMenu = new System.Windows.Forms.ContextMenu();

			menuItemDisplay = new System.Windows.Forms.MenuItem("显示", new System.EventHandler(TrayIconMenuItem_Click));
			menuItemStart = new System.Windows.Forms.MenuItem("开始", new System.EventHandler(TrayIconMenuItem_Click));
			menuItemPause = new System.Windows.Forms.MenuItem("暂停", new System.EventHandler(TrayIconMenuItem_Click));
			menuItemResume = new System.Windows.Forms.MenuItem("恢复", new System.EventHandler(TrayIconMenuItem_Click));
			menuItemCancel = new System.Windows.Forms.MenuItem("中止", new System.EventHandler(TrayIconMenuItem_Click));
			menuItemExit = new System.Windows.Forms.MenuItem("退出", new System.EventHandler(TrayIconMenuItem_Click));

			menuItemDisplay.DefaultItem = true;

			trayIconContextMenu.MenuItems.Add(menuItemDisplay);
			trayIconContextMenu.MenuItems.Add(menuItemStart);
			trayIconContextMenu.MenuItems.Add(menuItemPause);
			trayIconContextMenu.MenuItems.Add(menuItemResume);
			trayIconContextMenu.MenuItems.Add(menuItemCancel);
			trayIconContextMenu.MenuItems.Add(menuItemExit);

			trayIcon = new NotifyIcon();
			trayIcon.Text = "Pomoch";
			trayIcon.Icon = DrawingIcon();
			trayIcon.Visible = true;
			trayIcon.ContextMenu = trayIconContextMenu;

			trayIcon.DoubleClick += TrayIcon_DoubleClick;

			this.Icon = IconSource();
        }

		private void RestoreWindowDisplay()
		{
			this.Activate();
			if (this.WindowState == System.Windows.WindowState.Minimized)
			{
				this.WindowState = System.Windows.WindowState.Normal;
			}
		}

		private void TrayIconMenuItem_Click(object sender, EventArgs e)
		{
			if (sender == menuItemDisplay)
			{
				RestoreWindowDisplay();
			}
		}

		private void TrayIcon_DoubleClick(object sender, EventArgs e)
		{
			RestoreWindowDisplay();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			trayIcon.Visible = false;
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			if (this.WindowState == System.Windows.WindowState.Minimized)
			{
				this.ShowInTaskbar = false;
			}
			else
			{
				this.ShowInTaskbar = true;
			}
		}
    }
}
