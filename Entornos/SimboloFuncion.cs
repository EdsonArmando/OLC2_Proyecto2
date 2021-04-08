using Proyecto1_Compi2.Instrucciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Entornos
{
    class SimboloFuncion
    {
        public TipoDato tipo;
        public String id;
        public String idUnico;
        public int Size;
        public LinkedList<Parametros> listaParametros;
        public SimboloFuncion(PlantillaFuncion funcion, String id){
            this.tipo = funcion.tipo;
            this.id = funcion.id;
            this.Size = funcion.tamanio;
            this.idUnico = funcion.idUnico;
            this.listaParametros = funcion.param;
        }
    }
}
