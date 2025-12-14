using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
    public enum Ganador {
        Mano,
        Pie,
        Empate
    }
    public class Ronda(int numero)
    {
        public int Numero { get; private set; } = numero;
        public List<Turno> Turnos { get; private set; } = new();
        public Ganador GanadorRonda => DeterminarGanador();
        public void AgregarTurno(Turno turno){
            Turnos.Add(turno);
        }
        private Ganador DeterminarGanador(){
            var carta1 = Turnos[0].CartaJugada;
            var carta2 = Turnos[1].CartaJugada;
            return Operador.CompararCartas(carta1, carta2) switch
            {
                1 => Ganador.Mano,
                -1 => Ganador.Pie,
                _ => Ganador.Empate,
            };
        }
    }
}