using Truco.Core.Modelos;
namespace Truco.Core.Juego
{
    public class Turno{
        private string jugador;
        private Carta cartaJugada;
        public string Jugador => jugador;
        public Carta CartaJugada => cartaJugada;
        public Turno(string jugador, Carta carta){
            this.jugador = jugador;
            this.cartaJugada = carta;
        }
    }
}