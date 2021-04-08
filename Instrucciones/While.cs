using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class While : Instruccion
    {
        Expresion condicion;
        LinkedList<Instruccion> listaIntr;
        public While(Expresion condicion, LinkedList<Instruccion> listaIntr)
        {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            String labelWhile = instance.newLabel();
            instance.agregarComentario("Inicia While");
            instance.addLabel(labelWhile);
            Retornar condicionWhile = condicion.Compilar(ent);
            if (condicionWhile.tipo == Simbolo.EnumTipoDato.BOOLEAN)
            {
                instance.addLabel(condicionWhile.trueLabel);
                foreach (Instruccion ins in listaIntr) {
                    Retornar ret = ins.Compilar(ent, Ambito, AST);
                }
                instance.addGoto(labelWhile);
                instance.addLabel(condicionWhile.falseLabel);
                instance.agregarComentario("Finaliza While");
                return null;
            }
            else {
                Form1.salidaConsola.AppendText("No es una condicion boolena!!\n");
            }
            return null;       
        }
    }
}
