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

        public Array(String id, Object tipo, Expresion[,] tamanio)
        {
            this.Nombre_id = id;
            this.Tipo = tipo;
            this.valor = tamanio;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            /*
            * Insertar arreglo en 
            * tabla de Simbolos
            * 
            */
            //Significa que es de una dimension
            if (valor[0, 0] != null && valor[1, 0] == null)
            {
                Retornar PosiInicial = valor[0, 0].Compilar(ent);
                Retornar PosFinal = valor[0, 1].Compilar(ent);
                String temp = instance.newTemporal(); instance.freeTemp(temp);
                String tempAux = instance.newTemporal(); instance.freeTemp(tempAux);
                instance.addExpression(temp, PosFinal.getValue(), PosiInicial.getValue(), "-");
                //Guardar donde Inician cada Arreglo
                String[] posiciones = new String[2];
                posiciones[0] = PosiInicial.getValue();
                posiciones[1] = PosFinal.getValue();
                //Tamanio Total Arreglo + 2 porque en la pos 0 guardo el tamanio total del arreglo
                instance.addExpression(tempAux, temp, "2","+");
                String tempInicio = instance.newTemporal();instance.freeTemp(tempInicio);
                //Valor actual del Heap
                instance.addExpression(tempInicio, "h", "", "");
                //Ingresar en la primera posicion el tamanio del arreglo
                String tempTotal = instance.newTemporal();instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal,tempAux,"1","-");
                instance.addSetHeap(tempInicio,tempTotal);
                instance.nextHeap();
                //Temporal para el contadors
                String tempCont = instance.newTemporal();instance.freeTemp(tempCont);
                instance.addExpression(tempCont, "0", "", "");
                //Empiezo a reservar los espacios del arreglo
                String labelInicio = instance.newLabel();
                String labelFin = instance.newLabel();
                instance.addLabel(labelInicio);
                instance.addIf(tempAux,tempCont,"==",labelFin);                
                instance.addExpression(tempCont,tempCont,"1","+");
                instance.addSetHeap("h", 0);
                instance.nextHeap();
                instance.addGoto(labelInicio);
                instance.addLabel(labelFin);
                //Guardo La Variable en mi tabla de Simbolos
                Simbolo sim = ent.Insertar(Nombre_id, Simbolo.EnumTipoDato.ARRAY, false, false, new TipoDato(devTipoDato(Tipo.ToString()),null,null),posiciones,null,null);
                instance.addSetStack(sim.posicion.ToString(), tempInicio);

            }
            //Significa que es de dos dimension
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] == null)
            {

            }
            //Significa que es de Tres dimension
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] != null)
            {

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
