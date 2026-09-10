namespace src.Modelo;

public class NodoLibro
{
    public Libro Lib {get; set;}
    public NodoLibro? siguiente;

    public NodoLibro(Libro libro)
    {
        Lib=libro;
    }
}

public class ListaLibro
{
    NodoLibro? cabeza;

    //Agregamiento temporal aqui iria la logica de arbol AVL en libros
    public void AgregarLibro(Libro libro)
    {
        NodoLibro nuevoLibro = new NodoLibro(libro);

        if(cabeza==null ||  libro.ISBN < cabeza.Lib.ISBN)
        {
            nuevoLibro.siguiente=cabeza;
            cabeza=nuevoLibro;
        }
        else
        {
            NodoLibro actual=cabeza;
            while(actual.siguiente != null && libro.ISBN < actual.Lib.ISBN)
            {
                actual=actual.siguiente!;
            }
            nuevoLibro.siguiente=actual.siguiente;
            actual.siguiente=nuevoLibro;
        }
    }

    public NodoLibro? BuscarLibro(int isbn)
    {
        NodoLibro? actual=cabeza;
        while(actual != null)
        {
            if (actual.Lib.ISBN == isbn)
            {
                return actual;
            }
            actual=actual.siguiente;
        }

        return null;
    }

    public void EliminarLibro(int isbn)
    {
        if(cabeza != null)
        {
            NodoLibro actual=cabeza;
            if (cabeza.Lib.ISBN == isbn)
            {
                cabeza=cabeza.siguiente;
            }
            else
            {
                while(actual!.siguiente != null)
                {
                    if (actual.siguiente.Lib.ISBN == isbn)
                    {
                        actual.siguiente=actual.siguiente.siguiente;
                        return;
                    }
                    actual=actual.siguiente!;
                }
            }
        }
        
    }
}