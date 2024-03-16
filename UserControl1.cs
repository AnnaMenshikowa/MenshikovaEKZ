using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace demka
{
    public partial class UserControl1 : UserControl
    {
        public string Value
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Image Image
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }
        public string Label3Text
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }
        public string Label4Text
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }
        public string Label2Text
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }


        public UserControl1()
        {
            InitializeComponent();
        }
        
        
    }
}
   