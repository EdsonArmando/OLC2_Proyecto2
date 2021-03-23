using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class For : Instruccion
    {
        String id;
        Expresion valorInicio;
        Expresion valorFin;
        LinkedList<Instruccion> listaInstrucciones;
        bool descendente;
        public For(String id, Expresion valorInicio, Expresion valorFin, LinkedList<Instruccion> listaInstr, bool desce) {
            this.id = id;
            this.valorInicio = valorInicio;
            this.valorFin = valorFin;
            listaInstrucciones = listaInstr;
            this.descendente = desce;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }
    }
}
