using Ekzamen.Class;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;

namespace Ekzamen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Valuta> valuts = new List<Valuta>();
        private void GetInfoButton_Click(object sender, RoutedEventArgs e)
        {
            string path = @"http://www.nationalbank.kz/rss/rates_all.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            //XmlElement root = doc.CreateElement("Chanel");

            int size = 0;


            var root = doc.DocumentElement;

            foreach (XmlElement koren in root.ChildNodes)
            {
                if (koren.Name == "channel")
                {
                    //Console.WriteLine("KOREN" + item.Name);
                    //lab.Content = item.Name;
                    foreach (XmlElement vetv in koren.ChildNodes)
                    {
                        if (vetv.Name == "item")
                        {
                            Valuta val = new Valuta();
                            size++;

                            foreach (XmlElement svoistvo in vetv.ChildNodes)
                            {
                                if (svoistvo.Name == "title")
                                {
                                    val.nazvanie = svoistvo.InnerText;
                                }
                                else if (svoistvo.Name == "pubDate")
                                {
                                    val.data = DateTime.Parse(svoistvo.InnerText);
                                }
                                else if (svoistvo.Name == "description")
                                {
                                    val.cena = Double.Parse(svoistvo.InnerText.Replace(".",","));
                                }
                                else if (svoistvo.Name == "quant")
                                {
                                    val.kollvo = Int32.Parse(svoistvo.InnerText);
                                }
                                else if (svoistvo.Name == "index")
                                {
                                    val.izmenenie = svoistvo.InnerText;
                                }
                                else if (svoistvo.Name == "change")
                                {
                                    val.vel_Izmeneniya = double.Parse(svoistvo.InnerText.Replace(".", ","));
                                }
                                valuts.Add(val);
                            }
                        }
                    }
                }
                
            }

            //MessageBox.Show("общее колличество валют = " + size.ToString());
            foreach (Valuta va in valuts.Distinct())
            {
                ComboList1.Items.Add(va.nazvanie +"= "+ va.cena);

                ComboList2.Items.Add(va.nazvanie + "= " + va.cena);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double koef = 0;
            if (ComboList1.SelectedIndex == ComboList2.SelectedIndex)
            {
                MessageBox.Show("валюты одинаковые");
            }

            else if (valuts[ComboList1.SelectedIndex].cena < valuts[ComboList2.SelectedIndex].cena)
            {
                koef = valuts[ComboList1.SelectedIndex].cena / valuts[ComboList2.SelectedIndex].cena * Double.Parse(text1.Text);
                text2.Text = koef.ToString();
                //MessageBox.Show(text2.Text);
            }
            else if (valuts[ComboList1.SelectedIndex].cena > valuts[ComboList2.SelectedIndex].cena)
            {
                koef = valuts[ComboList2.SelectedIndex].cena / valuts[ComboList1.SelectedIndex].cena * Double.Parse(text1.Text);
                text2.Text = koef.ToString();
                //MessageBox.Show(text1.Text);
            }
        }
    }
}
