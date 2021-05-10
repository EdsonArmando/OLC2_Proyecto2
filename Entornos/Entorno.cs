using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Instrucciones;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Entorno 
    {
        public Hashtable tablaSimbolos;
        public Hashtable types;
        public Hashtable typesArray;
        public Hashtable funciones;
        public Entorno anterior;
        //Tamanio Funcion
        public int pos;
        public String Break;
        public String Continue;
        public String Return;
        public String prop;
        public SimboloFuncion actualFunc;
        public Entorno(Entorno entornoAnterior) : base()
        {
            this.tablaSimbolos = new Hashtable();
            this.types = new Hashtable();
            this.funciones = new Hashtable();
            this.typesArray = new Hashtable();
            this.anterior = entornoAnterior;
            this.pos = 0;
            this.prop = "main";
            // llamada del constructor de la clase padre
        }
        //Recorrer Tabla
        public void recorrer(Entorno ent) {
            Simbolo sim;
            foreach (String id in ent.tablaSimbolos.Keys) {
                sim = (Simbolo)ent.tablaSimbolos[id.ToLower()];
            }
        }
        //Insertar Types
        public bool existeVariable(String id)
        {
            bool isTrue = false;
            Entorno temp = this;
            while (temp != null) {
                if (temp.tablaSimbolos.ContainsKey(id.ToLower())) {
                    isTrue = true;
                }     
                temp = temp.anterior;
            }
            return isTrue;
            
        }
        //Entorno para la Funcion
        public void setEnviorementFunc(string prop, SimboloFuncion actualFunc , String ret)
        {
            this.pos = 1; //1 porque la posicion 0 es para el return
            this.prop = prop;
            this.Return = ret;
            this.actualFunc = actualFunc;
        }
        //Obtener Funcion
        public SimboloFuncion getFuncion(String id) {
            Entorno ent = this;
            while (ent != null) {
                SimboloFuncion sim = (SimboloFuncion)ent.funciones[id.ToLower()];
                if (sim!=null) {
                    return sim;
                }
                ent = ent.anterior;
            }
            return null;
        }
        //Obtener la variable del entorno
        public Simbolo obtener(string id, Entorno entorno)
        {
            Simbolo sim = null;
            if (entorno.tablaSimbolos.ContainsKey(id.ToLower()))
            {
                sim = (Simbolo)entorno.tablaSimbolos[id.ToLower()];             
                return sim;       
            }
            else if (entorno.anterior != null)
            {
                sim = obtener(id.ToLower(),entorno.anterior);
                return sim;
            }
            else {
                Sintactico.errores.AddLast(new Error("Semantico no existe variable",id,1,1));
                Form1.salidaConsola.AppendText("La variable '" + id + "' NO existe");
                return null;
            }
            
        }
        public Simbolo obtenerDos(string id, Entorno entorno)
        {
            Simbolo sim = null;
            if (entorno.tablaSimbolos.ContainsKey(id.ToLower()))
            {
                sim = (Simbolo)entorno.tablaSimbolos[id.ToLower()];
                return sim;
            }
            else if (entorno.anterior != null)
            {
                sim = obtener(id.ToLower(), entorno.anterior);
                return sim;
            }
            else
            {                
                return null;
            }

        }
        public bool addType(String id,int tamanio, LinkedList<Parametros> parametro) {
            if (this.types.ContainsKey(id.ToLower()))
            {
                return false;
            }
            else {
                this.types.Add(id,new SimboloStruct(id,tamanio,parametro));
                return true;
            }
        }
        //Agregar Type Array
        public bool addTypeArray(String id, Simbolo array)
        {
            if (this.typesArray.ContainsKey(id.ToLower()))
            {
                return false;
            }
            else
            {
                this.typesArray.Add(id, array);
                return true;
            }
        }
        public SimboloStruct getStruct(String id) {
            Entorno ent = this;
            id = id.ToLower();
            while (ent != null) {
                SimboloStruct sim = (SimboloStruct)ent.types[id];
                if (sim != null) {
                    return sim;
                }                
                ent = ent.anterior;
            }
            return null;
        }
        //Existe Struct
        public bool existeStruct(String id)
        {
            Entorno ent = this;
            id = id.ToLower();
            while (ent != null)
            {
                SimboloStruct sim = (SimboloStruct)ent.types[id];
                if (sim != null)
                {
                    return true;
                }
                ent = ent.anterior;
            }
            return false;
        }
        public void printVal() {
            foreach (Simbolo sim in this.tablaSimbolos) {
                foreach (String key in tablaSimbolos.Keys)
                {
                    var value = tablaSimbolos[key];
                }
            }
        }
        public void setVariable(string nombre, Simbolo valor,Entorno ent) {
            if (ent.tablaSimbolos.ContainsKey(nombre.ToLower()))
            {
                ent.tablaSimbolos.Remove(nombre.ToLower()); ;
                ent.tablaSimbolos.Add(nombre.ToLower(), valor);
                return;
            }
            else if (ent.anterior != null)
            {
                setVariable(nombre.ToLower(),valor,ent.anterior);
                return;
            }
        }
        public void Graficar(Entorno ent) {
            Simbolo sim = null;
             int conts = 1;
            StreamWriter archivo = new StreamWriter("C:\\compiladores2\\TablaSimbolos_201701029.html");
            archivo.Write("<html>");
            archivo.Write("<head>");
            archivo.Write("<style>"
                    + "table{"
                    + "  font-family: arial, sans-serif; border-collapse: collapse;    width: 100%;}"
                    + "td, th{"
                    + "border: 1px solid #dddddd;text-align: left;  padding: 8px;}"
                    + "tr:nth-child(even){"
                    + " background-color: #dddddd;}"
                    + "</style>");
            archivo.Write("</head>");
            archivo.Write("<body>");
            archivo.Write("<H1>Tabla de Simbolos</H1>");
            archivo.Write("<br><br>");
            archivo.Write("<table>");
            archivo.Write("<tr><th>No</th><th>Nombre</th><th>Tipo</th><th>Ambito</th><th>Posicion</th></tr>");
            foreach (String key in ent.tablaSimbolos.Keys)
            {
                sim = (Simbolo)tablaSimbolos[key];
                archivo.Write("<tr><td>" + conts + "</td><td>" + sim.id + "</td><td>" + sim.tipo + "</td><td>" + sim.ambito + "</td><td>"+ sim.posicion.ToString() +"</td></tr>");
                conts++;
            }

            archivo.Write("</table>");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            
        }
        public bool addFuncion(PlantillaFuncion funcion, String nombre) {

            if (this.funciones.ContainsKey(nombre)) {
                return false;
            }
            this.funciones.Add(funcion.id.ToLower(),new SimboloFuncion(funcion,nombre.ToLower()));
            return true;
        }
        public Simbolo Insertar(string nombre, Simbolo.EnumTipoDato tipo, bool isConst, bool isRef,TipoDato tipoStruct,String[] posX, String[] posY, String[] posZ, Expresion[,] valor2, String ambito)
        {
            nombre = nombre.ToLower();
            if (this.tablaSimbolos.ContainsKey(nombre) != false) {
                return null;
            }
            Simbolo sim = new Simbolo(tipo, nombre,this.pos++,isConst,this.anterior==null,isRef,tipoStruct,posX, posY, posZ,valor2,ambito);
            this.tablaSimbolos.Add(nombre,sim);
            return sim;
        }
    }
}
