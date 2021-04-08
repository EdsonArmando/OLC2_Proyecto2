using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;

namespace Proyecto1_Compi2.Abstracto
{
    class Retornar
    {
        private String valor;
        public bool isTemp;
        public Simbolo.EnumTipoDato tipo;
        public String trueLabel="";
        public String falseLabel="";
        public object sim;
        public TipoDato tip;
        public Retornar(String valor,bool isTemp, Simbolo.EnumTipoDato type, Object sim) {
            this.valor = valor;
            this.isTemp = isTemp;
            this.tipo = type;
            this.sim = sim;
        }
        public Retornar(String valor, bool isTemp, Simbolo.EnumTipoDato type, Object sim,TipoDato tipo)
        {
            this.valor = valor;
            this.isTemp = isTemp;
            this.tipo = type;
            this.sim = sim;
            this.tip = tipo;
        }
        public String getValue() {
            Generator3D.getInstance().freeTemp(this.valor);
            return this.valor;
        }        
    }
}