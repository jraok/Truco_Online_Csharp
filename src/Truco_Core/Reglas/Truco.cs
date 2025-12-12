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
        private int puntos;
        private string jugador;
        public TipoTruco Tipo => tipo;
        public int Puntos => puntos;
        public string Jugador => jugador;
        
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
}