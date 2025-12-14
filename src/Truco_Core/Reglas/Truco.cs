namespace Truco.Core.Reglas
{
    public enum TipoTruco{
        Truco,
        Retruco,
        ValeCuatro,
    }
    public class CantoTruco(TipoTruco tipo, string jugador)
    {
        public TipoTruco Tipo { get; private set; } = tipo;
        public string Jugador { get; private set; } = jugador;
    }
}