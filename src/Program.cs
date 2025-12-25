using Truco.Core.Reglas;
using Truco.Core.Juego;
using Truco.UI;

var arbitro = new Arbitro("Juan", "Pedro");
var pantallas = new Pantallas(arbitro);
var partida = arbitro.Partida;

pantallas.Titulo();
Console.WriteLine($"Partida: {partida.Jugador1.Nombre} vs {partida.Jugador2.Nombre}");
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
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
            pantallas.EsperarTecla();
        }
    }
}

Console.Clear();
pantallas.Titulo();
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
