using Truco.Core.Modelos;
namespace Truco.Core.Reglas
{
    public static class CalculadoraTruco{
        private const int BonusEnvido = 20;
        private static int ValorEnvido(Carta carta){
            if (carta.Numero >= 10) return 0;
            else return carta.Numero;
        }
        public static int CalcularEnvido(IReadOnlyList<Carta> cartas){
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
        public static int CalcularFlor(IReadOnlyList<Carta> cartas){
            if (cartas[0].Palo == cartas[1].Palo && cartas[1].Palo == cartas[2].Palo)
            return (cartas.Sum(carta => ValorEnvido(carta)) + BonusEnvido);
            else return 0;
        }
        public static int JerarquiaCarta(Carta carta){
            if (carta.Palo == Palos.Espada && carta.Numero == 1) return 14;
            if (carta.Palo == Palos.Basto && carta.Numero == 1) return 13;
            if (carta.Palo == Palos.Espada && carta.Numero == 7) return 12;
            if (carta.Palo == Palos.Oro && carta.Numero == 7) return 11;
            return carta.Numero switch
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

        public static int CompararCartas(Carta c1, Carta c2){
            if (JerarquiaCarta(c1) > JerarquiaCarta(c2))
            {
                return 1;
            }else if(JerarquiaCarta(c1) < JerarquiaCarta(c2)){
                return -1;
            }else{
                return 0;
            }
        }
    }
}