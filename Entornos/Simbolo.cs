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
        public string ambito;
        public string referencia_const;
        public int[] posicion_X;
        public int[] posicion_Y;
        public int[] posicion_Z;
        public Simbolo.EnumTipoDato tipoItem;

        public Simbolo(EnumTipoDato tipo, int valor, String id, String ambito,String referencia_const)
        {
            this.id = id;
            this.tipo = tipo;
            this.posicion = valor;
            this.ambito = ambito;
            this.referencia_const = referencia_const;
        }
        /*
         * Simbolo para ARRAYS
         * 
         * 
         * */
        public Simbolo(EnumTipoDato tipo, Object valor, String id, String ambito, String referencia_const,int[] posX,int[] posY, int[] posZ,Simbolo.EnumTipoDato tipoItem)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
            this.ambito = ambito;
            this.referencia_const = referencia_const;
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
            REAL,
            CONST
        }
    }
}
