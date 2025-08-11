using EspacioCarta;
namespace EspacioMazo
{
    public class Mazo{
        private List<Carta> cartas = new List<Carta>();
        public Mazo(){
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        cartas.Add(new Carta((palos)i, j+1));
                    }
                }
            }
        }
    }
}