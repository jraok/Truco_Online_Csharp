using System.Text.Json.Serialization;
using EspacioCarta;

namespace EspacioTurno
{
    public class Turno{
        private string jugador;
        private Carta cartaJugada;
        public string Jugador => jugador;
        public Carta CartaJugada => cartaJugada;
        [JsonConstructor]
        public Turno(string jugador, Carta carta){
            this.jugador = jugador;
            this.cartaJugada = carta;
        }
    }
}