using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class AccesoObject : Abstracto.Expresion
    {
        Expresion izquierdo;
        Expresion derecho;
        public AccesoObject(Expresion izquierdo,Expresion derecho) {
            this.izquierdo = izquierdo;
            this.derecho = derecho;
        }

        public override Retornar Compilar(Entorno ent,bool isFunc)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }                
    }
}
