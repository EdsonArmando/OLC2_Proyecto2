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
        public int[] posicion_X;
        public int[] posicion_Y;
        public int[] posicion_Z;
        public Simbolo.EnumTipoDato tipoItem;

        public Simbolo(EnumTipoDato tipo, String id, int valor,bool esconst,bool global, bool heap,TipoDato tipStruc)
        {
            this.id = id;
            this.tipo = tipo;
            this.posicion = valor;
            this.referencia_const = esconst;
            this.isHeap = heap;
            this.tipoStruc = tipStruc;
            this.isGlobal = global;
        }
        /*
         * Simbolo para ARRAYS
         * 
         * 
         * */
        public Simbolo(EnumTipoDato tipo, Object valor, String id, String ambito, bool referencia_consts,int[] posX,int[] posY, int[] posZ,Simbolo.EnumTipoDato tipoItem)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
            this.ambito = ambito;
            this.referencia_const = referencia_consts;
            this.posicion_X = posX;
            this.posicion_Y = posY;
            this.posicion_Z = posZ;
            this.tipoItem = tipoItem;
        }
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
