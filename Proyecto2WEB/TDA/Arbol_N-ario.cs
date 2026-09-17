using System.Text;
namespace Proyecto2WEB.Modelo;

public class NodoArbol
{
    public string nombreCategoria {get; set;}
    public string nodoPadre {get; set;} 
    public NodoArbol? PrimerHijo {get; set;}
    public NodoArbol? siguienteHermano {get; set;}
    public ListaLibro librosC {get; set;}

    public NodoArbol(string nombreC, string padreC)
    {
        nombreCategoria= nombreC;
        nodoPadre=padreC;
        PrimerHijo=null;
        siguienteHermano=null;
        librosC=new ListaLibro();
    }
}

public class Arbol
{
    private NodoArbol raiz {get; set;} = new NodoArbol("__RAIZ__","") ;
    private ColaP colap = new ColaP();

    public NodoArbol Cabeza()
    {
        return raiz;
    }

    public NodoArbol? BuscarCategorias(string nombre)
    {
        return BuscarCategoria(raiz, nombre.ToUpper());
    }

    private NodoArbol? BuscarCategoria(NodoArbol? actual, string categoria)
    {

        if(actual == null)
        {
            return null;
        }

        if (actual.nombreCategoria == categoria)
        {
            return  actual;
        }

        if(actual.PrimerHijo != null)
        {
            NodoArbol? encontrado=BuscarCategoria(actual.PrimerHijo, categoria);
            if(encontrado != null)
            {
                return encontrado;
            }
        }
           
            return BuscarCategoria(actual.siguienteHermano, categoria); 
    }

    public bool InsertarCategoria(NodoArbol ingresado, bool cola = true)
    {
        ingresado.nombreCategoria=ingresado.nombreCategoria.ToUpper();
        ingresado.nodoPadre=ingresado.nodoPadre?.ToUpper() ?? "";
        NodoArbol? lugarIngresada;
        if(string.IsNullOrEmpty(ingresado.nodoPadre))
        {
            lugarIngresada=raiz;
        }
        else
        {
            lugarIngresada=BuscarCategoria(raiz, ingresado.nodoPadre);
        }
        if(lugarIngresada != null)
        {
            NodoArbol? existe=BuscarCategoria(raiz,ingresado.nombreCategoria);
            if(existe != null)
            {
                return false;
            }
            else
            {
                NodoArbol actual= lugarIngresada;
                if(actual.PrimerHijo == null)
                {
                    actual.PrimerHijo=ingresado;
                }else if(string.Compare(actual.PrimerHijo.nombreCategoria, ingresado.nombreCategoria) > 0)
                {
                    ingresado.siguienteHermano=actual.PrimerHijo;
                    actual.PrimerHijo=ingresado;
                }
                else
                {
                    NodoArbol t2= actual.PrimerHijo;
                    while(t2.siguienteHermano != null && string.Compare(t2.siguienteHermano.nombreCategoria, ingresado.nombreCategoria) < 0)
                    {
                    t2= t2.siguienteHermano;
                    }
                    ingresado.siguienteHermano=t2.siguienteHermano;
                    t2.siguienteHermano=ingresado;
                }
                return true;
            }
        }
        else
        {
            if (cola)
            {
                colap.Agregar(ingresado);
            }      
        }
        return false;
    }

    public int IntentarIngresar()
    {
        int cuenta=colap.contador;
        int ingresada=0;
        
            for(int i = 0; i < cuenta; i++)
            {
                NodoArbol nuevoI= colap.Pop();
                bool ok=InsertarCategoria(nuevoI);
                if (ok)
                {
                    ingresada++;
                }
            }

            if(cuenta != colap.contador)
            {
                ingresada+=IntentarIngresar();
                return ingresada;
            }
            else
            {
            return ingresada;
            }
    }

    public string ListaCategorias()
    {
        StringBuilder sb = new StringBuilder();   
        RecorridoArbol(raiz.PrimerHijo, sb);                        
        return sb.ToString().Trim();      
    }

    private void RecorridoArbol(NodoArbol? actual, StringBuilder sb)
        {
            if (actual == null) return;

            sb.AppendLine(actual.nombreCategoria);
            RecorridoArbol(actual.PrimerHijo, sb);             
            RecorridoArbol(actual.siguienteHermano, sb);       
        }

    public string GenerarDotCategorias(NodoArbol categoria)
        {
        StringBuilder sb = new StringBuilder();

            sb.AppendLine("digraph AVL {");
            sb.AppendLine("  rankdir=LR;");
            sb.AppendLine("  size=\"10,3\";");
            sb.AppendLine("  ratio=compress;");
            sb.AppendLine("  nodesep=0.3;");
            sb.AppendLine("  node [shape=box, style=\"filled,rounded\", fillcolor=\"#EAF3FB\", color=\"#1B4F72\", fontname=\"Helvetica\", fontsize=11, margin=\"0.15,0.1\"];");
            sb.AppendLine("  edge [color=\"#1B4F72\"];");

            sb.AppendLine($" \"{categoria.nombreCategoria}\" [label=\"{categoria.nombreCategoria}\"];");

            EscribirDotCategorias(categoria.PrimerHijo, categoria, sb);
            sb.AppendLine("}");
            return sb.ToString();

        }

