using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class GraficarTS : Instruccion
    {
        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            ent.Graficar(ent);
            return null;
        }
    }
}
