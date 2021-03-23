using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class AccesoArray : Abstracto.Expresion
    {
        private String Nombre_id;
        private Expresion[] valor = new Expresion[2];

        public AccesoArray(String id, Expresion[] tamanio)
        {
            this.Nombre_id = id;
            this.valor = tamanio;
        }

        public override Retornar Compilar(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }


    }
}
