using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Error
    { 
        public String tipo;
        public String valor;
        public int fila, columna;
        public Error(String tipo, String valor, int fila,int columna) {
            this.tipo = tipo;
            this.valor = valor;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
