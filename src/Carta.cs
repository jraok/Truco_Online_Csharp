namespace EspacioCarta
{
    public enum palos{
        Oro,
        Copa,
        Espada,
        Basto
    }
    public class Carta
    {
        private string nombre;
        private palos palo;
        private int numero;

        public string Nombre => nombre;
        public palos Palo => palo;
        public int Numero => numero;

        public Carta(palos palo, int numero){
            this.palo = palo;
            this.numero = numero;
            string NuevoNombre = numero switch
            {
                10 => "Sota de",
                11 => "Caballo de",
                12 => "Rey de",
                _ => numero.ToString() + " de"
            };
            nombre = $"{NuevoNombre} de {palo}";
        }
    }
}