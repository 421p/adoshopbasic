using AdoShop.Entity;
using AdoShop.Entity.User;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using AdoShop.Utils;

namespace AdoShopManagement.ViewModel
{
    public class MainViewModel : ViewModelBase
    { 
        private ShopContext _context;
        private ObservableCollection<Good> _goods;
        private bool _isActiveProgressRing;
        private Visibility _signInButtonVisibility;
        private string _appLogin;
        private string _databaseLogin;

        public MainViewModel()
        {
            SaveCommand = new RelayCommand(SaveChangesInDB);
            SignInCommand = new RelayCommand(SignIn);
            SignOutCommand = new RelayCommand(SignOut);
        }

        public static ObservableCollection<Category> Categories { get; private set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand SignInCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        public string DatabasePassword { private get; set; }
        public string AppPassword { private get; set; }

        public string DatabaseLogin
        {
            get { return _databaseLogin ?? ConfigurationManager.AppSettings["databaseLogin"]; }
            set
            {
                _databaseLogin = value;
                RaisePropertyChanged("DatabaseLogin");
            }
        }
        public string AppLogin
        {
            get { return _appLogin ?? ConfigurationManager.AppSettings["appLogin"]; }
            set
            {
                _appLogin = value;
                RaisePropertyChanged("AppLogin");
            }
        }

        public ObservableCollection<Good> Goods
        {
            get { return _goods ??  _context.Goods.Local; }
            set { _goods = value; }
        }

        public bool IsActiveProgressRing
        {
            get { return _isActiveProgressRing; }
            set
            {
                _isActiveProgressRing = value;
                RaisePropertyChanged("IsActiveProgressRing");
            }
        }
        
        public Visibility SignInButtonVisibility
        {
            get { return _signInButtonVisibility; }
            set
            {
                _signInButtonVisibility = value;
                RaisePropertyChanged("SignInButtonVisibility");
            }
        }

        private void SaveChangesInDB()
        {
            string info = "Done! All changes saved!";

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                info = "Error occured! Please make sure operation you are trying to perform is valid.";
            }
            finally
            {
                Messenger.Default.Send(new NotificationMessage<string>(info, "ShowMessageBox"));
            }
        }

        private void SignIn()
        {
            ToggleProgressRingAndButton();

            string _cnStr = $"User ID={DatabaseLogin};Password={DatabasePassword};Host=localhost;Port=5432;Database=shop_ado;";
            _context = new ShopContext(_cnStr);

            var encrypted = Aes.Encrypt(AppPassword, UserSalts.Default);

            if (!_context.Database.Exists()
                || !_context.Users.Any(user =>
                user.Name == AppLogin
                && user.Password  == encrypted
                && user.Role == UserRole.Manager))
            {

                Messenger.Default.Send(new NotificationMessage<string>
                    ("Connection failed! Please make sure your data is correct and try again.", "ShowErrorBox"));
                ToggleProgressRingAndButton();
            }
            else
            {
                SetConfigData();
                SetEntitiesData();
                Messenger.Default.Send(new NotificationMessage<string>(string.Empty, "ShowMainWindow"));
            }
        }

        private void SignOut()
        {
            _context.Dispose();
            DatabasePassword = null;
            AppPassword = null;
            Messenger.Default.Send(new NotificationMessage<string>(string.Empty, "ShowStartWindow"));
            ToggleProgressRingAndButton();
        }


        private void ToggleProgressRingAndButton()
        {
            SignInButtonVisibility =
                 SignInButtonVisibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
            IsActiveProgressRing =
                IsActiveProgressRing == true ? false : true;
        }

        private void SetEntitiesData()
        {
            _context.Goods.Load();
            _context.Categories.Load();
            Categories = _context.Categories.Local;
        }

        private void SetConfigData()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["appLogin"].Value = AppLogin;
            config.AppSettings.Settings["databaseLogin"].Value = DatabaseLogin;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}