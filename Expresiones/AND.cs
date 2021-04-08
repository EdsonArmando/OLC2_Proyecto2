using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class AND : Expresion
    {
        Expresion izquierda;
        Expresion derecha;
        public AND(Expresion izq, Expresion dere) {
            this.izquierda = izq;
            this.derecha = dere;
        }
        public override Retornar Compilar(Entorno ent)
        {
            Generator3D generator = Generator3D.getInstance();
            this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
            this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;

            this.izquierda.truelabel = generator.newLabel();
            this.derecha.truelabel = this.truelabel;
            this.izquierda.falselabel = this.derecha.falselabel = this.falselabel;

            Retornar izq = this.izquierda.Compilar(ent);
            generator.addLabel(this.izquierda.truelabel);
            Retornar dere = this.derecha.Compilar(ent);

            /*Evaluar tipos */
            Retornar ret = new Retornar("",false,izq.tipo,"");
            ret.trueLabel = this.truelabel;
            ret.falseLabel = this.derecha.falselabel;
            return ret;           
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
