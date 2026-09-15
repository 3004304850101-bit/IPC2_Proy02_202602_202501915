using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
namespace src.Modelo;

public class CargaXML
{
    Control control = new Control();
    //LEER XML ARCHIVO
    public void CargarArchivo(string ruta)
    {
        string xml = File.ReadAllText(ruta);
        CargarDataXML(xml);
    }
    //LEER XML TEXTO
    public string CargarDataXML(string XML)
    {
        int contadorCE=0;
        int contadorCN=0;
        int contadorPend=0;
        int contadorL=0;
        int contadorLN=0;

        StringBuilder sb = new StringBuilder();
        XDocument documento = XDocument.Parse(XML);

        XElement? seccionCategorias = documento.Root?.Element("listaCategorias");
        if(seccionCategorias != null)
        {
        foreach(XElement e in seccionCategorias.Elements("categoria"))
        {
            string nombreCategoria= e.Value.Trim();
            XAttribute? atributoPadre = e.Attribute("padre");

            string padre;

            if(atributoPadre != null)
            {
                 padre = atributoPadre.Value.Trim();
            }
            else
            {
                 padre="";
            }
            
            string exitoso=control.RegistrarCategoria(nombreCategoria, padre);

            if(exitoso.StartsWith("REGISTRO EXITOSO CATEGORIA"))
            {
                contadorCE++;
            }
            else if(exitoso.StartsWith("YA EXISTE"))
            {
                contadorCN++;
            }
            else
            {
                contadorPend++;
            }
        }

        int resueltas = control.IntentarRegistrarPendientes();
        contadorCE+= resueltas;
        contadorCN+= (contadorPend - resueltas);
        sb.AppendLine($"CATEGORIAS ACEPTADAS: {contadorCE} y CATEGORIAS RECHAZADAS: {contadorCN}"); 
        }

        XElement? seccionLibros = documento.Root?.Element("listaLibros");

        if(seccionLibros != null)
        {
            foreach(XElement lib in seccionLibros.Elements("libro"))
        {
            XElement ISBN= lib.Element("ISBN")!;
            XElement titulo= lib.Element("titulo")!;
            XElement autor= lib.Element("autor")!;
            XElement categoria= lib.Element("categoria")!;

            bool exito = int.TryParse(ISBN.Value.Trim(), out int resultado);

            if (exito)
            {
                int isbnLibro=resultado;
                string tituloLibro=titulo.Value.Trim();
                string autorLibro=autor.Value.Trim();
                string categoriaLibro=categoria.Value.Trim();

                string registroE= control.RegistroLibro(isbnLibro,tituloLibro,autorLibro,categoriaLibro);

                if(registroE.StartsWith("REGISTRO EXITOSO LIBRO"))
                {
                    contadorL++;
                }
                else
                {
                    contadorLN++;
                }

            }
            else
            {
                contadorLN++;
            }
        }

        sb.AppendLine($"LIBROS ACEPTADOS: {contadorL} y LIBROS RECHAZADOS: {contadorLN}");
        }
        
        return sb.ToString().Trim();
    }
}