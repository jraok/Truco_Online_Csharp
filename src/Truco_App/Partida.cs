using Truco.Core.Reglas;
using Truco.Core.Modelos;
using Truco.Core.Juego;
namespace Truco.App
{
    public class Partida(string nombreJ1, string nombreJ2)
    {
        public Jugador Jugador1 { get; private set; } = new Jugador(nombreJ1);
        public Jugador Jugador2 { get; private set; } = new Jugador(nombreJ2);
        public Jugador? TurnoActual { get; private set; }
        public Mano? ManoActual { get; private set; }
        public int PuntosPartida { get; private set; } = 30;
        public int ManosJugadas { get; private set; } = 0;

        public void CambiarTurno(){
            TurnoActual = (TurnoActual == Jugador1) 
            ? Jugador2 
            : Jugador1;
        }
        public void SumarMano(){
            ManosJugadas++;
        }
        public void AsignarMano(Mano mano){
            ManoActual = mano;
        }
        public void AsignarTurno(Jugador jugador){
            if (Jugador1 != jugador && Jugador2 != jugador) throw new ArgumentException("El jugador no pertenece a la partida");
            TurnoActual = jugador;
        }
        public Jugador JugadorMano => (ManosJugadas % 2 != 0) ? Jugador1 : Jugador2;
        public Jugador JugadorPie => (ManosJugadas % 2 != 0) ? Jugador2 : Jugador1;
    }
}