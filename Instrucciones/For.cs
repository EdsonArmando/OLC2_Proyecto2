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

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Generator3D instance = Generator3D.getInstance();
            //Creo la Condicion
            Expresion condicion = null;
            //Obtengo la Variable a umentar
            Simbolo sim = ent.obtener(id,ent);
            //Compilo los Valores de Inicio
            Retornar valInicio = valorInicio.Compilar(ent, isFunc);
            if (sim.isGlobal)
            {
                instance.addSetStack(sim.posicion.ToString(), valInicio.getValue(), isFunc);
            }
            else {
                String temp = instance.newTemporal();
                instance.freeTemp(temp);
                instance.addExpression(temp, "sp", sim.posicion.ToString(), "+", isFunc);
                instance.addSetStack(temp, valInicio.getValue(), isFunc);
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
            instance.addLabel(labelFor, isFunc);
            Retornar condFor = condicion.Compilar(ent, isFunc);
            ent.Break = condFor.falseLabel;
            ent.Continue = labelFor;
            instance.addLabel(condFor.trueLabel, isFunc);
            foreach (Instruccion ins in listaInstrucciones) {
                ins.Compilar(ent,Ambito,AST, isFunc);
            }
            if (descendente == false)
            {
                aumento.Compilar(ent, Ambito, AST, isFunc);
            }
            else
            {
                decremento.Compilar(ent, Ambito, AST, isFunc);
            }
            instance.addGoto(labelFor, isFunc);
            instance.addLabel(condFor.falseLabel, isFunc);
            instance.agregarComentario("Finaliza For", isFunc);
            return null;
        }
    }
}
