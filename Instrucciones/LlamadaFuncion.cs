using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class LlamadaFuncion : Expresion, Instruccion
    {
        public String id;
        LinkedList<Expresion> parametros;
        public LlamadaFuncion(String id, LinkedList<Expresion> parametros, int fila, int columna)
        {
            this.id = id;
            this.parametros = parametros;
            this.fila = fila;
            this.columna = columna;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            instance.agregarComentario("Empieaza la Llamada a la Funcion");
            SimboloFuncion simFuncion = ent.getFuncion(id);
            LinkedList<Retornar> retParamValues = new LinkedList<Retornar>();
            int tam = instance.guardarTemps(ent);
            foreach (Expresion exp in parametros)
            {
                retParamValues.AddLast(exp.Compilar(ent));
            }
            String temp = instance.newTemporal();
            instance.freeTemp(temp);
            if (retParamValues.Count != 0)
            {
                instance.addExpression(temp, "p", (ent.pos + 1).ToString(), "+");
                for (int index = 0; index < retParamValues.Count; index++)
                {
                    Retornar temRet = retParamValues.ElementAt(index);
                    instance.addSetStack(temp, temRet.getValue());
                    if (index != retParamValues.Count - 1)
                        instance.addExpression(temp, temp, "1", "+");
                }
            }
            instance.nextEnt(ent.pos);
            instance.addCall(simFuncion.idUnico);
            instance.addGetStack(temp, "p");
            instance.antEnt(ent.pos);
            instance.RecuperarTemp(ent, tam);
            instance.aggregarTemp(temp);
            if (simFuncion.tipo.tipo != Simbolo.EnumTipoDato.BOOLEAN)
            {
                Retornar tempoRet = new Retornar(temp, true, simFuncion.tipo.tipo, simFuncion, simFuncion.tipo);
                tempoRet.getValue();
                return tempoRet;
            }
            Retornar reto = new Retornar("", false, simFuncion.tipo.tipo, simFuncion, simFuncion.tipo);
            this.truelabel = this.truelabel == "" ? instance.newLabel() : this.truelabel;
            this.falselabel = this.falselabel == "" ? instance.newLabel() : this.falselabel;
            instance.addIf(temp, "1", "==", this.truelabel);
            instance.addGoto(this.falselabel);
            reto.trueLabel = this.truelabel;
            reto.falseLabel = this.falselabel;
            reto.getValue();
            return reto;
        }

        public override Retornar Compilar(Entorno ent)
        {
            Generator3D instance = Generator3D.getInstance();
            SimboloFuncion simFuncion = ent.getFuncion(id.ToLower());
            LinkedList<Retornar> retParamValues = new LinkedList<Retornar>();
            int tam = instance.guardarTemps(ent);
            foreach (Expresion exp in parametros) {
                retParamValues.AddLast(exp.Compilar(ent));
            }
            String temp = instance.newTemporal();
            instance.freeTemp(temp);
            if (retParamValues.Count != 0) {
                instance.addExpression(temp, "p",(ent.pos + 1).ToString(), "+");
                for (int index=0;index < retParamValues.Count;index++) {
                    Retornar temRet = retParamValues.ElementAt(index);
                    instance.addSetStack(temp,temRet.getValue());
                    if (index != retParamValues.Count - 1)
                        instance.addExpression(temp,temp,"1","+");
                }
            }
            instance.nextEnt(ent.pos);
            instance.addCall(simFuncion.idUnico);
            instance.addGetStack(temp,"p");
            instance.antEnt(ent.pos);
            instance.RecuperarTemp(ent,tam);
            instance.aggregarTemp(temp);
            if (simFuncion.tipo.tipo != Simbolo.EnumTipoDato.BOOLEAN) { 
                Retornar tempoRet = new Retornar(temp, true, simFuncion.tipo.tipo, simFuncion, simFuncion.tipo);
                tempoRet.getValue();
                return tempoRet;
            }           
            Retornar reto = new Retornar("",false,simFuncion.tipo.tipo,simFuncion,simFuncion.tipo);
            this.truelabel = this.truelabel == "" ? instance.newLabel() : this.truelabel;
            this.falselabel = this.falselabel == "" ? instance.newLabel() : this.falselabel;
            instance.addIf(temp, "1", "==", this.truelabel);
            instance.addGoto(this.falselabel);
            reto.trueLabel = this.truelabel;
            reto.falseLabel = this.falselabel;
            reto.getValue();
            return reto;      
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}
