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

        public Jugador(string nombre){
            this.nombre = nombre;
            puntaje = 0;
            mano = new List<Carta>(3);
        }

        public void RecibirCartas(List<Carta> cartas){
            if ((mano.Count() + cartas.Count()) <= 3) mano.AddRange(cartas);
            else Console.WriteLine("El jugador ya tiene 3 cartas");
        }

        public Carta JugarCarta(int indice){
            if(indice >= 0 && indice < mano.Count()){
                var carta = mano[indice];
                mano.RemoveAt(indice);
                return carta;
            }
            else{
                Console.WriteLine("Indice incorrecto");
                return null;
            }
        }

        public void SumarPuntos(int puntos){
            puntaje += puntos;
        }
    }
}