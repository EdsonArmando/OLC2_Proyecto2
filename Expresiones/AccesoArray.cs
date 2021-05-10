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

        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            Generator3D instance = Generator3D.getInstance();
            instance.agregarComentario("Inicia Acceso a Array", isFunc);
            Simbolo sim = ent.obtener(Nombre_id,ent);
            if (sim == null)
                return null;
            //Acceso a un array de una dimension
            if (valor[0] != null && valor[1] == null && valor[2] == null)
            {
                Retornar ret = this.valor[0].Compilar(ent,isFunc);
                if (sim.isGlobal)
                {
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicioFin = sim.posicion_X;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, sim.posicion, isFunc);
                    //Genero temporal Donde se va almacenar mi valor en el Heap
                    String tempValor = instance.newTemporal(); instance.freeTemp(tempValor);
                    String temp2 = instance.newTemporal(); instance.freeTemp(temp2);
                    instance.addExpression(temp2, ret.getValue(), posInicioFin[0], "-", isFunc);
                    instance.addExpression(tempValor, temp2, temp, "+", isFunc);
                    //Posicion en el arreglo total
                    String total = instance.newTemporal(); instance.freeTemp(total);
                    instance.addExpression(total, tempValor, "1", "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, total, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
                else {
                    String tempP = instance.newTemporal();
                    instance.freeTemp(tempP);
                    instance.addExpression(tempP, "p", sim.posicion.ToString(), "+", isFunc);
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicioFin = sim.posicion_X;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, tempP, isFunc);
                    //Genero temporal Donde se va almacenar mi valor en el Heap
                    String tempValor = instance.newTemporal(); instance.freeTemp(tempValor);
                    String temp2 = instance.newTemporal(); instance.freeTemp(temp2);
                    instance.addExpression(temp2, ret.getValue(), posInicioFin[0], "-", isFunc);
                    instance.addExpression(tempValor, temp2, temp, "+", isFunc);
                    //Posicion en el arreglo total
                    String total = instance.newTemporal(); instance.freeTemp(total);
                    instance.addExpression(total, tempValor, "1", "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, total, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
            }
            //Acceso a un array de dos dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] == null)
            {
                Retornar posx = this.valor[0].Compilar(ent, isFunc);
                Retornar posy = this.valor[1].Compilar(ent, isFunc);
                if (sim.isGlobal)
                {
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, sim.posicion, isFunc);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, temp, "1", "+", isFunc);
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-", isFunc);
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-", isFunc);
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialY[1], posInicialY[0], "-", isFunc);
                    String op2 = instance.newTemporal(); instance.freeTemp(op2);
                    instance.addExpression(op2, op1, "1", "+", isFunc);
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, tempRestaX, op2, "*", isFunc);
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op3, tempRestaY, "+", isFunc);
                    //Finaliza obtener la posicion
                    //Sumon el inicio del array mas la posicin donde se encuentra el valor
                    String posTotal2 = instance.newTemporal(); instance.freeTemp(posTotal2);
                    instance.addExpression(posTotal2, tempInicioArray, posTotal, "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, posTotal2, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
                else {
                    String tempP = instance.newTemporal();
                    instance.freeTemp(tempP);
                    instance.addExpression(tempP, "p", sim.posicion.ToString(), "+", isFunc);
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, tempP, isFunc);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, temp, "1", "+", isFunc);
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-", isFunc);
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-", isFunc);
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialY[1], posInicialY[0], "-", isFunc);
                    String op2 = instance.newTemporal(); instance.freeTemp(op2);
                    instance.addExpression(op2, op1, "1", "+", isFunc);
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op3, tempRestaX, op2, "*", isFunc);
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op3, tempRestaY, "+", isFunc);
                    //Finaliza obtener la posicion
                    //Sumon el inicio del array mas la posicin donde se encuentra el valor
                    String posTotal2 = instance.newTemporal(); instance.freeTemp(posTotal2);
                    instance.addExpression(posTotal2, tempInicioArray, posTotal, "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, posTotal2, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
            }
            //Acceso a un array de tres dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] != null)
            {
                instance.agregarComentario("Inicia Acceso a Array 3 dimensiones", isFunc);
                Retornar posx = this.valor[0].Compilar(ent, isFunc);
                Retornar posy = this.valor[1].Compilar(ent, isFunc);
                Retornar posz = this.valor[2].Compilar(ent, isFunc);
                if (sim.isGlobal)
                {
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    String[] posInicialZ = sim.posicion_Z;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, sim.posicion, isFunc);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, temp, "1", "+", isFunc);
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-", isFunc);
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-", isFunc);
                    String tempRestaZ = instance.newTemporal(); instance.freeTemp(tempRestaZ);
                    instance.addExpression(tempRestaZ, posz.getValue(), posInicialZ[0], "-", isFunc);
                    //Posicion Total                   
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialZ[1], posInicialZ[0], "-", isFunc);
                    String op2 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op2, op1, "1", "+", isFunc);
                    //------------------------------------------------------
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op1, posInicialY[1], posInicialY[0], "-", isFunc);
                    String op4 = instance.newTemporal(); instance.freeTemp(op4);
                    instance.addExpression(op4, op3, "1", "+", isFunc);
                    String op5 = instance.newTemporal(); instance.freeTemp(op5);
                    instance.addExpression(op5, op4, tempRestaX, "*", isFunc);
                    String op6 = instance.newTemporal(); instance.freeTemp(op6);
                    instance.addExpression(op6, tempRestaY, op5, "+", isFunc);
                    //---------------------------------------------------------
                    String op7 = instance.newTemporal(); instance.freeTemp(op7);
                    instance.addExpression(op7, op6, op2, "*", isFunc);
                    //---------------------------------------------------------
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op7, tempRestaZ, "+", isFunc);
                    //Finaliza obtener la posicion
                    //Sumon el inicio del array mas la posicin donde se encuentra el valor
                    String posTotal2 = instance.newTemporal(); instance.freeTemp(posTotal2);
                    instance.addExpression(posTotal2, tempInicioArray, posTotal, "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, posTotal2, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
                else
                {
                    String tempP = instance.newTemporal();
                    instance.freeTemp(tempP);
                    instance.addExpression(tempP, "p", sim.posicion.ToString(), "+", isFunc);
                    //Donde se almacenan las posiciones de Inicio y Fin
                    String[] posInicialX = sim.posicion_X;
                    String[] posInicialY = sim.posicion_Y;
                    String[] posInicialZ = sim.posicion_Z;
                    //Temp Guarda donde se encuentra el inicio del arreglo en el heap
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addGetStack(temp, tempP, isFunc);
                    //Salto una posicion
                    String tempInicioArray = instance.newTemporal(); instance.freeTemp(tempInicioArray);
                    instance.addExpression(tempInicioArray, temp, "1", "+", isFunc);
                    //Hacer resta entre la posicion iniciall del arreglo
                    String tempRestaX = instance.newTemporal(); instance.freeTemp(tempRestaX);
                    instance.addExpression(tempRestaX, posx.getValue(), posInicialX[0], "-", isFunc);
                    String tempRestaY = instance.newTemporal(); instance.freeTemp(tempRestaY);
                    instance.addExpression(tempRestaY, posy.getValue(), posInicialY[0], "-", isFunc);
                    String tempRestaZ = instance.newTemporal(); instance.freeTemp(tempRestaZ);
                    instance.addExpression(tempRestaZ, posz.getValue(), posInicialZ[0], "-", isFunc);
                    //Posicion Total                   
                    //Posicion Total                   
                    String op1 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op1, posInicialZ[1], posInicialZ[0], "-", isFunc);
                    String op2 = instance.newTemporal(); instance.freeTemp(op1);
                    instance.addExpression(op2, op1, "1", "+", isFunc);
                    //------------------------------------------------------
                    String op3 = instance.newTemporal(); instance.freeTemp(op3);
                    instance.addExpression(op1, posInicialY[1], posInicialY[0], "-", isFunc);
                    String op4 = instance.newTemporal(); instance.freeTemp(op4);
                    instance.addExpression(op4, op3, "1", "+", isFunc);
                    String op5 = instance.newTemporal(); instance.freeTemp(op5);
                    instance.addExpression(op5, op4, tempRestaX, "*", isFunc);
                    String op6 = instance.newTemporal(); instance.freeTemp(op6);
                    instance.addExpression(op6, tempRestaY, op5, "+", isFunc);
                    //---------------------------------------------------------
                    String op7 = instance.newTemporal(); instance.freeTemp(op7);
                    instance.addExpression(op7, op6, op2, "*", isFunc);
                    //---------------------------------------------------------
                    String posTotal = instance.newTemporal(); instance.freeTemp(posTotal);
                    instance.addExpression(posTotal, op7, tempRestaZ, "+", isFunc);
                    //Finaliza obtener la posicion
                    //Sumon el inicio del array mas la posicin donde se encuentra el valor
                    String posTotal2 = instance.newTemporal(); instance.freeTemp(posTotal2);
                    instance.addExpression(posTotal2, tempInicioArray, posTotal, "+", isFunc);
                    //Recupero mi valor del Heap
                    String tempPosFinal = instance.newTemporal();
                    instance.addGetHeap(tempPosFinal, posTotal2, isFunc);
                    return new Retornar(tempPosFinal, true, sim.tipoStruc.tipo, sim, new TipoDato(Simbolo.EnumTipoDato.ARRAY, null, null));
                }
                instance.agregarComentario("Finaliza Acceso a Array 3 dimensiones", isFunc);
            }
            instance.agregarComentario("Finaliza Acceso a Array", isFunc);
            return null;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }


    }
}
