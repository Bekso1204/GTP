using GTP.ados;
using GTP.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GTP
{
    /// <summary>
    /// Logique d'interaction pour ElevesView.xaml
    /// </summary>
    public partial class ElevesView : UserControl
    {
        public ObservableCollection<Utilisateur> eleves;
        private ObservableCollection<Promotion> promotions;

        public Utilisateur UtilisateurMod;
        public ElevesView()
        {
            InitializeComponent();
            Charger();
            ChargerPromotions();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur u = lvEleves.SelectedItem as Utilisateur;
            string sMessageBoxText = "Voulez-vous vraiment supprimer l'élève sélectionnée ?";
            string sCaption = "Gestion des TPs";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    UtilisateurAdo.SupprimerUtilisateur(u);
                    break;
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
            Charger();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            UtilisateurMod.Nom = txtNom.Text;
            UtilisateurMod.Prenom = txtPrenom.Text;
            UtilisateurMod.Identifiant = txtID.Text;

            if (cmbPromotion.SelectedItem != null && cmbPromotion.SelectedItem is Promotion selectedPromotion)
            {
                UtilisateurMod.Promotion = selectedPromotion;
            }

            UtilisateurAdo.ModifierUtilisateur(UtilisateurMod);

            txtNom.Text = "";
            txtPrenom.Text = "";
            txtID.Text = "";
            cmbPromotion.SelectedIndex = -1; 
            btnAjouter.IsEnabled = true;
            btnSupprimer.IsEnabled = false;
            btnModifier.IsEnabled = false;
            Charger();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur u = new Utilisateur();
            u.Nom = txtNom.Text;
            u.Prenom = txtPrenom.Text;
            u.Identifiant = txtID.Text;

            if (cmbPromotion.SelectedItem != null && cmbPromotion.SelectedItem is Promotion selectedPromotion)
            {
                u.Promotion = selectedPromotion;
            }

            UtilisateurAdo.AjouterUtilisateur(u);

            txtNom.Text = "";
            txtPrenom.Text = "";
            txtID.Text = "";
            cmbPromotion.SelectedIndex = -1;

            Charger();
        }

        private void lvEleves_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UtilisateurMod = lvEleves.SelectedItem as Utilisateur;
            txtNom.Text = UtilisateurMod.Nom;
            txtPrenom.Text = UtilisateurMod.Prenom.ToString();
            txtID.Text = UtilisateurMod.Identifiant.ToString();
            if (UtilisateurMod.Promotion != null)
            {
                cmbPromotion.SelectedItem = promotions.FirstOrDefault(p => p.Id == UtilisateurMod.Promotion.Id);
            }

            btnModifier.IsEnabled = true;
            btnSupprimer.IsEnabled = true;
            btnAjouter.IsEnabled = false;
        }

        private void Charger()
        {
            eleves = new ObservableCollection<Utilisateur>(UtilisateurAdo.VoirUtilisateurs());
            lvEleves.ItemsSource = eleves;
        }

        private void ChargerPromotions()
        {
            promotions = new ObservableCollection<Promotion>(PromotionAdo.VoirPromotions());
            cmbPromotion.ItemsSource = promotions;
        }
    }
}
