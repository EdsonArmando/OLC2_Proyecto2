using Proyecto1_Compi2.Abstracto;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Simbolo  
    {
        public EnumTipoDato tipo;
        public String id;
        public Object valor;
        //Posicion relativa de la variable en el stack
        public int posicion;
        public bool estaHeap;
        public bool isGlobal;
        public bool referencia_const;
        public bool isHeap;
        public String ambito;
        public TipoDato tipoStruc;
        public String[] posicion_X;
        public String[] posicion_Y;
        public String[] posicion_Z;
        public Simbolo.EnumTipoDato tipoItem;
        public Expresion[,] valorArray;

        public Simbolo(EnumTipoDato tipo, String id, int valor,bool esconst,bool global, bool heap,TipoDato tipStruc,String[] posX, String[] posY, String[] posZ, Expresion[,] valor2)
        {
            this.id = id;
            this.tipo = tipo;
            this.posicion = valor;
            this.referencia_const = esconst;
            this.isHeap = heap;
            this.tipoStruc = tipStruc;
            this.isGlobal = global;
            this.posicion_X = posX;
            this.posicion_Y = posY;
            this.posicion_Z = posZ;
            this.valorArray = valor2;
        }
        /*
         * Simbolo para ARRAYS
         * 
         * 
         * */
        public Object getValor()
        {
            return this.valor;
        }
        public EnumTipoDato getTipo()
        {
            return this.tipo;
        }
        public enum EnumTipoDato
        {
            CHAR,
            STRING,
            INT,
            ARREGLO,
            DOUBLE,
            BOOLEAN,
            NULL,
            TYPE,
            ERROR,
            OBJETO_TYPE,
            FUNCION,
            ARRAY,
            VOID,
            REAL,
            CONST
        }
    }
}
