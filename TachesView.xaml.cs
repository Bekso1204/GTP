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
    /// Logique d'interaction pour TachesView.xaml
    /// </summary>
    public partial class TachesView : UserControl
    {
        public ObservableCollection<Tache> taches;
        private ObservableCollection<TP> tps;

        public Tache TacheMod;
        public TachesView()
        {
            InitializeComponent();
            Charger();
            ChargerTPs();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Tache t = lvTaches.SelectedItem as Tache;
            string sMessageBoxText = "Voulez-vous vraiment supprimer la tache sélectionné ?";
            string sCaption = "Gestion des taches";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    TacheAdo.SupprimerTache(t);
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
            TacheMod.Titre = txtTitre.Text;
            TacheMod.Description = txtDescription.Text;
            if (cmbTP.SelectedItem != null && cmbTP.SelectedItem is TP selectedTP)
            {
                TacheMod.Tp = selectedTP;
            }

            TacheAdo.ModifierTache(TacheMod);

            txtTitre.Text = "";
            txtDescription.Text = "";
            cmbTP.SelectedIndex = -1;
            btnAjouter.IsEnabled = true;
            btnSupprimer.IsEnabled = false;
            btnModifier.IsEnabled = false;
            Charger();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Tache Tache = new Tache();
            Tache.Titre = txtTitre.Text;
            Tache.Description = txtDescription.Text;

            if (cmbTP.SelectedItem != null && cmbTP.SelectedItem is TP selectedTP)
            {
                Tache.Tp = selectedTP;
            }

            TacheAdo.AjouterTache(Tache);

            txtTitre.Text = "";
            txtDescription.Text = "";
            cmbTP.SelectedIndex = -1;

            Charger();
        }

        private void lvTaches_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TacheMod = lvTaches.SelectedItem as Tache;
            txtTitre.Text = TacheMod.Titre;
            txtDescription.Text = TacheMod.Description.ToString();

            if (TacheMod.Tp != null)
            {
                cmbTP.SelectedItem = tps.FirstOrDefault(p => p.Id == TacheMod.Tp.Id);
            }

            btnModifier.IsEnabled = true;
            btnSupprimer.IsEnabled = true;
            btnAjouter.IsEnabled = false;
        }

        private void Charger()
        {
            taches = new ObservableCollection<Tache>(TacheAdo.VoirTache());
            lvTaches.ItemsSource = taches;
        }

        private void ChargerTPs()
        {
            tps = new ObservableCollection<TP>(TPAdo.VoirTPs());
            cmbTP.ItemsSource = tps;
        }
    }
}