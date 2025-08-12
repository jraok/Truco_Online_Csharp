using EspacioCarta;
namespace EspacioMazo
{
    public class Mazo{
        private List<Carta> naipes = new List<Carta>();
        public List<Carta> Naipes => naipes;
        public Mazo(){
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        naipes.Add(new Carta((Palos)i, j));
                    }
                }
            }
        }
        public void Barajar(){
            Random rnd = new Random();
            naipes = naipes.OrderBy(x => rnd.Next()).ToList();
        }

        public List<Carta> Repartir(int cantidad){
            List<Carta> mano = naipes.Take(cantidad).ToList();
            naipes.RemoveRange(0,cantidad);
            return mano;
        }
        
    }
}