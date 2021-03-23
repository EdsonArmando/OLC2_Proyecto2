using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Case : Instruccion
    {
        private Expresion condicion;
        private LinkedList<Instruccion> Case_Instr;
        private LinkedList<Instruccion> Else;
        private bool global_casIndividual;
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, LinkedList<Instruccion> Else, bool esIndividual) {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.Else = Else;
            this.global_casIndividual = esIndividual;
        }
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, bool esIndividual)
        {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.global_casIndividual = esIndividual;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }
    }
}
