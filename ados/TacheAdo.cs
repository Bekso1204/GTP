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
    public class TacheAdo
    {
        public static void AjouterTache(Tache t)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Tache(titre_tache,desc_tache,fk_id_tp) VALUES(@param1,@param2,@param3)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = t.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = t.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = t.Tp.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<Tache> VoirTache()
        {
            List<Tache> taches = new List<Tache>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Tache";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tache t = new Tache();
                    t.Id = reader.GetInt32(0);
                    t.Titre = reader.GetString(1);
                    t.Description = reader.GetString(2);
                    TP Tp = TPAdo.VoirTPs().First(x => x.Id == reader.GetInt32(3));
                    Tp.Taches.Add(t);
                    t.Tp = Tp;
                    taches.Add(t);
                }
            }
            return taches;
        }

        public static void SupprimerTache(Tache t)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Tache WHERE id_tache=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = t.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierTache(Tache t)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Tache SET titre_tache = @param1, desc_tache = @param2,fk_id_tp = @param3 WHERE id_tache = @param4;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = t.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = t.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = t.Tp.Id;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = t.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }
    }
}
