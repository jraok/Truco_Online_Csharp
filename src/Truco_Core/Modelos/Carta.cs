namespace Truco.Core.Modelos
{
    public enum Palos{
        Oro,
        Copa,
        Espada,
        Basto
    }
    public class Carta
    {
        private Palos palo;
        private int numero;
        public string Nombre => numero switch
            {
            10 => $"Sota de {palo}",
            11 => $"Caballo de {palo}",
            12 => $"Rey de {palo}",
            _  => $"{numero} de {palo}"
        };
        public Palos Palo => palo;
        public int Numero => numero;

        public Carta(Palos palo, int numero){
            this.palo = palo;
            this.numero = numero;
        }
    }
}