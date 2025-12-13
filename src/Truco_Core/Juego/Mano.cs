using Truco.Core.Modelos;
using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
    public class Mano{
        private List<Ronda> rondas = new();
        private List<CantoEnvido> secuenciaEnvido = new();
        private List<CantoTruco> secuenciaTruco = new();
        private List<CantoFlor> secuenciaFlor = new();
        private Jugador jugadorMano;
        private Jugador jugadorPie;
        
        public IReadOnlyList<Ronda> Rondas => rondas.AsReadOnly();
        public List<CantoEnvido> SecuenciaEnvido => secuenciaEnvido;
        public List<CantoTruco> SecuenciaTruco => secuenciaTruco;
        public List<CantoFlor> SecuenciaFlor => secuenciaFlor;
        public Ronda? RondaActual => rondas.LastOrDefault();
        public Mano(Jugador jugadorMano, Jugador jugadorPie){
            this.jugadorMano = jugadorMano;
            this.jugadorPie = jugadorPie;
        }
        public void IniciarSiguienteRonda(){
            if (rondas.Count >= 3) throw new InvalidOperationException("Una mano de truco solo tiene 3 rondas");
            rondas.Add(new Ronda(rondas.Count + 1));
        }
        public void AgregarRonda(Ronda ronda){
            rondas.Add(ronda);
        }
        public void AgregarCanto(TipoEnvido envido, string jugador){
            secuenciaEnvido.Add(new CantoEnvido(envido,jugador));
        }
        public void AgregarCanto(TipoTruco truco, string jugador){
            secuenciaTruco.Add(new CantoTruco(truco,jugador));
        }
        public void AgregarCanto(TipoFlor flor,string jugador){
            secuenciaFlor.Add(new CantoFlor(flor,jugador));
        }
    }
}