using System.Text.Json.Serialization;
namespace EspacioFlor{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoFlor{
        Flor,
        ContraFlor,
        ContraFlorResto,
    }
    public class CantoFlor{
        private TipoFlor tipo;
        private string jugador;
        private int puntos;
        public TipoFlor Tipo => tipo;
        public string Jugador => jugador;
        public int Puntos => puntos;

        [JsonConstructor]
        public CantoFlor(TipoFlor tipo, string jugador, int Puntos){
            this.tipo = tipo;
            this.jugador = jugador;
            this.puntos = tipo switch{
                TipoFlor.Flor => 3,
                TipoFlor.ContraFlor => 6,
                TipoFlor.ContraFlorResto => Puntos,
                _ => 0,
            };
        }
    }
}