using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Abstracto
{
    interface Instruccion
    {
        Retornar Compilar(Entornos.Entorno ent, String Ambito, Sintactico AST );      
    }
}
