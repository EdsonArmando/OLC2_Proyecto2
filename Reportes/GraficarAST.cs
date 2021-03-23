using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Reportes
{
    class GraficarAST
    {
        private ParseTreeNode raiz;
        private String text = "digraph lista{ \n rankdir=TB;node[shape = box, style = filled, color = white];\n";
        public GraficarAST(ParseTreeNode raiz) {
            this.raiz = raiz;
        }
        public ParseTreeNode recorrerRaiz(ParseTreeNode raiz)
        {
            text += "<nodo" + raiz.GetHashCode() + "> [label= " + "\"" + raiz.Term.Name.ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
            if (raiz.Token != null && raiz.Token.Text.ToString().ToLower() != raiz.Term.Name.ToLower())
            {
                text += "<nodo" + raiz.Token.GetHashCode() + "> [label= " + "\"" + raiz.Token.Text.ToString().ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
                text += "<nodo" + raiz.GetHashCode() + "> -> <" + "nodo" + raiz.Token.GetHashCode() + ">\n";
            }
            if (raiz.ChildNodes.Count > 0) {
                for (int i=0;i<raiz.ChildNodes.Count;i++) {     
                    text += "<nodo" + raiz.GetHashCode() + "> -> <" + "nodo" + raiz.ChildNodes.ElementAt(i).GetHashCode() + ">\n";
                    recorrerRaiz(raiz.ChildNodes.ElementAt(i));
                }
            }
            return null;
        }
        public void generarArchivo() {
            StreamWriter archivo = new StreamWriter("C:\\compiladores2\\AST_201701029.dot");
            archivo.Write(text);
            archivo.Write("\n}");
            archivo.Close();
            ejecutar(@"dot -Tsvg C:\\compiladores2\\AST_201701029.dot -o C:\\compiladores2\\AST_201701029.svg");
            
        }
        static void ejecutar(string _Command)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = false;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
        }
    }
}
