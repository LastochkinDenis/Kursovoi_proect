using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khlebnyy_magazin.Entities
{
    public partial class produkt
    {
        //Получение статусу видимости по роли пользователя
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
                    return "Hidden";
                }
            }
        }

        //Получение название категори по id категории 
        public string PrintKategori
        {
            get
            {
                if (id_kategori == 1)
                    return "Пирог";
                if (id_kategori == 2)
                    return "Печенье";
                if (id_kategori == 3)
                    return "Булочка";
                if (id_kategori == 4)
                    return "Хлеб";
                return "_";
            }
        }
    }
}
