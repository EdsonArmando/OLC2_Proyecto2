using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Instrucciones;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Arimetica : Abstracto.Expresion
    {
        private Expresion operadorDer;
        private Expresion operadorIzq;
        private Tipo_operacion tipo;
        public Arimetica(Expresion operadorIzq, Expresion operadorDer, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }
        public Arimetica(Expresion operadorIzq, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
        }
        public Arimetica(String a, Tipo_operacion tipo)
        {
            this.valor = a;
            this.tipo = tipo;
        }
        public Arimetica(Double a)
        {
            this.valor = a;
            this.tipo = Tipo_operacion.NUMERO;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
        public override Retornar Compilar(Entorno ent)
        {                       
            if (tipo == Tipo_operacion.DIVISION)
            {
                Retornar valorIzqu = operadorIzq.Compilar(ent);
                Retornar valorDerecho = operadorDer.Compilar(ent);
                String temp = Generator3D.getInstance().newTemporal();
                Generator3D.getInstance().addExpression(temp,valorIzqu.getValue(),valorDerecho.getValue(),"/");
                return new Retornar(temp,true,Simbolo.EnumTipoDato.DOUBLE,null);
            }
            else if (tipo == Tipo_operacion.NUMERO)
            {                
                return new Retornar(this.valor.ToString(), false, Simbolo.EnumTipoDato.DOUBLE, "");
            }
            else if (tipo == Tipo_operacion.SUMA)
            {
                Retornar valorIzqu = operadorIzq.Compilar(ent);
                Retornar valorDerecho = operadorDer.Compilar(ent);
                String temp = Generator3D.getInstance().newTemporal();
                Generator3D.getInstance().addExpression(temp, valorIzqu.getValue(), valorDerecho.getValue(), "+");
                return new Retornar(temp, true, Simbolo.EnumTipoDato.DOUBLE, "");
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                Retornar valorIzqu = operadorIzq.Compilar(ent);
                Retornar valorDerecho = operadorDer.Compilar(ent);
                String temp = Generator3D.getInstance().newTemporal();
                Generator3D.getInstance().addExpression(temp, valorIzqu.getValue(), valorDerecho.getValue(), "-");
                return new Retornar(temp, true, Simbolo.EnumTipoDato.DOUBLE, "");
            }
            else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                Retornar valorIzqu = operadorIzq.Compilar(ent);
                Retornar valorDerecho = operadorDer.Compilar(ent);
                String temp = Generator3D.getInstance().newTemporal();
                Generator3D.getInstance().addExpression(temp, valorIzqu.getValue(), valorDerecho.getValue(), "*");
                return new Retornar(temp, true, Simbolo.EnumTipoDato.DOUBLE, null);
            }
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {
                Retornar valorIzqu = this.operadorIzq.Compilar(ent);
                Retornar dere = null;
                switch (valorIzqu.tipo) {                    
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.REAL:
                        dere = this.operadorDer.Compilar(ent);
                        switch (dere.tipo) {
                            case Simbolo.EnumTipoDato.DOUBLE:
                            case Simbolo.EnumTipoDato.INT:
                            case Simbolo.EnumTipoDato.REAL:
                                this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
                                this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
                                Generator3D.getInstance().addIf(valorIzqu.getValue(), dere.getValue(), ">", this.truelabel);
                                Generator3D.getInstance().addGoto(this.falselabel);
                                Retornar ret = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, null);
                                ret.trueLabel = this.truelabel;
                                ret.falseLabel = this.falselabel;
                                return ret;                  
                        }
                        break;                                                               
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String trueLabel = Generator3D.getInstance().newLabel();
                        String falseLabel = Generator3D.getInstance().newLabel();
                        Generator3D.getInstance().addLabel(valorIzqu.trueLabel);
                        this.operadorDer.truelabel = trueLabel;
                        this.operadorDer.falselabel = falseLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        Generator3D.getInstance().addLabel(valorIzqu.falseLabel);
                        this.operadorDer.truelabel = falseLabel;
                        this.operadorDer.falselabel = trueLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        if ((Simbolo.EnumTipoDato)dere.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            Retornar retorno = new Retornar("", false, operadorIzq.tipo, null);
                            retorno.trueLabel = trueLabel;
                            retorno.falseLabel = falseLabel;
                            return retorno;
                        }
                        break;
                }                
            }
            else if (tipo == Tipo_operacion.DIFERENCIACION)
            {
                
            }           
            else if (tipo == Tipo_operacion.OR)
            {
                
            }
            else if (tipo == Tipo_operacion.XOR)
            {
               
            }
            else if (tipo == Tipo_operacion.DIFERENTE)
            {
               
            }
            else if (tipo == Tipo_operacion.IGUAL_QUE)
            {
                Retornar left = this.operadorIzq.Compilar(ent);
                Retornar dere = null;
                switch (left.tipo) {
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.REAL:
                        dere = this.operadorDer.Compilar(ent);
                    switch (dere.tipo)
                    {
                        case Simbolo.EnumTipoDato.DOUBLE:
                        case Simbolo.EnumTipoDato.INT:
                        case Simbolo.EnumTipoDato.REAL:
                            this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
                            this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
                            Generator3D.getInstance().addIf(left.getValue(), dere.getValue(), "==", this.truelabel);
                            Generator3D.getInstance().addGoto(this.falselabel);                                       
                            Retornar retorno = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, "");
                            retorno.trueLabel = this.truelabel;
                            retorno.falseLabel = this.falselabel;
                            return retorno;
                        default:
                            break;
                    }
                    break;
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String trueLabel = Generator3D.getInstance().newLabel();
                        String falseLabel = Generator3D.getInstance().newLabel();

                        Generator3D.getInstance().addLabel(left.trueLabel);
                        this.operadorDer.truelabel = trueLabel;
                        this.operadorDer.falselabel = falseLabel;
                        dere = this.operadorDer.Compilar(ent);

                        Generator3D.getInstance().addLabel(left.falseLabel);
                        this.operadorDer.truelabel = falseLabel;
                        this.operadorDer.falselabel = trueLabel;
                        dere = this.operadorDer.Compilar(ent);
                        if (dere.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            Retornar retorno = new Retornar("", false, left.tipo,null);
                            retorno.trueLabel = trueLabel;
                            retorno.falseLabel = falseLabel;
                            return retorno;
                        }
                        break;
                }
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                Retornar valorIzqu = this.operadorIzq.Compilar(ent);
                Retornar dere = null;
                switch (valorIzqu.tipo)
                {
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.REAL:
                        dere = this.operadorDer.Compilar(ent);
                        switch (dere.tipo)
                        {
                            case Simbolo.EnumTipoDato.INT:
                            case Simbolo.EnumTipoDato.REAL:
                            case Simbolo.EnumTipoDato.DOUBLE:
                                this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
                                this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
                                Generator3D.getInstance().addIf(valorIzqu.getValue(), dere.getValue(), "<", this.truelabel);
                                Generator3D.getInstance().addGoto(this.falselabel);
                                Retornar ret = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, null);
                                ret.trueLabel = this.truelabel;
                                ret.falseLabel = this.falselabel;
                                return ret;
                        }
                        break;
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String trueLabel = Generator3D.getInstance().newLabel();
                        String falseLabel = Generator3D.getInstance().newLabel();
                        Generator3D.getInstance().addLabel(valorIzqu.trueLabel);
                        this.operadorDer.truelabel = trueLabel;
                        this.operadorDer.falselabel = falseLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        Generator3D.getInstance().addLabel(valorIzqu.falseLabel);
                        this.operadorDer.truelabel = falseLabel;
                        this.operadorDer.falselabel = trueLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        if ((Simbolo.EnumTipoDato)dere.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            Retornar retorno = new Retornar("", false, operadorIzq.tipo, null);
                            retorno.trueLabel = trueLabel;
                            retorno.falseLabel = falseLabel;
                            return retorno;
                        }
                        break;
                }
            }
            else if (tipo == Tipo_operacion.MOD)
            {
                
            }
            else if (tipo == Tipo_operacion.MENOR_IGUAL_QUE)
            {
                Retornar valorIzqu = this.operadorIzq.Compilar(ent);
                Retornar dere = null;
                switch (valorIzqu.tipo)
                {
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.REAL:
                    case Simbolo.EnumTipoDato.DOUBLE:
                        dere = this.operadorDer.Compilar(ent);
                        switch (dere.tipo)
                        {
                            case Simbolo.EnumTipoDato.INT:
                            case Simbolo.EnumTipoDato.REAL:
                            case Simbolo.EnumTipoDato.DOUBLE:
                                this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
                                this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
                                Generator3D.getInstance().addIf(valorIzqu.getValue(), dere.getValue(), "<=", this.truelabel);
                                Generator3D.getInstance().addGoto(this.falselabel);
                                Retornar ret = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, null);
                                ret.trueLabel = this.truelabel;
                                ret.falseLabel = this.falselabel;
                                return ret;
                        }
                        break;
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String trueLabel = Generator3D.getInstance().newLabel();
                        String falseLabel = Generator3D.getInstance().newLabel();
                        Generator3D.getInstance().addLabel(valorIzqu.trueLabel);
                        this.operadorDer.truelabel = trueLabel;
                        this.operadorDer.falselabel = falseLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        Generator3D.getInstance().addLabel(valorIzqu.falseLabel);
                        this.operadorDer.truelabel = falseLabel;
                        this.operadorDer.falselabel = trueLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        if ((Simbolo.EnumTipoDato)dere.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            Retornar retorno = new Retornar("", false, operadorIzq.tipo, null);
                            retorno.trueLabel = trueLabel;
                            retorno.falseLabel = falseLabel;
                            return retorno;
                        }
                        break;
                }
            }
            else if (tipo == Tipo_operacion.MAYOR_IGUAL_QUE)
            {
                Retornar valorIzqu = this.operadorIzq.Compilar(ent);
                Retornar dere = null;
                switch (valorIzqu.tipo)
                {
                    case Simbolo.EnumTipoDato.INT:
                    case Simbolo.EnumTipoDato.DOUBLE:
                    case Simbolo.EnumTipoDato.REAL:
                        dere = this.operadorDer.Compilar(ent);
                        switch (dere.tipo)
                        {
                            case Simbolo.EnumTipoDato.INT:
                            case Simbolo.EnumTipoDato.DOUBLE:
                            case Simbolo.EnumTipoDato.REAL:
                                this.truelabel = this.truelabel == "" ? Generator3D.getInstance().newLabel() : this.truelabel;
                                this.falselabel = this.falselabel == "" ? Generator3D.getInstance().newLabel() : this.falselabel;
                                Generator3D.getInstance().addIf(valorIzqu.getValue(), dere.getValue(), ">=", this.truelabel);
                                Generator3D.getInstance().addGoto(this.falselabel);
                                Retornar ret = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, null);
                                ret.trueLabel = this.truelabel;
                                ret.falseLabel = this.falselabel;
                                return ret;
                        }
                        break;
                    case Simbolo.EnumTipoDato.BOOLEAN:
                        String trueLabel = Generator3D.getInstance().newLabel();
                        String falseLabel = Generator3D.getInstance().newLabel();
                        Generator3D.getInstance().addLabel(valorIzqu.trueLabel);
                        this.operadorDer.truelabel = trueLabel;
                        this.operadorDer.falselabel = falseLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        Generator3D.getInstance().addLabel(valorIzqu.falseLabel);
                        this.operadorDer.truelabel = falseLabel;
                        this.operadorDer.falselabel = trueLabel;
                        dere = this.operadorIzq.Compilar(ent);
                        if ((Simbolo.EnumTipoDato)dere.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                        {
                            Retornar retorno = new Retornar("", false, operadorIzq.tipo, null);
                            retorno.trueLabel = trueLabel;
                            retorno.falseLabel = falseLabel;
                            return retorno;
                        }
                        break;
                }
            }
            else if (tipo == Tipo_operacion.TRUE)
            {  
                Generator3D generator = Generator3D.getInstance();
                Retornar retorno = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN,null,new TipoDato(Simbolo.EnumTipoDato.BOOLEAN,null,null));
                this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;            
                generator.addGoto(this.truelabel);                                           
                retorno.trueLabel = this.truelabel;
                retorno.falseLabel = this.falselabel;
                return retorno;
            }
            else if (tipo == Tipo_operacion.FALSE)
            {
                Generator3D generator = Generator3D.getInstance();
                Retornar retorno = new Retornar("", false, Simbolo.EnumTipoDato.BOOLEAN, null, new TipoDato(Simbolo.EnumTipoDato.BOOLEAN, null, null));
                this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;
                generator.addGoto(this.falselabel);
                retorno.trueLabel = this.truelabel;
                retorno.falseLabel = this.falselabel;
                return retorno;
            }
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                Retornar izquier = this.operadorIzq.Compilar(ent);
                return new Retornar("-"+izquier.getValue(), false, Simbolo.EnumTipoDato.DOUBLE, "");
            }
            /*else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " * " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.CADENA)
            {
                return str.Append("'" + valor.ToString()+"'");
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " - " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.POTENCIA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " ^ " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.SUMA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " + " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            
            else if (tipo == Tipo_operacion.NUMERO)
            {
                return str.Append(valor.ToString());
            }
            else if (tipo == Tipo_operacion.TRUE)
            {
                return str.Append("true");
            }
            else if (tipo == Tipo_operacion.FALSE)
            {
                return str.Append("false");
            }
            else if (tipo == Tipo_operacion.IDENTIFICADOR)
            {
                return str.Append(valor.ToString());
            }
            */
            return null;
        }

        public enum Tipo_operacion
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            NEGATIVO,
            NUMERO,
            LETRA,
            IDENTIFICADOR,
            CADENA,
            POTENCIA,
            CONCATENACION,
            MAYOR_QUE,
            MENOR_QUE,
            IGUAL_QUE,
            AND,
            MOD,
            OR,
            TRUE,
            FALSE,
            XOR,
            DIFERENTE,
            MENOR_IGUAL_QUE,
            MAYOR_IGUAL_QUE,
            DIFERENCIACION
        }
    }
}
