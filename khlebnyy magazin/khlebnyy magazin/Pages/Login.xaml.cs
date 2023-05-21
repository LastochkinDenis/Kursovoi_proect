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

namespace khlebnyy_magazin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Lodin.xaml
    /// </summary>
    public partial class Lodin : Page
    {
        public Lodin()
        {
            InitializeComponent();
            
        }

        //Регистрация пользователя
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var currentUser = App.Context.Users.
                 FirstOrDefault(p => p.login == Login.Text.Trim() && p.password == Password.Password.Trim());//Contains 

                var log = App.Context.Users.Where(p => p.login == Login.Text.Trim()).Select(p => p.login).FirstOrDefault();
                var pas = App.Context.Users.Where(p => p.password == Password.Password.Trim()).Select(p => p.password).FirstOrDefault();
                if (currentUser != null && log.Trim() == Login.Text.Trim() && pas.Trim() == Password.Password.Trim())
                {
                    App.CurrentUser = currentUser;
                    NavigationService.Navigate(new ListProductPages());

                    //Создание сессии
                    var AddSesion = new Entities.session
                    {
                        login = Login.Text
                    };


                    App.Context.sessions.Add(AddSesion);
                    App.Context.SaveChanges();


                    App.Sesion = App.Context.sessions.Where(p => p.login == Login.Text).Select(p => p.id_session).OrderByDescending(p => p).FirstOrDefault();
                }
                else
                {
                    if (MessageBox.Show("Пользователь с такими данными не нет. Хотите зарегистрироваться", "Ошибка", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new AddUser());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }

        }

        private void egistration_Click(object sender, RoutedEventArgs e)
        {

        }

        //Переход на страницу добавление пользователя
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser());
        }
    }
}
