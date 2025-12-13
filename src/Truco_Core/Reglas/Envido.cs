namespace Truco.Core.Reglas
{
    public enum TipoEnvido
    {
        Envido,
        RealEnvido,
        FaltaEnvido,
    }
    public class CantoEnvido
    {
        private TipoEnvido tipo;
        private string jugador;
        public TipoEnvido Tipo => tipo;
        public string Jugador => jugador;

        public CantoEnvido(TipoEnvido tipo, string jugador, int puntos)
        {
            this.tipo = tipo;
            this.jugador = jugador;
        }
    }
}
    