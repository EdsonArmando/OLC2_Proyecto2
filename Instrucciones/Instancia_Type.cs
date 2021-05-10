using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Instancia_Type : Instruccion
    {
        public String nombreObjeto;
        public String nombreType;
        public Entorno entObjeto;
        public Instancia_Type(String nombre, String nombreType)
        {
            this.nombreObjeto = nombre;
            this.nombreType = nombreType;
            this.entObjeto = new Entorno(null);
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            throw new NotImplementedException();
        }
    }
}
