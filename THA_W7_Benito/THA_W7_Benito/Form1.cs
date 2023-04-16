using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace THA_W7_Benito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Label brandcinema;
        List<MovieClass> movies = new List<MovieClass>();
        int moviePointer = 0;
        string timePointer = "";
        List<CheckBox> checkBoxList = new List<CheckBox>();

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;

            string textfile = "abc.txt";
            string[] lines = File.ReadAllLines(textfile);

            int pic_loc_x = 50;
            int pic_loc_x_penambahan = 250;
            int pic_loc_y = 55;
            int row = 0;
            int row_penambahan = 400;
            int total_row = 1;

            foreach (string line in lines)
            {
                string[] arrayMaster = line.Split(';');
                string[] arrayTime = arrayMaster[2].Split(',');

                MovieClass movieclass = new MovieClass(arrayMaster[0], arrayMaster[1], arrayTime);
                movieclass.generateRandom();
                movies.Add(movieclass);

                PictureBox movieimage = new PictureBox();
                movieimage.ImageLocation = (arrayMaster[1]);
                movieimage.SizeMode = PictureBoxSizeMode.StretchImage;
                movieimage.Location = new Point(pic_loc_x, pic_loc_y + row * row_penambahan);
                movieimage.Size = new Size(200, 280);
                movieimage.Click += (sendter, EventArgs) => { image_click(sender, EventArgs, movieclass); };
                panel1.Controls.Add(movieimage);

                Label movielabel = new Label();
                movielabel.Text = arrayMaster[0];
                movielabel.Font = new Font("Arial", 10, FontStyle.Bold);
                movielabel.Size = new Size(200, 20);
                movielabel.ForeColor = Color.Black;
                movielabel.Location = new Point(pic_loc_x, 300 + pic_loc_y + row * row_penambahan);
                panel1.Controls.Add(movielabel);

                pic_loc_x += pic_loc_x_penambahan;
                total_row++;
                if(total_row % 5 == 0)
                {
                    row++;
                    pic_loc_x = 50;
                }
            }
           
            //brandcinema
            brandcinema = new Label();
            brandcinema.Text = "Nyuantuyyyy Cinema";
            brandcinema.Font = new Font("Arial", 24, FontStyle.Bold);
            brandcinema.Location = new Point(380, 10);
            brandcinema.Size = new Size(600, 37);
            brandcinema.ForeColor = Color.Black;
            this.Controls.Add(brandcinema);
        }
        void image_click(object sender, EventArgs e, MovieClass movieClass)
        {
            panel2.Visible = true;
           
            gambarFilmPanel2.ImageLocation = movieClass.Path;
            labelFilmPanel2.Text = movieClass.Name;
            groupBox1.Controls.Clear();

            int counter = 0;
            foreach(string time in movieClass.TimeArray)
            {
                RadioButton pilihan = new RadioButton();
                pilihan.Location = new Point(17, 100 + (counter * 40));
                pilihan.Text = time;
                pilihan.Click += (sendter, EventArgs) => { time_click(sender, EventArgs, movieClass, time); };
                if (counter == 0)
                {
                    pilihan.Checked = true;
                    timePointer = time;
                }
                groupBox1.Controls.Add(pilihan);
                counter++;
            }

            int cb_column = 28;
            int cb_row = 0;

            for(int i=1; i<101; i++)
            {
                CheckBox tempCheckbox = new CheckBox();
                tempCheckbox.Text = i.ToString();
                tempCheckbox.Size = new Size(45, 40);
                tempCheckbox.Location = new Point(cb_column, 44 + cb_row * 44);
                bool statusKursi = movieClass.ChairDict[movieClass.TimeArray[0]][i - 1];
                tempCheckbox.Enabled = !statusKursi;
                if(statusKursi)
                {
                    tempCheckbox.ForeColor = Color.Red;
                }
                else
                {
                    tempCheckbox.ForeColor = Color.Green;
                }
                checkBoxList.Add(tempCheckbox);
                groupBox2.Controls.Add(tempCheckbox);

                cb_column += 50;
                if (i%10==0)
                {
                    cb_row++;
                    cb_column = 28;
                }
            }
        }

        void generateCheckBoxKursi(MovieClass movieClass, string time)
        {
            int cb_column = 28;
            int cb_row = 0;
            timePointer = time;
            groupBox2.Controls.Clear();
            checkBoxList.Clear();

            for (int i = 1; i < 101; i++)
            {
                CheckBox tempCheckbox = new CheckBox();
                tempCheckbox.Text = i.ToString();
                tempCheckbox.Size = new Size(45, 40);
                tempCheckbox.Location = new Point(cb_column, 44 + cb_row * 44);
                bool statusKursi = movieClass.ChairDict[time][i - 1];
                tempCheckbox.Enabled = !statusKursi;
                if (statusKursi)
                {
                    tempCheckbox.ForeColor = Color.Red;
                }
                else
                {
                    tempCheckbox.ForeColor = Color.Green;
                }
                checkBoxList.Add(tempCheckbox);
                groupBox2.Controls.Add(tempCheckbox);

                cb_column += 50;
                if (i % 10 == 0)
                {
                    cb_row++;
                    cb_column = 28;
                }
            }
        }

        void time_click(object sender, EventArgs e, MovieClass movieClass, string time)
        {
            generateCheckBoxKursi(movieClass, time);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach(CheckBox checkbox in checkBoxList)
            {
                if(checkbox.Checked)
                {
                    movies[moviePointer].ChairDict[timePointer][int.Parse(checkbox.Text)-1] = true;
                }
            }
            MessageBox.Show("Your booking has been saved !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel2.Visible=false;
            panel1.Visible = true;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            movies[moviePointer].reset(timePointer);
            generateCheckBoxKursi(movies[moviePointer], timePointer);
        }
    }
}
