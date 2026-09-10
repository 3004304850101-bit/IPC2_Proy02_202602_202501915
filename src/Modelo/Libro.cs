namespace src.Modelo;

public class Libro
{
    public int ISBN {get; set;}
    public string Nombre {get; set;}
    public string Autor {get; set;}
    public string Categoria {get; set;}

    public Libro(int isbn, string nombre, string autor, string categoria)
    {
        ISBN=isbn;
        Nombre=nombre;
        Autor=autor;
        Categoria=categoria;
    }
}