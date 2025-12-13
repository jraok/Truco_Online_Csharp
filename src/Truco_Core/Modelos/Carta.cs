namespace Truco.Core.Modelos
{
    public enum Palos{
        Oro,
        Copa,
        Espada,
        Basto
    }
    public record Carta(Palos Palo, int Numero)
    {
        public string Nombre => Numero switch
            {
            10 => $"Sota de {Palo}",
            11 => $"Caballo de {Palo}",
            12 => $"Rey de {Palo}",
            _  => $"{Numero} de {Palo}"
        };
    }
}