using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
    public class Ronda(int numero)
    {
        public int Numero { get; private set; } = numero;
        public List<Turno> Turnos { get; private set; } = new();
        public string? GanadorRonda => DeterminarGanador(); 
        public void AgregarTurno(Turno turno){
            Turnos.Add(turno);
        }
        private string DeterminarGanador(){
            var carta1 = Turnos[0].CartaJugada;
            var carta2 = Turnos[1].CartaJugada;
            return Operador.CompararCartas(carta1, carta2) switch
            {
                1 => Turnos[0].Jugador,
                -1 => Turnos[1].Jugador,
                _ => "Empate",
            };
        }
        public bool RondaCompleta()
        {
            return Turnos.Count == 2;
        }
    }
}