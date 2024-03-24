using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.classes
{
    public class Promotion
    {
        public int Id {  get; set; }

        public string Nom {  get; set; }

        public int AnneeDebut { get; set; }

        public int AnneeFin { get; set; }

        public List<Utilisateur> Utilisateurs { get; set; }

        public List<TP> Tps { get; set; }

        public Promotion()
        {
            Utilisateurs = new List<Utilisateur>();
            Tps = new List<TP>();   
        }
    }
}
