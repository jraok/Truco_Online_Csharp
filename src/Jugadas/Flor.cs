namespace EspacioFlor {
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
        private int puntosJugada;
        public int PuntosJugada => puntosJugada;
        public void AgregarCanto(CantoFlor canto)
        {
            jugada.Add(canto);
        }
        public void CalcularPuntos()
        {
            if (jugada.Count() == 0) {
                puntosJugada = 0;
                return;
            }
            CantoFlor flor = jugada.Last();
            puntosJugada = flor.Puntos;
        }
    }
}