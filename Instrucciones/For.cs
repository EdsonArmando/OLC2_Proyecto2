using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class For : Instruccion
    {
        String id;
        Expresion valorInicio;
        Expresion valorFin;
        LinkedList<Instruccion> listaInstrucciones;
        bool descendente;
        public For(String id, Expresion valorInicio, Expresion valorFin, LinkedList<Instruccion> listaInstr, bool desce) {
            this.id = id;
            this.valorInicio = valorInicio;
            this.valorFin = valorFin;
            listaInstrucciones = listaInstr;
            this.descendente = desce;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            //Creo la Condicion
            Expresion condicion = null;
            //Obtengo la Variable a umentar
            Simbolo sim = ent.obtener(id,ent);
            //Compilo los Valores de Inicio
            Retornar valInicio = valorInicio.Compilar(ent);
            if (sim.isGlobal)
            {
                instance.addSetStack(sim.posicion.ToString(), valInicio.getValue());
            }
            else {
                String temp = instance.newTemporal();
                instance.freeTemp(temp);
                instance.addExpression(temp, "p", sim.posicion.ToString(), "+");
                instance.addSetStack(temp, valInicio.getValue());
            }
            //Aumentar o Decre variable
            Aumento aumento = null;
            Decremento decremento = null;
            //Significa que es un For ascendente
            if (descendente == false)
            {
                condicion = new Arimetica(new AccesoId(null,null,id), valorFin, Arimetica.Tipo_operacion.MENOR_IGUAL_QUE);
                aumento = new Aumento(id);
            }
            else {
                condicion = new Arimetica(new AccesoId(null, null, id), valorFin, Arimetica.Tipo_operacion.MAYOR_IGUAL_QUE);
                decremento = new Decremento(id);
            }
            //Creo El Ciclo en 3D
            String labelFor = instance.newLabel();
            instance.addLabel(labelFor);
            Retornar condFor = condicion.Compilar(ent);
            ent.Break = condFor.falseLabel;
            ent.Continue = labelFor;
            instance.addLabel(condFor.trueLabel);
            foreach (Instruccion ins in listaInstrucciones) {
                ins.Compilar(ent,Ambito,AST);
            }
            if (descendente == false)
            {
                aumento.Compilar(ent, Ambito, AST);
            }
            else
            {
                decremento.Compilar(ent, Ambito, AST);
            }
            instance.addGoto(labelFor);
            instance.addLabel(condFor.falseLabel);
            instance.agregarComentario("Finaliza For");
            return null;
        }
    }
}
