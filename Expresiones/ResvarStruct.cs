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
        private bool isStruct=false;
        private String id;
        private Parametros param;
        private String tempEncuentra;
        public ResvarStruct(String identificador) {
            this.id = identificador;
        }
        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            SimboloStruct sim = ent.getStruct(this.id);
            Generator3D generator = Generator3D.getInstance();
            if (sim==null) {
                
                return null;
            }
            String temp = generator.newTemporal();
            generator.addExpression(temp,"h","","", isFunc);
            foreach (Parametros param in sim.attributess) {
                switch (param.type.tipo) {
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.CHAR:
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        generator.addSetHeap("h",0, isFunc);
                        break;
                    case Simbolo.EnumTipoDato.STRING:
                        generator.addSetHeap("h", "-1", isFunc);
                        break;
                    case Simbolo.EnumTipoDato.ARRAY:
                        generator.addSetHeap("h", "-1", isFunc);
                        break;
                    case Simbolo.EnumTipoDato.OBJETO_TYPE:                    
                        isStruct = true;
                        this.param = param;
                        param.tipoStrucoArray = "instanciado";
                        this.tempEncuentra = Generator3D.getInstance().newTemporal();
                        Generator3D.getInstance().addExpression(tempEncuentra,"h","","", isFunc);
                        Generator3D.getInstance().freeTemp(tempEncuentra);
                        generator.addSetHeap("h","-1",isFunc);
                        break;
                }
                generator.nextHeap(isFunc);
            }
            if (isStruct == true) {
                Retornar ret = (new ResvarStruct(this.param.type.tipoId)).Compilar(ent, isFunc);
                generator.addSetHeap(tempEncuentra, ret.getValue(), isFunc);
            }
            return new Retornar(temp,true,Simbolo.EnumTipoDato.OBJETO_TYPE,null,new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE,sim.identifier,sim));
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
