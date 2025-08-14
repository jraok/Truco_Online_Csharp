namespace EspacioEnvido{
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
        private int puntosJugada;
        public List<CantoEnvido> Jugada => jugada;
        public int PuntosJugada => puntosJugada;

        public void AgregarCanto(CantoEnvido canto){
            jugada.Add(canto);
        }

        public void CalcularPuntos(){
            if (jugada.Any(canto => canto.Tipo == TipoEnvido.FaltaEnvido))
            {
                CantoEnvido cantoFalta = jugada.Where(canto => canto.Tipo == TipoEnvido.FaltaEnvido).First();
                puntosJugada = cantoFalta.Puntos;
            }else
            {
                puntosJugada = jugada.Sum(canto => canto.Puntos);

            }
        }

    }
}