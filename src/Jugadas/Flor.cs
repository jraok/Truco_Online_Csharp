using System.Text.Json.Serialization;
namespace EspacioFlor{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoFlor{
        Flor,
        ContraFlor,
        ContraFlorResto,
    }
    public class CantoFlor{
        public const int PuntosPartida = 30;
        private TipoFlor tipo;
        private string jugador;
        private int puntos;
        public TipoFlor Tipo => tipo;
        public string Jugador => jugador;
        public int Puntos => puntos;

        [JsonConstructor]
        public CantoFlor(TipoFlor tipo, string jugador){
            this.tipo = tipo;
            this.jugador = jugador;
            this.puntos = tipo switch{
                TipoFlor.Flor => 3,
                TipoFlor.ContraFlor => 6,
                TipoFlor.ContraFlorResto => PuntosPartida,
                _ => 0,
            };
        }
    }
    public class SecuenciaFlor
    {
        private List<CantoFlor> jugada = new List<CantoFlor>();
        public List<CantoFlor> Jugada => jugada;
        public int PuntosJugada => jugada.Count == 0 ? 0 : jugada.Last().Puntos;
        public void AgregarCanto(CantoFlor canto)
        {
            jugada.Add(canto);
        }
    }
}