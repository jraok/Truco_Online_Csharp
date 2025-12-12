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
        public int JerarquiaTruco => CalcularJerarquia(palo, numero);

        public Carta(Palos palo, int numero){
            this.palo = palo;
            this.numero = numero;
        }
        private int CalcularJerarquia(Palos palo, int numero){
            if (palo == Palos.Espada && numero == 1) return 14;
            if (palo == Palos.Basto && numero == 1) return 13;
            if (palo == Palos.Espada && numero == 7) return 12;
            if (palo == Palos.Oro && numero == 7) return 11;
            return numero switch
            {
                3 => 10,
                2 => 9,
                1 => 8,
                12 => 7,
                11 => 6,
                10 => 5,
                7 => 4,
                6 => 3,
                5 => 2,
                4 => 1,
                _ => 0
            };
        }
    }
}