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
using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp;
using Paragraph = iTextSharp.text.Paragraph;
using System.Diagnostics;

namespace khlebnyy_magazin.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        public BasketPage()
        {
            InitializeComponent();

            UpdateBasket();
        }

        //Добавление 1 единицу товара
        private void BasketAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Product = (sender as Button).DataContext as Entities.Basket_Produck;

                Product.kol++;

                App.Context.SaveChanges();
                UpdateBasket();
            }
            catch
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Вычетание 1 единицу тавара
        private void BasketDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Product = (sender as Button).DataContext as Entities.Basket_Produck;

                Product.kol--;

                if (Product.kol <= 0)
                {
                    App.Context.Basket_Produck.Remove(Product);
                }

                App.Context.SaveChanges();
                UpdateBasket();
            }
            catch
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
            
        }

        //Оплата товаров
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Product = App.Context.Basket_Produck.Where(p => p.id_session == App.Sesion).ToList();
                int count = 0;

                foreach (var p in Product)
                {
                    count++;
                    p.PayStarys = true;
                }

                App.Context.SaveChanges();

                PrintChek();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Очистка корзины
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Product = App.Context.Basket_Produck.Where(p => p.id_session == App.Sesion).ToList();


                foreach (var p in Product)
                {
                    if (p.PayStarys)
                    {
                        MessageBox.Show("Нельзя очистить корзину после покупки", "", MessageBoxButton.OK);
                        break;
                    }
                    App.Context.Basket_Produck.Remove(p);
                }

                App.Context.SaveChanges();

                UpdateBasket();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Обновление контента в корзине
        private void UpdateBasket()
        {
            try
            {
                BasletListProduct.ItemsSource = App.Context.Basket_Produck.Where(p => p.id_session == App.Sesion).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }

        //Печать чека
        private void PrintChek()
        {
            //Офомление документа
            var document = new Document(PageSize.A4, 20, 20, 30, 20);

            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALNBI.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            try
            {
                //Получение оплаченных товаров
                var IdProductFromBasket = App.Context.Basket_Produck.Where(p => p.id_session == App.Sesion && p.PayStarys == true).Select(p => p.id_produkta).ToList();


                using (var writer = PdfWriter.GetInstance(document, new FileStream("document.pdf", FileMode.Create)))
                {
                    //Заполнение pdf данными
                    document.Open();
                    document.NewPage();
                    document.Add(new Paragraph("Чек", font));

                    int AllSumBasket = 0;

                    foreach (int id in IdProductFromBasket)
                    {

                        string naz = App.Context.produkts.Where(p => p.id_produkta == id).Select(p => p.nazvaniye).FirstOrDefault();

                        int kol = App.Context.Basket_Produck.Where(p => p.id_session == App.Sesion && p.PayStarys == true && p.id_produkta == id).Select(p => p.kol).FirstOrDefault();

                        int cenaTovara = (int)App.Context.produkts.Where(p => p.id_produkta == id).Select(p => p.tsena).FirstOrDefault();

                        int sum = kol * cenaTovara;

                        AllSumBasket += sum;

                        document.Add(new Paragraph($"{naz} Цена:{cenaTovara} Ш.:{kol} = {sum}", font));
                    }

                    document.Add(new Paragraph($"Сумма: {AllSumBasket}", font));

                    document.Close();
                    writer.Close();
                }

                Process.Start("document.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
            }
        }


        private void Chek_Click(object sender, RoutedEventArgs e)
        {
            PrintChek();
        }

        public string PrintButtonChek
        {
            get
            {
                try
                {
                    var productPay = App.Context.Basket_Produck.Where(p => p.PayStarys == true && p.id_session == App.Sesion).Select(p => p.id_produkta).ToList();

                    if (productPay.Count >= 1)
                    {
                        return "Visible";
                    }
                    else
                    {
                        return "Collapsed";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
                }
                return "Collapsed";
            }
        }
    }
}
