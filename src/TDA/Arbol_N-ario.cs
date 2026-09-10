namespace src.Modelo;

public class NodoArbol
{
    public string nombreCategoria {get; set;}
    public string nodoPadre {get; set;} 
    public NodoArbol? PrimerHijo {get; set;}
    public NodoArbol? siguienteHermano {get; set;}
    public ListaLibro? librosC {get; set;}

    public NodoArbol(string nombreC, string padreC)
    {
        nombreCategoria= nombreC;
        nodoPadre=padreC;
        PrimerHijo=null;
        siguienteHermano=null;
        librosC=null;
    }
}

public class Arbol
{
    public NodoArbol raiz {get; set;} = new NodoArbol("__RAIZ__","") ;
    public ColaP colap = new ColaP();

    public NodoArbol? BuscarCategoria(NodoArbol? actual, string categoria)
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

    public void InsertarCategoria(NodoArbol ingresado)
    {
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
                return;
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
            }
        }
        else
        {
            colap.Agregar(ingresado);
        }
    }

    public void IntentarIngresar()
    {
        int cuenta=colap.contador;
        
            for(int i = 0; i < cuenta; i++)
            {
                NodoArbol nuevoI= colap.Pop();
                InsertarCategoria(nuevoI);
            }

            if(cuenta != colap.contador)
            {
                IntentarIngresar();
            }
            else
            {
            return;
            }
    }

}