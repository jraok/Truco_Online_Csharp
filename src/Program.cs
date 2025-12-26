using Truco.Core.Reglas;
using Truco.Core.Juego;
using Truco.UI;

Pantallas.Titulo();

string nombreJ1 = LeerNombreJugador("Nombre del Jugador 1 (Mano inicial): ", "Jugador 1");
string nombreJ2 = LeerNombreJugador("Nombre del Jugador 2 (Mano inicial): ", "Jugador 2");

var pantallas = new Pantallas();
var arbitro = new Arbitro(nombreJ1, nombreJ2);
pantallas.GuardarArbitro(arbitro);
var partida = arbitro.Partida;

Console.WriteLine($"\nPartida: {partida.Jugador1.Nombre} vs {partida.Jugador2.Nombre}");
Console.WriteLine($"Puntos para ganar: {partida.PuntosPartida}");
pantallas.EsperarTecla();

while (partida.Jugador1.Puntaje < partida.PuntosPartida && partida.Jugador2.Puntaje < partida.PuntosPartida)
{
    arbitro.IniciarMano();
    while(!partida.ManoActual.Finalizada && partida.Jugador1.Puntaje < partida.PuntosPartida && partida.Jugador2.Puntaje < partida.PuntosPartida)
    {
        Console.Clear();
        pantallas.EncabezadoMano();
        pantallas.MostrarCantosPendientes();
        pantallas.MostrarTurnoActual();
        pantallas.MostrarManoActual();
        pantallas.CartasEnMano(partida.TurnoActual);
        pantallas.Opciones();

        var opcion = Console.ReadLine()?.ToLower().Trim();

        try
        {
            ProcesarOpcion(opcion, arbitro, pantallas);   
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {ex.Message}");
            Console.ResetColor();
            pantallas.EsperarTecla();
        }
    }
}

Console.Clear();
Pantallas.Titulo();
var ganador = partida.Jugador1.Puntaje >= partida.PuntosPartida 
    ? partida.Jugador1 
    : partida.Jugador2;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n════════════════════════════════════");
Console.WriteLine($"    🏆 GANADOR: {ganador.Nombre,-18}");
Console.WriteLine("════════════════════════════════════");
Console.ResetColor();
Console.WriteLine($"\nPuntaje final:");
Console.WriteLine($"  {partida.Jugador1.Nombre}: {partida.Jugador1.Puntaje}");
Console.WriteLine($"  {partida.Jugador2.Nombre}: {partida.Jugador2.Puntaje}");

static string LeerNombreJugador(string mensaje, string nombrePorDefecto)
{
    while (true)
    {
        Console.Write(mensaje);
        var input = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Usando nombre por defecto: {nombrePorDefecto}");
            Console.ResetColor();
            return nombrePorDefecto;
        }
                if (input.Length > 20)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El nombre no puede tener más de 20 caracteres.");
            Console.ResetColor();
            continue;
        }
        
        if (input.All(c => char.IsWhiteSpace(c) || char.IsPunctuation(c)))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El nombre debe contener al menos una letra o número.");
            Console.ResetColor();
            continue;
        }
        return input;
    }
}

static int LeerNumero(string mensaje, int min, int max)
{
    Console.Write(mensaje);
    var input = Console.ReadLine();
    
    if (!int.TryParse(input, out int numero))
    {
        throw new InvalidOperationException("Entrada inválida. Ingresá un número.");
    }
    
    if (numero < min || numero > max)
    {
        throw new InvalidOperationException($"Debe ser un número entre {min} y {max}");
    }
    
    return numero;
}

static bool LeerConfirmacion(string mensaje)
{
    Console.Write(mensaje);
    var input = Console.ReadLine()?.ToLower().Trim();
    
    if (input == "s" || input == "si" || input == "sí")
        return true;
    if (input == "n" || input == "no")
        return false;
    
    throw new InvalidOperationException("Respuesta inválida. Ingresá 's' para sí o 'n' para no.");
}

static void ProcesarOpcion(string opcion, Arbitro arbitro, Pantallas pantallas)
{
    if (string.IsNullOrWhiteSpace(opcion))
        throw new InvalidOperationException("Debés elegir una opción");
    
    var jugador = arbitro.Partida.TurnoActual;
    switch (opcion)
    {
        case "c":
            var idx = LeerNumero("Índice de carta (0, 1, 2): ", 0, jugador.Cartas.Count - 1);
            arbitro.JugarCarta(jugador, idx);
            pantallas.MostrarMensaje($"{jugador.Nombre} jugó una carta");
            Thread.Sleep(1800);
            break;
        
        case "t":
            Console.WriteLine("\n1. Truco  2. Retruco  3. Vale Cuatro");
            var tipoTruco = LeerNumero("Elegí: ", 1, 3);
            arbitro.CantarTruco(jugador, (TipoTruco)(tipoTruco - 1));   
            pantallas.MostrarMensaje($"{jugador.Nombre} cantó {(TipoTruco)(tipoTruco - 1)}!");
            Thread.Sleep(1800);
            break;
        
        case "rt":
            var aceptaTruco = LeerConfirmacion("¿Aceptás el truco? (s/n): ");
            arbitro.ResponderTruco(jugador, aceptaTruco);
            pantallas.MostrarMensaje($"{jugador.Nombre} {(aceptaTruco ? "QUIERO" : "NO QUIERO")}");
            Thread.Sleep(1800);
            break;
        
        case "e":
            Console.WriteLine("\n1. Envido  \n2. Real Envido  \n3. Falta Envido");
            var tipoEnvido = LeerNumero("Elegí: ", 1, 3);
            arbitro.CantarEnvido(jugador, (TipoEnvido)(tipoEnvido - 1));
            pantallas.MostrarMensaje($"{jugador.Nombre} cantó {(TipoEnvido)(tipoEnvido - 1)}!");
            Thread.Sleep(1800);
            break;
        
        case "re":
            var aceptaEnvido = LeerConfirmacion("¿Aceptás el envido? (s/n): ");
            arbitro.ResponderEnvido(jugador, aceptaEnvido);
            pantallas.MostrarMensaje($"{jugador.Nombre} {(aceptaEnvido ? "QUIERO" : "NO QUIERO")}");
            if (aceptaEnvido) pantallas.MostrarEnvidos();
            Thread.Sleep(2500);
            break;

        case "m":
            arbitro.IrAlMazo(jugador);
            pantallas.MostrarMensaje($"{jugador.Nombre} se fue al mazo");
            Thread.Sleep(1800);
            break;

        default:
            throw new InvalidOperationException("Opción inválida. Elegí una opción del menú.");
    }
}
