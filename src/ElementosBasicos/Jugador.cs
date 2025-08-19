using System.Text.Json.Serialization;
using EspacioCarta;
namespace EspacioJugador{
    public class Jugador{
        private const int BonusEnvido = 20;
        private string nombre;
        private int puntaje;
        private List<Carta> cartas = new();
        public string Nombre => nombre;
        public int Puntaje => puntaje;
        public List<Carta> Cartas => cartas;
        public int PuntosEnvido => CalcularEnvido();
        public int PuntosFlor => CalcularFlor();

        [JsonConstructor]
        public Jugador(string nombre){
            this.nombre = nombre;
            puntaje = 0;
            cartas = new List<Carta>(3);
        }

        public void RecibirCartas(List<Carta> cartas){
            if ((this.cartas.Count + cartas.Count) <= 3) this.cartas.AddRange(cartas);
            else Console.WriteLine("El jugador ya tiene 3 cartas");
        }

        public Carta? JugarCarta(int indice){
            if(indice >= 0 && indice < cartas.Count){
                var carta = cartas[indice];
                cartas.RemoveAt(indice);
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
            for (int i = 0; i < cartas.Count; i++)
            {
                for (int j = i+1 ; j < cartas.Count; j++)
                {
                    if (cartas[i].Palo == cartas[j].Palo)
                    {
                        int suma = ValorEnvido(cartas[i]) + ValorEnvido(cartas[j]) + BonusEnvido;
                        if (suma > puntos) puntos = suma;
                    }
                }
            }
            if (puntos == 0) puntos = cartas.Max(carta => ValorEnvido(carta));
            return puntos;
        }
        private int CalcularFlor(){
            int puntos = 0;
            if (cartas.Count == 3 && cartas[0].Palo == cartas[1].Palo && cartas[1].Palo == cartas[2].Palo)
            {
                puntos = cartas.Sum(carta => ValorEnvido(carta)) + BonusEnvido;
            }
            return puntos;
        }
    }
}