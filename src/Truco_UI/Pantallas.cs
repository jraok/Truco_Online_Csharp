using Truco.Core.Juego;
using Truco.Core.Modelos;
namespace Truco.UI
{
    public class Pantallas(Arbitro arbi)
    {
        public void Titulo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine("████████╗ ██████╗  ██╗   ██╗  ██████╗  ██████╗");
            Console.WriteLine("╚══██╔══╝ ██╔══██╗ ██║   ██║ ██╔════╝ ██╔═══██║");
            Console.WriteLine("   ██║    ██████╔╝ ██║   ██║ ██║      ██║   ██║");
            Console.WriteLine("   ██║    ██╔══██╗ ██║   ██║ ██║      ██║   ██║");
            Console.WriteLine("   ██║    ██║  ██║ ╚██████╔╝ ╚██████╗ ╚██████╔╝");
            Console.WriteLine("   ╚═╝    ╚═╝  ╚═╝  ╚═════╝   ╚═════╝  ╚═════╝ ");
            Console.WriteLine("===============================================");
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void MenuInicio()
        {
            Console.WriteLine("1. Jugar");
            Console.WriteLine("2. Reglas");
            Console.WriteLine("3. Salir");
        }
        public void EncabezadoMano()
        {
            Console.WriteLine($"\n{'─', 50}");
            Console.WriteLine($"MANO #{arbi.Partida.ManosJugadas}");
            Console.WriteLine($"Mano: {arbi.Partida.JugadorMano.Nombre} | Pie: {arbi.Partida.JugadorPie.Nombre}");
            Console.WriteLine($"{'─', 50}\n");
        }
        public void Opciones()
        {
            Console.WriteLine("\nOpciones:");

            if (arbi.PuedeJugarCarta)
                Console.WriteLine("  [c]  Jugar carta");

            if (arbi.PuedeCantarTruco)
                Console.WriteLine("  [t]  Cantar Truco");

            if (arbi.PuedeResponderTruco)
                Console.WriteLine("  [rt] Responder Truco");

            if (arbi.PuedeCantarEnvido)
                Console.WriteLine("  [e]  Cantar Envido");

            if (arbi.PuedeResponderEnvido)
                Console.WriteLine("  [re] Responder Envido");

            if (arbi.PuedeIrAlMazo)
                Console.WriteLine("  [m]  Irse al mazo");

            Console.Write("\nElegí una opción: ");
        }
        public void CartasEnMano(Jugador jugador)
        {
            Console.WriteLine("Cartas en mano:");
            for (int i = 0; i < jugador.Cartas.Count; i++)
            {
                Console.Write($"\t[{i}].");
                Console.ForegroundColor = ColorPorPalo(jugador.Cartas[i].Palo);
                Console.WriteLine(jugador.Cartas[i].Nombre);
                Console.ResetColor();
            }
        }
        public void Puntajes()
        {
            Console.WriteLine("\n┌─────────── PUNTAJES ───────────┐");
            Console.WriteLine($"│ {arbi.Partida.Jugador1.Nombre,-15} {arbi.Partida.Jugador1.Puntaje,3} pts │");
            Console.WriteLine($"│ {arbi.Partida.Jugador2.Nombre,-15} {arbi.Partida.Jugador2.Puntaje,3} pts │");
            Console.WriteLine("└─────────────────────────────────┘");
        }
        public void MostrarTurnoActual()
        {
                var turno = arbi.Partida.TurnoActual;
            Console.WriteLine($"\n>> TURNO DE {turno?.Nombre.ToUpper()}");
        }
        public void MostrarRondaActual()
        {
            var ronda = arbi.Partida.ManoActual?.RondaActual;
            if (ronda == null || ronda.Turnos.Count == 0) return;

            Console.WriteLine($"\nRonda {ronda.Numero}:");
            foreach (var turno in ronda.Turnos)
            {
                Console.Write($"  {turno.Jugador}: ");
                Console.ForegroundColor = ColorPorPalo(turno.CartaJugada.Palo);
                Console.WriteLine(turno.CartaJugada.Nombre);
                Console.ResetColor();
            }
        }
        public void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n→ {mensaje}");
            Console.ResetColor();
        }
        public void MostrarEnvidos()
        {
            Console.WriteLine($"\nTantos: {arbi.Partida.Jugador1.Nombre} {arbi.Partida.Jugador1.PuntosEnvido} | {arbi.Partida.Jugador2.Nombre} {arbi.Partida.Jugador2.PuntosEnvido}");
        }
        public void MostrarFlores()
        {
            Console.WriteLine($"\nTantos: {arbi.Partida.Jugador1.Nombre} {arbi.Partida.Jugador1.PuntosFlor} | {arbi.Partida.Jugador2.Nombre} {arbi.Partida.Jugador2.PuntosFlor}");
        }
        public void EsperarTecla(string mensaje = "Presioná cualquier tecla para continuar...")
        {
            Console.WriteLine($"\n{mensaje}");
            Console.ReadKey();
        }
        private static ConsoleColor ColorPorPalo(Palos palo)
        {
            return palo switch
            {
                Palos.Espada => ConsoleColor.Cyan,
                Palos.Basto  => ConsoleColor.Green,
                Palos.Oro    => ConsoleColor.Yellow,
                Palos.Copa   => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
        
    }
}