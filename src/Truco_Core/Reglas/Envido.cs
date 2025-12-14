namespace Truco.Core.Reglas
{
    public enum TipoEnvido
    {
        Envido,
        RealEnvido,
        FaltaEnvido,
    }
    public class CantoEnvido(TipoEnvido tipo, string jugador)
    {
        public TipoEnvido Tipo { get; private set; } = tipo;
        public string Jugador { get; private set; } = jugador;
    }
}
    