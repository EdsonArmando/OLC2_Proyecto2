using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
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
            Generator3D instance = Generator3D.getInstance();
            Simbolo sim = ent.obtener(Nombre_id,ent);
            if (sim == null)
                return null;
            //Acceso a un array de una dimension
            if (valor[0] != null && valor[1] == null && valor[2] == null)
            {
                Retornar ret = this.valor[0].Compilar(ent);
                if (sim.isGlobal) {
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicioFin = sim.posicion_X;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal();instance.freeTemp(temp);                    
                    instance.addGetStack(temp, sim.posicion);
                    //Genero temporal Donde se va almacenar mi valor en el Heap
                    String tempValor = instance.newTemporal();instance.freeTemp(tempValor);
                    String temp2 = instance.newTemporal();instance.freeTemp(temp2);
                    instance.addExpression(temp2,ret.getValue(),posInicioFin[0],"-");
                    instance.addExpression(tempValor,temp2,temp,"+");
                    //Posicion en el arreglo total
                    String total = instance.newTemporal();instance.freeTemp(total);
                    instance.addExpression(total,tempValor,"1","+");
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal,total);
                    return new Retornar(tempPosFinal, true,Simbolo.EnumTipoDato.ARRAY,sim,new TipoDato(Simbolo.EnumTipoDato.ARRAY,null,null));
                }
            }
            //Acceso a un array de dos dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] == null)
            {
                
            }
            //Acceso a un array de tres dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] != null)
            {
                
            }
            return null;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }


    }
}
