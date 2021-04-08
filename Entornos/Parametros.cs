using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Entornos
{
    class Parametros
    {
        public String id;
        public TipoDato type;
        public Parametros(String iden, TipoDato tipo) {
            this.id = iden;
            this.type = tipo;
        }
        public String getUnicType() {
        if(this.type.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE){
            return this.type.tipoId;
        }
        return this.type.tipo.ToString();
        }
    }
}
