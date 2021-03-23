using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class AsignacionTypeObjcet : Instruccion
    {
        public LinkedList<String> listId;
        public Expresion valor;
        public AsignacionTypeObjcet(LinkedList<String> listId, Expresion valor) {
            this.listId = listId;
            this.valor = valor;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            throw new NotImplementedException();
        }

        public void setExpresion(LinkedList<String> accesos, Entorno ent, Expresion res, String Ambito)
        {            
            
        }    
    }
}
