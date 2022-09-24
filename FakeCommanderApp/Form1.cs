using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeCommanderApp
{
    public partial class Form1 : Form
    {
        private string PathOneDirectory;
        public Form1()
        {
            InitializeComponent();
            CreateListView(ListDataFilesOne);
            CreateListView2(ListDataFilesTwo);

        }
        

        #region Обработка методов
        private void CreateListView(ListView lv) {
            lv.View = View.Details;

            lv.Columns.Add("Тип файла");
            lv.Columns[0].Width = 90;
            lv.Columns.Add("Имя файла");
            lv.Columns[1].Width = 200;
            lv.Columns.Add("Размер");
            lv.Columns[2].Width = 90;

            lv.FullRowSelect = true;
        }

        private void EventListOneView(ListView lsv, string folderData, string pattern="*.*")
        {
            lsv.Items.Clear();
            DirectoryInfo dirinfo = new DirectoryInfo(folderData);
            DirectoryInfo[] directories = dirinfo.GetDirectories();
            FileInfo[] file = dirinfo.GetFiles(pattern);

            foreach (DirectoryInfo item in directories)
            {
                lsv.Items.Add(new ListViewItem(new string[]
                    { 
                        "Directory",
                        item.Name,
                        Directory.GetFiles(item.FullName, "*", SearchOption.AllDirectories)
                        .Sum(x => new FileInfo(x).Length / 1024).ToString() + " Кбайт"
                    }
                    ));
            }

            foreach (FileInfo item in file)
            {
                if (item.Name.Equals("desktop.ini"))
                {
                    continue;
                }

                lsv.Items.Add(new ListViewItem(new string[] {
                    "File",
                    item.Name,
                    (item.Length / 1024).ToString() + " Кбайт"
                })) ;
            }
        }



        #endregion

        private void BtnOpenDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    PathOnePanel.Text = fbd.SelectedPath;
                    PathOneDirectory = fbd.SelectedPath;
                    EventListOneView(ListDataFilesOne, fbd.SelectedPath);
                }
            }
        }

            private void CreateListView2(ListView lv)
            {
                lv.View = View.Details;

                lv.Columns.Add("Тип файла");
                lv.Columns[0].Width = 90;
                lv.Columns.Add("Имя файла");
                lv.Columns[1].Width = 200;
                lv.Columns.Add("Размер");
                lv.Columns[2].Width = 90;

                lv.FullRowSelect = true;
            }
        private void EventListOneView2(ListView lsv, string folderData, string pattern = "*.*")
        {
            lsv.Items.Clear();
            DirectoryInfo dirinfo = new DirectoryInfo(folderData);
            DirectoryInfo[] directories = dirinfo.GetDirectories();
            FileInfo[] file = dirinfo.GetFiles(pattern);

            foreach (DirectoryInfo item in directories)
            {
                lsv.Items.Add(new ListViewItem(new string[]
                    {
                        "Directory",
                        item.Name,
                        Directory.GetFiles(item.FullName, "*", SearchOption.AllDirectories)
                        .Sum(x => new FileInfo(x).Length / 1024).ToString() + " Кбайт"
                    }
                    ));
            }

            foreach (FileInfo item in file)
            {
                if (item.Name.Equals("desktop.ini"))
                {
                    continue;
                }

                lsv.Items.Add(new ListViewItem(new string[] {
                    "File",
                    item.Name,
                    (item.Length / 1024).ToString() + " Кбайт"
                }));
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    PathTwoPanel.Text = fbd.SelectedPath;
                    PathOneDirectory = fbd.SelectedPath;
                    EventListOneView2(ListDataFilesTwo, fbd.SelectedPath);
                }
            }
        }

    }
}
