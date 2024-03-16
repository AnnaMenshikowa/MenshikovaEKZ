using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace demka
{
    public static class Program
    {
        public static NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;UserName=postgres;Password=12345678;Database=demka");
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
       
        }
    }

