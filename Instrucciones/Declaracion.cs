using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Instruccion
    {
        public Simbolo.EnumTipoDato tipoVariable;
        public LinkedList<Expresion> variables;
        public String nombreVariable;
        public Expresion expresion;
        public int fila, columna;
        public String esReferencia_const;
        public String nameArra;
        public String tipoDinamico;

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String tip)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.tipoDinamico = tip;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esreferencia,String tip)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tip;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.variables = valores;
            this.esReferencia_const = esreferencia;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String esReferencia_const, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tipoDi;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esReferencia_const,String nameArray, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.variables = valores;
            this.expresion = expresion;
            this.fila = fila;
            this.tipoDinamico = tipoDi;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna, String esReferencia_const, String nameArray,String tipoDi)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.tipoDinamico = tipoDi;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }

        public void setExpresion(Expresion expr) {
            
        }        
    }
}
