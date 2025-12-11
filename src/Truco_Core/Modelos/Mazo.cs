using EspacioCarta;
namespace EspacioMazo
{
    public class Mazo{
        private static readonly Random rnd = new Random();
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
        public void Barajar(){
            Naipes = Naipes.OrderBy(x => rnd.Next()).ToList();
        }

        public List<Carta> Repartir(int cantidad){
            List<Carta> mano = Naipes.Take(cantidad).ToList();
            Naipes.RemoveRange(0,cantidad);
            return mano;
        }
        
    }
}