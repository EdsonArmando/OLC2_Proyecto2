using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class LlamadaFuncion : Expresion, Instruccion
    {
        public String id;
        LinkedList<Expresion> parametros;
        public LlamadaFuncion(String id, LinkedList<Expresion> parametros, int fila, int columna)
        {
            this.id = id;
            this.parametros = parametros;
            this.fila = fila;
            this.columna = columna;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }

        public override Retornar Compilar(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
