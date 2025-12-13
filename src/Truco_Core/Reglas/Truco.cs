namespace Truco.Core.Reglas
{
    public enum TipoTruco{
        Truco,
        Retruco,
        ValeCuatro,
    }
    public class CantoTruco
    {
        private TipoTruco tipo;
        private string jugador;
        public TipoTruco Tipo => tipo;
        public string Jugador => jugador;
        
        public CantoTruco(TipoTruco tipo, string jugador){
            this.tipo = tipo;
            this.jugador = jugador;
        }
    }
}