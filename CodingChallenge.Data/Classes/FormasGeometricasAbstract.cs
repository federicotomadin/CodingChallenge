using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes
{
    public abstract class FormasGeometricasAbstract
    {

        //public abstract decimal Lado { get; set; }
        public abstract EnumFormas NombreForma { get;  set; }




        public abstract decimal CalcularArea();

        public abstract decimal CalcularPerimetro();


    }
}
