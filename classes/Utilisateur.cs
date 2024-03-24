using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.classes
{
    public class Utilisateur
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Identifiant { get; set; }

        public Promotion Promotion { get; set; }

        public Dictionary<Tache,bool> Validations { get; set; }

        public Dictionary<Tache, bool> Aides { get; set; }

        public Utilisateur() {
            Validations = new Dictionary<Tache, bool>();
            Aides = new Dictionary<Tache, bool>();
        }
    }
}
