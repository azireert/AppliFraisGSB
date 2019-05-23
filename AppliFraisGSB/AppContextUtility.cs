using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliFraisGSB
{
    public class AppContextUtility
    {
        public static string Role { get; set; }
        public static string Nom { get; set; }
        public static string Prenom { get; set; }
        public static int Id { get; set; }
        public static string ConnexionBDD { get; set; } = "SERVER=192.168.0.1;DATABASE=gsb;UID=siosuiviA;PASSWORD=sio;";


    }
}
