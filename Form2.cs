using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace demka
{
    public partial class Form2 : Form
    {



        public Form2()
        {
            InitializeComponent();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            PictureBox pictureBox1 = new PictureBox();
            SortComboBox.Items.Add("По возрастанию");
            SortComboBox.Items.Add("По убыванию");

            FiltrComboBox.Items.Add("Супер мягкая");
            FiltrComboBox.Items.Add("Один слой");
            FiltrComboBox.Items.Add("Два слоя");
            FiltrComboBox.Items.Add("Три слоя");
            FiltrComboBox.Items.Add("Детская");


            this.Controls.Add(flowLayoutPanel1);
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=12345678;Database=demka"))
                {
                    conn.Open();
                    DataTable productDataTable = new DataTable();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT articlenumber, image, mincostforagent, title, title FROM product", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            productDataTable.Load(reader);
                        }
                    }
                    foreach (DataRow row in productDataTable.Rows)
                    {
                        string articlenumber = row["articlenumber"].ToString();
                        string imageUrl = row["image"].ToString();
                        string minCost = row["mincostforagent"].ToString();
                        string productTypeId = row["title"].ToString();
                        string titlen = row["title1"].ToString();
                        Image image;
                        using (var webClient = new WebClient())
                        {
                            byte[] imageBytes = webClient.DownloadData(imageUrl);
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                image = Image.FromStream(ms);
                            }
                        }

                        image = ResizeImageToFitPictureBox(image, pictureBox1);
                        string title = "";
                        using (NpgsqlCommand typeCmd = new NpgsqlCommand("SELECT pt.title FROM product p INNER JOIN producttype pt ON p.producttypeid = pt.id WHERE p.articlenumber = @articlenumber", conn))
                        {
                            typeCmd.Parameters.AddWithValue("@articlenumber", articlenumber);
                            using (NpgsqlDataReader typeReader = typeCmd.ExecuteReader())
                            {
                                if (typeReader.Read())
                                {
                                    title = typeReader["title"].ToString();
                                }
                            }
                        }
                        UserControl1 userControl = new UserControl1();
                        userControl.Value = articlenumber;
                        userControl.Image = image;
                        userControl.Label3Text = minCost;
                        userControl.Label4Text = title;
                        userControl.Label2Text = titlen;
                        flowLayoutPanel1.Controls.Add(userControl);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Image ResizeImageToFitPictureBox(Image image, PictureBox pictureBox)
        {
            if (image.Width > pictureBox.Width || image.Height > pictureBox.Height)
            {
                double widthRatio = (double)pictureBox.Width / image.Width; double heightRatio = (double)pictureBox.Height /
image.Height; double ratio = Math.Min(widthRatio, heightRatio);
                int newWidth = (int)(image.Width * ratio); int newHeight = (int)(image.Height * ratio); return new Bitmap(image, new Size(newWidth, newHeight));
            }
            else
            {
                return image;
            }
        }

        private void SortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSortOption = SortComboBox.SelectedItem.ToString();

            if (selectedSortOption == "По возрастанию")
            {
                SortByCostAscending();
            }
            else if (selectedSortOption == "По убыванию")
            {
                SortByCostDescending();
            }

        }

        private void SortByCostDescending()
        {
            flowLayoutPanel1.Controls.Clear();
        }
        private void SortByCostAscending()
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.ToLower();
            foreach (var item in flowLayoutPanel1.Controls)
            {
                if (item is UserControl1 userControl)
                {
                    if (userControl.label2.Text.ToLower().Contains(searchQuery))
                    {
                        userControl.Visible = true;
                    }
                    else
                    {
                        userControl.Visible = false;
                    }
                }
            }
        }

        private void FiltrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = FiltrComboBox.SelectedItem.ToString();

            foreach (UserControl1 userprod in flowLayoutPanel1.Controls.OfType<UserControl1>())
            {
                if (userprod.Label4Text == selectedType)
                {
                    userprod.Visible = true;
                }
                else
                {
                    userprod.Visible = false;
                }
            }
        }
    }
}


            
                    

