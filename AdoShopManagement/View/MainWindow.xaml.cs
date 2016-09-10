using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Windows;

namespace AdoShopManagement.View
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<string>>(this, NotificationMessageReceived);
        }

        private async void NotificationMessageReceived(NotificationMessage<string> msg)
        {
            switch (msg.Notification)
            {
                case "ShowMessageBox":
                    await this.ShowMessageAsync("Info box", msg.Content);
                    break;

                case "ShowStartWindow":
                    StartWindow startWindow;
                    if (Application.Current.Windows.OfType<StartWindow>().Any())
                        startWindow = Application.Current.Windows.OfType<StartWindow>().First();
                    else
                        startWindow = new StartWindow();
                    Close();
                    startWindow.Show();
                    break;
            }
        }
    }
}
