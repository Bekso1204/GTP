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
    /// Logique d'interaction pour PromotionsView.xaml
    /// </summary>
    public partial class PromotionsView : UserControl
    {
        public ObservableCollection<Promotion> promos;

        public Promotion promotionMod;
        public PromotionsView()
        {
            InitializeComponent();
            Charger();

        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Promotion p = lvPromotions.SelectedItem as Promotion;
            string sMessageBoxText = "Voulez-vous vraiment supprimer la promotion sélectionnée ?";
            string sCaption = "Gestion des TPs";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    PromotionAdo.SupprimerPromotion(p);
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
            promotionMod.Nom = txtNom.Text;
            promotionMod.AnneeDebut = Convert.ToInt32(txtAD.Text);
            promotionMod.AnneeFin = Convert.ToInt32(txtAF.Text);

            PromotionAdo.ModifierPromotion(promotionMod);

            txtNom.Text = "";
            txtAD.Text = "";
            txtAF.Text = "";
            btnAjouter.IsEnabled = true;
            btnSupprimer.IsEnabled = false;
            btnModifier.IsEnabled = false;
            Charger();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Promotion p = new Promotion();
            p.Nom = txtNom.Text;
            p.AnneeDebut = Convert.ToInt32(txtAD.Text);
            p.AnneeFin = Convert.ToInt32(txtAF.Text);

            PromotionAdo.AjouterPromotion(p);

            txtNom.Text = "";
            txtAD.Text = "";
            txtAF.Text = "";

            Charger();
        }

        private void lvPromotions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            promotionMod = lvPromotions.SelectedItem as Promotion;
            txtNom.Text = promotionMod.Nom;
            txtAD.Text = promotionMod.AnneeDebut.ToString();
            txtAF.Text = promotionMod.AnneeFin.ToString();

            btnModifier.IsEnabled = true;
            btnSupprimer.IsEnabled = true;
            btnAjouter.IsEnabled = false;
        }

        public void Charger()
        {
            promos = new ObservableCollection<Promotion>(PromotionAdo.VoirPromotions());
            lvPromotions.ItemsSource = promos;
        }
    }
}