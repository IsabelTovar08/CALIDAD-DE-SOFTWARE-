using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // Pedir rutas
        Console.WriteLine("Escribe la ruta del archivo ORIGINAL:");
        string rutaOriginal = Console.ReadLine().Trim();

        Console.WriteLine("Escribe la ruta del archivo MODIFICADO:");
        string rutaNuevo = Console.ReadLine().Trim();

        // Verificar existencia
        if (!File.Exists(rutaOriginal) || !File.Exists(rutaNuevo))
        {
            Console.WriteLine("Alguno de los archivos no existe.");
            return;
        }

        // Leer contenido
        string[] original = File.ReadAllLines(rutaOriginal);
        string[] nuevo = File.ReadAllLines(rutaNuevo);

        // Contar diferencias
        int diferencias = ContarDiferenciasLineaPorLinea(original, nuevo);

        // Mostrar análisis del original
        Console.WriteLine("\n--- Análisis del archivo ORIGINAL ---");
        AnalizarArchivo(original);

        // Mostrar análisis del modificado
        Console.WriteLine("\n--- Análisis del archivo MODIFICADO ---");
        AnalizarArchivo(nuevo);

        // Mostrar diferencias
        Console.WriteLine($"\nLíneas modificadas entre ambos archivos: {diferencias}");
    }

    // Método para analizar un archivo (líneas, clases y métodos)
    static void AnalizarArchivo(string[] lineas)
    {
        int totalLineas = lineas.Length;
        int clases = 0, metodos = 0;

        foreach (var linea in lineas)
        {
            string l = linea.Trim();

            // Buscar clases
            if (l.Contains("class "))
                clases++;

            // Buscar métodos (simplificado: paréntesis + tipo/modificador)
            if ((l.StartsWith("public") || l.StartsWith("private") || l.StartsWith("protected") || l.StartsWith("internal") || l.Contains(" void ") || l.Contains(" int ") || l.Contains(" string ") || l.Contains(" bool ")) 
                && l.Contains("(") && l.Contains(")"))
            {
                metodos++;
            }
        }

        Console.WriteLine($"Total de líneas: {totalLineas}");
        Console.WriteLine($"Clases encontradas: {clases}");
        Console.WriteLine($"Métodos encontrados: {metodos}");
    }

    // Método para contar diferencias línea por línea
    static int ContarDiferenciasLineaPorLinea(string[] a, string[] b)
    {
        int max = Math.Max(a.Length, b.Length);
        int dif = 0;

        for (int i = 0; i < max; i++)
        {
            string lineaA = (i < a.Length) ? a[i].Trim() : "";
            string lineaB = (i < b.Length) ? b[i].Trim() : "";

            if (lineaA != lineaB)
                dif++;
        }

        return dif;
    }
}
