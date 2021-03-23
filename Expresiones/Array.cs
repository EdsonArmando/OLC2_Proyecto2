using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Array : Instruccion
    {
        private String Nombre_id;
        private Object Tipo;
        private Expresion[,] valor;
         
        public Array(String id, Object tipo, Expresion[,] tamanio) {
            this.Nombre_id = id;
            this.Tipo = tipo;
            this.valor = tamanio;
        }     

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }
    }
}
