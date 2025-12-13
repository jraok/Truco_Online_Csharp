namespace Truco.Core.Modelos
{
    public class Mazo{
        private readonly Random rnd = new Random();
        public List<Carta> Naipes { get; private set; } = new List<Carta>();

        public Mazo(){
            Inicializar();
        }
        public void Inicializar(){
            Naipes.Clear();
            foreach (var palo in Enum.GetValues<Palos>())
            {
                for (int j = 1; j < 13; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        Naipes.Add(new Carta(palo, j));
                    }
                }
            }
        }
        public void Barajar()
        {
            Inicializar();
            int n = Naipes.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (Naipes[n], Naipes[k]) = (Naipes[k], Naipes[n]);
            }
        }

        public List<Carta> Repartir(int cantidad){
            List<Carta> mano = Naipes.Take(cantidad).ToList();
            Naipes.RemoveRange(0,cantidad);
            return mano;
        }
        
    }
}