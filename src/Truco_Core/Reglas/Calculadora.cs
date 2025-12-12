using Truco.Core.Modelos;
namespace Truco.Core.Reglas
{
    public static class Calculadora{
        private const int BonusEnvido = 20;
        private static int ValorEnvido(Carta carta){
            if (carta.Numero >= 10) return 0;
            else return carta.Numero;
        }
        private static int CalcularEnvido(IReadOnlyList<Carta> cartas){
            int puntos = 0;
            for (int i = 0; i < cartas.Count; i++)
            {
                for (int j = i+1 ; j < cartas.Count; j++)
                {
                    if (cartas[i].Palo == cartas[j].Palo)
                    {
                        int suma = ValorEnvido(cartas[i]) + ValorEnvido(cartas[j]) + BonusEnvido;
                        if (suma > puntos) puntos = suma;
                    }
                }
            }
            if (puntos == 0) puntos = cartas.Max(carta => ValorEnvido(carta));
            return puntos;
        }
        private static int CalcularFlor(IReadOnlyList<Carta> cartas){
            if (cartas[0].Palo == cartas[1].Palo && cartas[1].Palo == cartas[2].Palo)
            return (cartas.Sum(carta => ValorEnvido(carta)) + BonusEnvido);
            else return 0;
        }
    }
}