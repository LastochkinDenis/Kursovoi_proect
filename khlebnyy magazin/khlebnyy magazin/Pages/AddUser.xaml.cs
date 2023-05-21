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
using CheckPassword;

namespace khlebnyy_magazin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        //Оценка сложности пароля
        private int lvl = 0;

        public AddUser()
        {
            InitializeComponent();
        }

        //Добавление нового пользователя
        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length != 0)
            {
                if (lvl >= 3)
                {
                    if (Passwor1.Password == Passwor2.Password)
                    {
                        var User = new Entities.User
                        {
                            login = Login.Text,
                            password = Passwor1.Password,
                            id_role = 1
                        };
                        try
                        {
                            App.Context.Users.Add(User);
                            App.Context.SaveChanges();
                            NavigationService.GoBack();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен обладать средней сложностью", "Ошибка", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Поле Логин не должно быть пустым", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Проверка на сложность пароля
        private void Passwor1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPasswordClass check = new CheckPasswordClass();

            string str = Passwor1.Password;

            lvl = check.CP(str);

            if (lvl == 0)
            {
                LvlPassword.Text = "Легкий";
            }
            else if (lvl >= 1 && lvl < 3)
            {
                LvlPassword.Text = "Не сложный";
            }
            else if (lvl >= 3 && lvl <= 4)
            {
                LvlPassword.Text = "Средний";
            }
            else if (lvl == 5)
            {
                LvlPassword.Text = "Сложный";
            }
        }
    }
}
