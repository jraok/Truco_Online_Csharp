using System.Text.Json.Serialization;
using EspacioCarta;
namespace EspacioJugador{
    public class Jugador{
        private const int BonusEnvido = 20;
        private string nombre;
        private int puntaje;
        private List<Carta> mano;
        private int puntosEnvido;
        private int puntosFlor;
        public string Nombre => nombre;
        public int Puntaje => puntaje;
        public List<Carta> Mano => mano;
        public int PuntosEnvido => puntosEnvido;
        public int PuntosFlor => puntosFlor;

        [JsonConstructor]
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
        private int ValorEnvido(Carta carta){
            if (carta.Numero >= 10) return 0;
            else return carta.Numero;
        }

        public void CalcularEnvido(){
            puntosEnvido = 0;
            for (int i = 0; i < mano.Count(); i++)
            {
                for (int j = i+1 ; j < mano.Count(); j++)
                {
                    if (mano[i].Palo == mano[j].Palo)
                    {
                        int suma = ValorEnvido(mano[i]) + ValorEnvido(mano[j]) + BonusEnvido;
                        if (suma > puntosEnvido) puntosEnvido = suma;
                    }
                }
            }
            if (puntosEnvido == 0) puntosEnvido = mano.Max(carta => ValorEnvido(carta));
        }
        public void CalcularFlor(){
            puntosFlor = 0;
            if (mano.Count() == 3 && mano[0].Palo == mano[1].Palo && mano[1].Palo == mano[2].Palo)
            {
                puntosFlor = mano.Sum(carta => ValorEnvido(carta)) + BonusEnvido;
            }
        }
    }
}