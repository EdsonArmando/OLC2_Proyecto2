using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Type_Object : Instruccion
    {
        public String nombreType;
        public LinkedList<Instruccion> listaVariables;    
        public Type_Object(String nombre, LinkedList<Instruccion> variables) {
            this.nombreType = nombre.ToLower();
            this.listaVariables = variables;          
        }
        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            LinkedList<Parametros> temp = devListParametros(listaVariables,ent);
            ent.addType(this.nombreType, temp.Count, temp);
            return null;
        }

        public LinkedList<Parametros> devListParametros(LinkedList<Instruccion> declaraciones,Entorno ent) {
            LinkedList<Parametros> listParam = new LinkedList<Parametros>();            
            foreach (Declaracion dcl in declaraciones) {
                if (dcl.tipoVariable == Simbolo.EnumTipoDato.OBJETO_TYPE)
                {
                    //Existe el Type del Array
                    if (ent.existeVariable(dcl.nameArra)) {
                        listParam.AddLast(new Parametros(dcl.nombreVariable, new TipoDato(Simbolo.EnumTipoDato.ARRAY, dcl.nameArra, null), null));
                    }
                    else {
                        listParam.AddLast(new Parametros(dcl.nombreVariable, new TipoDato(dcl.tipoVariable, dcl.nameArra, null), null));
                    }
                }
                else {
                    listParam.AddLast(new Parametros(dcl.nombreVariable, new TipoDato(dcl.tipoVariable, dcl.nameArra, null), null));
                }                
            }
            return listParam;
        }
    }
}
