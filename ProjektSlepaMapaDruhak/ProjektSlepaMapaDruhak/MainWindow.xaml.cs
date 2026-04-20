using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektSlepaMapaDruhak
{
    public partial class MainWindow : Window
    {
        public class Mesto
        {
            public string Nazev { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
        }

        private List<Mesto> seznamMest = new List<Mesto>();
        private Mesto aktualniHledaneMesto;
        private int skore = 0;
        private int kolo = 0;

        public MainWindow()
        {
            InitializeComponent();

            // 1. PŘIDEJ MĚSTA DO SEZNAMU
            seznamMest.Add(new Mesto { Nazev = "Praha", X = 247, Y = 200 });
            seznamMest.Add(new Mesto { Nazev = "Brno", X = 461, Y = 305 });
            seznamMest.Add(new Mesto { Nazev = "Ostrava", X = 603, Y = 225 });

            // 2. VYTVOŘ TLAČÍTKA NA MAPĚ (Tahle funkce byla v tom smazaném kódu!)
            VytvorTlacitkaMest();

            // 3. ZAČNI PRVNÍ KOLO
            DalsiKolo();
        }

        private void VytvorTlacitkaMest()
        {
            foreach (var mesto in seznamMest)
            {
                Button btn = new Button();
                btn.Content = mesto.Nazev;
                btn.Width = 60;
                btn.Height = 25;
                btn.Tag = mesto;
                btn.Click += TlacitkoMesta_Click;

                // Tady říkáme tlačítku, kde na mapě má ležet
                Canvas.SetLeft(btn, mesto.X - 30);
                Canvas.SetTop(btn, mesto.Y - 12);

                // Přidáme tlačítko DO Canvasu (nad obrázek)
                mapCanvas.Children.Add(btn);
            }
        }

        private void DalsiKolo()
        {
            if (kolo < seznamMest.Count)
            {
                aktualniHledaneMesto = seznamMest[kolo];
                txtAktivniMesto.Text = aktualniHledaneMesto.Nazev;
            }
            else
            {
                MessageBox.Show($"Konec! Skóre: {skore} / {seznamMest.Count}");
            }
        }

        private void TlacitkoMesta_Click(object sender, RoutedEventArgs e)
        {
            Button kliknute = (Button)sender;
            Mesto vybrane = (Mesto)kliknute.Tag;

            if (vybrane.Nazev == aktualniHledaneMesto.Nazev)
            {
                MessageBox.Show("Správně!");
                skore++;
            }
            else
            {
                MessageBox.Show($"Špatně, hledali jsme {aktualniHledaneMesto.Nazev}");
            }

            txtSkore.Text = skore.ToString();
            kolo++;
            DalsiKolo();
        }

        private void MapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(mapCanvas);
            MessageBox.Show($"X={Math.Round(p.X)}, Y={Math.Round(p.Y)}");
        }
    }
}