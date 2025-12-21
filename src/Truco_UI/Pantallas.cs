using Truco.Core.Juego;
using Truco.Core.Modelos;
namespace Truco.UI
{
    public class Pantallas(Arbitro arbitro)
    {
        private Arbitro arbitro = arbitro;
        public static void MostarTitulo()
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
        public static void JugarPartida()
        {

        }
        static void MostrarOpciones(Arbitro arbitro)
        {
            Console.WriteLine("\nOpciones:");

            if (arbitro.PuedeJugarCarta)
                Console.WriteLine("  [c]  Jugar carta");

            if (arbitro.PuedeCantarTruco)
                Console.WriteLine("  [t]  Cantar Truco");

            if (arbitro.PuedeResponderTruco)
                Console.WriteLine("  [rt] Responder Truco");

            if (arbitro.PuedeCantarEnvido)
                Console.WriteLine("  [e]  Cantar Envido");

            if (arbitro.PuedeResponderEnvido)
                Console.WriteLine("  [re] Responder Envido");

            if (arbitro.PuedeIrAlMazo)
                Console.WriteLine("  [m]  Irse al mazo");

            Console.Write("\nElegí una opción: ");
        }
        public static void MostrarCartasEnMano(List<Carta> cartas)
        {
            Console.WriteLine("Cartas en mano:");
            for (int i = 0; i < cartas.Count; i++)
            {
                Console.Write($"\t[{i}].");
                Console.ForegroundColor = ColorPorPalo(cartas[i].Palo);
                Console.WriteLine(cartas[i].Nombre);
                Console.ResetColor();
            }
        }
        public static void MostrarPuntaje(Arbitro arbitro)
        {
            Console.WriteLine("Puntajes");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{arbitro.Partida.JugadorMano.Nombre}: {arbitro.Partida.JugadorMano.Puntaje} ");
            Console.WriteLine($"{arbitro.Partida.JugadorPie.Nombre}: {arbitro.Partida.JugadorPie.Puntaje} ");
            Console.WriteLine("------------------------------------------------");
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