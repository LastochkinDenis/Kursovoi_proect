using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khlebnyy_magazin.Entities
{
    public partial class Basket_Produck
    {
        //Получение имени по id продукта
        public string GetNameProduct
        {
            get
            {
                return App.Context.produkts.Where(p => p.id_produkta == id_produkta).Select(p => p.nazvaniye).FirstOrDefault();
            }
        }

        //Получение цены по id продукта
        public int GetProductPrice
        {
            get
            {
                return (int)App.Context.produkts.Where(p => p.id_produkta == id_produkta).Select(p => p.tsena).FirstOrDefault();
            }
        }

        //Получение суммы по продукту
        public int GetSumProduct
        {
            get
            {
                int tsena = (int)App.Context.produkts.Where(p => p.id_produkta == id_produkta).Select(p => p.tsena).FirstOrDefault();

                int count = (int)App.Context.Basket_Produck.Where(p => p.id_produkta == id_produkta && p.id_session == App.Sesion).Select(p => p.kol).FirstOrDefault();

                return tsena * count;
            }
        }
    }
}
