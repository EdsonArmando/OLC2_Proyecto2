using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Write : Instruccion
    {
        LinkedList<Abstracto.Expresion> valores;
        int fila, columna;
        public Write(LinkedList<Abstracto.Expresion> valores, int fila, int columna)
        {
            this.valores = valores;
            this.fila = fila;
            this.columna = columna;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Retornar value = null;
            foreach (Expresion exp in valores)
            {
                value = exp.Compilar(ent, isFunc);
                switch (value.tipo)
                {
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.INT:
                        Generator3D.getInstance().addPrint("\"%.1f\"", "(double)" + value.getValue(), isFunc);
                        break;
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String labelTemp = Generator3D.getInstance().newLabel();
                        Generator3D.getInstance().addLabel(value.trueLabel, isFunc);
                        Generator3D.getInstance().addPrintTrue(isFunc);
                        Generator3D.getInstance().addGoto(labelTemp, isFunc);
                        Generator3D.getInstance().addLabel(value.falseLabel, isFunc);
                        Generator3D.getInstance().addPrintFalse(isFunc);
                        Generator3D.getInstance().addLabel(labelTemp, isFunc);
                        break;
                    case Simbolo.EnumTipoDato.OBJETO_TYPE:
                        Generator3D.getInstance().addPrint("\"%.1f\"", value.getValue(), isFunc);
                        break;
                    case Simbolo.EnumTipoDato.ARRAY:
                        Generator3D.getInstance().addPrint("\"%.1f\"", value.getValue(), isFunc);
                        break;
                    case Simbolo.EnumTipoDato.STRING:
                        Generator3D.getInstance().nextEnt(ent.pos, isFunc);
                        Generator3D.getInstance().addSetStack("p", value.getValue(), isFunc);
                        Generator3D.getInstance().addCall("Native_PrintString", isFunc);
                        Generator3D.getInstance().antEnt(ent.pos, isFunc);
                        break;
                }
            }
            return null;
        }
    }
}
