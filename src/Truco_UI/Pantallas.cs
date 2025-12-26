using Truco.Core.Juego;
using Truco.Core.Modelos;
namespace Truco.UI
{
    public class Pantallas
    {
        private Arbitro arbi;
        public void GuardarArbitro(Arbitro arbi)
        {
            this.arbi = arbi;
        }
        public static void Titulo()
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
            Console.WriteLine($"MANO #{arbi.Partida.ManosJugadas}");
            Console.WriteLine($"Mano: {arbi.Partida.JugadorMano.Nombre} {arbi.Partida.JugadorMano.Puntaje}pts | Pie: {arbi.Partida.JugadorPie.Nombre} {arbi.Partida.JugadorPie.Puntaje}pts");
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

            // if (arbi.PuedeCantarFlor)
            //     Console.WriteLine("  [f]  Cantar Flor");

            // if (arbi.PuedeResponderFlor)
            //     Console.WriteLine("  [rf] Responder Flor");

            if (arbi.PuedeIrAlMazo)
                Console.WriteLine("  [m]  Irse al mazo");

            Console.Write("\nElegí una opción: ");
        }
        public void CartasEnMano(Jugador jugador)
        {
            Console.WriteLine("\nCartas en mano:");
            for (int i = 0; i < jugador.Cartas.Count; i++)
            {
                Console.Write($"  [{i}].  ");
                Console.ForegroundColor = ColorPorPalo(jugador.Cartas[i].Palo);
                Console.WriteLine(jugador.Cartas[i].Nombre);
                Console.ResetColor();
            }
        }
        public void Puntajes()
        {
            Console.WriteLine("\n─────────── PUNTAJES ───────────");
            Console.WriteLine($"\t{arbi.Partida.Jugador1.Nombre,-15} {arbi.Partida.Jugador1.Puntaje,3} pts");
            Console.WriteLine($"\t{arbi.Partida.Jugador2.Nombre,-15} {arbi.Partida.Jugador2.Puntaje,3} pts");
            Console.WriteLine("─────────────────────────────────");
        }
        public void MostrarTurnoActual()
        {
            var turno = arbi.Partida.TurnoActual;
            Console.WriteLine($"\n>> TURNO DE {turno?.Nombre.ToUpper()}");
        }
        public void MostrarManoActual()
        {
            var mano = arbi.Partida.ManoActual;
            if (mano == null || mano.Rondas.First().Turnos.Count == 0) return;

            var turnosMano = mano.Rondas.SelectMany(r => r.Turnos).Where(t => t.Jugador == mano.JugadorMano.Nombre).ToList();
            
            var turnosPie = mano.Rondas.SelectMany(r => r.Turnos).Where(t => t.Jugador == mano.JugadorPie.Nombre).ToList();

            Console.Write($"\n{mano.JugadorMano.Nombre}: ");
            foreach (var turno in turnosMano)
            {
                Console.ForegroundColor = ColorPorPalo(turno.CartaJugada.Palo);
                Console.Write($"[{turno.CartaJugada.Nombre}] ");
                Console.ResetColor();
            }
            Console.WriteLine();

            // Mostrar cartas del Pie
            Console.Write($"{mano.JugadorPie.Nombre}: ");
            foreach (var turno in turnosPie)
            {
                Console.ForegroundColor = ColorPorPalo(turno.CartaJugada.Palo);
                Console.Write($"[{turno.CartaJugada.Nombre}] ");
                Console.ResetColor();
            }
            Console.WriteLine();
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
        // public void MostrarFlores()
        // {
        //     Console.WriteLine($"\nTantos: {arbi.Partida.Jugador1.Nombre} {arbi.Partida.Jugador1.PuntosFlor} | {arbi.Partida.Jugador2.Nombre} {arbi.Partida.Jugador2.PuntosFlor}");
        // }
        public void MostrarCantosPendientes()
        {
            var mano = arbi.Partida.ManoActual;
            if (mano == null) return;

            bool hayCantos = false;

            if (arbi.PuedeResponderTruco && mano.SecuenciaTruco.Count != 0)
            {
                var ultimoTruco = mano.SecuenciaTruco.Last();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{ultimoTruco.Jugador} cantó {ultimoTruco.Tipo}");
                Console.ResetColor();
                hayCantos = true;
            }

            if (arbi.PuedeResponderEnvido && mano.SecuenciaEnvido.Count != 0)
            {
                var ultimoEnvido = mano.SecuenciaEnvido.Last();
                var puntosAcumulados = mano.SecuenciaEnvido.Count > 1 
                    ? $" (Total: {mano.SecuenciaEnvido.Count} cantos)" 
                    : "";
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{ultimoEnvido.Jugador} cantó {ultimoEnvido.Tipo}{puntosAcumulados}");
                Console.ResetColor();
                hayCantos = true;
            }

            // if (arbi.PuedeResponderFlor && mano.SecuenciaFlor.Count != 0)
            // {
            //     var ultimaFlor = mano.SecuenciaFlor.Last();
            //     Console.ForegroundColor = ConsoleColor.Green;
            //     Console.WriteLine($"{ultimaFlor.Jugador} cantó {ultimaFlor.Tipo}");
            //     Console.ResetColor();
            //     hayCantos = true;
            // }

            if (hayCantos) Console.WriteLine();
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