using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
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
            throw new NotImplementedException();
        }
    }
}
