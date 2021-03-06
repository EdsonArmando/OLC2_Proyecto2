using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using Proyecto2_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Instruccion
    {
        public Simbolo.EnumTipoDato tipoVariable;
        public LinkedList<Expresion> variables;
        public String nombreVariable;
        public Expresion expresion;
        public int fila, columna;
        public bool isStruct_Array = false;
        public String esReferencia_const;
        public String nameArra;
        public String tipoDinamico;
        public TipoDato tipoStruct = new TipoDato(Simbolo.EnumTipoDato.NULL,"",null);

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String tip)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.tipoDinamico = tip;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esreferencia,String tip)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tip;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.variables = valores;
            this.esReferencia_const = esreferencia;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String esReferencia_const, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tipoDi;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esReferencia_const,String nameArray, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.variables = valores;
            this.expresion = expresion;
            this.fila = fila;
            this.tipoDinamico = tipoDi;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna, String esReferencia_const, String nameArray,String tipoDi)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.tipoDinamico = tipoDi;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Generator3D instance = Generator3D.getInstance();
            Retornar value = null;
            
            if (expresion == null && tipoVariable==Simbolo.EnumTipoDato.BOOLEAN) {
                expresion = new Arimetica(new Literal(Simbolo.EnumTipoDato.BOOLEAN, false), Arimetica.Tipo_operacion.FALSE);     
            }
            if (tipoVariable == Simbolo.EnumTipoDato.OBJETO_TYPE) {
                value = (new ResvarStruct(nameArra)).Compilar(ent, isFunc);
            }
            this.esType(ent);
            //Lista de Expre
            if (variables!=null) {
                foreach (AccesoId nombre in variables) {
                    Simbolo sim = ent.Insertar(nombre.idVariable, tipoVariable, false, false, tipoStruct, null, null, null,null,Ambito);
                    if (sim.isGlobal)
                    {
                        if (expresion != null)
                            value = this.expresion.Compilar(ent, isFunc);

                        if (this.tipoVariable == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            String templabel = instance.newLabel();
                            instance.addLabel(value.trueLabel, isFunc);
                            instance.addSetStack(sim.posicion.ToString(), "1", isFunc);
                            instance.addGoto(templabel, isFunc);
                            instance.addLabel(value.falseLabel, isFunc);
                            instance.addSetStack(sim.posicion.ToString(), "0", isFunc);
                            instance.addLabel(templabel, isFunc);
                        }
                        else
                        {
                            if (value == null)
                            {                               
                                instance.addSetStack(sim.posicion.ToString(), "0", isFunc);
                            }
                            else
                            {
                                instance.addSetStack(sim.posicion.ToString(), value.getValue(), isFunc);
                            }
                        }
                    }
                    else
                    {
                        String temp = instance.newTemporal(); instance.freeTemp(temp);
                        instance.addExpression(temp, "sp", sim.posicion.ToString(), "+", isFunc);
                        if (expresion != null)
                            value = this.expresion.Compilar(ent, isFunc);

                        if (this.tipoVariable == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            String templabel = instance.newLabel();
                            instance.addLabel(value.trueLabel, isFunc);
                            instance.addSetStack(temp, "1", isFunc);
                            instance.addGoto(templabel, isFunc);
                            instance.addLabel(value.falseLabel, isFunc);
                            instance.addSetStack(temp, "0", isFunc);
                            instance.addLabel(templabel, isFunc);
                        }
                        else
                        {
                            if (expresion == null)
                            {
                                instance.addSetStack(temp, "0", isFunc);
                            }
                            else
                            {
                                instance.addSetStack(temp, value.getValue(), isFunc);
                            }
                        }
                    }
                }
            }
            //Variables del tipo var id : tipo = expr;
            if (variables==null) {
                Simbolo sim;
                //Solo para arreglos
                if (nameArra != null && isStruct_Array == false )
                {
                    Simbolo temp = ent.obtener(nameArra,ent);
                    sim = ent.Insertar(nombreVariable, temp.tipo, false, false, temp.tipoStruc, temp.posicion_X, temp.posicion_Y, temp.posicion_Z,null,Ambito);
                }
                else {
                    sim = ent.Insertar(nombreVariable, tipoVariable, false, false, tipoStruct, null, null, null,null,Ambito);
                }                
                if (sim.isGlobal) {
                    if (expresion != null)
                        value = this.expresion.Compilar(ent, isFunc);

                    if (this.tipoVariable == Simbolo.EnumTipoDato.BOOLEAN)
                    {
                        String templabel = instance.newLabel();
                        instance.addLabel(value.trueLabel, isFunc);
                        instance.addSetStack(sim.posicion.ToString(), "1", isFunc);
                        instance.addGoto(templabel, isFunc);
                        instance.addLabel(value.falseLabel, isFunc);
                        instance.addSetStack(sim.posicion.ToString(), "0", isFunc);
                        instance.addLabel(templabel, isFunc);
                    }
                    else
                    {
                        if (value == null)
                        {
                            instance.addSetStack(sim.posicion.ToString(), "0", isFunc);
                        }
                        else {
                            instance.addSetStack(sim.posicion.ToString(), value.getValue(), isFunc);
                        }                        
                    }
                }
                else {
                    String temp = instance.newTemporal(); instance.freeTemp(temp);
                    instance.addExpression(temp, "sp", sim.posicion.ToString(), "+", isFunc);
                    if (expresion != null)
                        value = this.expresion.Compilar(ent, isFunc);

                    if (this.tipoVariable == Simbolo.EnumTipoDato.BOOLEAN)
                    {
                        String templabel = instance.newLabel();
                        instance.addLabel(value.trueLabel, isFunc);
                        instance.addSetStack(temp, "1", isFunc);
                        instance.addGoto(templabel, isFunc);
                        instance.addLabel(value.falseLabel, isFunc);
                        instance.addSetStack(temp, "0", isFunc);
                        instance.addLabel(templabel, isFunc);
                    }
                    else
                    {
                        if (value == null)
                        {
                            instance.addSetStack(temp, "0",isFunc);
                        }
                        else {
                            instance.addSetStack(temp, value.getValue(), isFunc);
                        }                        
                    }
                }
            }
            return null;
        }
        private void esType(Entorno ent) {
            if (tipoVariable == Simbolo.EnumTipoDato.OBJETO_TYPE  && ent.existeStruct(nameArra)) {
                SimboloStruct structTemp = ent.getStruct(nameArra);
                this.tipoStruct.sim = structTemp;
                this.isStruct_Array = true;
                this.tipoStruct.tipo = tipoVariable;
            }
        }
        public void setExpresion(Expresion expr) {
            
        }        
    }
}
