using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BMI_WSB
{
    public partial class wynik : Window
    {
        public wynik(double wzrost, double masa) //przyjęcie argumentów z okna MainWindow
        {
            InitializeComponent();

            double bmi = ObliczBMI(wzrost, masa); //obliczenie wskaźnika BMI

            string wynik = ZakresBMI(bmi); //zwrócenie komunikatu na temat wskaźnika (czy waga jest ok czy nok)

            if(wynik!="WAGA PRAWIDŁOWA") //w przypadku wagi innej niż prawidłowa, zmienia się kolor tekstu oraz obrazek
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("nok.png", UriKind.Relative);
                bi.EndInit();
                buzka.Source = bi;
                txtWynik.Foreground = Brushes.Red;
            }
            txtWynik.Text = $" Twoje BMI={Math.Round(bmi,2)} - {wynik}"; //wypisanie wyniku na ekranie oraz zakrąglenie BMI do dwóch cyfr po przecinku
        }

        private double ObliczBMI(double wzrost, double masa) //metoda zwracająca wartość BMI
        {
            return (masa / Math.Pow(wzrost,2));
        }

        private string ZakresBMI(double bmi) //metoda zwracająca wynik tekstowy, na podstawie konkretnej wartości wskaźnika BMI
        {
            string wynik = string.Empty;
            switch(bmi)
            {
                case var exp when bmi < 16.0:
                    wynik = "Wygłodzenie!";
                    break;
                case var exp when bmi < 16.99:
                    wynik = "Wychudzenie!";
                    break;
                case var exp when bmi < 18.49:
                    wynik = "Niedowaga!";
                    break;
                case var exp when bmi < 24.99:
                    wynik = "WAGA PRAWIDŁOWA";
                    break;
                case var exp when bmi < 29.99:
                    wynik = "Nadwaga!";
                    break;
                case var exp when bmi < 34.99:
                    wynik = "1 stopień otyłości!";
                    break;
                case var exp when bmi < 39.99:
                    wynik = "2 stopień otyłości!";
                    break;
                case var exp when bmi > 40.0:
                    wynik = "Nadwaga!";
                    break;
                default:
                    wynik = "Nie udało się określić wyniku obliczeń... :(";
                    break;
            }
            return wynik;

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //zamykanie całego okna z wynikiem po kliknięci LPM w jego polu
        {
            this.Close();
        }
    }
}
