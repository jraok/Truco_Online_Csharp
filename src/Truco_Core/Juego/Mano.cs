using Truco.Core.Modelos;
using Truco.Core.Reglas;
namespace Truco.Core.Juego
{
    public class Mano(Jugador jugadorMano, Jugador jugadorPie)
    {
        public List<Ronda> Rondas { get; private set; } = new();
        public List<String> RegistroGanadores { get; private set; } = new();
        public List<CantoEnvido> SecuenciaEnvido { get; private set; }= new();
        public List<CantoTruco> SecuenciaTruco { get; private set; }= new();
        public List<CantoFlor> SecuenciaFlor { get; private set; }= new();
        public Jugador JugadorMano { get; private set; } = jugadorMano;
        public Jugador JugadorPie { get; private set; } = jugadorPie;
        public Ronda? RondaActual => Rondas.LastOrDefault();
        public bool Finalizada { get; private set; } = false;

        public void IniciarSiguienteRonda(){
            if (Rondas.Count >= 3) throw new InvalidOperationException("Una mano de truco solo tiene 3 Rondas");
            Rondas.Add(new Ronda(Rondas.Count + 1));
        }
        public void RegistrarGanadorRonda()
        {
            RegistroGanadores.Add(RondaActual.GanadorRonda);
            if (GanadorMano() != null) FinalizarMano();
        }
        public string? GanadorMano()
        {
            if (RegistroGanadores.Count == 0) return null;

            int ganadasMano = RegistroGanadores.Count(n => n == JugadorMano.Nombre);
            int ganadasPie = RegistroGanadores.Count(n => n == JugadorPie.Nombre);
            int empates = RegistroGanadores.Count(n => n == "Empate");

            if (ganadasMano == 2) return JugadorMano.Nombre;
            if (ganadasPie == 2) return JugadorPie.Nombre;

            if (RegistroGanadores.Count == 2)
            {
                if ((ganadasMano == 1 || ganadasPie == 1) && empates == 1)
                    return (ganadasMano == 1) ? JugadorMano.Nombre : JugadorPie.Nombre;
                if (empates == 2) return null;
            }
            if (RegistroGanadores.Count == 3)
            {
                if (empates == 3) return JugadorMano.Nombre;
                if (RegistroGanadores[2] == "Empate") return RegistroGanadores[0];
            }
            return null;
        }
        public void FinalizarMano(){
            Finalizada = true;
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