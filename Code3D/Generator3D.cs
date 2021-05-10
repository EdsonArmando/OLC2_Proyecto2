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
        private StringBuilder codeFuncion = new StringBuilder();
        public int contTemp = 0;
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
            this.tempStorage.AddLast(temp);
            return temp;

        }
        
        //Agregar Fin de Funcion
        public void addFinal()
        {
            this.codeFuncion.Append("return; \n}\n");
        }
        //Agregar inicio Procedimiento en C
        public void addInicioProc(String id)
        {
            this.codeFuncion.Append("void " + id + " (){\n");
        }
        public void LimpiarStorage()
        {
            this.tempStorage.Clear();
        }
        //Guardar Temporales
        public int guardarTemps(Entorno ent,bool isFunc) {
            if (tempStorage.Count > 0) {
                String temp = this.newTemporal();
                this.freeTemp(temp);
                int num = 0;
                this.agregarComentario("Guarda Temporales", isFunc);
                this.addExpression(temp,"sp",ent.pos.ToString(),"+", isFunc);
                foreach (String valor in tempStorage) {
                    num++;
                    this.addSetStack(temp,valor, isFunc);
                    if (num != this.tempStorage.Count) {
                        this.addExpression(temp,temp,"1","+", isFunc);
                    }
                }
                this.agregarComentario("finalizo guardar temporales", isFunc);
            }
            int puntero = ent.pos;
            ent.pos = puntero + tempStorage.Count;
            return puntero;
        }
        //Cambio Simulado
        public void nextEnt(int size, bool isFunc) {
            if (!isFunc)
            {
                this.code.Append("sp=sp + " + size.ToString() + ";\n");
            }
            else {
                this.codeFuncion.Append("sp=sp + " + size.ToString() + ";\n");
            }
                
           
        }
        //Funcion para Imprimir String
        public void NativePrintString(bool isFunc) {
            addInicioProc("Native_PrintString");
            String T1 = newTemporal(); String T2 = newTemporal(); String T3 = newTemporal();
            String L1 = newLabel(); String L2 = newLabel(); String L3 = newLabel();
            addExpression(T1, "sp", "0", "+", isFunc);
            addGetStack(T2, T1, isFunc);
            addIf(T2, "-1", "!=", L1, isFunc);
            addPrint("\"% c\"", "32", isFunc);
            addGoto(L3, isFunc);
            addLabel(L1, isFunc);
            addGetHeap(T3, T2, isFunc);
            addLabel(L2, isFunc);
            addIf(T3, "-1", "==", L3, isFunc);
            addPrint("\"% c\"", "(char)" + T3, isFunc);
            addExpression(T2, T2, "1","+", isFunc);
            addGetHeap(T3,T2, isFunc);
            addGoto(L2, isFunc);
            addLabel(L3, isFunc);
            addFinal();
            freeTemp(T1);freeTemp(T2);freeTemp(T3);
        }
        //Llamada Funcion
        public void addCall(String id, bool isFunc) {
            if (!isFunc)
            {
                this.code.Append(id + "() ;\n");
            }
            else {
                this.codeFuncion.Append(id + "() ;\n");
            }
                           
        }
        //Recuperar Temporales
        public void RecuperarTemp(Entorno ent,int pos,bool isFunc) {
            if (tempStorage.Count > 0) {
                String temp = newTemporal();
                freeTemp(temp);
                int size = 0;
                agregarComentario("Inicia Recuperacion temporales",isFunc);
                addExpression(temp, "sp", pos.ToString(), "+", isFunc);
                foreach (String tempSt in tempStorage) {
                    size++;
                    addGetStack(tempSt,temp, isFunc);
                    if (size != this.tempStorage.Count)
                        addExpression(temp,temp,"1","+", isFunc);
                }
                agregarComentario("Finaliza Recuperacion", isFunc);
                ent.pos = pos;
            }
        }
        //Agregar Temporal
        public void aggregarTemp(String temp) {
            if (!this.tempStorage.Contains(temp))
                this.tempStorage.AddLast(temp);
        }
        //Regreso Entorno
        public void antEnt(int size, bool isFunc)
        {
            if (!isFunc)
            {
                this.code.Append("sp=sp - " + size.ToString() + ";\n");
            }
            else {
                this.codeFuncion.Append("sp=sp - " + size.ToString() + ";\n");
            }                            
        }
        public LinkedList<String> getTempStorage()
        {
            return this.tempStorage;
        }
        public void setTempStorage(object tempStorage)
        {
            this.tempStorage = (LinkedList<string>)tempStorage;
        }
        public void addSetHeap(object index, object value, bool isFunc)
        {
            if (!isFunc)
            {
                this.code.Append("Heap[(int)" + index + "] = " + value + ";\n");
            }
            else {
                this.codeFuncion.Append("Heap[(int)" + index + "] = " + value + ";\n");
            }               
            
        }
        public void nextHeap(bool isFunc)
        {
            if (!isFunc)
            {
                this.code.Append("sh = sh + 1;\n");
            }
            else {
                this.codeFuncion.Append("sh = sh + 1;\n");
            }                           
        }
        public String newLabel()
        {
            String label = "L" + this.contLabel++.ToString();
            this.labels.AddLast(label);
            return label;

        }
        public void freeTemp(String temp) {
            if (this.tempStorage.Contains(temp)) {
                this.tempStorage.Remove(temp);
            }
        }
        public void AgregarExpresionNumerica(bool isFunc) {
            if (!isFunc)
            {
                code.Append("Hola Mundo");
            }
            else {
                codeFuncion.Append("Hola Mundo");
            }                            
        }
        public void agregarComentario(String coment, bool isFunc) {
            if (!isFunc)
            {
                code.Append("//" + coment + "\n");
            }
            else {
                codeFuncion.Append("//" + coment + "\n");
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
        internal void addSetStack(String posicion, string v, bool isFunc)
        {
            if (!isFunc) {
                this.code.Append("Stack[(int)" + posicion + "] =" + v + ";\n");
            }
            else {
                this.codeFuncion.Append("Stack[(int)" + posicion + "] =" + v + ";\n");
            }
            
        }
        public void addGetStack(object target ,object index,bool isFunc)
        {
            if (!isFunc) {
                this.code.Append(target.ToString() + "=" + "Stack[(int)" + index.ToString() + "];\n");
            }
            else {
                this.codeFuncion.Append(target.ToString() + "=" + "Stack[(int)" + index.ToString() + "];\n");
            }
           
        }
        public void addGetHeap(object target, object index, bool isFunc)
        {
            if (!isFunc) {
                this.code.Append(target.ToString() + " = " + "Heap[(int)" + index + "];\n");
            }
            else{
                this.codeFuncion.Append(target.ToString() + " = " + "Heap[(int)" + index + "];\n");
            }
            
        }
        public void addNextEnv(int size, bool isFunc)
        {
            if (!isFunc) {
                this.code.Append("sp = sp + " + size + ";\n");
            }
            else {
                this.codeFuncion.Append("sp = sp + " + size + ";\n");
            }
            
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
        public void addPrint(String format, object value,bool isFunc) {
            if (!isFunc) {
                this.code.Append("printf(" + format + "," + value + ");\n");
            }
            else{
                this.codeFuncion.Append("printf(" + format + "," + value + ");\n");
            }
            
        }
        public String addExpression(string temp, string v1, string v2, string v3, bool isFunc)
        {
            if (!isFunc) {
                this.code.Append(temp + " = " + v1 + v3 + v2 + ";\n");
            }
            else {
                this.codeFuncion.Append(temp + " = " + v1 + v3 + v2 + ";\n");
            }
            
            return "";
        }
        public StringBuilder getCode() {
            return this.code;
        }
        public StringBuilder getCodeFuncion()
        {
            return this.codeFuncion;
        }
        public void addPrintTrue(bool isFunc) {
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("t")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("r")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("u")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("e")[0], isFunc);  
        }
        public void addLabel(String label, bool isFunc) {
            if (!isFunc) {
                this.code.Append(label + ":\n");
            }
            else {
                this.codeFuncion.Append(label + ":\n");
            }
           
        }
        public void addGoto(String label,bool isFunc) {
            if (!isFunc) {
                this.code.Append("goto " + label + " ;\n");
            }
            else {
                this.codeFuncion.Append("goto " + label + " ;\n");
            }
            
        }
        public void addIf(object izq, object dere, String operador, String label, bool isFunc) {
            if (!isFunc) {
                this.code.Append("if (" + izq + operador + dere + ") goto " + label + " ;\n");
            }
            else {
                this.codeFuncion.Append("if (" + izq + operador + dere + ") goto " + label + " ;\n");
            }
            
        }
        public void addPrintFalse(bool isFunc)
        {
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("f")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("a")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("l")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("s")[0], isFunc);
            this.addPrint("\"%c\"", Encoding.ASCII.GetBytes("e")[0], isFunc);
        }
    }
}
