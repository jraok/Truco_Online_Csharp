namespace EspacioCarta
{
    public enum Palos{
        Oro,
        Copa,
        Espada,
        Basto
    }
    public class Carta
    {
        private string nombre;
        private Palos palo;
        private int numero;
        private int jerarquiaTruco;

        public string Nombre => nombre;
        public Palos Palo => palo;
        public int Numero => numero;
        public int JerarquiaTruco => jerarquiaTruco;

        public Carta(Palos palo, int numero){
            this.palo = palo;
            this.numero = numero;
            string NuevoNombre = numero switch
            {
                10 => "Sota de",
                11 => "Caballo de",
                12 => "Rey de",
                _ => numero.ToString() + " de"
            };
            nombre = $"{NuevoNombre} {palo}";
            jerarquiaTruco = CalcularJerarquia(palo, numero);
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