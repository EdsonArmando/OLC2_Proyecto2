using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;
using static Proyecto1_Compi2.Entornos.Simbolo;

namespace Proyecto1_Compi2.Expresiones
{
    class Literal : Abstracto.Expresion
    {
        public string id;
        public string ambito;
        public string referencia_const;
        public int[] posicion_X;
        public int[] posicion_Y;
        public int[] posicion_Z;
        public EnumTipoDato tipoItem;

        public Literal(EnumTipoDato tipo, Object valor, String id, String ambito, String referencia_const, int[] posX, int[] posY, int[] posZ, Simbolo.EnumTipoDato tipoItem) {
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
        public Literal(Simbolo.EnumTipoDato tipo, Object valor)
        {            
            this.tipo = tipo;
            this.valor = valor;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Retornar Compilar(Entorno ent, bool isFunc)
        {
            throw new NotImplementedException();
        }
    }
}
