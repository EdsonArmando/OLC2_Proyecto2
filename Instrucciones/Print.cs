using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
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
            throw new NotImplementedException();
        }
    }
}
