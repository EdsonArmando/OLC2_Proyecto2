using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Proyecto1_Compi2.Expresiones;
using Proyecto1_Compi2.Analizadores;

namespace Proyecto1_Compi2.Instrucciones
{
    class Funcion : PlantillaFuncion, Instruccion
    {
        public String id;
        public LinkedList<Instruccion> param_Formales;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public String nombreOriginal;
        public Funcion(String id, LinkedList<Instruccion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales, String nombreOri)
        {
            this.id = id;
            this.param_Formales = param_Formales;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
            this.nombreOriginal = nombreOri;
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

        public override void setParametros(LinkedList<Expresion> lista)
        {
            throw new NotImplementedException();
        }
    }
}
