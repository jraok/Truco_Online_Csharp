using Truco.Core.Reglas;

namespace Truco.Core.Modelos
{
    public class Jugador{
        private readonly string nombre;
        private int puntaje;
        private int puntosEnvido = 0;
        // private int puntosFlor = 0;
        private readonly List<Carta> cartas = new();
        
        public string Nombre => nombre;
        public int Puntaje => puntaje;
        public int PuntosEnvido => puntosEnvido;
        // public int PuntosFlor => puntosFlor;
        public IReadOnlyList<Carta> Cartas => cartas.AsReadOnly();

        public Jugador(string nombre){
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El nombre no puede ser nulo o vacío", nameof(nombre));
            }
            this.nombre = nombre;
            cartas = new List<Carta>(3);
        }

        public void RecibirCartas(List<Carta> cartas){
            if (this.cartas.Count + cartas.Count > 3){
                throw new InvalidOperationException($"El jugador {nombre} no puede recibir más cartas");
            };
            this.cartas.AddRange(cartas);
            AsignarEnvido(Operador.CalcularEnvido(this.cartas));
            // AsignarFlor(Operador.CalcularFlor(this.cartas));
        }

        public Carta TirarCarta(int indice){
            if(indice < 0 || indice >= cartas.Count){
                throw new ArgumentOutOfRangeException(nameof(indice),"El índice no existe");
            }
            var carta = cartas[indice];
            cartas.RemoveAt(indice);
            return carta;
        }
        private void AsignarEnvido(int puntos){
            puntosEnvido = puntos;
        }
        // private void AsignarFlor(int puntos){
        //     puntosFlor = puntos;
        // }
        public void SumarPuntos(int puntos){
            puntaje += puntos;
        }
        public void LimpiarCartas(){
            cartas.Clear();
        }
    }
}