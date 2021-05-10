using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Asignacion : Instruccion
    {
        String id;
        Expresion valor;
        private Expresion posX;
        private Expresion posY;
        private Expresion posZ;

        public Asignacion(String id, Expresion posX, Expresion posy, Expresion posZ, Expresion val)
        {
            this.id = id;
            this.posX = posX;
            this.posY = posy;
            this.posZ = posZ;
            this.valor = val;
        }
        public Asignacion(String id, Expresion valor) {
            this.id = id;
            this.valor = valor;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            if (id.ToLower() == Ambito.ToLower())
            {
                Return returnNombre = new Return(valor);
                returnNombre.Compilar(ent, Ambito, AST);
                return null;
            }
            Generator3D instance = Generator3D.getInstance();
            Simbolo sim = ent.obtener(id,ent);
            Retornar value = valor.Compilar(ent);            
            if (sim.isGlobal)
            {
                //Arreglo de una dimension
                if (posX != null && posY == null && posZ == null)
                {
                    String[] posInicialX = sim.posicion_X;
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo Global");
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String tempInicio = instance.newTemporal();instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio,sim.posicion);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempResta = instance.newTemporal(); instance.freeTemp(tempResta);
                    instance.addExpression(tempResta, posx.getValue(),posInicialX[0], "-");
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal();instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, tempResta, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());
                    return null;
                }
                //Arreglo de dos dimensiones
                else if (posX != null && posY != null && posZ == null)
                {
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo Global");
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    Retornar posy = posY.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio, sim.posicion);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-");
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-");
                    //Posicion Total                   
                    String op1 = instance.newTemporal();instance.freeTemp(op1);
                    instance.addExpression(op1,posInicialY[1], posInicialY[0],"-");
                    String op2 = instance.newTemporal(); instance.freeTemp(op2);
                    instance.addExpression(op2,op1,"1","+");
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, tempRestaX,op2, "*");
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal,op3, tempRestaY,"+");
                    //Finaliza obtener la posicion
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal(); instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, posTotal, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());

                    return null;
                }
                //Arreglo de 3 dimensiones
                else if (posX != null && posY != null && posZ != null)
                {
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    String[] posInicialZ = sim.posicion_Z;
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo Global");
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    Retornar posy = posY.Compilar(ent);
                    Retornar posz = posZ.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio, sim.posicion);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-");
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-");
                    String tempRestaZ = instance.newTemporal(); instance.freeTemp(tempRestaZ);
                    instance.addExpression(tempRestaZ, posz.getValue(), posInicialZ[0], "-");
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialZ[1], posInicialZ[0], "-");
                    String op2 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op2, op1, "1", "+");
                    //------------------------------------------------------
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, posInicialY[1], posInicialY[0], "-");
                    String op4 = instance.newTemporal(); instance.freeTemp(op4);
                    instance.addExpression(op4, op3, "1", "+");
                    String op5 = instance.newTemporal(); instance.freeTemp(op5);
                    instance.addExpression(op5, op4, tempRestaX, "*");
                    String op6 = instance.newTemporal(); instance.freeTemp(op6);
                    instance.addExpression(op6, tempRestaY, op5, "+");
                    //---------------------------------------------------------
                    String op7 = instance.newTemporal(); instance.freeTemp(op7);
                    instance.addExpression(op7, op6, op2, "*");
                    //---------------------------------------------------------
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op7, tempRestaZ, "+");
                    //Finaliza obtener la posicion
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal(); instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, posTotal, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());

                    return null;
                }
                else {
                    if (value.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                    {
                        String templabel = instance.newLabel();
                        instance.addLabel(value.trueLabel);
                        instance.addSetStack(sim.posicion.ToString(), "1");
                        instance.addGoto(templabel);
                        instance.addLabel(value.falseLabel);
                        instance.addSetStack(sim.posicion.ToString(), "0");
                        instance.addLabel(templabel);
                        return null;
                    }
                    else {
                        instance.addSetStack(sim.posicion.ToString(), value.getValue());
                        return null;
                    }                    
                }                
            }
            else {
                //Arreglo de una dimension
                if (posX != null && posY == null && posZ == null)
                {
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo no Global");
                    String temp = instance.newTemporal();
                    instance.freeTemp(temp);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    String[] posInicialX = sim.posicion_X;    
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio, temp);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempResta = instance.newTemporal(); instance.freeTemp(tempResta);
                    instance.addExpression(tempResta, posx.getValue(), posInicialX[0], "-");
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal(); instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, tempResta, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());
                    return null;
                }
                //Arreglo de dos dimensiones
                else if (posX != null && posY != null && posZ == null)
                {
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo no Global");
                    String temp = instance.newTemporal();
                    instance.freeTemp(temp);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo Global");
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    Retornar posy = posY.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio, temp);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-");
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-");
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialY[1], posInicialY[0], "-");
                    String op2 = instance.newTemporal(); instance.freeTemp(op2);
                    instance.addExpression(op2, op1, "1", "+");
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, tempRestaX, op2, "*");
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op3, tempRestaY, "+");
                    //Finaliza obtener la posicion
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal(); instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, posTotal, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());
                    return null;
                }
                //Arreglo de 3 dimensiones
                else if (posX != null && posY != null && posZ != null)
                {
                    /*
                     * Sim
                     */
                    Simbolo sim2 = (Simbolo)value.sim;
                    ResvarStruct reservar = new ResvarStruct(sim2.tipoStruc.sim.identifier);
                    Retornar ret = reservar.Compilar(ent);
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    String[] posInicialZ = sim.posicion_Z;
                    instance.agregarComentario("Iniciando Proceso de Asignacion de arreglo Global");
                    //Posicion en la que se va a insertar en el arreglo
                    Retornar posx = posX.Compilar(ent);
                    Retornar posy = posY.Compilar(ent);
                    Retornar posz = posZ.Compilar(ent);
                    //Valor en la poscion x del arreglo
                    Retornar val = valor.Compilar(ent);
                    //Obtener el valor del Stack
                    String temp = instance.newTemporal();
                    instance.freeTemp(temp);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                    instance.addGetStack(tempInicio, temp);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, tempInicio, "1", "+");
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-");
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-");
                    String tempRestaZ = instance.newTemporal(); instance.freeTemp(tempRestaZ);
                    instance.addExpression(tempRestaZ, posz.getValue(), posInicialZ[0], "-");
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialZ[1], posInicialZ[0], "-");
                    String op2 = instance.newTemporal(); instance.freeTemp(op2);
                    instance.addExpression(op2, op1, "1", "+");
                    //------------------------------------------------------
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, posInicialY[1], posInicialY[0], "-");
                    String op4 = instance.newTemporal(); instance.freeTemp(op4);
                    instance.addExpression(op4, op3, "1", "+");
                    String op5 = instance.newTemporal(); instance.freeTemp(op5);
                    instance.addExpression(op5, op4, tempRestaX, "*");
                    String op6 = instance.newTemporal(); instance.freeTemp(op6);
                    instance.addExpression(op6, tempRestaY, op5, "+");
                    //---------------------------------------------------------
                    String op7 = instance.newTemporal(); instance.freeTemp(op7);
                    instance.addExpression(op7, op6, op2, "*");
                    //---------------------------------------------------------
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op7, tempRestaZ, "+");
                    //Finaliza obtener la posicion
                    //Temporal donde se Guardar posicion a insertar en el Heap
                    String tempHeap = instance.newTemporal(); instance.freeTemp(tempHeap);
                    instance.addExpression(tempHeap, tempInicioArray, posTotal, "+");
                    //String tempHeap2 = instance.newTemporal(); instance.freeTemp(tempHeap2);
                    //instance.addExpression(tempHeap2, tempHeap, "1", "+");
                    //Ingreso el valor en el Heap
                    instance.addSetHeap(tempHeap, val.getValue());
                    //Seteo Valor nuevo a la variable en el stack
                    String tempNuevo = instance.newTemporal();instance.freeTemp(tempNuevo);
                    instance.addExpression(tempNuevo,"p",sim2.posicion.ToString(),"+");
                    instance.addSetStack(tempNuevo,ret.getValue());
                    return null;
                }
                else if (value.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                {

                    String templabel = instance.newLabel();
                    instance.freeTemp(templabel);
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    
                    instance.addLabel(value.trueLabel);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    instance.addSetStack(temp, "1");
                    instance.addGoto(templabel);
                    instance.addLabel(value.falseLabel);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    instance.addSetStack(temp, "0");
                    instance.addLabel(templabel);
                }
                else {
                    String temp = instance.newTemporal();
                    instance.freeTemp(temp);
                    instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                    instance.addSetStack(temp, value.getValue());
                }               
                return null;
            }            
        }       
    }
}
