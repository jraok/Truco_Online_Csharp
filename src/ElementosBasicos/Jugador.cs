using System.Text.Json.Serialization;
using EspacioCarta;
namespace EspacioJugador{
    public class Jugador{
        private const int BonusEnvido = 20;
        private string nombre;
        private int puntaje;
        private List<Carta> mano;
        public string Nombre => nombre;
        public int Puntaje => puntaje;
        public List<Carta> Mano => mano;
        public int PuntosEnvido => CalcularEnvido();
        public int PuntosFlor => CalcularFlor();

        [JsonConstructor]
        public Jugador(string nombre){
            this.nombre = nombre;
            puntaje = 0;
            mano = new List<Carta>(3);
        }

        public void RecibirCartas(List<Carta> cartas){
            if ((mano.Count + cartas.Count) <= 3) mano.AddRange(cartas);
            else Console.WriteLine("El jugador ya tiene 3 cartas");
        }

        public Carta? JugarCarta(int indice){
            if(indice >= 0 && indice < mano.Count){
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
        
        private static int ValorEnvido(Carta carta){
            if (carta.Numero >= 10) return 0;
            else return carta.Numero;
        }

        private int CalcularEnvido(){
            int puntos = 0;
            for (int i = 0; i < mano.Count; i++)
            {
                for (int j = i+1 ; j < mano.Count; j++)
                {
                    if (mano[i].Palo == mano[j].Palo)
                    {
                        int suma = ValorEnvido(mano[i]) + ValorEnvido(mano[j]) + BonusEnvido;
                        if (suma > puntos) puntos = suma;
                    }
                }
            }
            if (puntos == 0) puntos = mano.Max(carta => ValorEnvido(carta));
            return puntos;
        }
        private int CalcularFlor(){
            int puntos = 0;
            if (mano.Count == 3 && mano[0].Palo == mano[1].Palo && mano[1].Palo == mano[2].Palo)
            {
                puntos = mano.Sum(carta => ValorEnvido(carta)) + BonusEnvido;
            }
            return puntos;
        }
    }
}