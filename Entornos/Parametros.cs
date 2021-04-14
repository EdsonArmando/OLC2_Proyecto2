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
        public String tipoStrucoArray;
        public Parametros(String iden, TipoDato tipo, String tipoArray_Struct) {
            this.id = iden;
            this.type = tipo;
            this.tipoStrucoArray = tipoArray_Struct;
        }
        public String getUnicType() {
        if(this.type.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE){
            return this.type.tipoId;
        }
        return this.type.tipo.ToString();
        }
    }
}
