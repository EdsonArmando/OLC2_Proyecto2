using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class Or : Expresion
    {
        Expresion izquierda;
        Expresion derecha;
        public Or(Expresion izq, Expresion dere)
        {
            this.izquierda = izq;
            this.derecha = dere;
        }
        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
            this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
            this.izquierda.truelabel = this.derecha.truelabel = this.truelabel;
            this.izquierda.falselabel = Generator3D.getInstance().newLabel();
            this.derecha.falselabel = this.falselabel;

            Retornar left = this.izquierda.Compilar(ent, isFunc);
            Generator3D.getInstance().addLabel(this.izquierda.falselabel, isFunc);
            Retornar right = this.derecha.Compilar(ent, isFunc);
            if (left.tipo == Simbolo.EnumTipoDato.BOOLEAN && right.tipo == Simbolo.EnumTipoDato.BOOLEAN)
            {
                Retornar retorno = new Retornar("", false, left.tipo,null);
                retorno.trueLabel = this.truelabel;
                retorno.falseLabel = this.derecha.falselabel;
                return retorno;
            }
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
