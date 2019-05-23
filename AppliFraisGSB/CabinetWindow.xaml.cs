using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace AppliFraisGSB
{
    /// <summary>
    /// Logique d'interaction pour CabinetWindow.xaml
    /// </summary>
    public partial class CabinetWindow : Window
    {
        string connectionString = AppContextUtility.ConnexionBDD;
        public CabinetWindow()
        {
            InitializeComponent();
            dgUsers.ItemsSource = this.GetListCabinet();
            this.SetListRegion();
            this.SetListDepartement();
   
           

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            HomeWindow main = new HomeWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            DialogCabinet.IsOpen = true;
            ButtonCreate.Visibility = Visibility.Visible;
            dgUsers.IsEnabled = false;
            ButtonAdd1.IsEnabled = false;
            ButtonBack1.IsEnabled = false;
            dialogLabel.Content = "Ajouter un cabinet";
        }
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            DialogCabinet.IsOpen = false;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.IsEnabled = true;
            ButtonAdd1.IsEnabled = true;
            ButtonBack1.IsEnabled = true;
            ResetForm();

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Visite selectedClient = (Visite)dgUsers.SelectedItem;
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("Delete FROM visites WHERE id_visite = @id_visite", connection);
            cmd.Parameters.AddWithValue("@id_visite", selectedClient.id.ToString());
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }



        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AppContextUtility.Role != "medecin")
            {
                DialogCabinet.IsOpen = true;
                dialogLabel.Content = "Modifier un cabinet";
                ButtonUpdate.Visibility = Visibility.Visible;
                dgUsers.IsEnabled = false;
                ButtonAdd1.IsEnabled = false;
                ButtonBack1.IsEnabled = false;
                Cabinet selectedClient = (Cabinet)dgUsers.SelectedItem;
                id.Text = selectedClient.IdCabinet.ToString();
                adresse.Text = selectedClient.Adresse;
                region.Text = selectedClient.Region;
                departement.Text = selectedClient.Departement;
                commune.Text = selectedClient.Commune;
                code_postal.Text = selectedClient.CodePostal;
                lat.Text = selectedClient.Lat;
                lng.Text = selectedClient.Lng;
            }

        }

        private List<Cabinet> GetListCabinet()
        {
            List<Cabinet> cabinets = new List<Cabinet>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cabinet;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                cabinets.Add(new Cabinet((int)read["id_cabinet"],read["adresse"].ToString(), read["lat"].ToString(), read["lng"].ToString(), read["region"].ToString(), read["departement"].ToString(), read["commune"].ToString(), read["code_postal"].ToString()));
            }
            connection.Close();
            return cabinets;
        }

        private void CreateCabinet(object sender, RoutedEventArgs e)
        {
            Cabinet cabinet = new Cabinet(adresse.Text, lat.Text, lng.Text, region.Text, departement.Text, commune.Text, code_postal.Text);
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("INSERT INTO cabinet(adresse, region, departement, commune, code_postal, lat, lng) VALUES (@adresse, @region, @departement, @commune, @code_postal, @lat, @lng);", connection);
                cmd.Parameters.AddWithValue("@adresse", cabinet.Adresse);
                cmd.Parameters.AddWithValue("@region", cabinet.Region);
                cmd.Parameters.AddWithValue("@departement", cabinet.Departement);
                cmd.Parameters.AddWithValue("@commune", cabinet.Commune);
                cmd.Parameters.AddWithValue("@code_postal", cabinet.CodePostal);
                cmd.Parameters.AddWithValue("@lat", cabinet.Lat);
                cmd.Parameters.AddWithValue("@lng", cabinet.Lng);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                ResetForm();
                DialogCabinet.IsOpen = false;
                dgUsers.IsEnabled = true;
                ButtonAdd1.IsEnabled = true;
                ButtonBack1.IsEnabled = true;
                ButtonCreate.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Hidden;
                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = this.GetListCabinet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void UpdateCabinet(object sender, RoutedEventArgs e)
        {
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("UPDATE cabinet set adresse = @adresse, region = @region, departement = @departement, commune = @commune, code_postal = @code_postal, lat = @lat, lng = @lng WHERE id_cabinet = @id", connection);
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@adresse", adresse.Text);
            cmd.Parameters.AddWithValue("@region", region.Text);
            cmd.Parameters.AddWithValue("@departement", departement.Text);
            cmd.Parameters.AddWithValue("@commune", commune.Text);
            cmd.Parameters.AddWithValue("@code_postal", code_postal.Text);
            cmd.Parameters.AddWithValue("@lat", lat.Text);
            cmd.Parameters.AddWithValue("@lng", lng.Text);


            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            ResetForm();
            DialogCabinet.IsOpen = false;
            dgUsers.IsEnabled = true;
            ButtonAdd1.IsEnabled = true;
            ButtonBack1.IsEnabled = true;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = this.GetListCabinet();

        }

        private void SetListRegion()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT Reg_Nom FROM region;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                region.Items.Add(read["Reg_Nom"]);
            }

            connection.Close();
        }

        private void SetListDepartement()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT Dpt_Nom FROM departement;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                departement.Items.Add(read["Dpt_Nom"]);
            }

            connection.Close();
        }


        private void ResetForm()
        {
            adresse.Text = "";
            region.Text = "";
            departement.Text = "";
            commune.Text = "";
            code_postal.Text = "";
            lat.Text = "";
            lng.Text = "";
        }
    }

}
