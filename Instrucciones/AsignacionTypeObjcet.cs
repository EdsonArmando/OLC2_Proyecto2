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
            Generator3D instan = Generator3D.getInstance();

            LinkedList<String> tempId = new LinkedList<String>();
            foreach (String ST in listId)
            {
                tempId.AddLast(ST);
            }
            instan.agregarComentario("Empieza Asignacion");
            Retornar value = valor.Compilar(ent);
            Simbolo sim = ent.obtener(tempId.ElementAt(0),ent);
            tempId.RemoveFirst();
            setExpresion(tempId,sim,value,Ambito,ent,sim.tipoStruc.sim,null);
            instan.agregarComentario("Finaliza Asignacion");
            return null;
        }
        public Retornar setExpresion(LinkedList<String> accesos, Simbolo sim, Retornar res, String Ambito, Entorno ent, SimboloStruct structSim,Retornar retor)
        {
            
            int index = structSim.getPosAttribute(accesos.ElementAt(0));
            Parametros param = structSim.getAttribute(accesos.ElementAt(0));            
            if (accesos.Count >=2) {
                if (param.type.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE) {
                    Retornar ret = null;
                    if (param.tipoStrucoArray == "instanciado") {
                        SimboloStruct simStruct = ent.getStruct(param.type.tipoId);
                        TipoDato tipo = new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE,simStruct.identifier,simStruct);
                        //Obtengo donde empieza el struct
                        String tempaux2 = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux2);
                        String temp2 = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(temp2);
                        String temp4 = Generator3D.getInstance().newTemporal();
                        if (!sim.isGlobal)
                        {
                            String tempaux = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);                           
                            String tempauxHeap = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                            Generator3D.getInstance().addExpression(tempauxHeap, "p", sim.posicion.ToString(), "+");
                            Generator3D.getInstance().addGetStack(tempaux2, tempauxHeap);
                            Generator3D.getInstance().addExpression(temp2, tempaux2, index.ToString(), "+");
                            Generator3D.getInstance().addGetHeap(temp4, temp2);
                            ret = new Retornar(temp4, true, Simbolo.EnumTipoDato.OBJETO_TYPE, null, tipo);
                            accesos.RemoveFirst();
                            setExpresion(accesos, sim, res, Ambito, ent, ret.tip.sim, ret);
                            return ret;
                        }
                        else {
                            Generator3D.getInstance().addGetStack(tempaux2, sim.posicion);
                            Generator3D.getInstance().addExpression(temp2, tempaux2, index.ToString(), "+");
                            Generator3D.getInstance().addGetHeap(temp4, temp2);
                            ret = new Retornar(temp4, true, Simbolo.EnumTipoDato.OBJETO_TYPE, null, tipo);
                            accesos.RemoveFirst();
                            setExpresion(accesos, sim, res, Ambito, ent, ret.tip.sim, ret);
                            return ret;
                        }                        
                    }
                    else {
                        ret = (new ResvarStruct(param.type.tipoId)).Compilar(ent);
                        param.tipoStrucoArray = "instanciado";
                    }
                    if (!sim.isGlobal)
                    {
                        String tempaux = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                        String temp = Generator3D.getInstance().newTemporal();
                        String tempauxHeap = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                        Generator3D.getInstance().addExpression(tempauxHeap, "p", sim.posicion.ToString(), "+");
                        Generator3D.getInstance().addGetStack(tempaux, tempauxHeap);
                        Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                        Generator3D.getInstance().addSetHeap(temp, ret.getValue());
                        accesos.RemoveFirst();
                        setExpresion(accesos, sim, res, Ambito, ent, ret.tip.sim, ret);
                        return ret;
                    }
                    else {
                        String tempaux = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                        String temp = Generator3D.getInstance().newTemporal();
                        Generator3D.getInstance().addGetStack(tempaux, sim.posicion);
                        Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                        Generator3D.getInstance().addSetHeap(temp, ret.getValue());
                        accesos.RemoveFirst();
                        setExpresion(accesos, sim, res, Ambito, ent, ret.tip.sim, ret);
                        return ret;
                    }
                    
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
                Simbolo simTemp = null;
                Retornar rettemp = null;
                if (!sim.isGlobal)
                {
                    String tempauxHeap = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                    Generator3D.getInstance().addExpression(tempauxHeap,"p",sim.posicion.ToString(),"+");
                    String tempStack = Generator3D.getInstance().newTemporal(); Generator3D.getInstance().freeTemp(tempaux);
                    Generator3D.getInstance().addGetStack(tempStack,tempauxHeap);
                    Generator3D.getInstance().addExpression(temp, tempStack, index.ToString(), "+");
                    rettemp = new Retornar(temp, true, param.type.tipo, new Simbolo(param.type.tipo, accesos.ElementAt(0), index, false, false, true, param.type, null, null, null,null));
                    simTemp = (Simbolo)rettemp.sim;
                }
                else {
                    Generator3D.getInstance().addGetStack(tempaux, sim.posicion);
                    Generator3D.getInstance().addExpression(temp, tempaux, index.ToString(), "+");
                    rettemp = new Retornar(temp, true, param.type.tipo, new Simbolo(param.type.tipo, accesos.ElementAt(0), index, false, false, true, param.type, null, null, null,null));
                    simTemp = (Simbolo)rettemp.sim;
                }                               
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
