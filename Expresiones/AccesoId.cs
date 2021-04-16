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
        //Derecha
        public Expresion id;
        //Izquierda
        private Expresion anterior;
        public String idVariable;
        public AccesoId(Expresion anteri, Expresion identificador, String variable)
        {
            this.id = identificador;
            this.anterior = anteri;
            this.idVariable = variable;
        }
        public override Retornar Compilar(Entorno ent)
        {
            Generator3D generator = Generator3D.getInstance();
            //Entorno Global
            Retornar padre = null;
            if (anterior != null)
                padre = anterior.Compilar(ent);
            if (anterior != null)
            {
                //Parametro que se desa Buscar
                Retornar ID = id.Compilar(ent);
                //Simbolo que trae el anterior   
                if (padre.tipo == Simbolo.EnumTipoDato.STRING) {
                    return padre;
                }
                Simbolo sim = (Simbolo)padre.sim;
                TipoDato tip = sim.tipoStruc;
                SimboloStruct structSim = tip.sim;                
                Parametros attribute = structSim.getAttribute(ID.getValue());
                //Posicion en donde se encuentra
                int index = structSim.getPosAttribute(ID.getValue());
                //Buscar la Variable
                String tempAux = generator.newTemporal(); generator.freeTemp(tempAux);
                String temp = generator.newTemporal();
                generator.addExpression(tempAux, padre.getValue(), index.ToString(), "+"); //Busca la posicion del atributo
                generator.addGetHeap(temp, tempAux); //Trae el valor del heap
                if (attribute.type.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE) {
                    SimboloStruct sim2 = ent.getStruct(attribute.type.tipoId);
                    AccesoId idTotal = (AccesoId)this.id;
                    if (idTotal.id == null) {
                        return new Retornar(temp, true, attribute.type.tipo, null);
                    }
                    Retornar idTemp = idTotal.id.Compilar(ent);
                    //Busco la Variable
                    Parametros attribute2 = sim2.getAttribute(idTemp.getValue());
                    //Posicion en donde se encuentra
                    int index2 = sim2.getPosAttribute(idTemp.getValue());
                    //Buscar la Variable
                    String tempAux2 = generator.newTemporal(); generator.freeTemp(tempAux);
                    String temp2 = generator.newTemporal();
                    generator.addExpression(tempAux2, temp, index2.ToString(), "+"); //Busca la posicion del atributo
                    generator.addGetHeap(temp2, tempAux2); //Trae el valor del heap
                    return new Retornar(temp2, true, attribute2.type.tipo, null);
                }
                return new Retornar(temp, true, attribute.type.tipo, null);
            }
            if (anterior == null)
            {
                if (!ent.existeVariable(idVariable))
                    return new Retornar(idVariable, false, Simbolo.EnumTipoDato.STRING, null);
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
                    if (sim.tipo != Simbolo.EnumTipoDato.BOOLEAN) return new Retornar(temp, true, sim.tipo, sim, sim.tipoStruc);
                    Retornar retorno = new Retornar("", false, sim.tipo, sim, sim.tipoStruc);
                    this.truelabel = this.truelabel == "" ? generator.newLabel() : this.truelabel;
                    this.falselabel = this.falselabel == "" ? generator.newLabel() : this.falselabel;
                    generator.addIf(temp, "1", "==", this.truelabel);
                    generator.addGoto(this.falselabel);
                    retorno.trueLabel = this.truelabel;
                    retorno.falseLabel = this.falselabel;
                    return retorno;
                }
            }
            return null;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }
    }
}

