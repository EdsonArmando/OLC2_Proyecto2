using Proyecto1_Compi2;
using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class ResvarStruct : Expresion
    {
        private String id;
        public ResvarStruct(String identificador) {
            this.id = identificador;
        }
        public override Retornar Compilar(Entorno ent)
        {
            SimboloStruct sim = ent.getStruct(this.id);
            Generator3D generator = Generator3D.getInstance();
            if (sim==null) {
                Form1.salidaConsola.AppendText("No existe el Struc!!!\n");
                return null;
            }
            String temp = generator.newTemporal();
            generator.addExpression(temp,"h","","");
            foreach (Parametros param in sim.attributess) {
                switch (param.type.tipo) {
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.CHAR:
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        generator.addSetHeap("h",0);
                        break;
                    case Simbolo.EnumTipoDato.STRING:
                    case Simbolo.EnumTipoDato.OBJETO_TYPE:
                    case Simbolo.EnumTipoDato.ARRAY:
                        generator.addSetHeap("h","-1");
                        break;
                }
                generator.nextHeap();
            }
            return new Retornar(temp,true,Simbolo.EnumTipoDato.OBJETO_TYPE,null,new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE,sim.identifier,sim));
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
