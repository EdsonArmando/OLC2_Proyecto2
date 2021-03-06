using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Array : Instruccion
    {
        private String Nombre_id;
        private Object Tipo;
        private Expresion[,] valor;
        private bool isType;
        private String structsd;
        public Array(String id, Object tipo, Expresion[,] tamanio,bool isType)
        {
            this.Nombre_id = id;
            this.Tipo = tipo;
            this.valor = tamanio;
            this.isType = isType;
        }
        public Array(String id, Object tipo, Expresion[,] tamanio, bool isType, String structs)
        {
            this.Nombre_id = id;
            this.Tipo = tipo;
            this.valor = tamanio;
            this.isType = isType;
            this.structsd = structs;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Generator3D instance = Generator3D.getInstance();

            //Si se trata de Un Type
            
            /*
            * Insertar arreglo en 
            * tabla de Simbolos
            * 
            */
            //Significa que es de una dimension
            if (valor[0, 0] != null && valor[1, 0] == null)
            {
                Retornar PosiInicial = valor[0, 0].Compilar(ent, isFunc);
                Retornar PosFinal = valor[0, 1].Compilar(ent, isFunc);
                String temp = instance.newTemporal(); instance.freeTemp(temp);
                String tempAux = instance.newTemporal(); instance.freeTemp(tempAux);
                //Guardar donde Inician cada Arreglo
                String[] posiciones = new String[2];
                posiciones[0] = PosiInicial.getValue();
                posiciones[1] = PosFinal.getValue();
                if (isType)
                {
                    //sE TRATA DE sTRUCT
                    if (devTipoDato(Tipo.ToString()) == Simbolo.EnumTipoDato.NULL) {
                        Simbolo tempSimp2 = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE, this.structsd, null), posiciones, null, null,null,Ambito);
                        return null;
                    }
                    //Ingreso el Type a la Tabla de Simbolos
                    Simbolo tempSimp = ent.Insertar(Nombre_id,Simbolo.EnumTipoDato.ARRAY,false,false,new TipoDato(devTipoDato(Tipo.ToString()),null,null),posiciones,null,null,null,Ambito);
                    return null;
                }
                
                instance.addExpression(temp, PosFinal.getValue(), PosiInicial.getValue(), "-", isFunc);
                
                //Tamanio Total Arreglo + 2 porque en la pos 0 guardo el tamanio total del arreglo
                instance.addExpression(tempAux, temp, "2","+", isFunc);
                String tempInicio = instance.newTemporal();instance.freeTemp(tempInicio);
                //Valor actual del Heap
                instance.addExpression(tempInicio, "sh", "", "", isFunc);
                //Ingresar en la primera posicion el tamanio del arreglo
                String tempTotal = instance.newTemporal();instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal,tempAux,"1","-", isFunc);
                instance.addSetHeap(tempInicio,tempTotal, isFunc);
                instance.nextHeap(isFunc);
                //Temporal para el contadors
                String tempCont = instance.newTemporal();instance.freeTemp(tempCont);
                instance.addExpression(tempCont, "0", "", "", isFunc);
                //Empiezo a reservar los espacios del arreglo
                String labelInicio = instance.newLabel();
                String labelFin = instance.newLabel();
                instance.addLabel(labelInicio, isFunc);
                instance.addIf(tempAux,tempCont,"==",labelFin, isFunc);                
                instance.addExpression(tempCont,tempCont,"1","+", isFunc);
                instance.addSetHeap("sh", 0, isFunc);
                instance.nextHeap(isFunc);
                instance.addGoto(labelInicio, isFunc);
                instance.addLabel(labelFin, isFunc);
                //Guardo La Variable en mi tabla de Simbolos
                Simbolo sim = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()),null,null),posiciones,null,null,valor,Ambito);
                if (!sim.isGlobal)
                {
                    String temp2 = instance.newTemporal(); instance.freeTemp(temp2);
                    instance.addExpression(temp2, "sp", sim.posicion.ToString(), "+", isFunc);
                    instance.addSetStack(temp2, tempInicio, isFunc);
                }
                else {
                    instance.addSetStack(sim.posicion.ToString(), tempInicio, isFunc);
                }                

            }
            //Significa que es de dos dimensiones
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] == null)
            {
                instance.agregarComentario("Inicia Declaracion de arreglo", isFunc);
                //Donde Inicia cada dimension del arreglo
                Retornar PosiInicialX = valor[0, 0].Compilar(ent, isFunc);
                Retornar PosFinalX = valor[0, 1].Compilar(ent, isFunc);
                Retornar PosiInicialY = valor[1, 0].Compilar(ent, isFunc);
                Retornar PosFinalY = valor[1, 1].Compilar(ent, isFunc);
                //Guardo temporales donde inicia cada arreglo
                String[] posicionesX = new String[2];
                posicionesX[0] = PosiInicialX.getValue();
                posicionesX[1] = PosFinalX.getValue();
                String[] posicionesY = new String[2];
                posicionesY[0] = PosiInicialY.getValue();
                posicionesY[1] = PosFinalY.getValue();
                if (isType)
                {
                    //sE TRATA DE sTRUCT
                    if (devTipoDato(Tipo.ToString()) == Simbolo.EnumTipoDato.NULL)
                    {
                        Simbolo tempSimp2 = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE, this.structsd, null), posicionesX, posicionesY, null, null,Ambito);
                        return null;
                    }
                    //Ingreso el Type a la Tabla de Simbolos
                    Simbolo tempSimp = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()), null, null), posicionesX, posicionesY, null, null,Ambito);
                    return null;
                }
                //Tamanio del Vector
                //X
                String tempTamanio = instance.newTemporal();instance.freeTemp(tempTamanio);
                instance.addExpression(tempTamanio,PosFinalX.getValue(),PosiInicialX.getValue(),"-", isFunc);
                String Op1 = instance.newTemporal(); instance.freeTemp(tempTamanio);
                instance.addExpression(Op1, tempTamanio,"1", "+", isFunc);
                //Y
                String tempTamanioy = instance.newTemporal(); instance.freeTemp(tempTamanioy);
                instance.addExpression(tempTamanioy, PosFinalY.getValue(), PosiInicialY.getValue(), "-", isFunc);
                String Op2 = instance.newTemporal(); instance.freeTemp(tempTamanio);
                instance.addExpression(Op2, tempTamanioy, "1", "+", isFunc);
                //Total
                String tamanioTotal = instance.newTemporal(); instance.freeTemp(tamanioTotal);
                instance.addExpression(tamanioTotal, Op1, Op2, "*", isFunc);
                //Inicializacion del arreglo
                String tempAux = instance.newTemporal(); instance.freeTemp(tempAux);

                //Tamanio Total Arreglo + 2 porque en la pos 0 guardo el tamanio total del arreglo
                instance.addExpression(tempAux, tamanioTotal, "2", "+", isFunc);
                String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                //Valor actual del Heap
                instance.addExpression(tempInicio, "sh", "", "", isFunc);
                //Ingresar en la primera posicion el tamanio del arreglo
                String tempTotal = instance.newTemporal(); instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal, tempAux, "1", "-", isFunc);
                instance.addSetHeap(tempInicio, tamanioTotal, isFunc);
                instance.nextHeap( isFunc);
                //Temporal para el contadors
                String tempCont = instance.newTemporal(); instance.freeTemp(tempCont);
                instance.addExpression(tempCont, "0", "", "", isFunc);
                //Empiezo a reservar los espacios del arreglo
                String labelInicio = instance.newLabel();
                String labelFin = instance.newLabel();
                instance.addLabel(labelInicio, isFunc);
                instance.addIf(tempTotal, tempCont, "==", labelFin, isFunc);
                instance.addExpression(tempCont, tempCont, "1", "+", isFunc);
                instance.addSetHeap("sh", 0, isFunc);
                instance.nextHeap(isFunc);
                instance.addGoto(labelInicio, isFunc);
                instance.addLabel(labelFin, isFunc);
                //Guardo La Variable en mi tabla de Simbolos
                Simbolo sim = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()), null, null), posicionesX, posicionesY, null, valor,Ambito);
                if (!sim.isGlobal)
                {
                    String temp2 = instance.newTemporal(); instance.freeTemp(temp2);
                    instance.addExpression(temp2, "sp", sim.posicion.ToString(), "+", isFunc);
                    instance.addSetStack(temp2, tempInicio, isFunc);
                }
                else
                {
                    instance.addSetStack(sim.posicion.ToString(), tempInicio, isFunc);
                }
                instance.agregarComentario("Finaliza Declaracion de arreglo", isFunc);
            }
            //Significa que es de Tres dimensiones
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] != null)
            {
                instance.agregarComentario("Inicia Declaracion de arreglo", isFunc);
                //Donde Inicia cada dimension del arreglo
                Retornar PosiInicialX = valor[0, 0].Compilar(ent, isFunc);
                Retornar PosFinalX = valor[0, 1].Compilar(ent, isFunc);
                Retornar PosiInicialY = valor[1, 0].Compilar(ent, isFunc);
                Retornar PosFinalY = valor[1, 1].Compilar(ent, isFunc);
                Retornar PosiInicialZ = valor[2, 0].Compilar(ent, isFunc);
                Retornar PosFinalZ = valor[2, 1].Compilar(ent, isFunc);
                //Guardo temporales donde inicia cada arreglo
                String[] posicionesX = new String[2];
                posicionesX[0] = PosiInicialX.getValue();
                posicionesX[1] = PosFinalX.getValue();
                String[] posicionesY = new String[2];
                posicionesY[0] = PosiInicialY.getValue();
                posicionesY[1] = PosFinalY.getValue();
                String[] posicionesZ = new String[2];
                posicionesZ[0] = PosiInicialZ.getValue();
                posicionesZ[1] = PosFinalZ.getValue();
                if (isType)
                {
                    //sE TRATA DE sTRUCT
                    if (devTipoDato(Tipo.ToString()) == Simbolo.EnumTipoDato.NULL)
                    {
                        Simbolo tempSimp2 = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE, this.structsd, null), posicionesX, posicionesY, posicionesZ, null,Ambito);
                        return null;
                    }
                    //Ingreso el Type a la Tabla de Simbolos
                    Simbolo tempSimp = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()), null, null), posicionesX, posicionesY, posicionesZ, null,Ambito);
                    return null;
                }
                //Tamanio del Vector
                //X
                String tempTamanio = instance.newTemporal(); instance.freeTemp(tempTamanio);
                instance.addExpression(tempTamanio, PosFinalX.getValue(), PosiInicialX.getValue(), "-", isFunc);
                String Op1 = instance.newTemporal(); instance.freeTemp(Op1);
                instance.addExpression(Op1, tempTamanio, "1", "+", isFunc);
                //Y
                String tempTamanioy = instance.newTemporal(); instance.freeTemp(tempTamanioy);
                instance.addExpression(tempTamanioy, PosFinalY.getValue(), PosiInicialY.getValue(), "-", isFunc);
                String Op2 = instance.newTemporal(); instance.freeTemp(Op2);
                instance.addExpression(Op2, tempTamanioy, "1", "+", isFunc);
                //Z                
                String tempTamanioZ = instance.newTemporal(); instance.freeTemp(tempTamanioZ);
                instance.addExpression(tempTamanioZ, PosFinalZ.getValue(), PosiInicialZ.getValue(), "-", isFunc);
                String Op3 = instance.newTemporal(); instance.freeTemp(Op3);
                instance.addExpression(Op3, tempTamanioZ, "1", "+", isFunc);
                //Total
                String tamanioTotal = instance.newTemporal(); instance.freeTemp(tamanioTotal);
                instance.addExpression(tamanioTotal, Op1, Op2, "*",isFunc);
                String tamanioTotal2 = instance.newTemporal(); instance.freeTemp(tamanioTotal2);
                instance.addExpression(tamanioTotal2, Op3, tamanioTotal, "*", isFunc);
                //Inicializacion del arreglo
                String tempAux = instance.newTemporal(); instance.freeTemp(tempAux);

                //Tamanio Total Arreglo + 2 porque en la pos 0 guardo el tamanio total del arreglo
                instance.addExpression(tempAux, tamanioTotal2, "2", "+", isFunc);
                String tempInicio = instance.newTemporal(); instance.freeTemp(tempInicio);
                //Valor actual del Heap
                instance.addExpression(tempInicio, "sh", "", "", isFunc);
                //Ingresar en la primera posicion el tamanio del arreglo
                String tempTotal = instance.newTemporal(); instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal, tempAux, "1", "-", isFunc);
                instance.addSetHeap(tempInicio, tamanioTotal2, isFunc);
                instance.nextHeap(isFunc);
                //Temporal para el contadors
                String tempCont = instance.newTemporal(); instance.freeTemp(tempCont);
                instance.addExpression(tempCont, "0", "", "", isFunc);
                //Empiezo a reservar los espacios del arreglo
                String labelInicio = instance.newLabel();
                String labelFin = instance.newLabel();
                instance.addLabel(labelInicio, isFunc);
                instance.addIf(tempTotal, tempCont, "==", labelFin, isFunc);
                instance.addExpression(tempCont, tempCont, "1", "+", isFunc);
                instance.addSetHeap("sh", 0, isFunc);
                instance.nextHeap(isFunc);
                instance.addGoto(labelInicio, isFunc);
                instance.addLabel(labelFin, isFunc);
                //Guardo La Variable en mi tabla de Simbolos
                Simbolo sim = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()), null, null), posicionesX, posicionesY, posicionesZ, valor,Ambito);
                if (!sim.isGlobal)
                {
                    String temp2 = instance.newTemporal(); instance.freeTemp(temp2);
                    instance.addExpression(temp2, "sp", sim.posicion.ToString(), "+", isFunc);
                    instance.addSetStack(temp2, tempInicio, isFunc);
                }
                else
                {
                    instance.addSetStack(sim.posicion.ToString(), tempInicio, isFunc);
                }
                instance.agregarComentario("Finaliza Declaracion de arreglo", isFunc);
            }
            else
            {
                Form1.salidaConsola.AppendText("Ocurrio un error al guardar el array !!! \n");
                Sintactico.errores.AddLast(new Error("Semantico Ocurrio un error al guardar el array", Nombre_id, 1, 1));
            }
            return null;
        }
        public Simbolo.EnumTipoDato devTipoDato(String actual)
        {
            string valor = actual.ToString().Split(' ')[0];
            switch (valor.ToLower())
            {
                case "integer":
                case "int":
                    return Simbolo.EnumTipoDato.INT;
                case "type":
                    return Simbolo.EnumTipoDato.TYPE;
                case "array":
                    return Simbolo.EnumTipoDato.ARRAY;
                case "char":
                    return Simbolo.EnumTipoDato.CHAR;
                case "string":
                    return Simbolo.EnumTipoDato.STRING;
                case "double":
                    return Simbolo.EnumTipoDato.DOUBLE;
                case "real":
                    return Simbolo.EnumTipoDato.REAL;
                default:
                    return Simbolo.EnumTipoDato.NULL;
            }
        }
    }
}
