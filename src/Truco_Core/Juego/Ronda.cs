using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
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
        
        public Ronda(int numero){
            this.numero = numero;
        }
        public void AgregarTurno(Turno turno){
            turnos.Add(turno);
        }
        private Ganador DeterminarGanador(){
            var carta1 = turnos[0].CartaJugada;
            var carta2 = turnos[1].CartaJugada;
            return CalculadorTruco.CompararCartas(carta1, carta2) switch
            {
                1 => Ganador.Mano,
                -1 => Ganador.Pie,
                _ => Ganador.Empate,
            };
        }

    }
}