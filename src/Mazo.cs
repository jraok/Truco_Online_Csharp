using EspacioCarta;
namespace EspacioMazo
{
    public class Mazo{
        private List<Carta> cartas = new List<Carta>();
        public Mazo(){
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        cartas.Add(new Carta((Palos)i, j));
                    }
                }
            }
        }
    }
}