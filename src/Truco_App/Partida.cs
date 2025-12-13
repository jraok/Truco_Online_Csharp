using Truco.Core.Reglas;
using Truco.Core.Modelos;
using Truco.Core.Juego;

namespace Truco.App
{
    public class Partida
    {
        public Jugador jugador1 {get; private set; }
        public Jugador jugador2 {get; private set; }
        public Jugador? TurnoActual { get; private set; }
        public Mano? ManoActual { get; private set; }
        private int PuntosPartida = 30;

        public Partida(string nombreJ1, string nombreJ2)
        {
            jugador1 = new Jugador(nombreJ1);
            jugador2 = new Jugador(nombreJ2);
        }

        public void IniciarMano()
        {
            if (jugador1.Puntaje >= PuntosPartida || jugador2.Puntaje >= PuntosPartida) throw new InvalidOperationException("La partida terminó");

            var mazo = new Mazo();
            mazo.Barajar();

            var Mano = jugador1;
            var Pie = jugador2;

            Mano.RecibirCartas(mazo.Repartir(3));
            Pie.RecibirCartas(mazo.Repartir(3));

        }

    }
}