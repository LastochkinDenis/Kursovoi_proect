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
    /// Логика взаимодействия для ListProductPages.xaml
    /// </summary>
    public partial class ListProductPages : Page
    {
        public ListProductPages()
        {
            InitializeComponent();
            UpdateList();

            ComboBoxKategorii.SelectedIndex = 0;
            SelectionPrice.SelectedIndex = 0;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Добавление нового продукта
        private void AddProduks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPages());
        }

        //Загрузка списка товаров
        private void UpdateList(object sender, RoutedEventArgs e)
        {
            UpdateList();
            if (App.CurrentUser.id_role == 2)
            {
                AddProduks.Visibility = Visibility.Visible;
            }
            else
            {
                AddProduks.Visibility = Visibility.Hidden;
            }
        }

        //Редактирование продукта
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var contect = (sender as Button).DataContext as Entities.produkt;
                NavigationService.Navigate(new AddProductPages(contect));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }            
        }

        private void BtnDelet_Click(object sender, RoutedEventArgs e)
        {
            var currwntService = (sender as Button).DataContext as Entities.produkt;

            if (MessageBox.Show($"Вы уверены, что хотите удалить товар {currwntService.nazvaniye.ToString().Trim()}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    App.Context.produkts.Remove(currwntService);
                    App.Context.SaveChanges();
                    LViewProduct.ItemsSource = App.Context.produkts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
                }
            }
        }


        public string PaintButton
        {
            get
            {
                if (App.CurrentUser.id_role == 2)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }

        private void ComboBoxKategorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void SelectionPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        //Обновление списка товаров
        private void UpdateList()
        {
            try
            {
                var lp = App.Context.produkts.ToList();


                if (SelectionPrice.SelectedIndex == 0)
                {
                    lp = lp.OrderBy(p => p.tsena).ToList();
                }
                else
                {
                    lp = lp.OrderByDescending(p => p.tsena).ToList();
                }

                if (ComboBoxKategorii.SelectedIndex == 1)
                {
                    lp = lp.Where(p => p.id_kategori == 1).ToList();
                }
                if (ComboBoxKategorii.SelectedIndex == 2)
                {
                    lp = lp.Where(p => p.id_kategori == 2).ToList();
                }
                if (ComboBoxKategorii.SelectedIndex == 3)
                {
                    lp = lp.Where(p => p.id_kategori == 3).ToList();
                }
                if (ComboBoxKategorii.SelectedIndex == 4)
                {
                    lp = lp.Where(p => p.id_kategori == 4).ToList();
                }

                lp = lp.Where(p => p.nazvaniye.ToLower().Contains(TextBoxFind.Text.ToLower())).ToList();

                LViewProduct.ItemsSource = lp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Поиск по продуктов
        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        //Добавление 1 единицу продукта в корзину
        private void AppProductBasket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Product = (sender as Button).DataContext as Entities.produkt;

                var ProductAdd = App.Context.Basket_Produck.Where(p => p.id_produkta == Product.id_produkta && p.id_session == App.Sesion).FirstOrDefault();

                if (ProductAdd == null)
                {
                    var PrdudctAdd = new Entities.Basket_Produck
                    {
                        id_produkta = Product.id_produkta,
                        id_session = App.Sesion,
                        kol = 1,
                        PayStarys = false,
                    };

                    App.Context.Basket_Produck.Add(PrdudctAdd);
                    App.Context.SaveChanges();
                }
                else
                {
                    ProductAdd.kol += 1;

                    App.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }

        }

        //Переход на страницу корзины
        private void BasketBtv_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BasketPage());
        }
    }
}
