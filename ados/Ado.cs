using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.ados
{
    public static class Ado
    {
        public static SqlConnection OpenConnexion()
        {
            SqlConnection sqlc = new SqlConnection("Data Source=DELL-ALEXIAN\\SQLEXPRESS;Initial Catalog=GTP;Integrated Security=SSPI; TrustServerCertificate=True;");
            sqlc.Open();
            return sqlc;
        }
        public static void CloseConnexion(SqlConnection sqlc)
        {
            sqlc.Close();
        }
    }
}
