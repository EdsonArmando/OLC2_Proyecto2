using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Aumento : Instruccion
    {
        String id;
        public Aumento(String id)
        {
            this.id = id;
        }

        public Retornar Compilar(Entorno ent, string Ambito, Sintactico AST)
        {
            Generator3D instance = Generator3D.getInstance();
            Simbolo variable = ent.obtener(id.ToLower(), ent);
            if (variable.isGlobal)
            {
                //Temporal donde se Guardara el Valor de Variable Actual
                String tempVal = instance.newTemporal();instance.freeTemp(tempVal);
                instance.addGetStack(tempVal,variable.posicion);
                //Temp dondeGuardo el valor de la Variable
                String tempTotal = instance.newTemporal();instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal, tempVal, "1","+") ;
                instance.addSetStack(variable.posicion.ToString(), tempTotal);
            }
            else {

                //Seteo El nuevo Valor
                String temp = instance.newTemporal();
                instance.freeTemp(temp);
                //Posicion de donde se encuentra la Variable
                instance.addExpression(temp, "p", variable.posicion.ToString(), "+");
                //Temporal donde se Guardara el Valor de Variable Actual
                String tempVal = instance.newTemporal(); instance.freeTemp(tempVal);
                instance.addGetStack(tempVal, temp);
                //Temp dondeGuardo el valor de la Variable
                String tempTotal = instance.newTemporal(); instance.freeTemp(tempTotal);
                instance.addExpression(tempTotal, tempVal, "1", "+");
                instance.addSetStack(temp,tempTotal);
            }

            return null;
        }
    }
}
