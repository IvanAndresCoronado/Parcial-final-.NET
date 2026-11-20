using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using ParcialFinal.Data;
using ParcialFinal.Models;
using ParcialFinal.Services;

namespace ParcialFinal;

public partial class MainWindow : Window
{
    private readonly VideojuegoService _service;
    private readonly ObservableCollection<Videojuego> _items = new();
    private int _selectedId = 0;

    public MainWindow()
    {
        InitializeComponent();

        var dao = new VideojuegoDao();
        _service = new VideojuegoService(dao);

        LstVideojuegos.ItemsSource = _items;
        CargarDatos();
    }

    private void CargarDatos()
    {
        _items.Clear();
        foreach (var juego in _service.ListarVideojuegos())
        {
            _items.Add(juego);
        }
    }

    private void BtnGuardar_Click(object? sender, RoutedEventArgs e)
    {
        var resultado = _service.Registrar(
            TxtTitulo.Text ?? string.Empty,
            TxtPlataforma.Text ?? string.Empty,
            TxtGenero.Text ?? string.Empty,
            TxtPrecio.Text ?? string.Empty,
            TxtCantidad.Text ?? string.Empty
        );

        MostrarEstado(resultado.Ok, resultado.Mensaje);
        if (resultado.Ok)
        {
            CargarDatos();
            LimpiarFormulario();
        }
    }

    private void BtnActualizar_Click(object? sender, RoutedEventArgs e)
    {
        var resultado = _service.Actualizar(
            _selectedId,
            TxtTitulo.Text ?? string.Empty,
            TxtPlataforma.Text ?? string.Empty,
            TxtGenero.Text ?? string.Empty,
            TxtPrecio.Text ?? string.Empty,
            TxtCantidad.Text ?? string.Empty
        );

        MostrarEstado(resultado.Ok, resultado.Mensaje);
        if (resultado.Ok)
        {
            CargarDatos();
            LimpiarFormulario();
        }
    }

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
    {
        var resultado = _service.Eliminar(_selectedId);
        MostrarEstado(resultado.Ok, resultado.Mensaje);
        if (resultado.Ok)
        {
            CargarDatos();
            LimpiarFormulario();
        }
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        LimpiarFormulario();
        MostrarEstado(true, "Formulario limpio.");
    }

    private void LstVideojuegos_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LstVideojuegos.SelectedItem is not Videojuego seleccionado)
            return;

        _selectedId = seleccionado.Id;
        TxtTitulo.Text = seleccionado.Titulo;
        TxtPlataforma.Text = seleccionado.Plataforma;
        TxtGenero.Text = seleccionado.Genero;
        TxtPrecio.Text = seleccionado.Precio.ToString("0.00");
        TxtCantidad.Text = seleccionado.Cantidad.ToString();
    }

    private void LimpiarFormulario()
    {
        _selectedId = 0;
        TxtTitulo.Text = string.Empty;
        TxtPlataforma.Text = string.Empty;
        TxtGenero.Text = string.Empty;
        TxtPrecio.Text = string.Empty;
        TxtCantidad.Text = string.Empty;
        LstVideojuegos.SelectedItem = null;
    }

    private void MostrarEstado(bool ok, string mensaje)
    {
        TxtEstado.Text = mensaje;
        TxtEstado.Foreground = ok ? Brushes.LightGreen : Brushes.Salmon;
    }
}