using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class Diferenciacion : Expresion
    {
        Expresion izquierda;
        Expresion derecha;
        public Diferenciacion(Expresion izq, Expresion dere)
        {
            this.izquierda = izq;
            this.derecha = dere;
        }
        public override Retornar Compilar(Entorno ent)
        {
            Retornar left = this.izquierda.Compilar(ent);
            Retornar right = this.derecha.Compilar(ent);
            Generator3D generator = Generator3D.getInstance();
            switch (left.tipo) {
                case Simbolo.EnumTipoDato.INT:
                case Simbolo.EnumTipoDato.DOUBLE:
                case Simbolo.EnumTipoDato.CHAR:
                    switch (right.tipo)
                    {
                        case Simbolo.EnumTipoDato.INT:
                        case Simbolo.EnumTipoDato.CHAR:
                        case Simbolo.EnumTipoDato.DOUBLE:
                            this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                            this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;
                            generator.addIf(left.getValue(), right.getValue(), "!=", this.truelabel);
                            generator.addGoto(this.falselabel);
                            Retornar retorno = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN,null);
                            retorno.trueLabel = this.truelabel;
                            retorno.falseLabel = this.falselabel;
                            return retorno;
                        default:
                            break;
                    }
                    break;
            }
            return null;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
