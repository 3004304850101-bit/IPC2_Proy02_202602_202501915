using System.Text;

namespace src.Modelo;

public class Control
{
    private Arbol arbolCategorias;
    private ArbolAVL arbolLibros;
   
    private Reportes reporte;
    public Control()
    {
        arbolCategorias= new Arbol();
        arbolLibros= new ArbolAVL();
        reporte =new Reportes(arbolCategorias);
    }

    //Ingresar Categoria
    public string RegistrarCategoria(string nombreCategoria, string padreCategoria, bool cola = true)
    {
        if(arbolCategorias.BuscarCategorias(nombreCategoria) != null)
        {
            return $"YA EXISTE DICHA CATEGORIA: {nombreCategoria}";
        }
            NodoArbol categoriaNueva=new NodoArbol(nombreCategoria, padreCategoria);
            bool Insertar=arbolCategorias.InsertarCategoria(categoriaNueva, cola : cola);

            if (Insertar)
            {
                return $"REGISTRO EXITOSO CATEGORIA {nombreCategoria}";
        }
        else
        {
            return $"NO SE PUDO REGISTRAR: la categoria padre '{padreCategoria}' no existe";
        }
        
    }

    public int IntentarRegistrarPendientes()
    {
        return arbolCategorias.IntentarIngresar();
    }
    //Registrar Libro
    public string RegistroLibro(int ISBN, string nombreLibro, string nombreAutor, string categoriaLibro)
    {
        if(arbolLibros.BuscarNodo(ISBN) == null)
        {
            NodoArbol? existe=arbolCategorias.BuscarCategorias(categoriaLibro);
            if(existe != null)
            {
                 Libro nuevoLibro= new Libro(ISBN, nombreLibro, nombreAutor, categoriaLibro.ToUpper());
                //se añade a los arboles correspondientes
                arbolLibros.Insertar(nuevoLibro);
                existe.librosC.AgregarLibro(nuevoLibro);

                return $"REGISTRO EXITOSO LIBRO {nombreLibro}"; //Para el caso de ingreso manual se muestra mensaje
            }
            else
            {
                return $"CATEGORIA NO EXISTE: {categoriaLibro}"; //Para el caso de ingreso manual se muestra mensaje
            }
        }
        else
        {
            return $"YA EXISTE DICHA ISBN: {ISBN}"; //Para el caso de ingreso manual se muestra mensaje
        }
    }
    //Eliminar libro
    public string EliminarLibro(int ISBN)
    {
        Libro? existeL=arbolLibros.BuscarNodo(ISBN);
        if(existeL != null)
        {
            //Eliminar de los arboles
            arbolLibros.EliminarNodo(ISBN);

            NodoArbol pertenece= arbolCategorias.BuscarCategorias(existeL.Categoria)!;
            pertenece.librosC.EliminarLibro(ISBN);

            return $" LIBRO ELIMINADO "; //Para el caso de ingreso manual se muestra mensaje
        }
        else
        {
            return $"ISBN NO EXISTE: {ISBN}"; //Para el caso de ingreso manual se muestra mensaje
        }
    }
    //Buscar Libro ISBN
    public string BuscarISBN(int ISBN)
    {
        Libro? existeL=arbolLibros.BuscarNodo(ISBN);
        if(existeL != null)
        {
            StringBuilder sb= new StringBuilder();
            sb.AppendLine($"ISBN {existeL.ISBN}");
            sb.AppendLine($"Nombre: {existeL.Nombre}");
            sb.AppendLine($"Autor: {existeL.Autor}");
            sb.AppendLine($"Categoria: {existeL.Categoria}");

            return sb.ToString().Trim(); 
        }
        else
        {
            return $"ISBN NO EXISTE: {ISBN}"; //Para el caso de ingreso manual se muestra mensaje
        }
    }
    //Libros Ascendente
    public string listaLibAsc()
    {
        return arbolLibros.RecorridoInorden();
    }
    //Lista Categorias
    public string listCategorias()
    {
        return arbolCategorias.ListaCategorias();
    }
    //Libro Mayor y Menor ISBN
    public string MayoryMenor()
    {
        Libro? libroMax=arbolLibros.MayorISBN();
        Libro? libroMin=arbolLibros.MenorISBN();

        StringBuilder sb= new StringBuilder();

        if(libroMax != null)
        {
            sb.AppendLine($"ISBN {libroMax.ISBN}");
            sb.AppendLine($"Nombre: {libroMax.Nombre}");
            sb.AppendLine($"Autor: {libroMax.Autor}");
            sb.AppendLine($"Categoria: {libroMax.Categoria}");
        }
        
            sb.AppendLine();
        
        if(libroMin != null)
        {
            sb.AppendLine($"ISBN {libroMin.ISBN}");
            sb.AppendLine($"Nombre: {libroMin.Nombre}");
            sb.AppendLine($"Autor: {libroMin.Autor}");
            sb.AppendLine($"Categoria: {libroMin.Categoria}");
        }

        return sb.ToString().Trim(); 
            
    }

    //LLAMAR A REPORTES
    public string? ReporteCategorias(string categoria)
    {
        NodoArbol? ctg;
        if (string.IsNullOrEmpty(categoria))
        {
            ctg=arbolCategorias.Cabeza().PrimerHijo;//Caso raiz virtual para mostrar el arbol general en caso el usuario no ingrese nada
        }
        else
        {
            ctg=arbolCategorias.BuscarCategorias(categoria);  
        } 

        if(ctg != null)
        {
            return reporte.MostrarArbolCategorias(ctg);
        }
        
        return null;
    }

    public string? ReporteLibros(string categoria)
    {
        NodoArbol? ctg;
        if (string.IsNullOrEmpty(categoria))
        {
            ctg=arbolCategorias.Cabeza().PrimerHijo;//Caso raiz virtual para mostrar el arbol general en caso el usuario no ingrese nada
        }
        else
        {
            ctg=arbolCategorias.BuscarCategorias(categoria);
            if(ctg != null)
            {
                return reporte.MostrarArbolLibros(ctg);
            }
        } 

        return null;
    }
}