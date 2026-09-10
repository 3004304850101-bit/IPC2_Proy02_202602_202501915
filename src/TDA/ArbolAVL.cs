using System.Text;
namespace src.Modelo;

public class ArbolAVL
    {
        
        private class Nodo
        {
            public Libro Dato;     
            public Nodo? Izquierda; 
            public Nodo? Derecha;    
            public int Altura; 

            // constructor del nodo: recibe el ticket a guardar
            public Nodo(Libro dato)
            {
                Dato = dato;        
                Izquierda = null;   
                Derecha = null;     
                Altura = 1;         
            }
        }

        private Nodo? raiz;

        private int AlturaNodo(Nodo? nodo)
        {
            return nodo==null ? 0 : nodo.Altura;
        }

        private void ActualizarAltura(Nodo nodo)
        {
            int izq = AlturaNodo(nodo.Izquierda);
            int der = AlturaNodo(nodo.Derecha);
            nodo.Altura = 1 + (izq > der ? izq : der);
        }

        private int FactorBalance(Nodo nodo)
        {
            if (nodo == null) return 0;
            return AlturaNodo(nodo.Izquierda) - AlturaNodo(nodo.Derecha);
        }

        //Rotaciones
        //Izquierda-Izquierda
        private Nodo RotarDerecho(Nodo y)
        {
            Nodo? x= y.Izquierda;
            Nodo? t2= x.Derecha;

            x.Derecha=y;
            y.Izquierda=t2;

            ActualizarAltura(y);
            ActualizarAltura(x);

            return x;
        }

        //Derecha-Derecha
        private Nodo RotarIzquierda(Nodo y)
        {
            Nodo? x= y.Derecha;
            Nodo? t2= x.Izquierda;

            x.Izquierda=y;
            y.Derecha=t2;

            ActualizarAltura(y);
            ActualizarAltura(x);

            return x;
        }

        private Nodo Balancear(Nodo nodo)
        {
            ActualizarAltura(nodo);

            int fb=FactorBalance(nodo);

            if(fb > 1)
            {
                if (FactorBalance(nodo.Izquierda) < 0)
                {
                    nodo.Izquierda=RotarIzquierda(nodo.Izquierda);
                }

                return RotarDerecho(nodo);
            }

            if(fb < -1)
            {
                if (FactorBalance(nodo.Derecha) > 0)
                {
                    nodo.Derecha=RotarDerecho(nodo.Derecha);
                }

                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        public void Insertar(Libro libro)
        {
            raiz=InsertarNodo( raiz, libro);

        }

        private Nodo InsertarNodo(Nodo? actual, Libro libro)
        {
            if (actual == null)
            {
                return new Nodo(libro);
            }

            if (libro.ISBN < actual.Dato.ISBN)
            {
                actual.Izquierda = InsertarNodo(actual.Izquierda, libro);
            }
            else if (libro.ISBN > actual.Dato.ISBN)
            {
                actual.Derecha = InsertarNodo(actual.Derecha, libro);
            }
            else
            {
                //Igual
                return actual;
            }
            return Balancear(actual);
        }

        public Libro? BuscarNodo(int ISBN)
        {
            Nodo? actual=raiz;
            while(actual != null)
            {
            if (ISBN == actual.Dato.ISBN)
            {
                return actual.Dato;
            }else if (ISBN < actual.Dato.ISBN)
            {
                actual=actual.Izquierda;
            }else
            {
                actual=actual.Derecha;
            }
            }

            return null;
        }

        public void EliminarNodo(int isbn)
        {
            raiz=EliminarNodo(raiz, isbn);

        }

         private Nodo? EliminarNodo(Nodo? actual, int isbn)
        {
            if(actual==null) return null;

            if( isbn < actual.Dato.ISBN)
            {
                actual.Izquierda = EliminarNodo(actual.Izquierda, isbn);
            }else if( isbn > actual.Dato.ISBN)
            {
                actual.Derecha = EliminarNodo(actual.Derecha, isbn);
            }
            else
            {
                if (actual.Izquierda == null)
                {
                    actual=actual.Derecha;
                }else if(actual.Derecha == null)
                {
                    actual=actual.Izquierda;
                }else{
                    Nodo sucesor = Sucesor(actual.Derecha);
                    actual.Dato= sucesor.Dato;
                    actual.Derecha=EliminarNodo(actual.Derecha,sucesor.Dato.ISBN);
                }
            }

             if (actual == null) return null;

             return Balancear(actual);
        }

        private Nodo Sucesor(Nodo nodo)
        {
            while (nodo.Izquierda != null)
            {
                nodo = nodo.Izquierda;
            }
            return nodo;
        }

        public Libro?  MayorISBN()
        {
            if (raiz == null) return null;
            Nodo? actual=raiz;
            while(actual.Derecha != null)
            {
                actual=actual.Derecha;
            }   

            return actual.Dato;
        }

        public Libro? MenorISBN()
        {

            if (raiz == null) return null;

            Nodo actual=raiz;
            while(actual.Izquierda != null)
            {
                actual=actual.Izquierda;
            }   

            return actual.Dato;
        }

         public string RecorridoInorden()
        {
            StringBuilder sb = new StringBuilder();   
            Inorden(raiz, sb);                        
            return sb.ToString().Trim();               
        }

        private void Inorden(Nodo? actual, StringBuilder sb)
        {
            if (actual == null) return;                
            Inorden(actual.Izquierda, sb);             
            sb.AppendLine(actual.Dato.Nombre);
            Inorden(actual.Derecha, sb);               
        }
    }
