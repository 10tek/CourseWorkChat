using Chat.DataAccess;
using Chat.Domain;
using Chat.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat.UI
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private AuthManager authManager;
        private bool isReg = false;

        public Auth()
        {
            InitializeComponent();
            authManager = new AuthManager();
            nameL.Visibility = Visibility.Hidden;
            surnameL.Visibility = Visibility.Hidden;
            nameTB.Visibility = Visibility.Hidden;
            surnameTB.Visibility = Visibility.Hidden;
        }

        private async void SignInBtnClick(object sender, RoutedEventArgs e)
        {
            var user = await authManager.SignIn(loginTB.Text, passTB.Password);
            if (user is null)
            {
                MessageBox.Show("Некорректные данные.");
                passTB.Password = string.Empty;
                return;
            }

            var window = new MainWindow(user);
            window.Show();
            this.Close();
        }

        private async void SignUpBtnClick(object sender, RoutedEventArgs e)
        {
            if (!isReg)
            {
                nameL.Visibility = Visibility.Visible;
                surnameL.Visibility = Visibility.Visible;
                nameTB.Visibility = Visibility.Visible;
                surnameTB.Visibility = Visibility.Visible;
                isReg = true;
                signUpBtn.Content = "Зарегистрироваться";
                return;
            }
            var user = await authManager.SignIn(loginTB.Text, passTB.Password);
            if (user != null)
            {
                MessageBox.Show("Некорректные данные, Либо пользователь существует.");
                passTB.Password = string.Empty;
                return;
            }
            else if (passTB.Password == string.Empty)
            {
                MessageBox.Show("Пароль - обязательное поле для заполнения!");
                return;
            }
            user = await authManager.SignUp(loginTB.Text, passTB.Password, nameTB.Text, surnameTB.Text);

            var window = new MainWindow(user);
            window.Show();
            this.Close();
        }


    }
}
