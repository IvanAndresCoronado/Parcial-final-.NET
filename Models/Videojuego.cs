namespace ParcialFinal.Models;

public class Videojuego
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    public Videojuego() { }

    public Videojuego(int id, string titulo, string plataforma, string genero, decimal precio, int cantidad)
    {
        Id = id;
        Titulo = titulo;
        Plataforma = plataforma;
        Genero = genero;
        Precio = precio;
        Cantidad = cantidad;
    }

    public bool TieneStock() => Cantidad > 0;

    public string Resumen() => $"[#{Id}] {Titulo} — {Plataforma} ({Genero}) | ${Precio:0.00} | Stock: {Cantidad}";
}