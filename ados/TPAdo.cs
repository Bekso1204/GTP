using GTP.classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GTP.ados
{
    public class TPAdo
    {
        public static void AjouterTP(TP tp)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Tp(titre_tp,desc_titre,fk_id_promotion) VALUES(@param1,@param2,@param3)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = tp.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = tp.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = tp.Promotion.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<TP> VoirTPs()
        {
            List<TP> tps = new List<TP>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Tp";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TP tp = new TP();
                    tp.Id = reader.GetInt32(0);
                    tp.Titre = reader.GetString(1);
                    tp.Description = reader.GetString(2);
                    Promotion promotion = PromotionAdo.VoirPromotions().First(x => x.Id == reader.GetInt32(3));
                    promotion.Tps.Add(tp);
                    tp.Promotion = promotion;
                    tps.Add(tp);
                }
            }
            return tps;
        }

        public static void SupprimerTP(TP tp)
        {
            try
            {
                SqlConnection sqlc = Ado.OpenConnexion();
                string query = "DELETE FROM Tp WHERE id_tp=@param1";
                using (SqlCommand cmd = new SqlCommand(query, sqlc))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = tp.Id;
                    cmd.ExecuteNonQuery();
                }
                Ado.CloseConnexion(sqlc);
            }
            catch {
                string sMessageBoxText = "Vous ne pouvez supprimer ce TP, supprimez déjà les tâches associées.";
                string sCaption = "Erreur lors de la suppression";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.OK:

                        break;
                }
            }
        }

        public static void ModifierTP(TP tp)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Tp SET titre_tp = @param1, desc_titre = @param2,fk_id_promotion = @param3 WHERE id_tp = @param4;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = tp.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = tp.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = tp.Promotion.Id;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = tp.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<TP> GetTPsByUtilisateur(int utilisateurId)
        {
            List<TP> tps = new List<TP>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = @"SELECT TP.* FROM TP 
                     INNER JOIN Promotion ON TP.fk_id_promotion = Promotion.id_promo
                     INNER JOIN Utilisateur ON Promotion.id_promo = Utilisateur.fk_id_promotion
                     WHERE Utilisateur.id_utilisateur = @utilisateurId";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@utilisateurId", SqlDbType.Int).Value = utilisateurId;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TP tp = new TP
                        {
                            Id = reader.GetInt32(0),
                            Titre = reader.GetString(1),
                            // Ajoutez d'autres propriétés du TP si nécessaire
                        };
                        tps.Add(tp);
                    }
                }
            }
            Ado.CloseConnexion(sqlc);
            return tps;
        }

    }
}
