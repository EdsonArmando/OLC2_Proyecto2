using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Entornos
{
    class TipoDato
    {
        public Simbolo.EnumTipoDato tipo;
        public String tipoId;
        public SimboloStruct sim;
        public TipoDato(Simbolo.EnumTipoDato tipo,String typeI, SimboloStruct strucSim) {
            this.tipo = tipo;
            this.tipoId = typeI;
            this.sim = strucSim;
        }
    }
}
