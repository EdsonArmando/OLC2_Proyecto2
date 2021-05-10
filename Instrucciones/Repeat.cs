using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Repeat : Instruccion
    {
        Expresion condicion;
        LinkedList<Instruccion> listaIntr;
        public Repeat(Expresion condicion, LinkedList<Instruccion> listaIntr)
        {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Generator3D instance = Generator3D.getInstance();
            instance.agregarComentario("Inicia Repeat", isFunc);
            this.condicion.truelabel = instance.newLabel();
            this.condicion.falselabel = instance.newLabel();
            instance.addLabel(this.condicion.falselabel, isFunc);
            foreach (Instruccion ins in listaIntr) {
                Retornar ret = ins.Compilar(ent,Ambito,AST, isFunc);
            }
            Retornar cond = this.condicion.Compilar(ent, isFunc);
            if (cond.tipo == Simbolo.EnumTipoDato.BOOLEAN) {
                instance.addLabel(cond.trueLabel, isFunc);
                instance.agregarComentario("Finaliza Repeat", isFunc);
                return null;
            }
            return null;
        }
    }
}
