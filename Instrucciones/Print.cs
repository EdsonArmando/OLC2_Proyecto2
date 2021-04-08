using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Print : Instruccion
    {
        LinkedList<Abstracto.Expresion> valores;
        int fila, columna;
        public Print(LinkedList<Abstracto.Expresion> valores, int fila, int columna) {
            this.valores = valores;
            this.fila = fila;
            this.columna = columna;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Retornar value = null;
            foreach (Expresion exp in valores) {
                value = exp.Compilar(ent);                
            }
            switch (value.tipo) {
                case Simbolo.EnumTipoDato.DOUBLE:
                case Simbolo.EnumTipoDato.INT:
                    Generator3D.getInstance().addPrint("\"%.1f\"", value.getValue());
                    break;
                case Simbolo.EnumTipoDato.BOOLEAN:
                    String labelTemp = Generator3D.getInstance().newLabel();
                    Generator3D.getInstance().addLabel(value.trueLabel);
                    Generator3D.getInstance().addPrintTrue();
                    Generator3D.getInstance().addGoto(labelTemp);
                    Generator3D.getInstance().addLabel(value.falseLabel);
                    Generator3D.getInstance().addPrintFalse();
                    Generator3D.getInstance().addLabel(labelTemp);
                    break;
                case Simbolo.EnumTipoDato.OBJETO_TYPE:
                    Generator3D.getInstance().addPrint("\"%.1f\"", value.getValue());
                    break;
            }           
            return  null;
        }
    }
}
