using Proyecto2_Compi2.Code3D;
using System;

namespace Proyecto1_Compi2.Abstracto
{
    class Retornar
    {
        private String valor;
        public bool isTemp;
        public object tipo;
        public String trueLabel;
        public String falseLabel;
        public object sim;
        public Retornar(String valor,bool isTemp, object type, Object sim) {
            this.valor = valor;
            this.isTemp = isTemp;
            this.tipo = type;
            this.sim = sim;
        }
        public String getValue() {
            Generator3D.getInstance().freeTemp(this.valor);
            return this.valor;
        }
    }
}