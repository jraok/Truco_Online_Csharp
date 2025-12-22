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
    while(!partida.ManoActual.Finalizada){
        Console.Clear();
        pantallas.EncabezadoMano();
        pantallas.Puntajes();
        pantallas.MostrarTurnoActual();
        pantallas.MostrarManoActual();
        pantallas.CartasEnMano(partida.TurnoActual);
        pantallas.Opciones();

        var opcion = Console.ReadLine()?.ToLower().Trim();

        try
        {
            ProcesarOpcion(opcion, arbitro,pantallas);   
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
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


static void ProcesarOpcion(string opcion, Arbitro arbitro, Pantallas pantallas)
{
    var jugador = arbitro.Partida.TurnoActual;
    
    switch (opcion)
    {
        case "c":
            Console.Write("Índice de carta (0, 1, 2): ");
            var idx = int.Parse(Console.ReadLine() ?? "0");
            arbitro.JugarCarta(jugador, idx);
            pantallas.MostrarMensaje($"{jugador.Nombre} jugó una carta");
            Thread.Sleep(800);
            break;
        
        case "t":
            Console.WriteLine("\n1. Truco  2. Retruco  3. Vale Cuatro");
            Console.Write("Elegí: ");
            var tipoTruco = int.Parse(Console.ReadLine() ?? "1");
            arbitro.CantarTruco(jugador, (TipoTruco)(tipoTruco - 1));   
            pantallas.MostrarMensaje($"{jugador.Nombre} cantó {(TipoTruco)(tipoTruco - 1)}!");
            Thread.Sleep(800);
            break;
        
        case "rt":
            Console.Write("¿Aceptás el truco? (s/n): ");
            var aceptaTruco = Console.ReadLine()?.ToLower() == "s";
            arbitro.ResponderTruco(jugador, aceptaTruco);
            pantallas.MostrarMensaje($"{jugador.Nombre} {(aceptaTruco ? "QUIERO" : "NO QUIERO")}");
            Thread.Sleep(800);
            break;
        
        case "e":
            Console.WriteLine("\n1. Envido  \n2. Real Envido  \n3. Falta Envido");
            Console.Write("Elegí: ");
            var tipoEnvido = int.Parse(Console.ReadLine() ?? "1");
            arbitro.CantarEnvido(jugador, (TipoEnvido)(tipoEnvido - 1));
            pantallas.MostrarMensaje($"{jugador.Nombre} cantó {(TipoEnvido)(tipoEnvido - 1)}!");
            Thread.Sleep(800);
            break;
        
        case "re":
            Console.Write("¿Aceptás el envido? (s/n): ");
            var aceptaEnvido = Console.ReadLine()?.ToLower() == "s";
            arbitro.ResponderEnvido(jugador, aceptaEnvido);
            pantallas.MostrarMensaje($"{jugador.Nombre} {(aceptaEnvido ? "QUIERO" : "NO QUIERO")}");
            if (aceptaEnvido) pantallas.MostrarEnvidos();
            Thread.Sleep(1500);
            break;

        case "f":
            Console.WriteLine("\n1. Flor \n2. Contra Flor\n3. Contra Flor al Resto");
            Console.Write("Elegí: ");
            var tipoFlor = int.Parse(Console.ReadLine() ?? "1");
            arbitro.CantarFlor(jugador, (TipoFlor)(tipoFlor - 1));
            pantallas.MostrarMensaje($"{jugador.Nombre} cantó {(TipoFlor)(tipoFlor - 1)}!");
            Thread.Sleep(800);
            break;

        case "rf":
            Console.Write("¿Aceptás la flor? (s/n): ");
            var aceptaFlor = Console.ReadLine()?.ToLower() == "s";
            arbitro.ResponderFlor(jugador, aceptaFlor);
            pantallas.MostrarMensaje($"{jugador.Nombre} {(aceptaFlor ? "QUIERO" : "NO QUIERO")}");
            if (aceptaFlor) pantallas.MostrarFlores();
            Thread.Sleep(1500);
            break;

        case "m":
            arbitro.IrAlMazo(jugador);
            pantallas.MostrarMensaje($"{jugador.Nombre} se fue al mazo");
            Thread.Sleep(800);
            break;
        
        default:
            throw new InvalidOperationException("Opción inválida");
    }
}
