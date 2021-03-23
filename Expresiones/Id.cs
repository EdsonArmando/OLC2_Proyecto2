using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Id : Abstracto.Expresion
    {
        public string id;
        public Id(string id)
        {
            this.id = id;
        }

        public override Retornar Compilar(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            return this.tipo;
        }

    }
}
