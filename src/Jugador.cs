using EspacioCarta;
namespace EspacioJugador
{
    public class Jugador{
        private string nombre;
        private int puntaje;
        private List<Carta> mano;

        public string Nombre => nombre;
        public int Puntaje => puntaje;
        public List<Carta> Mano => mano;
    }
}