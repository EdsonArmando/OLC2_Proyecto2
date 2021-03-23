using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Instrucciones;
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
        public Entorno anterior;
        //Tamanio Funcion
        int tamanio;
        String Break;
        String Continue;
        String Return;
        String prop;
        public Entorno(Entorno entornoAnterior) : base()
        {
            this.tablaSimbolos = new Hashtable();
            this.anterior = entornoAnterior;
            this.tamanio = 0;
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
            return this.tablaSimbolos.ContainsKey(id.ToLower());
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
            archivo.Write("<tr><th>No</th><th>Nombre</th><th>Tipo</th><th>Ambito</th></tr>");
            foreach (String key in ent.tablaSimbolos.Keys)
            {
                sim = (Simbolo)tablaSimbolos[key];
                archivo.Write("<tr><td>" + conts + "</td><td>" + sim.id + "</td><td>" + sim.tipo + "</td><td>" + sim.ambito + "</td></tr>");
                conts++;
            }

            archivo.Write("</table>");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            
        }
        public void Insertar(string nombre, Simbolo valor)
        {
            if (this.tablaSimbolos.ContainsKey(nombre.ToLower()))
            {
                Form1.salidaConsola.AppendText("La variable '" + nombre + "' YA existe");

                return;
            }
            this.tablaSimbolos.Add(nombre.ToLower(), valor);

        }
    }
}
