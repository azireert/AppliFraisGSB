using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    class Cabinet
    {
        public int IdCabinet { get; set; }
        public string Adresse { get; set;}
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Region { get; set; }
        public string Departement { get; set; }
        public string Commune { get; set; }
        public string CodePostal { get; set; }

        public Cabinet(int IdCabinet, string adresse, string Lat, string Lng, string Region, string Departement, string Commune, string CodePostal)
        {
            this.IdCabinet = IdCabinet;
            this.Adresse = adresse;
            this.Lat = Lat;
            this.Lng = Lng;
            this.Region = Region;
            this.Departement = Departement;
            this.CodePostal = CodePostal;
            this.Commune = Commune;
        }

        public Cabinet(string adresse, string Lat, string Lng, string Region, string Departement, string Commune, string CodePostal)
        {
            this.Adresse = adresse;
            this.Lat = Lat;
            this.Lng = Lng;
            this.Region = Region;
            this.Departement = Departement;
            this.CodePostal = CodePostal;
            this.Commune = Commune;
        }
    }
}
