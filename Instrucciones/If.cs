using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class If : Instruccion
    {
        Abstracto.Expresion condicion;
        LinkedList<Instruccion> listaInstrucciones;
        LinkedList<Instruccion> listaInsElse;
        Instruccion subIf;
        int fila, columna;
        public If(Abstracto.Expresion expresionBoolena,LinkedList<Instruccion> listaInst,LinkedList<Instruccion> ElseSt,int fila,int columna) {
            this.condicion = expresionBoolena;
            this.listaInstrucciones = listaInst;
            this.listaInsElse = ElseSt;
            this.fila = fila;
            this.columna = columna;
        }
        public If(Abstracto.Expresion expresionBoolena, LinkedList<Instruccion> listaInst, Instruccion subIf, int fila, int columna,bool EssubIf)
        {
            this.condicion = expresionBoolena;
            this.listaInstrucciones = listaInst;
            this.subIf = subIf;
            this.fila = fila;
            this.columna = columna;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            instance.agregarComentario("Iniciando el IF");
            Retornar condition = this.condicion.Compilar(ent);
            if (condition.tipo == Simbolo.EnumTipoDato.BOOLEAN)
            {
                String temp = instance.newLabel();
                instance.addLabel(condition.trueLabel);
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar ret = ins.Compilar(ent, Ambito, AST);
                }
                instance.addGoto(temp);
                if (subIf != null)
                {
                    String templabel = instance.newLabel();
                    instance.addGoto(templabel);
                    instance.addLabel(condition.falseLabel);
                    this.subIf.Compilar(ent, Ambito, AST);
                    instance.addLabel(templabel);
                }
                else
                {
                    instance.addLabel(condition.falseLabel);
                }
                if (listaInsElse!=null)
                {
                    foreach (Instruccion ins in listaInsElse) {
                        ins.Compilar(ent,Ambito,AST);
                    }
                }
                instance.addLabel(temp);
            }
            else {
                Form1.salidaConsola.AppendText("La condicion no es booleana!!!\n");
            }
            return null;
        }
    }
}
