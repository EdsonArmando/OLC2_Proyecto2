using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Code3D
{
    class Generator3D
    {
        private static Generator3D singleton;
        private LinkedList<int> temporal;
        private LinkedList<String> tempStorage;
        private LinkedList<String> temporalNoUsados;
        private LinkedList<String> labels;
        private StringBuilder code = new StringBuilder();
        private int contTemp = 0;
        private int contLabel = 0;
        String isFunc = "";
        public Generator3D() { 
            this.temporal = new LinkedList<int>();
            this.labels = new LinkedList<String>();
            this.temporalNoUsados = new LinkedList<String>();
            this.tempStorage = new LinkedList<String>();
        }
        public String newTemporal() {
            String temp = "T" + this.contTemp++.ToString();         
            return temp;

        }
        //Agregar Fin de Funcion
        public void addFinal()
        {
            this.code.Append("return; \n}\n");
        }
        //Agregar inicio Procedimiento en C
        public void addInicioProc(String id)
        {
            this.code.Append("void " + id + " (){\n");
        }
        public void LimpiarStorage()
        {
            this.tempStorage.Clear();
        }
        //Guardar Temporales
        public int guardarTemps(Entorno ent) {
            if (tempStorage.Count > 0) {
                String temp = this.newTemporal();
                this.freeTemp(temp);
                int num = 0;
                this.agregarComentario("Guarda Temporales");
                this.addExpression(temp,"p",ent.pos.ToString(),"+");
                foreach (String valor in tempStorage) {
                    num++;
                    this.addSetStack(temp,valor);
                    if (num != this.tempStorage.Count) {
                        this.addExpression(temp,temp,"1","+");
                    }
                }
                this.agregarComentario("finalizo guardar temporales");
            }
            int puntero = ent.pos;
            ent.pos = puntero + tempStorage.Count;
            return puntero;
        }
        //Cambio Simulado
        public void nextEnt(int size) {
            this.code.Append("p=p + " +size.ToString()+";\n");
        }
        //Llamada Funcion
        public void addCall(String id) {
            this.code.Append(id + "() ;\n");
        }
        //Recuperar Temporales
        public void RecuperarTemp(Entorno ent,int pos) {
            if (tempStorage.Count > 0) {
                String temp = newTemporal();
                freeTemp(temp);
                int size = 0;
                agregarComentario("Inicia Recuperacion temporales");
                addExpression(temp, "p", pos.ToString(), "+");
                foreach (String tempSt in tempStorage) {
                    size++;
                    addGetStack(tempSt,temp);
                    if (size != this.tempStorage.Count)
                        addExpression(temp,temp,"1","+");
                }
                agregarComentario("Finaliza Recuperacion");
                ent.pos = pos;
            }
        }
        //Agregar Temporal
        public void aggregarTemp(String temp) {
            if (!this.tempStorage.Contains(temp))
                this.tempStorage.AddLast(temp);
        }
        //Regreso Entorno
        public void antEnt(int size)
        {
            this.code.Append("p=p - " + size.ToString()+";\n");
        }
        public LinkedList<String> getTempStorage()
        {
            return this.tempStorage;
        }
        public void setTempStorage(object tempStorage)
        {
            this.tempStorage = (LinkedList<string>)tempStorage;
        }
        public void addSetHeap(object index,object value)
        {
            this.code.Append("Heap[(int)"+index+"] = "+value+";\n");
        }
        public void nextHeap()
        {
            this.code.Append("h = h + 1;\n");
        }
        public String newLabel()
        {
            String label = "L" + this.contLabel++.ToString();
            this.labels.AddLast(label);
            return label;

        }
        public void addFunctionPrintString() { 
            
        }
        public void freeTemp(String temp) {
            if (this.temporalNoUsados.Contains(temp)) {
                this.temporalNoUsados.Remove(temp);
            }
        }
        public void AgregarExpresionNumerica() {
            code.Append("Hola Mundo");
        }
        public void agregarComentario(String coment) {
            code.Append("//"+coment + "\n");
        }
        public static Generator3D getInstance()
        {
            if (singleton == null)
            {
                singleton = new Generator3D();
            }
            return singleton;
        }
        internal void addSetStack(String posicion, string v)
        {
            this.code.Append("Stack[(int)"+posicion+"] ="+ v + ";\n");
        }
        public void addGetStack(object target ,object index)
        {
            this.code.Append(target.ToString() +  "=" + "Stack[(int)" + index.ToString()+ "];\n");
        }
        public void addGetHeap(object target, object index)
        {
            this.code.Append(target.ToString() +  " = " +  "Heap[(int)" + index+ "];\n");
        }
        public void addNextEnv(int size)
        {
            this.code.Append("p = p + " +size + ";\n");
        }
        public StringBuilder agregarEncabezado() {
            StringBuilder encabezado = new StringBuilder();
            if (contTemp > 0) {                
                encabezado.Append("float ");
                string temp = "";
                for (int i = 0; i < contTemp; i++)
                {
                    temp += "T" + i + ",";
                }
                temp = temp.Remove(temp.Length - 1);
                encabezado.Append(temp + ";");
                return encabezado;
            }
            return encabezado;
        }
        public void addPrint(String format, object value) {
            this.code.Append("printf(" + format +","+value+ ");\n");
        }
        public String addExpression(string temp, string v1, string v2, string v3)
        {
            this.code.Append(temp + " = " + v1 + v3 + v2 + ";\n");
            return "";
        }
        public StringBuilder getCode() {
            return this.code;
        }
        public void addPrintTrue() {
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("t")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("r")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("u")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("e")[0]);  
        }
        public void addLabel(String label) {
            this.code.Append(label + ":\n");
        }
        public void addGoto(String label) {
            this.code.Append("goto " + label + " ;\n");
        }
        public void addIf(object izq, object dere, String operador, String label) {
            this.code.Append("if (" + izq + operador + dere + ") goto " + label +" ;\n");
        }
        public void addPrintFalse()
        {
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("f")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("a")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("l")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("s")[0]);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("e")[0]);
        }
    }
}
