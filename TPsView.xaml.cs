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
    /// Logique d'interaction pour TPsView.xaml
    /// </summary>
    public partial class TPsView : UserControl
    {
        public ObservableCollection<TP> tps;
        private ObservableCollection<Promotion> promotions;

        public TP TPMod;
        public TPsView()
        {
            InitializeComponent();
            Charger();
            ChargerPromotions();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            TP p = lvTPs.SelectedItem as TP;
            string sMessageBoxText = "Voulez-vous vraiment supprimer le TP sélectionné ?";
            string sCaption = "Gestion des TPs";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    TPAdo.SupprimerTP(p);
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
            TPMod.Titre = txtTitre.Text;
            TPMod.Description = txtDescription.Text;
            if (cmbPromotion.SelectedItem != null && cmbPromotion.SelectedItem is Promotion selectedPromotion)
            {
                TPMod.Promotion = selectedPromotion;
            }

            TPAdo.ModifierTP(TPMod);

            txtTitre.Text = "";
            txtDescription.Text = "";
            cmbPromotion.SelectedIndex = -1;
            btnAjouter.IsEnabled = true;
            btnSupprimer.IsEnabled = false;
            btnModifier.IsEnabled = false;
            Charger();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            TP tp = new TP();
            tp.Titre = txtTitre.Text;
            tp.Description = txtDescription.Text;

            if (cmbPromotion.SelectedItem != null && cmbPromotion.SelectedItem is Promotion selectedPromotion)
            {
                tp.Promotion = selectedPromotion;
            }

            TPAdo.AjouterTP(tp);

            txtTitre.Text = "";
            txtDescription.Text = "";
            cmbPromotion.SelectedIndex = -1;

            Charger();
        }

        private void lvTPs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TPMod = lvTPs.SelectedItem as TP;
            txtTitre.Text = TPMod.Titre;
            txtDescription.Text = TPMod.Description.ToString();

            if (TPMod.Promotion != null)
            {
                cmbPromotion.SelectedItem = promotions.FirstOrDefault(p => p.Id == TPMod.Promotion.Id);
            }

            btnModifier.IsEnabled = true;
            btnSupprimer.IsEnabled = true;
            btnAjouter.IsEnabled = false;
        }

        private void Charger()
        {
            tps = new ObservableCollection<TP>(TPAdo.VoirTPs());
            lvTPs.ItemsSource = tps;
        }

        private void ChargerPromotions()
        {
            promotions = new ObservableCollection<Promotion>(PromotionAdo.VoirPromotions());
            cmbPromotion.ItemsSource = promotions;
        }
    }
}