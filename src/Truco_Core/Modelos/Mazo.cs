namespace Truco.Core.Modelos
{
    public class Mazo{
        private readonly Random rnd = new Random();
        public List<Carta> Naipes { get; private set; } = new List<Carta>();

        public Mazo(){
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        Naipes.Add(new Carta((Palos)i, j));
                    }
                }
            }
        }
        public void Barajar()
        {
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