using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Type_Object : Instruccion, ICloneable
    {
        public String nombreType;
        public LinkedList<Instruccion> listaVariables;
        public Entorno entObjeto;
        public Type_Object(String nombre, LinkedList<Instruccion> variables) {
            this.nombreType = nombre.ToLower();
            this.listaVariables = variables;
            this.entObjeto = new Entorno(null);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }
    }
}
