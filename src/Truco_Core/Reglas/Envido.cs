namespace Truco.Core.Reglas
{
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
}