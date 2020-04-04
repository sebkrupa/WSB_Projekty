using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;

namespace KursyWalut_SebKrupa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SprawdzPoprawnosc(); //pobieranie kursów z serwera NBP
        }
        public ObservableCollection<KursyNBP> kursyWalutNBP = new ObservableCollection<KursyNBP>(); //kolekcja kursów walut na której opiera się program

        private void Button_ZaktualizujBaze(object sender, RoutedEventArgs e) //wymuszenie pobrania danych z serwera NBP
        {
            SprawdzPoprawnosc();
        }

        private string numer_tabeli = string.Empty;
        private string data_notowania = string.Empty;
        private string data_publikacji = string.Empty;

        private ObservableCollection<KursyNBP> PobierzKursy() //pobieranie danych do kolekcji
        {
            XDocument kursyLista = XDocument.Load(@"http://www.nbp.pl/kursy/xml/LastC.xml"); //łączenie się z serwerem i pobieranie danych
            numer_tabeli = kursyLista.Root.Elements("numer_tabeli").Select(x => x.Value).FirstOrDefault().ToString(); //kategoryzowanie tabeli xml
            data_notowania = kursyLista.Root.Elements("data_notowania").Select(x => x.Value).FirstOrDefault().ToString();
            data_publikacji = kursyLista.Root.Elements("data_publikacji").Select(x => x.Value).FirstOrDefault().ToString();

            numerTabeli.Content = $"Numer tabeli Narodowego Banku Polskiego: {numer_tabeli}"; //wyświetlenie danych dotyczących daty notowania, publikacji oraz numeru tabeli
            dataNotowania.Content = $"Notowanie z dnia: {data_notowania}";
            dataPublikacji.Content = $"Data publikacji: {data_publikacji}";

            var kursy = from c in XElement.Load(kursyLista.CreateReader()).Elements("pozycja") select c; //zaciąganie danych do tymczasowego xml'a

            ObservableCollection<KursyNBP> lista = new ObservableCollection<KursyNBP>(); //utworzenie nowej kolekcji danych

            foreach (var item in kursy) //uzupełnianie kolekcji danych kursami walut
                lista.Add(new KursyNBP()
                {
                    nazwa_waluty = item.Element("nazwa_waluty").Value.ToString(),
                    przelicznik = Convert.ToDouble(item.Element("przelicznik").Value),
                    kod_waluty = item.Element("kod_waluty").Value.ToString(),
                    kurs_kupna = Convert.ToDouble(item.Element("kurs_kupna").Value),
                    kurs_sprzedazy = Convert.ToDouble(item.Element("kurs_sprzedazy").Value),
                    kurs_sredni = (Convert.ToDouble(item.Element("kurs_kupna").Value) + Convert.ToDouble(item.Element("kurs_sprzedazy").Value))/2 //obliczanie kursu średniego
                });

            lista.Add(new KursyNBP { nazwa_waluty = "Polski Złoty", kod_waluty = "PLN", kurs_kupna = 1, kurs_sprzedazy = 1, przelicznik = 1, kurs_sredni=1 }); //dodanie waluty PLN do kolekcji
            return lista;
        }

        private void SprawdzPoprawnosc() //główna metoda pobierająca dane z serwera NBP
        {
            try //próba pobrania danych z serwera
            {
                kursyWalutNBP.Clear();
                kursyWalutNBP = PobierzKursy();
                if (!kursyWalutNBP.Any())
                    throw new Exception();
            }
            catch //uzupełnienie kolekcji przykładowymi danymi w przypadku problemów z połączeniem
            {
                //informacja o problemie z połączeniem dla użytkownika
                MessageBox.Show("Nie udało się pobrać aktualnych kursów walut. \n" +
                    "Do działania programu niezbędne jest połączenie z internetem!\n " +
                    "Baza została wypełniona kilkoma przykładowymi kursami. \n" +
                    "Aby odświeżyć bazę po uzyskaniu połączenia z internetem, należy wybrać opcję 'Pobierz Aktualne Kursy Walut'");
                WypelnijPrzykladowymiDanymi(); //metoda wypełniająca kolekcję przykładowymi danymi
            }
            finally
            {
                dgKursy.ItemsSource = kursyWalutNBP; //wiązanie kolekcji z tabelą UI
                UzupelnijCombo(cbWaluta1); //wypełnienie pól w ComboBoxach
                UzupelnijCombo(cbWaluta2);
                cbWaluta1.SelectedValue = "PLN"; //wybranie domyślnej waluty1
                cbWaluta2.SelectedValue = "EUR"; //wybranie domyslnej waluty2
            }
        }

        private void UzupelnijCombo(ComboBox cb)
        {
            //uzupełnianie kontrolki ComboBox kodami walut
            foreach (var item in kursyWalutNBP)
                cb.Items.Add(item.kod_waluty);
        }



        private void WypelnijPrzykladowymiDanymi()
        {
            //wypełnianie tabeli przykładowymi danymi
            kursyWalutNBP.Clear();
            kursyWalutNBP.Add(new KursyNBP { nazwa_waluty = "dolar amerykański", kod_waluty = "USD", kurs_kupna = 3.7388, kurs_sprzedazy = 3.8144, przelicznik = 1, kurs_sredni=3.77 });
            kursyWalutNBP.Add(new KursyNBP { nazwa_waluty = "dolar australijski", kod_waluty = "AUD", kurs_kupna = 2.5842, kurs_sprzedazy = 2.6364, przelicznik = 1, kurs_sredni=2.60 });
            kursyWalutNBP.Add(new KursyNBP { nazwa_waluty = "Polski Złoty", kod_waluty = "PLN", kurs_kupna = 1, kurs_sprzedazy = 1, przelicznik = 1, kurs_sredni=1 });
            kursyWalutNBP.Add(new KursyNBP { nazwa_waluty = "euro", kod_waluty = "EUR", kurs_kupna = 4.2146, kurs_sprzedazy = 4.2998, przelicznik = 1, kurs_sredni=4.25 });

        }

        private void TxtWaluta1_TextChanged(object sender, TextChangedEventArgs e) //dynamiczne obliczanie kursu po zmianie wartości w polu tekstowym waluta1
        {
            PrzeliczKurs();
        }

        private void TxtWaluta2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CbWaluta1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) //dynamiczne przeliczanie waluty po zmianie waluty
        {
            PrzeliczKurs();
        }

        private void PrzeliczKurs()
        {
            try
            {
                string waluta1 = cbWaluta1.SelectedValue.ToString(); //przypisanie wybranych przez użytkownika walut to zmiennych pomocniczych
                string waluta2 = cbWaluta2.SelectedValue.ToString();

                double przelicznik1 = kursyWalutNBP.Where(x => x.kod_waluty == waluta1).Select(x => x.przelicznik).FirstOrDefault(); //pobieranie niezbędnych do obliczeń danych z kolekcji
                double przelicznik2 = kursyWalutNBP.Where(x => x.kod_waluty == waluta2).Select(x => x.przelicznik).FirstOrDefault();
                double valueWaluta1 = kursyWalutNBP.Where(x => x.kod_waluty == waluta1).Select(x => x.kurs_sredni).FirstOrDefault() / przelicznik1;
                double valueWaluta2 = kursyWalutNBP.Where(x => x.kod_waluty == waluta2).Select(x => x.kurs_sredni).FirstOrDefault() / przelicznik2;

                txtWaluta2.Text = (Math.Round((valueWaluta1 * Convert.ToDouble(txtWaluta1.Text)) / valueWaluta2,2)).ToString(); //wzór na przeliczenie waluty
            }
            catch
            { }
        }

        private void MenuItem_UkryjKursy(object sender, RoutedEventArgs e) //metoda odpowiadająca za ukrywanie tabeli z kursami walut
        {
            MenuItem box = (MenuItem)sender;
            if ((bool)box.IsChecked) //jeżeli pole jest zaznaczone, zmień szerokość okna oraz ukryj tabelę
            {
                spKursy.Visibility = Visibility.Collapsed;
                spPrzelicznik.SetValue(Grid.ColumnSpanProperty, 2);
                okno.Width = 400;
            }
            else //jeżeli pole nie jest zaznaczone, zwiększ szerokość okna oraz pokaż tabelę
            {
                spKursy.Visibility = Visibility.Visible;
                spPrzelicznik.SetValue(Grid.ColumnSpanProperty, 1);
                okno.Width = 800;
            }
        }

        private void MenuItem_ZamknijProgram(object sender, RoutedEventArgs e) //zamykanie programu
        {
            this.Close();
        }

        private void MenuItem_Click_OpisProgramu(object sender, RoutedEventArgs e) //wyświetlenie opisu programu oraz twórców aplikacji
        {
            MessageBox.Show("Kalkulator walut automatycznie pobierający aktualne kursy z serwera Narodowego Banku Polskiego.\n\n" +
                "Program przygotowany przez:\n" +
                "Sebastian Krupa\n" +
                "Marcin Kriger\n" +
                "---\n" +
                "WSB - Informatyka, studia zaoczne, Rok 2 (semestr 4)\n" +
                "2019");
        }
    }

    public class KursyNBP //obiekt przechowujący dane konkretnej waluty
    {
        [DisplayName("Nazwa")]
        public string nazwa_waluty { get; set; }
        [DisplayName("Przelicznik")]
        public double przelicznik { get; set; }
        [DisplayName("Kurs Średni")]
        public double kurs_sredni { get; set; }
        [DisplayName("Kod Waluty")]
        public string kod_waluty { get; set; }
        [DisplayName("Kurs Kupna")]
        public double kurs_kupna { get; set; }
        [DisplayName("Kurs Sprzedaży")]
        public double kurs_sprzedazy { get; set; }
    }
}