    private void EscribirDotCategorias(NodoArbol? actual, NodoArbol padre, StringBuilder sb)
    {
        if (actual == null) return;

        sb.AppendLine($" \"{actual.nombreCategoria}\" [label=\"{actual.nombreCategoria}\"];");

        if(actual.PrimerHijo != null)
        {
            sb.AppendLine($"  \"{actual.nombreCategoria}\" -> \"{actual.PrimerHijo.nombreCategoria}\";");
            EscribirDotCategorias(actual.PrimerHijo, actual, sb);
        }
        if (actual.siguienteHermano != null)
        {
            sb.AppendLine($"  \"{padre.nombreCategoria}\" -> \"{actual.siguienteHermano.nombreCategoria}\";");
            EscribirDotCategorias(actual.siguienteHermano, padre, sb);
        }
    }

    public string GenerarDotCategoriasyLibros(NodoArbol categoria)
        {
        StringBuilder sb = new StringBuilder();

            sb.AppendLine("digraph AVL {");
            sb.AppendLine("  rankdir=LR;");
            sb.AppendLine("  size=\"10,3\";");
            sb.AppendLine("  ratio=compress;");
            sb.AppendLine("  nodesep=0.3;");
            sb.AppendLine("  node [shape=box, style=\"filled,rounded\", fillcolor=\"#EAF3FB\", color=\"#1B4F72\", fontname=\"Helvetica\", fontsize=11, margin=\"0.15,0.1\"];");
            sb.AppendLine("  edge [color=\"#1B4F72\"];");

            sb.AppendLine($" \"{categoria.nombreCategoria}\" [label=\"{categoria.nombreCategoria}\"];");

            if(!categoria.librosC.EsVacia())
        {
            NodoLibro? actuallib= categoria.librosC.ObtenerCabeza();
            sb.AppendLine("  node [shape=note, style=\"filled\", fillcolor=\"#FDF3D8\", color=\"#8A6D1D\"];");
            while(actuallib != null)
            {
                sb.AppendLine($" \"{actuallib.Lib.ISBN}\" [label=\"{actuallib.Lib.ISBN}\\n{actuallib.Lib.Nombre}\"];");
                sb.AppendLine($"  \"{categoria.nombreCategoria}\" -> \"{actuallib.Lib.ISBN}\";");
                actuallib=actuallib.siguiente;
            }
            sb.AppendLine("  node [shape=box, style=\"filled,rounded\", fillcolor=\"#EAF3FB\", color=\"#1B4F72\"];");
        }

            EscribirDotCategoriasyLibros(categoria.PrimerHijo, categoria, sb);
            sb.AppendLine("}");
            return sb.ToString();

        }

    private void EscribirDotCategoriasyLibros(NodoArbol? actual, NodoArbol padre, StringBuilder sb)
    {
        if (actual == null) return;

        sb.AppendLine($" \"{actual.nombreCategoria}\" [label=\"{actual.nombreCategoria}\"];");

        if(!actual.librosC.EsVacia())
        {
            NodoLibro? actuallib= actual.librosC.ObtenerCabeza();
            sb.AppendLine("  node [shape=note, style=\"filled\", fillcolor=\"#FDF3D8\", color=\"#8A6D1D\"];");
            while(actuallib != null)
            {
                sb.AppendLine($" \"{actuallib.Lib.ISBN}\" [label=\"{actuallib.Lib.ISBN}\\n{actuallib.Lib.Nombre}\"];");
                sb.AppendLine($"  \"{actual.nombreCategoria}\" -> \"{actuallib.Lib.ISBN}\";");
                actuallib=actuallib.siguiente;
            }
            sb.AppendLine("  node [shape=box, style=\"filled,rounded\", fillcolor=\"#EAF3FB\", color=\"#1B4F72\"];");
        }

        if(actual.PrimerHijo != null)
        {
            sb.AppendLine($"  \"{actual.nombreCategoria}\" -> \"{actual.PrimerHijo.nombreCategoria}\";");
            EscribirDotCategoriasyLibros(actual.PrimerHijo, actual, sb);
        }
        if (actual.siguienteHermano != null)
        {
            sb.AppendLine($"  \"{padre.nombreCategoria}\" -> \"{actual.siguienteHermano.nombreCategoria}\";");
            EscribirDotCategoriasyLibros(actual.siguienteHermano, padre, sb);
        }
    }

}