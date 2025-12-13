using Truco.Core.Modelos;
namespace Truco.Core.Reglas
{
    public static class Operador
    {
        private const int BonusEnvido = 20;
        private const int PuntosPartida = 30;
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
        public static int CalcularResto(Jugador J1, Jugador J2){
            if (J1.Puntaje > J2.Puntaje)
            {
                return (PuntosPartida - J1.Puntaje);
            }else{
                return (PuntosPartida - J2.Puntaje);
            }
        }
        public static int CalcularEnvido(IReadOnlyList<Carta> cartas){
            if (cartas.Count != 3)
            {
                throw new InvalidOperationException("El envido solo se calcula con 3 cartas");
            }
            int puntos = 0;
            for (int i = 0; i < cartas.Count; i++)
            {
                for (int j = i+1 ; j < cartas.Count; j++)
                {
                    if (cartas[i].Palo == cartas[j].Palo)
                    {
                        int Valor = cartas[i].Numero >= 10 ? 0 : cartas[i].Numero;
                        int suma = Valor + cartas[j].Numero + BonusEnvido;
                        if (suma > puntos) puntos = suma;
                    }
                }
            }
            if (puntos == 0) puntos = cartas.Max(c => c.Numero >= 10 ? 0 : c.Numero);
            return puntos;
        }
        public static int CalcularFlor(IReadOnlyList<Carta> cartas){
            if (cartas[0].Palo == cartas[1].Palo && cartas[1].Palo == cartas[2].Palo)
            return (cartas.Sum(c => c.Numero >= 10 ? 0 : c.Numero) + BonusEnvido);
            else return 0;
        }
        public static int SumaDeEnvido(List<CantoEnvido> cantos, int resto){
            if (!cantos.Any()) return 0; 
            if (cantos.Last().Tipo == TipoEnvido.FaltaEnvido) return resto;
            return cantos.Sum(c => c.Tipo switch
            {
                TipoEnvido.Envido => 2,
                TipoEnvido.RealEnvido => 3,
                _ => 0,
            });
        }
        public static int SumaDeTruco(List<CantoTruco> cantos){
            if (!cantos.Any()) return 1;
            return cantos.Last().Tipo switch
            {
                TipoTruco.Truco => 2,
                TipoTruco.Retruco => 3,
                TipoTruco.ValeCuatro => 4,
                _ => 1,
            };
        }
        public static int SumaDeFlor(List<CantoFlor> cantos, int resto){
            if (!cantos.Any()) return 0;
            return cantos.Last().Tipo switch
            {
                TipoFlor.Flor => 3,
                TipoFlor.ContraFlor => 6,
                TipoFlor.ContraFlorResto => resto,
                _ => 0,
            };
        }
    }
}