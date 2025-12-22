namespace Truco.Core.Reglas
{
    public enum TipoFlor
    {
        Flor,
        ContraFlor,
        ContraFlorResto,
    }
    public class CantoFlor(TipoFlor tipo, string jugador)
    {
        public TipoFlor Tipo { get; private set; } = tipo;
        public string Jugador { get; private set; } = jugador;
    }
}