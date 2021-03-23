using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Procedure : PlantillaFuncion, Instruccion
    {
        public String id;
        public int fila, columna;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Abstracto.Instruccion> param_Formales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public String nombreOriginal;

        public Procedure(String id, LinkedList<Abstracto.Instruccion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales, int fila, int columna, String nombreOriginal)
        {
            this.id = id;
            this.param_Formales = param_Formales;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
            this.fila = fila;
            this.columna = columna;
            this.nombreOriginal = nombreOriginal;
        }

        public override Retornar Compilar(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override void setParametros(LinkedList<Expresion> parametros)
        {
            this.param_Actuales = parametros;
        }
    }
}
