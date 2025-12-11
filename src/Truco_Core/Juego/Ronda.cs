using System.Text.Json.Serialization;
namespace Truco.Core.Juego
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Ganador {
        Mano,
        Pie,
        Empate
    }
    public class Ronda{
        private int numero;
        private List<Turno> turnos = new();
        public int Numero => numero;
        public List<Turno> Turnos => turnos;
        public Ganador GanadorRonda => DeterminarGanador();
        [JsonConstructor]
        public Ronda(int numero){
            this.numero = numero;
        }
        public void AgregarTurno(Turno turno){
            turnos.Add(turno);
        }
        private Ganador DeterminarGanador(){
            var carta1 = turnos[0].CartaJugada;
            var carta2 = turnos[1].CartaJugada;
            if (carta1.JerarquiaTruco > carta2.JerarquiaTruco)
            {
                return Ganador.Mano;
            }else if(carta1.JerarquiaTruco < carta2.JerarquiaTruco){
                return Ganador.Pie;
            }else{
                return Ganador.Empate;
            }
        }
    }
}