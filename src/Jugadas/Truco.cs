using System.Text.Json.Serialization;
namespace EspacioTruco{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoTruco{
        Truco,
        Retruco,
        ValeCuatro,
    }
    public class CantoTruco
    {
        private TipoTruco tipo;
        private int puntos;
        private string jugador;
        public TipoTruco Tipo => tipo;
        public int Puntos => puntos;
        public string Jugador => jugador;
        
        [JsonConstructor]
        public CantoTruco(TipoTruco tipo, string jugador){
            this.tipo = tipo;
            this.jugador = jugador;
            this.puntos = tipo switch

            {
                TipoTruco.Truco => 2,
                TipoTruco.Retruco => 3,
                TipoTruco.ValeCuatro => 4,
                _ => 0,
            };
        }
    }
    public class SecuenciaTruco{
        private List<CantoTruco> jugada = new List<CantoTruco>();
        public List<CantoTruco> Jugada => jugada;
        public int PuntosJugada => jugada.Last().Puntos;
        public void AgregarCanto(CantoTruco canto){
            jugada.Add(canto);
        }
    }
}