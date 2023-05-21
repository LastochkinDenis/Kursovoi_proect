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
using Microsoft.Win32;
using System.IO;

namespace khlebnyy_magazin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPages.xaml
    /// </summary>
    public partial class AddProductPages : Page
    {
        private Entities.produkt UpdateProduct;
        private byte[] AddImageProduct;

        public AddProductPages()
        {
            InitializeComponent();

            GetIdKotegorii.SelectedIndex = 0;
        }

        //Заполнение формы редактирования данными товара
        public AddProductPages(Entities.produkt produkt)
        {
            InitializeComponent();

            //Заполнение формы данными
            UpdateProduct = produkt;
            Title = "Редоктирование товара";
            NameProduct.Text = UpdateProduct.nazvaniye;
            Price.Text = UpdateProduct.tsena.ToString();
            GetIdKotegorii.SelectedIndex = (int)UpdateProduct.id_kategori - 1;
            Opisanie.Text = UpdateProduct.opisanie;

            if (UpdateProduct.image != null)
            {
                ImageProduct.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(UpdateProduct.image);
            }

        }

        //Добавление и редактирование картинки у товара
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image | *.png; *.jpg; *.jpeg";

            if(ofd.ShowDialog() == true)
            {
                AddImageProduct = File.ReadAllBytes(ofd.FileName);
                ImageProduct.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(AddImageProduct);
            }
        }

        //Сохранение или редактирование товара
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            var errorMassage = CheckErrors();

            if (errorMassage.Length > 0)
            {
                MessageBox.Show(errorMassage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try {
                    if (UpdateProduct == null)
                    {
                        var product = new Entities.produkt
                        {
                            nazvaniye = NameProduct.Text,
                            tsena = int.Parse(Price.Text),
                            opisanie = Opisanie.Text,
                            id_kategori = GetIdKotegorii.SelectedIndex + 1,
                            image = AddImageProduct,

                        };

                        App.Context.produkts.Add(product);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        UpdateProduct.nazvaniye = NameProduct.Text;
                        UpdateProduct.tsena = int.Parse(Price.Text);
                        UpdateProduct.opisanie = Opisanie.Text;
                        UpdateProduct.id_kategori = GetIdKotegorii.SelectedIndex + 1;
                        if (AddImageProduct != null)
                            UpdateProduct.image = AddImageProduct;

                        App.Context.SaveChanges();
                    }
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Соединение с сервером потеряно", "Ошибка", MessageBoxButton.OK);
                }
            }
        }

        //Проверка товара на ошибки заполнение перед сохранением
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NameProduct.Text))
            {
                errorBuilder.AppendLine("Название товара обязательно для заполнения");
            }

            var productBD = App.Context.produkts.ToList().
                FirstOrDefault(p => p.nazvaniye.Trim().ToLower() == NameProduct.Text.Trim().ToLower());
            if (productBD != null && productBD.ToString() == UpdateProduct.nazvaniye)
            {
                errorBuilder.AppendLine("Такой товар уже есть в базе даных");
            }

            int cost = 0;
            if (int.TryParse(Price.Text, out cost) == false || cost <= 0 )
            {
                errorBuilder.AppendLine("Стоимость товара должна быть положительным числом");
            }

            string opisanie = Opisanie.Text;
            if (opisanie.Length > 250)
            {
                errorBuilder.AppendLine("Описание должно быть меньше 250 символов");
            }

            return errorBuilder.ToString();
        }

        private void GetIdKotegorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
