using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace khlebnyy_magazin
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {   

        public static Entities.khlebnyy_magazinEntities Context
        { get; } = new Entities.khlebnyy_magazinEntities();

        public static Entities.User CurrentUser = null;

        public static int Sesion;
    }
}
