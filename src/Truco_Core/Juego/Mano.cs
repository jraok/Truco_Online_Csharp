using Truco.Core.Modelos;
using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
    public class Mano(Jugador jugadorMano, Jugador jugadorPie)
    {
        public List<Ronda> Rondas { get; private set; } = new();
        public List<CantoEnvido> SecuenciaEnvido { get; private set; }= new();
        public List<CantoTruco> SecuenciaTruco { get; private set; }= new();
        public List<CantoFlor> SecuenciaFlor { get; private set; }= new();
        public Jugador JugadorMano { get; private set; } = jugadorMano;
        public Jugador JugadorPie { get; private set; } = jugadorPie;
        public Ronda? RondaActual => Rondas.LastOrDefault();

        public void IniciarSiguienteRonda(){
            if (Rondas.Count >= 3) throw new InvalidOperationException("Una mano de truco solo tiene 3 Rondas");
            Rondas.Add(new Ronda(Rondas.Count + 1));
        }
        public void AgregarCanto(TipoEnvido envido, string jugador){
            SecuenciaEnvido.Add(new CantoEnvido(envido,jugador));
        }
        public void AgregarCanto(TipoTruco truco, string jugador){
            SecuenciaTruco.Add(new CantoTruco(truco,jugador));
        }
        public void AgregarCanto(TipoFlor flor,string jugador){
            SecuenciaFlor.Add(new CantoFlor(flor,jugador));
        }
    }
}