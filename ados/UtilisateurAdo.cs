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
    public class UtilisateurAdo
    {
        public static void AjouterUtilisateur(Utilisateur u)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Utilisateur(nom,prenom,identifiant,fk_id_promotion) VALUES(@param1,@param2,@param3,@param4)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = u.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = u.Prenom;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = u.Identifiant;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = u.Promotion.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<Utilisateur> VoirUtilisateurs()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Utilisateur";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Utilisateur u = new Utilisateur();
                    u.Id = reader.GetInt32(0);
                    u.Nom = reader.GetString(1);
                    u.Prenom = reader.GetString(2);
                    u.Identifiant = reader.GetString(3);
                    Promotion promotion = PromotionAdo.VoirPromotions().First(x => x.Id == reader.GetInt32(4));
                    promotion.Utilisateurs.Add(u);
                    u.Promotion = promotion;
                    utilisateurs.Add(u);
                }
            }
            return utilisateurs;
        }

        public static void SupprimerUtilisateur(Utilisateur u)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Utilisateur WHERE id_utilisateur=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = u.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierUtilisateur(Utilisateur u)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Utilisateur SET nom = @param1, prenom = @param2,identifiant = @param3, fk_id_promotion = @param4 WHERE id_utilisateur = @param5;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = u.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = u.Prenom;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = u.Identifiant;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = u.Promotion.Id;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = u.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static Utilisateur GetUtilisateurByNomEtIdentifiant(string nom, string identifiant)
        {
            Utilisateur utilisateur = null;
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Utilisateur WHERE nom = @nom AND identifiant = @identifiant";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = nom;
                cmd.Parameters.Add("@identifiant", SqlDbType.VarChar).Value = identifiant;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        utilisateur = new Utilisateur();
                        utilisateur.Id = reader.GetInt32(0);
                        utilisateur.Nom = reader.GetString(1);
                        utilisateur.Prenom = reader.GetString(2);
                        utilisateur.Identifiant = reader.GetString(3);
                    }
                }
            }
            Ado.CloseConnexion(sqlc);
            return utilisateur;
        }
    }
}
