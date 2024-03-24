using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.classes
{
    public class Jalon
    {
        public int Id {  get; set; }

        public string Titre { get; set; }

        public List<Tache> Taches { get; set; }

        public TP Tp { get; set; }

        public Jalon() { 
            Taches = new List<Tache>();
        }

    }
}
