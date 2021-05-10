using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Continue : Instruccion
    {
        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D.getInstance().addGoto(ent.Continue);
            return null;
        }
    }
}
