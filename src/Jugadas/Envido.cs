using System.Text.Json.Serialization;
namespace EspacioEnvido{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoEnvido{
        Envido,
        RealEnvido,
        FaltaEnvido,
    }
    public class CantoEnvido{
        private TipoEnvido tipo;
        private string jugador;
        private int puntos;
        public TipoEnvido Tipo => tipo;
        public string Jugador => jugador;
        public int Puntos => puntos;

        [JsonConstructor]
        public CantoEnvido(TipoEnvido tipo, string jugador, int puntos){
            this.tipo = tipo;
            this.jugador = jugador;
            this.puntos = tipo switch{
                TipoEnvido.Envido => 2,
                TipoEnvido.RealEnvido => 3,
                TipoEnvido.FaltaEnvido => puntos,
                _ => 0,
            };
        }
    }

    public class SecuenciaEnvido{
        private List<CantoEnvido> jugada = new List<CantoEnvido>();
        public List<CantoEnvido> Jugada => jugada;
        public int PuntosJugada => CalcularPuntos();

        public void AgregarCanto(CantoEnvido canto){
            jugada.Add(canto);
        }

        private int CalcularPuntos(){
            int puntos = 0;
            if (jugada.Any(canto => canto.Tipo == TipoEnvido.FaltaEnvido))
            {
                puntos = jugada.Where(canto => canto.Tipo == TipoEnvido.FaltaEnvido).First().Puntos;
            }else{
                puntos = jugada.Sum(canto => canto.Puntos);
            }
            return puntos;
        }
    }
}