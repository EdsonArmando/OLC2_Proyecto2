using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class Cadena : Expresion
    {
        private Simbolo.EnumTipoDato tipoDat;
        private String valoCadenar;
        public Cadena(Simbolo.EnumTipoDato tipodato, String val) {
            this.tipoDat = tipodato;
            this.valoCadenar = val;
        }
        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            Generator3D generator = Generator3D.getInstance();
            String temp = generator.newTemporal();
            generator.addExpression(temp, "h","","", isFunc);
            for (int i = 0; i < this.valoCadenar.Length; i++)
            {
                generator.addSetHeap('h', Encoding.ASCII.GetBytes(valoCadenar.Substring(i,1))[0].ToString(), isFunc);
                generator.nextHeap(isFunc);
            }
            generator.addSetHeap("h", "-1", isFunc);
            generator.nextHeap(isFunc);
            return new Retornar(temp,true,Simbolo.EnumTipoDato.STRING,null,new TipoDato(this.tipoDat,"String",null));
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
