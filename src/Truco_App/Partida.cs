using Truco.Core.Reglas;
using Truco.Core.Modelos;
using Truco.Core.Juego;
using System.Security.Principal;
namespace Truco.App
{
    public class Partida
    {
        public Jugador jugador1 {get; private set; }
        public Jugador jugador2 {get; private set; }
        public Jugador? TurnoActual { get; private set; }
        public Mano? ManoActual { get; private set; }
        private int PuntosPartida = 30;
        private int ManosJugadas = 0;

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

            Jugador Mano, Pie;
            ManosJugadas++;
            if (ManosJugadas % 2 != 0)
            {
                Mano = jugador1;
                Pie = jugador2;
            }else{
                Mano = jugador2;
                Pie = jugador1;
            }

            Mano.LimpiarCartas();
            Pie.LimpiarCartas();

            Mano.RecibirCartas(mazo.Repartir(3));
            Pie.RecibirCartas(mazo.Repartir(3));

            ManoActual = new Mano(Mano, Pie);
            ManoActual.IniciarSiguienteRonda();
            TurnoActual = Mano;
        }
        public void JugarCarta(string nombreJugador, int indiceCarta){
            if (ManoActual == null) throw new InvalidOperationException("No hay mano en juego");
            if (nombreJugador != TurnoActual.Nombre) throw new InvalidOperationException($"No es el turno de {nombreJugador}");
            
            var carta = TurnoActual.TirarCarta(indiceCarta);
            ManoActual.RondaActual!.AgregarTurno(new Turno(TurnoActual.Nombre, carta));

        }
        public void CambiarTurno(){
            TurnoActual = (TurnoActual == jugador1) ? jugador2 : jugador1;
        }

        public void ResolverGanadorRonda()
        {
            Jugador? Winner = null;
        }
    }
}