using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class AccesoArreglo : Abstracto.Expresion
    {
        private String id;
        private Expresion posX;
        private Expresion posY;
        private Expresion posZ;

        public AccesoArreglo(String id, Expresion posX, Expresion posy,Expresion posZ) {
            this.id = id;
            this.posX = posX;
            this.posY = posy;
            this.posZ = posZ;
        }

        public override Retornar Compilar(Entorno ent,bool isFunc)
        {
            throw new NotImplementedException();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
