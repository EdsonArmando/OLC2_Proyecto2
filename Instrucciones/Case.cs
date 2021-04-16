using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Case : Instruccion
    {
        private Expresion condicion;
        private LinkedList<Instruccion> Case_Instr;
        private LinkedList<Instruccion> Else;
        private bool global_casIndividual;
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, LinkedList<Instruccion> Else, bool esIndividual) {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.Else = Else;
            this.global_casIndividual = esIndividual;
        }
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, bool esIndividual)
        {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.global_casIndividual = esIndividual;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            String LabelSalida = instance.newLabel();
            foreach (Case ca in Case_Instr) {                
                Arimetica condicionIgual =new Arimetica (condicion,ca.condicion,Arimetica.Tipo_operacion.IGUAL_QUE);
                condicionIgual.Compilar(ent);
                instance.addLabel(condicionIgual.truelabel);
                LinkedList<Instruccion> inst = ca.Case_Instr;
                foreach (Instruccion ins in inst)
                {
                    ins.Compilar(ent,Ambito,AST);
                }
                instance.addGoto(LabelSalida);
                instance.addLabel(condicionIgual.falselabel);                
            }
            if (Else != null) {
                foreach (Instruccion ins in Else) {
                    ins.Compilar(ent,Ambito,AST);
                }           
            }
            instance.addLabel(LabelSalida);
            return null;
        }
    }
}
