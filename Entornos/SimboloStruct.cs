using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto2_Compi2.Entornos
{
    class SimboloStruct
    {
        public String identifier;
        public int size;
        public LinkedList<Parametros> attributess;
        public SimboloStruct(String iden, int tamanio, LinkedList<Parametros> listPara ) {
            this.identifier = iden;
            this.size = tamanio;
            this.attributess = listPara;
        }
        public int getPosAttribute(String id) {
            for (int i=0;i<attributess.Count;i++) {
                Parametros value = attributess.ElementAt(i);
                if (value.id == id) {
                    return i;
                }
            }
            return 0;
        }
        public Parametros getAttribute(String id)
        {
            for (int i = 0; i < attributess.Count; i++)
            {
                Parametros value = attributess.ElementAt(i);
                if (value.id == id)
                {
                    return value;
                }
            }
            return null;
        }
        public Simbolo.EnumTipoDato getTipo(String id)
        {
            for (int i = 0; i < attributess.Count; i++)
            {
                Parametros value = attributess.ElementAt(i);
                if (value.id == id)
                {
                    return value.type.tipo;
                }
            }
            return 0;
        }
    }
}
