using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Code3D
{
    class Generator3D
    {
        private static Generator3D singleton;
        private LinkedList<int> temporal;
        private LinkedList<String> temporalNoUsados;
        private LinkedList<int> labels;
        private StringBuilder code;
        private int contTemp = 0;
        String isFunc = "";
        public Generator3D() { 
            this.temporal = new LinkedList<int>();
            this.labels = new LinkedList<int>();
            this.temporalNoUsados = new LinkedList<String>();
        }
        public String newTemporal() {
            String temp = "T" + this.contTemp++.ToString();
            this.temporalNoUsados.AddLast(temp);
            return temp;

        }
        public void freeTemp(String temp) {
            if (this.temporalNoUsados.Contains(temp)) {
                this.temporalNoUsados.Remove(temp);
            }
        }
        public static Generator3D getInstance()
        {
            if (singleton == null)
            {
                singleton = new Generator3D();
            }
            return singleton;
        }

        internal void addExpression(string temp, string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}
