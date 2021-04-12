using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using Proyecto2_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class AsignacionTypeObjcet : Instruccion
    {
        public LinkedList<String> listId;
        public Expresion valor;
        public AsignacionTypeObjcet(LinkedList<String> listId, Expresion valor) {
            this.listId = listId;
            this.valor = valor;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            LinkedList<String> tempId = new LinkedList<String>();
            foreach (String ST in listId)
            {
                tempId.AddLast(ST);
            }
            Retornar value = valor.Compilar(ent);
            Simbolo sim = ent.obtener(tempId.ElementAt(0),ent);
            tempId.RemoveFirst();
            setExpresion(tempId,sim,value,Ambito,ent,null,null);
            return null;
        }
        public Retornar setExpresion(LinkedList<String> accesos, Simbolo sim, Retornar res, String Ambito, Entorno ent, SimboloStruct structSim,Retornar retor)
        {
            TipoDato tip = sim.tipoStruc;
            SimboloStruct simStruct = tip.sim;
            int index = simStruct.getPosAttribute(accesos.ElementAt(0));
            Parametros param = simStruct.getAttribute(accesos.ElementAt(0));            
            if (accesos.Count >=2) {
                if (param.type.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE) {
                    Retornar ret = (new ResvarStruct(param.type.tipoId)).Compilar(ent);
                    String tempaux = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                    String temp = Generator3D.getInstance().newTemporal();
                    Generator3D.getInstance().addGetStack(tempaux, sim.posicion);
                    Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                    Generator3D.getInstance().addSetHeap(temp, ret.getValue());
                    accesos.RemoveFirst();
                    setExpresion(accesos,sim,res,Ambito,ent,structSim,ret);
                    return ret;
                    //Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                }
                accesos.RemoveFirst();
                //tExpresion();
            }
            else {
                if (retor != null) {                                             
                    String temp2 = Generator3D.getInstance().newTemporal();
                    Generator3D.getInstance().addExpression(temp2, retor.getValue(), index.ToString(), "+");
                    Generator3D.getInstance().addSetHeap(temp2, res.getValue());
                    return null;
                }
                String tempaux = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                String temp = Generator3D.getInstance().newTemporal();
                Generator3D.getInstance().addGetStack(tempaux, sim.posicion);
                Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                Retornar rettemp = new Retornar(temp, true, param.type.tipo, new Simbolo(param.type.tipo, accesos.ElementAt(0), index, false, false, true, param.type,null,null,null));
                Simbolo simTemp = (Simbolo)rettemp.sim;
                if (simTemp.isHeap)
                {
                    if (rettemp.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                    {

                    }
                    else
                    {
                        Generator3D.getInstance().addSetHeap(rettemp.getValue(), res.getValue());
                    }
                }
            }
            return null;
        }    
    }
}
