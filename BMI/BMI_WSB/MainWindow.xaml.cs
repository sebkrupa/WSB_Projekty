using System;
using System.Windows;

namespace BMI_WSB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ObliczBMI(object sender, RoutedEventArgs e) //metoda wywoływana po kliknięciu przycisku
        {
            if (Double.TryParse(txtWaga.Text, out double result) && Double.TryParse(txtWzrost.Text, out double result1)) //weryfikowanie czy wprowadzone przez użytkownika dane są poprawne (sąliczbami typu double)
            {
                wynik okno = new wynik(Convert.ToDouble(txtWzrost.Text) / 100, Convert.ToDouble(txtWaga.Text)); //przekazanie wzrostu (konwersja z cm na m) oraz wagi
                okno.ShowDialog(); //przedstawienie wyniku użytkownikowi w nowym oknie
            }
            else
                MessageBox.Show("Podane wartości są nieprawidłowe!"); //komunikat w przypadku wykrycia nieprawidłowych danych

        }
    }
}
