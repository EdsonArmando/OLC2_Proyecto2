using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Instrucciones;
using Proyecto2_Compi2.Code3D;
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
            Retornar valorIzqu = operadorIzq.Compilar(ent);
            Retornar valorDerecho = operadorDer.Compilar(ent);
            String temp = Generator3D.getInstance().newTemporal();
            if (tipo == Tipo_operacion.DIVISION)
            {
                Generator3D.getInstance().addExpression(temp,valorIzqu.getValue(),valorDerecho.getValue(),"/");
                return new Retornar(temp,true,Simbolo.EnumTipoDato.DOUBLE,null);
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
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                return str.Append("-" + operadorIzq.Traducir(ent, temp).ToString());
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
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {

                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " > " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.DIFERENCIACION)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " <> " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.AND)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " and " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.OR)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " or " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.XOR)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " > " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.DIFERENTE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString());
            }
            else if (tipo == Tipo_operacion.IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " = " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " < " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MOD)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " % " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MENOR_IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " <= " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MAYOR_IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " >= " + operadorDer.Traducir(ent, tempDere).ToString());
            }*/
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
