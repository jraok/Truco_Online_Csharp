namespace Truco.Core.Reglas
{
    public enum TipoFlor
    {
        Flor,
        ContraFlor,
        ContraFlorResto,
    }
    public class CantoFlor
    {
        private TipoFlor tipo;
        private string jugador;
        public TipoFlor Tipo => tipo;
        public string Jugador => jugador;

        public CantoFlor(TipoFlor tipo, string jugador)
        {
            this.tipo = tipo;
            this.jugador = jugador;
        }
    }
}
    
