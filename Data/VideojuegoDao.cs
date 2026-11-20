using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using ParcialFinal.Models;

namespace ParcialFinal.Data;

public class VideojuegoDao
{
    private readonly string _connectionString;

    public VideojuegoDao(string dbPath = "inventario_videojuegos.db")
    {
        _connectionString = $"Data Source={dbPath}";
        InicializarBaseDatos();
    }

    private void InicializarBaseDatos()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Videojuegos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NOT NULL,
                Plataforma TEXT NOT NULL,
                Genero TEXT NOT NULL,
                Precio REAL NOT NULL,
                Cantidad INTEGER NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }

    public int Insertar(Videojuego juego)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Videojuegos (Titulo, Plataforma, Genero, Precio, Cantidad)
            VALUES (@Titulo, @Plataforma, @Genero, @Precio, @Cantidad);
            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("@Titulo", juego.Titulo);
        command.Parameters.AddWithValue("@Plataforma", juego.Plataforma);
        command.Parameters.AddWithValue("@Genero", juego.Genero);
        command.Parameters.AddWithValue("@Precio", (double)juego.Precio);
        command.Parameters.AddWithValue("@Cantidad", juego.Cantidad);

        var id = (long)(command.ExecuteScalar() ?? 0L);
        juego.Id = (int)id;
        return juego.Id;
    }

    public List<Videojuego> ObtenerTodos()
    {
        var lista = new List<Videojuego>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, Titulo, Plataforma, Genero, Precio, Cantidad
            FROM Videojuegos
            ORDER BY Id DESC;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Videojuego(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetDecimal(4),
                reader.GetInt32(5)
            ));
        }

        return lista;
    }

    public bool Actualizar(Videojuego juego)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Videojuegos
            SET Titulo = @Titulo,
                Plataforma = @Plataforma,
                Genero = @Genero,
                Precio = @Precio,
                Cantidad = @Cantidad
            WHERE Id = @Id;
            """;
        command.Parameters.AddWithValue("@Id", juego.Id);
        command.Parameters.AddWithValue("@Titulo", juego.Titulo);
        command.Parameters.AddWithValue("@Plataforma", juego.Plataforma);
        command.Parameters.AddWithValue("@Genero", juego.Genero);
        command.Parameters.AddWithValue("@Precio", (double)juego.Precio);
        command.Parameters.AddWithValue("@Cantidad", juego.Cantidad);

        return command.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Videojuegos WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        return command.ExecuteNonQuery() > 0;
    }
}