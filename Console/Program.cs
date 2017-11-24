using KoelhoRapido.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var endereco1 = new Endereco("rua Cícero tristão,220", "Juiz de fora", "MG", "Brasil", "");
            var endereco2 = new Endereco("rua santo antonio, 700", "Juiz de fora", "MG", "Brasil", "36015-001");
            //var endereco2 = new Endereco("R. Quintino Bocaiúva, 2339 - São Cristóvão", "Porto Velho", "RO", "Brasil", "76804-076");

            Endereco.DistanceBetween(endereco1, endereco2);
        }
    }
}
