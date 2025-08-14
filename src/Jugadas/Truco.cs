namespace EspacioTruco{
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
    public class SecuenciaTruco{
        private List<CantoTruco> jugada = new List<CantoTruco>();
        private int puntosJugada;
        public List<CantoTruco> Jugada => jugada;
        public int PuntosJugada => puntosJugada;
        public void AgregarCanto(CantoTruco canto){
            jugada.Add(canto);
        }
        public void CalcularPuntos(){
            if (jugada.Count() == 0) {
                puntosJugada = 0;
                return;
            }
            puntosJugada = jugada.Last().Puntos;
        }
    }
}