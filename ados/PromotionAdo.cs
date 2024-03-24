using GTP.classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.ados
{
    public static class PromotionAdo
    {
        public static void AjouterPromotion(Promotion p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Promotion(nom_promo,annee_debut,annee_fin) VALUES(@param1,@param2,@param3)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = p.AnneeDebut;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = p.AnneeFin;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<Promotion> VoirPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Promotion";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Promotion p = new Promotion();
                    p.Id = reader.GetInt32(0);
                    p.Nom = reader.GetString(1);
                    p.AnneeDebut = reader.GetInt32(2);
                    p.AnneeFin = reader.GetInt32(3);   
                    promotions.Add(p);
                }
            }
            return promotions;
        }

        public static void SupprimerPromotion(Promotion p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Promotion WHERE id_promo=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierPromotion(Promotion p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Promotion SET nom_promo = @param1, annee_debut = @param2,annee_fin = @param3 WHERE id_promo = @param4;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = p.AnneeDebut;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = p.AnneeFin;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }
    }
}
