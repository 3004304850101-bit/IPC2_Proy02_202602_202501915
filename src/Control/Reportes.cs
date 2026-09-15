using System.Diagnostics;
namespace src.Modelo;

public class Reportes
{
    private Arbol arbol;

    public Reportes(Arbol arbolC)
    {
        arbol=arbolC;
    }

    private string GenerarGraphCategorias(NodoArbol categoria)
    {
        string contenido;
        contenido=arbol.GenerarDotCategorias(categoria);
        return contenido;
    }

    public string? MostrarArbolCategorias(NodoArbol category)
    {
        string rutaSalida = Path.Combine(Directory.GetCurrentDirectory(), "Reportes");
        Directory.CreateDirectory(rutaSalida);
        string rutaDot = Path.Combine(rutaSalida, $"arbolCategorias.dot");
        string rutaImagen = Path.Combine(rutaSalida, $"arbolCategorias.png");

        string contenidoDot = GenerarGraphCategorias(category);
        File.WriteAllText(rutaDot, contenidoDot);

        ProcessStartInfo InfoDot = new ProcessStartInfo();
        InfoDot.FileName = "dot";
        InfoDot.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
        InfoDot.UseShellExecute = false;
        Process.Start(InfoDot)!.WaitForExit();

        return rutaImagen;
    }

    private string GenerarGraphLibros(NodoArbol categoria)
    {
        string contenido;
        contenido=arbol.GenerarDotCategoriasyLibros(categoria);
        return contenido;
    }

    public string? MostrarArbolLibros(NodoArbol category)
    {
        string rutaSalida = Path.Combine(Directory.GetCurrentDirectory(), "Reportes");
        Directory.CreateDirectory(rutaSalida);

        string rutaDot = Path.Combine(rutaSalida, $"arbolLibro.dot");
        string rutaImagen = Path.Combine(rutaSalida, $"arbolLibro.png");

        string contenidoDot = GenerarGraphLibros(category);
        File.WriteAllText(rutaDot, contenidoDot);

        ProcessStartInfo InfoDot = new ProcessStartInfo();
        InfoDot.FileName = "dot";
        InfoDot.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
        InfoDot.UseShellExecute = false;
        Process.Start(InfoDot)!.WaitForExit();

        return rutaImagen;
    }

}