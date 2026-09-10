namespace src.Modelo;

public class NodoCola
{
    public NodoArbol pendiente {get; set;}
    public NodoCola? siguientePendiente {get; set;}
    

    public NodoCola(NodoArbol pend)
    {
        pendiente=pend;
    }
}

public class ColaP
{
    private NodoCola? cabeza;
    private NodoCola? cola;
    public int contador=0;

    public void Agregar(NodoArbol pendient)
    {
        NodoCola nuevoC= new NodoCola (pendient);
        if(cabeza == null)
        {
            cabeza=nuevoC;
            cola=nuevoC;
            contador++;
        }
        else
        {
            cola!.siguientePendiente=nuevoC;
            cola=nuevoC;
            contador++;
        }

    }

    public NodoArbol Pop()
    {

        if (cabeza== null)
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        NodoArbol extraer=cabeza.pendiente;
        cabeza=cabeza.siguientePendiente;

        if (cabeza == null)
        {
            cola=null;
        }

        return extraer;

    }

}