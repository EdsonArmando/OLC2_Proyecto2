using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Return : Instruccion
    {
        Expresion valorReturn;
        public Return(Expresion valor)
        {
            this.valorReturn = valor;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST,bool isFunc)
        {
            Retornar ret = new Retornar("0",false,Simbolo.EnumTipoDato.VOID,null,new TipoDato(Simbolo.EnumTipoDato.VOID,null,null));
            if (valorReturn != null) {
                ret = valorReturn.Compilar(ent, isFunc);
            }
            SimboloFuncion simFuncion = ent.actualFunc;
            if (simFuncion.tipo.tipo == Simbolo.EnumTipoDato.BOOLEAN)
            {
                String templabel = Generator3D.getInstance().newLabel();
                Generator3D.getInstance().addLabel(ret.trueLabel, isFunc);
                Generator3D.getInstance().addSetStack("sp", "1", isFunc);
                Generator3D.getInstance().addGoto(templabel, isFunc);
                Generator3D.getInstance().addLabel(ret.falseLabel, isFunc);
                Generator3D.getInstance().addSetStack("sp", "0", isFunc);
                Generator3D.getInstance().addLabel(templabel, isFunc);
            }
            else if (simFuncion.tipo.tipo != Simbolo.EnumTipoDato.VOID)
                Generator3D.getInstance().addSetStack("sp",ret.getValue(), isFunc);
            Generator3D.getInstance().addGoto(ent.Return, isFunc);
            return null;
        }
    }
}
