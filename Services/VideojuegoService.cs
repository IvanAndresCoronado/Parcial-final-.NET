using System.Collections.Generic;
using System.Globalization;
using ParcialFinal.Data;
using ParcialFinal.Models;

namespace ParcialFinal.Services;

public class VideojuegoService
{
    private readonly VideojuegoDao _dao;

    public VideojuegoService(VideojuegoDao dao)
    {
        _dao = dao;
    }

    public List<Videojuego> ListarVideojuegos() => _dao.ObtenerTodos();

    public (bool Ok, string Mensaje) Registrar(string titulo, string plataforma, string genero, string precioTexto, string cantidadTexto)
    {
        if (!ValidarCampos(titulo, plataforma, genero, precioTexto, cantidadTexto, out var precio, out var cantidad, out var error))
            return (false, error);

        var juego = new Videojuego(0, titulo.Trim(), plataforma.Trim(), genero.Trim(), precio, cantidad);
        _dao.Insertar(juego);
        return (true, $"Videojuego '{juego.Titulo}' guardado en SQLite.");
    }

    public (bool Ok, string Mensaje) Actualizar(int id, string titulo, string plataforma, string genero, string precioTexto, string cantidadTexto)
    {
        if (id <= 0)
            return (false, "Selecciona un registro de la lista antes de actualizar.");

        if (!ValidarCampos(titulo, plataforma, genero, precioTexto, cantidadTexto, out var precio, out var cantidad, out var error))
            return (false, error);

        var juego = new Videojuego(id, titulo.Trim(), plataforma.Trim(), genero.Trim(), precio, cantidad);
        var actualizado = _dao.Actualizar(juego);
        return actualizado
            ? (true, $"Videojuego '{juego.Titulo}' actualizado correctamente.")
            : (false, "No se encontró el registro seleccionado en la base de datos.");
    }

    public (bool Ok, string Mensaje) Eliminar(int id)
    {
        if (id <= 0)
            return (false, "Selecciona un registro de la lista antes de eliminar.");

        var eliminado = _dao.Eliminar(id);
        return eliminado
            ? (true, "Videojuego eliminado correctamente.")
            : (false, "No se pudo eliminar el registro seleccionado.");
    }

    private static bool ValidarCampos(
        string titulo,
        string plataforma,
        string genero,
        string precioTexto,
        string cantidadTexto,
        out decimal precio,
        out int cantidad,
        out string error)
    {
        precio = 0;
        cantidad = 0;

        if (string.IsNullOrWhiteSpace(titulo))
        {
            error = "El título es obligatorio.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(plataforma))
        {
            error = "La plataforma es obligatoria.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(genero))
        {
            error = "El género es obligatorio.";
            return false;
        }

        var precioNormalizado = (precioTexto ?? string.Empty).Trim().Replace(',', '.');
        if (!decimal.TryParse(precioNormalizado, NumberStyles.Any, CultureInfo.InvariantCulture, out precio) || precio < 0)
        {
            error = "Ingresa un precio numérico válido mayor o igual a 0.";
            return false;
        }

        if (!int.TryParse((cantidadTexto ?? string.Empty).Trim(), out cantidad) || cantidad < 0)
        {
            error = "Ingresa una cantidad entera válida mayor o igual a 0.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}