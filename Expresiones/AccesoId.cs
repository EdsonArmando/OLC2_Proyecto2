using Proyecto1_Compi2;
using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2_Compi2.Expresiones
{
    class AccesoId : Expresion
    {
        private Expresion id;
        private Expresion anterior;
        public String idVariable;
        public AccesoId(Expresion anteri,Expresion identificador, String variable) {
            this.id = identificador;
            this.anterior = anteri;
            this.idVariable = variable;
        }
        public override Retornar Compilar(Entorno ent)
        {
            Generator3D generator = Generator3D.getInstance();
            Retornar izquierda = null;
            if (anterior != null)  izquierda = anterior.Compilar(ent);
            if (idVariable != null)
            {
                if (!ent.existeVariable(idVariable)) return new Retornar(idVariable,false,Simbolo.EnumTipoDato.STRING,null);
                Simbolo sim = ent.obtener(idVariable, ent);
                String temp = generator.newTemporal();
                if (sim.isGlobal)
                {
                    generator.addGetStack(temp, sim.posicion);
                    if (sim.tipo != Simbolo.EnumTipoDato.BOOLEAN) return new Retornar(temp, true, sim.tipo, sim);
                    Retornar retorno = new Retornar("", false, sim.tipo, sim);
                    this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                    this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;
                    generator.addIf(temp, "1", "==", this.truelabel);
                    generator.addGoto(this.falselabel);
                    retorno.trueLabel = this.truelabel;
                    retorno.falseLabel = this.falselabel;
                    return retorno;
                }
                else
                {
                    //La variable esta en el Heap
                    String tempAux = generator.newTemporal(); generator.freeTemp(tempAux);
                    generator.addExpression(tempAux, "p", sim.posicion.ToString(), "+");
                    generator.addGetStack(temp, tempAux);
                    if (sim.tipo != Simbolo.EnumTipoDato.BOOLEAN) return new Retornar(temp, true, sim.tipo, sim,sim.tipoStruc);

                    Retornar retorno = new Retornar("", false, sim.tipo,sim,sim.tipoStruc);
                    this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                    this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;
                    generator.addIf(temp, "1", "==", this.truelabel);
                    generator.addGoto(this.falselabel);
                    retorno.trueLabel = this.truelabel;
                    retorno.falseLabel = this.falselabel;
                    return retorno;
                }

            }
            else {
                Retornar ret = id.Compilar(ent);
                Simbolo sim = (Simbolo)izquierda.sim;
                TipoDato tipoSim = sim.tipoStruc;
                SimboloStruct structSim = tipoSim.sim;
                Parametros attribute = structSim.getAttribute(ret.getValue());
                int index = structSim.getPosAttribute(ret.getValue());
                String tempAux = generator.newTemporal(); generator.freeTemp(tempAux);
                String temp = generator.newTemporal();
                generator.addExpression(tempAux, izquierda.getValue(), index.ToString(), "+"); //Busca la posicion del atributo
                generator.addGetHeap(temp, tempAux); //Trae el valor del heap
                return new Retornar(temp, true, attribute.type.tipo,null);
            }
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}

