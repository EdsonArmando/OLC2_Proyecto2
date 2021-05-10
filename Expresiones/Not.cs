using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class Not : Expresion
    {
        Expresion izquierda;
        Expresion derecha;
        public Not(Expresion izq, Expresion dere)
        {
            this.izquierda = izq;
            this.derecha = dere;
        }
        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
            this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;

            this.izquierda.truelabel = this.falselabel;
            this.izquierda.falselabel = this.truelabel;
            Retornar value = this.izquierda.Compilar(ent, isFunc);
            if (value.tipo == Simbolo.EnumTipoDato.BOOLEAN)
            {
                Retornar retorno = new Retornar("", false, value.tipo,null);
                retorno.trueLabel = this.truelabel;
                retorno.falseLabel = this.falselabel;
                return retorno;
            }
            return null;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
