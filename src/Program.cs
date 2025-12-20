using Truco.Core.Juego;
using Truco.Core.Reglas;
using Truco.Core.Modelos;

var arbitro = new Arbitro("Juan", "Pedro");
var partida = arbitro.Partida;

Console.WriteLine("=== TRUCO ARGENTINO ===");
Console.WriteLine($"Partida: {partida.Jugador1.Nombre} vs {partida.Jugador2.Nombre}");
Console.WriteLine($"Puntos para ganar: {partida.PuntosPartida}");
Console.WriteLine();

while (partida.Jugador1.Puntaje < partida.PuntosPartida && 
       partida.Jugador2.Puntaje < partida.PuntosPartida)
    {
        // Iniciar nueva mano
        arbitro.IniciarMano();
        Console.WriteLine($"\n--- MANO #{partida.ManosJugadas} ---");
        Console.WriteLine($"Mano: {partida.ManoActual.JugadorMano.Nombre}");
        Console.WriteLine($"Pie: {partida.ManoActual.JugadorPie.Nombre}");
        Console.WriteLine();

        MostrarCartas(partida.Jugador1);
        MostrarCartas(partida.Jugador2);
        
        // Loop de juego de la mano
        while (!partida.ManoActual.Finalizada)
        {
            var turno = partida.TurnoActual;
            Console.WriteLine($"\n>> Turno de {turno.Nombre}");
            Console.WriteLine($"Cartas: {string.Join(", ", turno.Cartas.Select((c, i) => $"[{i}] {c.Nombre}"))}");
            
            MostrarOpciones(arbitro, turno.Nombre);
            
            var opcion = Console.ReadLine()?.ToLower();
            
            try
            {
                switch (opcion)
                {
                    case "c": // Jugar carta
                        Console.Write("Índice de carta (0, 1, 2): ");
                        var idx = int.Parse(Console.ReadLine() ?? "0");
                        arbitro.JugarCarta(turno.Nombre, idx);
                        Console.WriteLine($"{turno.Nombre} jugó: {turno.Cartas.Count} cartas restantes");
                        break;
                    
                    case "t": // Cantar truco
                        Console.WriteLine("1. Truco  2. Retruco  3. Vale Cuatro");
                        var tipoTruco = int.Parse(Console.ReadLine() ?? "1");
                        arbitro.CantarTruco(turno.Nombre, (TipoTruco)(tipoTruco - 1));
                        Console.WriteLine($"{turno.Nombre} cantó {(TipoTruco)(tipoTruco - 1)}!");
                        break;
                    
                    case "rt": // Responder truco
                        Console.Write("¿Aceptás? (s/n): ");
                        var aceptaTruco = Console.ReadLine()?.ToLower() == "s";
                        arbitro.ResponderTruco(turno.Nombre, aceptaTruco);
                        Console.WriteLine($"{turno.Nombre} {(aceptaTruco ? "aceptó" : "rechazó")} el truco");
                        break;
                    
                    case "e": // Cantar envido
                        Console.WriteLine("1. Envido  2. Real Envido  3. Falta Envido");
                        var tipoEnvido = int.Parse(Console.ReadLine() ?? "1");
                        arbitro.CantarEnvido(turno.Nombre, (TipoEnvido)(tipoEnvido - 1));
                        Console.WriteLine($"{turno.Nombre} cantó {(TipoEnvido)(tipoEnvido - 1)}!");
                        break;
                    
                    case "re": // Responder envido
                        Console.Write("¿Aceptás? (s/n): ");
                        var aceptaEnvido = Console.ReadLine()?.ToLower() == "s";
                        arbitro.ResponderEnvido(turno.Nombre, aceptaEnvido);
                        Console.WriteLine($"{turno.Nombre} {(aceptaEnvido ? "aceptó" : "rechazó")} el envido");
                        MostrarEnvidos(partida);
                        break;
                    
                    case "m": // Irse al mazo
                        arbitro.IrAlMazo(turno.Nombre);
                        Console.WriteLine($"{turno.Nombre} se fue al mazo");
                        break;
                    
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Thread.Sleep(1800); // Espera 1.8 segundos
                Console.Clear();    // Limpia la consola
            }
            
            MostrarPuntajes(partida);
        }
        
        // Fin de mano
        var ganadorMano = partida.ManoActual.GanadorMano();
        Console.WriteLine($"\n*** Mano ganada por: {ganadorMano ?? "Nadie (empate)"} ***");
        MostrarPuntajes(partida);
    }

    // Fin de partida
    Console.WriteLine("\n" + new string('=', 40));
    Console.WriteLine("*** FIN DE LA PARTIDA ***");
    var ganador = partida.Jugador1.Puntaje >= partida.PuntosPartida 
        ? partida.Jugador1 
        : partida.Jugador2;
    Console.WriteLine($"Ganador: {ganador.Nombre}");
    Console.WriteLine($"Puntaje final: {partida.Jugador1.Nombre} {partida.Jugador1.Puntaje} - {partida.Jugador2.Nombre} {partida.Jugador2.Puntaje}");
    Console.WriteLine(new string('=', 40));

    // === FUNCIONES AUXILIARES ===

    static void MostrarCartas(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}: {string.Join(", ", jugador.Cartas.Select(c => c.Nombre))}");
    }

    static void MostrarPuntajes(Partida partida)
    {
        Console.WriteLine($"\nPuntajes: {partida.Jugador1.Nombre} {partida.Jugador1.Puntaje} - {partida.Jugador2.Nombre} {partida.Jugador2.Puntaje}");
    }

    static void MostrarEnvidos(Partida partida)
    {
        var envido1 = Operador.CalcularEnvido(partida.Jugador1.Cartas);
        var envido2 = Operador.CalcularEnvido(partida.Jugador2.Cartas);
        Console.WriteLine($"Envidos: {partida.Jugador1.Nombre} tiene {envido1}, {partida.Jugador2.Nombre} tiene {envido2}");
    }

    static void MostrarOpciones(Arbitro arbitro, string nombreJugador)
    {
        Console.WriteLine("\nOpciones:");
        Console.WriteLine("  [c]  Jugar carta");
        
        // Solo mostrar opciones válidas según el estado
        try
        {
            if (arbitro.Partida.ManoActual.SecuenciaTruco.Count == 0 || 
                arbitro.Partida.ManoActual.SecuenciaTruco.Last().Jugador != nombreJugador)
            {
                Console.WriteLine("  [t]  Cantar Truco");
            }
        }
        catch { }
        
        try
        {
            if (arbitro.Partida.ManoActual.SecuenciaTruco.Count > 0)
            {
                Console.WriteLine("  [rt] Responder Truco");
            }
        }
        catch { }
        
        try
        {
            if (arbitro.Partida.ManoActual.SecuenciaEnvido.Count == 0 && 
                !arbitro.Partida.ManoActual.RondaActual.RondaCompleta())
            {
                Console.WriteLine("  [e]  Cantar Envido");
            }
        }
        catch { }
        
        try
        {
            if (arbitro.Partida.ManoActual.SecuenciaEnvido.Count > 0)
            {
                Console.WriteLine("  [re] Responder Envido");
            }
        }
        catch { }
        
        Console.WriteLine("  [m]  Irse al mazo");
        Console.Write("\nElegí una opción: ");
    }