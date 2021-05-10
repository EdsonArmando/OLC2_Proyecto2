using Irony.Parsing;
using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using Proyecto1_Compi2.Instrucciones;
using Proyecto1_Compi2.Reportes;
using Proyecto2_Compi2.Code3D;
using Proyecto2_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Analizadores
{
    class Sintactico
    {
        public static LinkedList<Error> errores  = new LinkedList<Error>();
        private int fila = 0;
        private LinkedList<Type_Object> types = new LinkedList<Type_Object>();
        public void analizar(String entrada)
        {
            errores.Clear();
            types.Clear();
            Singleton.getInstance().limpiarEntorno();
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null || arbol.ParserMessages.Count() > 0)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++)
                {
                    errores.AddLast(new Error("Error", arbol.ParserMessages.ElementAt(i).Message, (arbol.ParserMessages.ElementAt(i).Location.Line + 1), arbol.ParserMessages.ElementAt(i).Location.Column));
                    Form1.salidaConsola.AppendText(arbol.ParserMessages.ElementAt(i).Level.ToString() + " Fila: " + (arbol.ParserMessages.ElementAt(i).Location.Line + 1)
                        + " Columna: " + arbol.ParserMessages.ElementAt(i).Location.Column
                        + "\n");
                }
                GraficarErrores();
                return;
            }
            else
            {
                /*GraficarAST graficar = new GraficarAST(raiz);
                graficar.recorrerRaiz(raiz);
                graficar.generarArchivo();*/               
                LinkedList<Abstracto.Instruccion> AST = Listainstrucciones(raiz.ChildNodes.ElementAt(3));
                Entornos.Entorno ent = new Entornos.Entorno(null);
                Generator3D.getInstance().getCode().Clear();
                Generator3D.getInstance().LimpiarStorage();
                Generator3D.getInstance().getCodeFuncion().Clear();
                Generator3D.getInstance().contTemp = 0;
                Generator3D.getInstance().NativePrintString(true);
                foreach (Abstracto.Instruccion ins in AST)
                {
                    ins.Compilar(ent, "global", this,false);
                }               
                StringBuilder encabezado = Generator3D.getInstance().agregarEncabezado();                
                StringBuilder code = Generator3D.getInstance().getCode();
                StringBuilder codeFuncion = Generator3D.getInstance().getCodeFuncion();
                Form1.salidaConsola.AppendText("#include <stdio.h>\nfloat Heap[100000];\nfloat Stack[100000];\nfloat h=0;\nfloat p=0;\n" + encabezado.ToString() + "\n" + codeFuncion.ToString() + "\nint main(){\n"+ code.ToString()  +"\nreturn 0;\n}");
            }
        }

        private void GraficarErrores()
        {
            int conts = 1;
            StreamWriter archivo = new StreamWriter("C:\\compiladores2\\TablaErrores_201701029.html");
            archivo.Write("<html>");
            archivo.Write("<head>");
            archivo.Write("<style>"
                    + "table{"
                    + "  font-family: arial, sans-serif; border-collapse: collapse;    width: 100%;}"
                    + "td, th{"
                    + "border: 1px solid #dddddd;text-align: left;  padding: 8px;}"
                    + "tr:nth-child(even){"
                    + " background-color: #dddddd;}"
                    + "</style>");
            archivo.Write("</head>");
            archivo.Write("<body>");
            archivo.Write("<H1>Tabla de Errores</H1>");
            archivo.Write("<br><br>");
            archivo.Write("<table>");
            archivo.Write("<tr><th>No</th><th>Nombre</th><th>Tipo</th><th>fila</th><th>columna</th></tr>");
            foreach (Error err in errores)
            {
                archivo.Write("<tr><td>" + conts + "</td><td>" + err.valor + "</td><td>" + err.tipo + "</td><td>" + err.fila+ "</td><td>"+ err.columna  + " </td></tr>");
                conts++;
            }

            archivo.Write("</table>");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
        }

        /*internal void traducir(string entrada)
        {
            errores.Clear();
            types.Clear();
            Singleton.getInstance().limpiarEntorno();
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null || arbol.ParserMessages.Count() > 0)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++)
                {
                    errores.AddLast(new Error("Error", arbol.ParserMessages.ElementAt(i).Message, (arbol.ParserMessages.ElementAt(i).Location.Line + 1), arbol.ParserMessages.ElementAt(i).Location.Column));
                    Form1.salidaConsola.AppendText(arbol.ParserMessages.ElementAt(i).Level.ToString() + " Fila: " + (arbol.ParserMessages.ElementAt(i).Location.Line + 1)
                        + " Columna: " + arbol.ParserMessages.ElementAt(i).Location.Column
                        + "\n");
                }
                GraficarErrores();
                return;
            }
            else
            {
                GraficarAST graficar = new GraficarAST(raiz);
                graficar.recorrerRaiz(raiz);
                graficar.generarArchivo();
                Form1.salidaConsola.AppendText("Se analizo correctamente\n");
                LinkedList<Abstracto.Instruccion> AST = Listainstrucciones(raiz.ChildNodes.ElementAt(3));
                Entornos.Entorno ent = new Entornos.Entorno(null);                
                foreach (Abstracto.Instruccion ins in AST)
                {
                    ins.Compilar(ent,"global",null);
                }           
            }
        }*/

        private Expresion[,] Dimensiones(ParseTreeNode actual, Expresion[,] valores)
        {
            if (actual.ChildNodes.Count == 4)
            {
                Expresion Inicio = expresion_numerica(actual.ChildNodes.ElementAt(0));
                Expresion ValorFin = expresion_numerica(actual.ChildNodes.ElementAt(3));
                valores[fila, 0] = Inicio;
                valores[fila, 1] = ValorFin;
                return valores;
            }
            else
            {
                Dimensiones(actual.ChildNodes.ElementAt(0), valores);
                fila += 1;
                Expresion Inicio = expresion_numerica(actual.ChildNodes.ElementAt(2));
                Expresion ValorFin = expresion_numerica(actual.ChildNodes.ElementAt(5));
                valores[fila, 0] = Inicio;
                valores[fila, 1] = ValorFin;
                return valores;
            }
        }
        //Recorrer Raiz
        private LinkedList<Abstracto.Instruccion> Listainstrucciones(ParseTreeNode actual)
        {
            LinkedList<Abstracto.Instruccion> instrucciones = new LinkedList<Abstracto.Instruccion>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                Abstracto.Instruccion nuevo = instruccion(actual.ChildNodes.ElementAt(i), "", null, instrucciones);
            }
            return instrucciones;
        }
        private LinkedList<Abstracto.Instruccion> Listainstrucciones2(ParseTreeNode actual)
        {
            LinkedList<Abstracto.Instruccion> instrucciones = new LinkedList<Abstracto.Instruccion>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                String esFuncion = actual.ChildNodes.ElementAt(i).ChildNodes.ElementAt(0).Term.Name;
                if (esFuncion != "funcion" && esFuncion != "procedure")
                {
                    Abstracto.Instruccion nuevo = instruccion(actual.ChildNodes.ElementAt(i), "", null, instrucciones);
                }
            }
            return instrucciones;
        }
        private LinkedList<Abstracto.Expresion> listaExpresiones(ParseTreeNode actual)
        {

            return null;
        }
        private LinkedList<Abstracto.Instruccion> listInstr2Temp(ParseTreeNode actual)
        {
            LinkedList<Abstracto.Instruccion> instrucciones = new LinkedList<Abstracto.Instruccion>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                Abstracto.Instruccion nuevo = devDeclaracionProcedure_Funciones(actual.ChildNodes.ElementAt(i));
                instrucciones.AddLast(nuevo);
            }
            return instrucciones;
        }
        private Abstracto.Instruccion devDeclaracionProcedure_Funciones(ParseTreeNode actual)
        {
            string NoTerminal = actual.Term.Name;
            switch (NoTerminal.ToLower())
            {
                case "declaracion":
                    if (actual.ChildNodes.Count == 3)
                    {
                        return new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], null, 1, 1, "", actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0]);
                    }
                    else if (actual.ChildNodes.Count == 4)
                    {
                        if (actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "var")
                        {
                            return new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], null, 1, 1, "var", actual.ChildNodes.ElementAt(3).ToString().Split(' ')[0]);
                        }                       
                    }                    
                    else if (actual.ChildNodes.Count == 9)
                    {
                        Expresion[,] nuevo = new Expresion[4, 2];
                        Dimensiones(actual.ChildNodes.ElementAt(5), nuevo);
                        fila = 0;
                        if (actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "var")
                        {
                            //instrucciones.AddLast(new Expresiones.Array(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0],devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8)),nuevo));
                        }
                        else if (actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "type")
                        {
                            return new Expresiones.Array(actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], devTipoDato(actual.ChildNodes.ElementAt(8)), nuevo,true);
                        }
                    }
                    else if (actual.ChildNodes.Count == 6)
                    {
                        if (actual.ChildNodes.ElementAt(0).Term.Name == "const")
                        {
                            return new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5)), 1, 1, "const", "const");
                        }
                        else {
                            return new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(5)), 1, 1, "", actual.ChildNodes.ElementAt(3).ToString().Split(' ')[0]);
                        }
                    }
                    break;
                case "if":
                    String tokenSubIf = "";
                    if (actual.ChildNodes.ElementAt(7).ChildNodes.Count > 0)
                    {
                        tokenSubIf = actual.ChildNodes.ElementAt(7).ChildNodes.ElementAt(1).Term.Name;
                    }
                    if (tokenSubIf.ToLower() == "if")
                    {
                        return new If(expresion_numerica(actual.ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(4)), devDeclaracionProcedure_Funciones(actual.ChildNodes.ElementAt(7).ChildNodes.ElementAt(1)), 1, 1, true);
                    }
                    if (actual.ChildNodes.ElementAt(7).ChildNodes.Count != 0)
                    {
                        return new If(expresion_numerica(actual.ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(4)), Listainstrucciones(actual.ChildNodes.ElementAt(7).ChildNodes.ElementAt(2)), 1, 1);
                    }
                    else
                    {
                        return new If(expresion_numerica(actual.ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(4)), null, 1, 1, true);
                    }
            }
            return null;
        }
        /*
       Funcion para logra desanidar las funciones
        */
        private Abstracto.Instruccion instruccion(ParseTreeNode actual, String padre, ParseTreeNode ListaParametrosPadre, LinkedList<Abstracto.Instruccion> instrucciones)
        {
            string tokenOperador = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (tokenOperador.ToLower())
            {
                /*
                    Desanidamiento de Funciones
                 */
                case "funcion":
                    ParseTreeNode funcion = actual.ChildNodes.ElementAt(0);
                    String funcionHija;
                    Funcion temp;
                    if (padre != "")
                    {
                        funcionHija = padre + "_" + funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0];

                    }
                    else
                    {
                        funcionHija = funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0];

                    }

                    /*
                        Funcion tiene 10 nodos en el nodo 8 se encuentran la funciones anidadas
                     */
                    if (funcion.ChildNodes.Count == 13)
                    {
                        for (int i = 0; i < funcion.ChildNodes.ElementAt(8).ChildNodes.Count; i++)
                        {
                            if (funcion.ChildNodes.ElementAt(8).ChildNodes.ElementAt(i).ChildNodes.ElementAt(0).Term.Name != "declaracion")
                            {
                                Abstracto.Instruccion nuevo = instruccion(funcion.ChildNodes.ElementAt(8).ChildNodes.ElementAt(i), funcionHija, funcion.ChildNodes.ElementAt(3), instrucciones);
                            }
                        }
                        temp = new Funcion(funcionHija, listInstr2Temp(funcion.ChildNodes.ElementAt(3)), Listainstrucciones(funcion.ChildNodes.ElementAt(10)), Listainstrucciones2(funcion.ChildNodes.ElementAt(8)), funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0],devTipoDato(funcion.ChildNodes.ElementAt(6)));
                        instrucciones.AddLast(temp);
                        return temp;
                    }
                    else
                    {
                        for (int i = 0; i < funcion.ChildNodes.ElementAt(5).ChildNodes.Count; i++)
                        {
                            if (funcion.ChildNodes.ElementAt(5).ChildNodes.ElementAt(i).ChildNodes.ElementAt(0).Term.Name != "declaracion")
                            {
                                instruccion(funcion.ChildNodes.ElementAt(5).ChildNodes.ElementAt(i), funcionHija, ListaParametrosPadre, instrucciones);
                            }
                        }
                        temp = new Funcion(funcionHija, null, Listainstrucciones(funcion.ChildNodes.ElementAt(7)), Listainstrucciones2(funcion.ChildNodes.ElementAt(5)), funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0], devTipoDato(funcion.ChildNodes.ElementAt(6)));
                        instrucciones.AddLast(temp);
                        return temp;
                    }
                case "types_object":
                    instrucciones.AddLast(new Type_Object(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4))));
                    return null;
                case "graficar_ts":
                    instrucciones.AddLast(new GraficarTS());
                    return null;
                case "procedure":
                    ParseTreeNode procedure = actual.ChildNodes.ElementAt(0);
                    String ProcedureHija;
                    Procedure temp2;
                    if (padre != "")
                    {
                        ProcedureHija = padre + "_" + procedure.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    }
                    else
                    {
                        ProcedureHija = procedure.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    }

                    /*
                        Procedure tiene 11 nodos en el nodo 8 se encuentran la funciones anidadas
                     */
                    if (procedure.ChildNodes.Count == 11)
                    {
                        for (int i = 0; i < procedure.ChildNodes.ElementAt(6).ChildNodes.Count; i++)
                        {
                            if (procedure.ChildNodes.ElementAt(6).ChildNodes.ElementAt(i).ChildNodes.ElementAt(0).Term.Name != "declaracion")
                            {
                                Abstracto.Instruccion nuevo = instruccion(procedure.ChildNodes.ElementAt(6).ChildNodes.ElementAt(i), ProcedureHija, procedure.ChildNodes.ElementAt(3), instrucciones);
                            }
                        }
                        temp2 = new Procedure(ProcedureHija, listInstr2Temp(procedure.ChildNodes.ElementAt(3)), Listainstrucciones(procedure.ChildNodes.ElementAt(8)), Listainstrucciones2(procedure.ChildNodes.ElementAt(6)), 1, 1, procedure.ChildNodes.ElementAt(1).ToString().Split(' ')[0]);
                        instrucciones.AddLast(temp2);
                        return temp2;
                    }
                    else
                    {
                        for (int i = 0; i < procedure.ChildNodes.ElementAt(3).ChildNodes.Count; i++)
                        {
                            if (procedure.ChildNodes.ElementAt(3).ChildNodes.ElementAt(i).ChildNodes.ElementAt(0).Term.Name != "declaracion")
                            {
                                Abstracto.Instruccion nuevo = instruccion(procedure.ChildNodes.ElementAt(3).ChildNodes.ElementAt(i), ProcedureHija, ListaParametrosPadre, instrucciones);
                            }
                        }
                        temp2 = new Procedure(ProcedureHija, null, Listainstrucciones(procedure.ChildNodes.ElementAt(5)), Listainstrucciones2(procedure.ChildNodes.ElementAt(3)), 1, 1, procedure.ChildNodes.ElementAt(1).ToString().Split(' ')[0]);
                        instrucciones.AddLast(temp2);
                        return temp2;
                    }
                case "writeln":
                    instrucciones.AddLast(new Print(devListExpresiones(actual.ChildNodes.ElementAt(2)), 1, 1));
                    return null;
                case "write":
                    instrucciones.AddLast(new Write(devListExpresiones(actual.ChildNodes.ElementAt(2)), 1, 1));
                    return null;
                case "declaracion":
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 4)
                    {
                        if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "var")
                        {
                            Simbolo.EnumTipoDato tipo = devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3));
                            if (tipo != Simbolo.EnumTipoDato.NULL)
                            {
                                if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ChildNodes.Count == 0)
                                {

                                    instrucciones.AddLast(new Declaracion(tipo, actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], null, 1, 1, "var", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0]));
                                }
                                else
                                {
                                    instrucciones.AddLast(new Declaracion(tipo, devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), null, 1, 1, "var", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0]));
                                }

                            }
                            else
                            {
                                if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).Term.Name == "listexpr")
                                {
                                    instrucciones.AddLast(new Declaracion(Simbolo.EnumTipoDato.OBJETO_TYPE, devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), null, 1, 1, "var", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0].ToLower(), ""));
                                }
                                else
                                {
                                    instrucciones.AddLast(new Declaracion(Simbolo.EnumTipoDato.OBJETO_TYPE, actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0].ToLower(), null, 1, 1, "var", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0].ToLower(), ""));
                                }
                            }

                        }                        
                    }                    
                    else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 9)
                    {
                        Expresion[,] nuevo = new Expresion[4, 2];
                        Dimensiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5), nuevo);
                        fila = 0;
                        if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "var")
                        {
                            Simbolo.EnumTipoDato tipo = devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8));
                            instrucciones.AddLast(new Expresiones.Array(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8).ToString().Split(' ')[0], nuevo,false));
                        }
                        else if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "type")
                        {
                            instrucciones.AddLast(new Expresiones.Array(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8)), nuevo,true, actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8).ToString().Split(' ')[0]));
                        }
                    }
                    else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 6)
                    {
                        if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).Term.Name.ToLower() == "listexpr")
                        {
                            instrucciones.AddLast(new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5)), 1, 1, "", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0]));
                            return null;
                        }
                        else if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name.ToLower() == "const")
                        {
                            instrucciones.AddLast(new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5)), 1, 1, "const", "const"));
                        }
                        else {
                            instrucciones.AddLast(new Declaracion(devTipoDato(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5)), 1, 1, "", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3).ToString().Split(' ')[0]));
                        }
                    }
                    return null;
                case "llamadafuncion":
                    instrucciones.AddLast(new LlamadaFuncion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)), 1, 1));
                    return null;
                case "returnfuncion_asignacion":
                    String token = actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                    if (token.ToLower() == "exit")
                    {
                        instrucciones.AddLast(new Return(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2))));
                    }
                    else
                    {
                        if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 8)
                        {
                            instrucciones.AddLast(new Asignacion(token, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)), null, null, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6))));
                        }
                        else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 10)
                        {
                            instrucciones.AddLast(new Asignacion(token, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), null, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(8))));
                        }
                        else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 12)
                        {
                            instrucciones.AddLast(new Asignacion(token, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(10))));
                        }
                        else
                        {
                            instrucciones.AddLast(new Asignacion(token, expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3))));
                        }

                    }
                    return null;
                case "repeat":
                    instrucciones.AddLast(new Repeat(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1))));
                    return null;
                case "if":
                    String tokenSubIf = "";
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(7).ChildNodes.Count != 0)
                    {
                        tokenSubIf = actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(7).ChildNodes.ElementAt(1).Term.Name;
                    }
                    if (tokenSubIf.ToLower() == "if")
                    {
                        instrucciones.AddLast(new If(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), devDeclaracionProcedure_Funciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(7).ChildNodes.ElementAt(1)), 1, 1, true));
                        return null;
                    }
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(7).ChildNodes.Count != 0)
                    {
                        instrucciones.AddLast(new If(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(7).ChildNodes.ElementAt(2)), 1, 1));
                        return null;
                    }
                    else
                    {
                        instrucciones.AddLast(new If(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), null, 1, 1, true));
                        return null;
                    }
                case "for":
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(5).ToString().Split(' ')[0].ToLower() == "to")
                    {
                        instrucciones.AddLast(new For(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(9)),false));
                    }
                    else {
                        instrucciones.AddLast(new For(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4)), expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(9)),true));
                    }                    
                    return null;
                case "break":
                    instrucciones.AddLast(new Break());
                    return null;
                case "continue":
                    instrucciones.AddLast(new Continue());
                    return null;
                case "asignacion":
                    instrucciones.AddLast(new Asignacion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3))));
                    return null;
                case "while":
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 5)
                    {
                        instrucciones.AddLast(new While(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3))));
                    }
                    else {
                        instrucciones.AddLast(new While(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4))));
                    }                    
                    return null;
                case "case":
                    if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 6)
                    {
                        instrucciones.AddLast(new Case(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)),true));
                    }
                    else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 11)
                    {
                        instrucciones.AddLast(new Case(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6)),false));
                    }
                    return null;
                case "instrcase":
                    instrucciones.AddLast(new Case(expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0)), Listainstrucciones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3)), true));
                    return null;
                case "listid":
                    instrucciones.AddLast(new AsignacionTypeObjcet(listIds(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(3))));
                    return null;
                default:
                    return null;
            }
        }
        //Lista de IDs
        private LinkedList<String> listIds(ParseTreeNode actual)
        {
            LinkedList<String> parametros = new LinkedList<string>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                ParseTreeNode temp = actual.ChildNodes.ElementAt(i);
                if (temp.ChildNodes.Count == 2)
                {
                    parametros.AddLast(temp.ChildNodes.ElementAt(1).ToString().Split(' ')[0]);
                }
                else
                {
                    parametros.AddLast(temp.ToString().Split(' ')[0]);
                }

            }
            return parametros;
        }

        /*
            Resolviendo expresiones Arimeticas
        */
        public Abstracto.Expresion expresion_logica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 1)
            {
                string tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                if (tokenValor == "true")
                {
                    return new Arimetica(new Literal(Simbolo.EnumTipoDato.BOOLEAN, true), Arimetica.Tipo_operacion.TRUE);
                }                
                else if (tokenValor == "false")
                {
                    return new Arimetica(new Literal(Simbolo.EnumTipoDato.BOOLEAN, false), Arimetica.Tipo_operacion.FALSE);
                }
                else
                {
                    return new AccesoId(null, null,tokenValor);
                }
            }else if (actual.ChildNodes.Count == 2)
            {
                if (actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0] == "not")
                {
                    return new Not(expresion_numerica(actual.ChildNodes.ElementAt(1)), null);
                }
                else
                {
                    return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(1)), Arimetica.Tipo_operacion.NEGATIVO);
                }
            }
            string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            if (tokenOperador.Equals("<"))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MENOR_QUE);
            }
            else if (tokenOperador.Equals("<="))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MENOR_IGUAL_QUE);
            }
            else if (tokenOperador.Equals(">="))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MAYOR_IGUAL_QUE);
            }
            else if (tokenOperador.Equals("<>"))
            {
                return new Diferenciacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)));
            }
            else if (tokenOperador.Equals("="))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.IGUAL_QUE);
            }
            else if (tokenOperador.Equals("and"))
            {
                return new AND(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)));
            }
            else if (tokenOperador.Equals("mod"))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MOD);
            }
            else if (tokenOperador.Equals("or"))
            {
                return new Or(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)));
            }
            else if (tokenOperador.Equals("^"))
            {
                return new Arimetica(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.XOR);
            }
            else if (tokenOperador.Equals("not"))
            {
                return new Not(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)));
            }
            else if (tokenOperador.Equals(">"))
            {
                return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MAYOR_QUE);
            }
            else
            {
                return expresion_logica(actual.ChildNodes.ElementAt(1));
            }

        }
        public Abstracto.Expresion expresion_numerica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (tokenOperador)
                {
                    case "+":
                        return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.SUMA);
                    case ".":
                        return new AccesoId(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)),null);
                    case "pow":
                        return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.POTENCIA);
                    case "-":
                        return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.RESTA);
                    case "*":
                        return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.MULTIPLICACION);
                    case "div":
                        return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Arimetica.Tipo_operacion.DIVISION);
                    default:
                        if (tokenOperador.Equals(">") || tokenOperador.Equals("<") || tokenOperador.Equals(">=") || tokenOperador.Equals("<=") || tokenOperador.Equals("=")
                        || tokenOperador.Equals("and") || tokenOperador.Equals("mod") || tokenOperador.Equals("<>") || tokenOperador.Equals("or") || tokenOperador.Equals("^") || tokenOperador.Equals("not"))
                        {
                            return expresion_logica(actual);
                        }
                        else
                        {
                            return expresion_numerica(actual.ChildNodes.ElementAt(1));
                        }
                }

            }
            else if (actual.ChildNodes.Count == 2)
            {
                if (actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0] == "not")
                {
                    return new Not(expresion_numerica(actual.ChildNodes.ElementAt(1)),null);
                }
                else
                {
                    return new Arimetica(expresion_numerica(actual.ChildNodes.ElementAt(1)), Arimetica.Tipo_operacion.NEGATIVO);
                }
            }
            else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 8 && actual.ChildNodes.ElementAt(0).Term.Name == "accesoarray")
            {
                Expresion[] posiciones = new Expresion[3];
                posiciones[0] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2));
                posiciones[1] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4));
                posiciones[2] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(6));
                return new AccesoArray(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], posiciones);
            }
            else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 6 && actual.ChildNodes.ElementAt(0).Term.Name == "accesoarray")
            {
                Expresion[] posiciones = new Expresion[3];
                posiciones[0] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2));
                posiciones[1] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4));
                return new AccesoArray(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], posiciones);
            }
            else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 4 && actual.ChildNodes.ElementAt(0).Term.Name == "accesoarray")
            {
                Expresion[] posiciones = new Expresion[3];
                posiciones[0] = expresion_numerica(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2));
                return new AccesoArray(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], posiciones);
            }
            else
            {
                BnfTerm tipo = actual.ChildNodes.ElementAt(0).Term;
                if (tipo.Name == "ID")
                {
                    string tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                    return new AccesoId(null, null,tokenValor);
                }
                else if (tipo.Name == "cadena")
                {
                    String tokenValor = actual.ChildNodes.ElementAt(0).ToString();
                    return new Cadena(Simbolo.EnumTipoDato.STRING,tokenValor.Remove(tokenValor.ToCharArray().Length - 9, 9));
                }
                else if (tipo.Name == "asignacionob")
                {
                    return expresion_numerica(actual.ChildNodes.ElementAt(0));
                }
                else if (tipo.Name == "cadena2")
                {
                    String tokenValor = actual.ChildNodes.ElementAt(0).ToString();
                    return new Cadena(Simbolo.EnumTipoDato.STRING,tokenValor.Remove(tokenValor.ToCharArray().Length - 10, 10));
                }
                else if (tipo.Name.ToLower() == "llamadafuncion")
                {
                    return new LlamadaFuncion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)), 1, 1);
                }
                else if (tipo.Name.ToLower() == "true")
                {
                    return new Arimetica(new Literal(Simbolo.EnumTipoDato.BOOLEAN, true), Arimetica.Tipo_operacion.TRUE);
                }
                else if (tipo.Name.ToLower() == "false")
                {
                    return new Arimetica(new Literal(Simbolo.EnumTipoDato.BOOLEAN, false), Arimetica.Tipo_operacion.FALSE);
                }
                else
                {
                    return new Arimetica(Double.Parse(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]));
                }

            }
        }
        private Simbolo.EnumTipoDato devTipoDato(ParseTreeNode actual)
        {
            string valor = actual.ToString().Split(' ')[0];
            switch (valor.ToLower())
            {
                case "integer":
                    return Simbolo.EnumTipoDato.INT;
                case "type":
                    return Simbolo.EnumTipoDato.TYPE;
                case "array":
                    return Simbolo.EnumTipoDato.ARRAY;
                case "boolean":
                    return Simbolo.EnumTipoDato.BOOLEAN;
                case "char":
                    return Simbolo.EnumTipoDato.CHAR;
                case "string":
                    return Simbolo.EnumTipoDato.STRING;
                case "double":
                    return Simbolo.EnumTipoDato.DOUBLE;
                case "real":
                    return Simbolo.EnumTipoDato.REAL;
                default:
                    return Simbolo.EnumTipoDato.NULL;
            }
        }
        /*
            Devolviendo Lista de Expresiones
        */
        private LinkedList<Abstracto.Expresion> devListExpresiones(ParseTreeNode actual)
        {
            LinkedList<Abstracto.Expresion> ListaExpre = new LinkedList<Abstracto.Expresion>();
            ParseTreeNode temp;
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                temp = actual.ChildNodes.ElementAt(i);
                BnfTerm tipo = temp.ChildNodes.ElementAt(0).Term;
                if (tipo.ErrorAlias == "cadena")
                {
                    String tokenValor = temp.ChildNodes.ElementAt(0).ToString();
                    ListaExpre.AddLast(new Cadena(Entornos.Simbolo.EnumTipoDato.STRING, tokenValor.Remove(tokenValor.ToCharArray().Length - 9, 9)));
                }
                else if (tipo.ErrorAlias == "ID")
                {
                    if (temp.ChildNodes.ElementAt(0).ChildNodes.Count != 3)
                    {
                        ListaExpre.AddLast(new AccesoId(null, null, temp.ChildNodes.ElementAt(0).ToString().Split(' ')[0]));
                    }
                }
                else if (tipo.ErrorAlias == "cadena2")
                {
                    String tokenValor = temp.ChildNodes.ElementAt(0).ToString();
                    ListaExpre.AddLast(new Cadena(Entornos.Simbolo.EnumTipoDato.STRING, tokenValor.Remove(tokenValor.ToCharArray().Length - 10, 10)));
                }
                else
                {
                    ListaExpre.AddLast(expresion_numerica(temp));
                }
            }
            return ListaExpre;
        }
    }
}