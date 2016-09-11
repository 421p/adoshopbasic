using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdoShopManagement.View
{
    public partial class StartWindow 
    {
        public StartWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<string>>(this, NotificationMessageReceived);
        }

        private async void NotificationMessageReceived(NotificationMessage<string> msg)
        {
            switch (msg.Notification)
            {
                case "ShowErrorBox":
                    await this.ShowMessageAsync("ERROR", msg.Content);
                    break;

                case "ShowMainWindow":
                    MainWindow mainWindow;
                    if (Application.Current.Windows.OfType<MainWindow>().Any())
                        mainWindow = Application.Current.Windows.OfType<MainWindow>().First();
                    else
                        mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                    break;
            }
        }

        private void txtAppPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext).AppPassword = ((PasswordBox)sender).Password;
            }
        }

        private void txtDatabasePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext).DatabasePassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
