using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Proyecto1_Compi2.Expresiones;
using Proyecto1_Compi2.Analizadores;
using Proyecto2_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;

namespace Proyecto1_Compi2.Instrucciones
{
    class Funcion : PlantillaFuncion, Instruccion
    {
        public int fila, columna;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public LinkedList<Instruccion> formalesParam;
        public bool compilado;
        Simbolo.EnumTipoDato valor;
        public Funcion(String id, LinkedList<Instruccion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales, String nombreOri,Simbolo.EnumTipoDato tipo)
        {
            this.tipo = new TipoDato(valor, "void", null);
            this.compilado = true;
            this.id = id;
            this.param = this.devListParametros(param_Formales);
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
            this.idUnico = nombreOri;
            this.valor = tipo;
        }
        public override Retornar Compilar(Entorno ent)
        {
            return null;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            if (this.compilado)
            {
                this.compilado = false;
                String idUnico = this.uniqueId(ent);
                if (!ent.addFuncion(this, idUnico))
                    Form1.salidaConsola.AppendText("Ya existe una Funcion con ese nombre!!!");
            }
            SimboloFuncion sim = ent.getFuncion(this.id);
            sim.tipo.tipo = valor;
            if (sim != null)
            {
                Generator3D instance = Generator3D.getInstance();
                Entorno entFunc = new Entorno(ent);
                String returnLabel = instance.newLabel();
                object tempSt = instance.getTempStorage();
                entFunc.setEnviorementFunc(this.id, sim, returnLabel);
                foreach (Parametros param in this.param)
                {
                    //Significan que recibo un array o struct en la funcion
                    if (param.tipoStrucoArray != null)
                    {
                        SimboloStruct structSim = ent.getStruct(param.tipoStrucoArray);
                        if (structSim != null)
                        {
                            entFunc.Insertar(param.id, Simbolo.EnumTipoDato.OBJETO_TYPE, false, false, new TipoDato(Simbolo.EnumTipoDato.OBJETO_TYPE, "", structSim), null, null, null,null);
                        }
                        else
                        {
                            Simbolo array = ent.obtener(param.tipoStrucoArray, ent);
                            entFunc.Insertar(param.id, Simbolo.EnumTipoDato.ARRAY, false, false, array.tipoStruc, array.posicion_X, array.posicion_Y, array.posicion_Z,null);
                        }
                    }
                    else
                    {
                        entFunc.Insertar(param.id, param.type.tipo, false, false, param.type, null, null, null,null);
                    }
                }
                instance.LimpiarStorage();
                instance.addInicioProc(sim.idUnico);
                instance.agregarComentario("Declaraciones Locales");
                foreach (Instruccion ins in listVarLocales)
                {
                    ins.Compilar(entFunc, sim.idUnico, AST);
                }
                foreach (Instruccion ins in listInstrucciones)
                {
                    ins.Compilar(entFunc, sim.idUnico, AST);
                }
                instance.addLabel(returnLabel);
                instance.addFinal();
                instance.setTempStorage(tempSt);
            }
            return null;
        }
        public String uniqueId(Entorno ent)
        {
            String id = ent.prop + "_" + this.id;
            if (this.param.Count == 0)
                return id + "_empty";
            foreach (Parametros para in this.param)
            {
                id += "_" + para.getUnicType();
            }
            return id;
        }
        private LinkedList<Parametros> devListParametros(LinkedList<Instruccion> declaraciones)
        {
            LinkedList<Parametros> listParam = new LinkedList<Parametros>();
            foreach (Declaracion dcl in declaraciones)
            {
                if (dcl.tipoVariable == Simbolo.EnumTipoDato.NULL)
                {
                    listParam.AddLast(new Parametros(dcl.nombreVariable, new TipoDato(dcl.tipoVariable, dcl.nameArra, null), dcl.tipoDinamico));
                }
                else
                {
                    listParam.AddLast(new Parametros(dcl.nombreVariable, new TipoDato(dcl.tipoVariable, dcl.nameArra, null), null));
                }
            }
            return listParam;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override void setParametros(LinkedList<Expresion> parametros)
        {
            this.param_Actuales = parametros;
        }
    }
}
