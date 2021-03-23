using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    abstract class PlantillaFuncion :  Expresion
    {
        public abstract void setParametros(LinkedList<Expresion> lista);
    }
}
