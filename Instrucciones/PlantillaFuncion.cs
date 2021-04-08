using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    abstract class PlantillaFuncion :  Expresion
    {
        public TipoDato tipo;
        public String id;
        public LinkedList<Parametros> param;
        public String idUnico;
        public int tamanio;
        public abstract void setParametros(LinkedList<Expresion> lista);
    }
}
