using System.Text.Json.Serialization;
using EspacioRonda;
using EspacioEnvido;
using EspacioTruco;
using EspacioFlor;
using EspacioJugador;

namespace EspacioMano{
    public class Mano{
        private const int Puntos = 30;
        private List<Ronda> rondas = new();
        private List<CantoEnvido> secuenciaEnvido = new();
        private List<CantoTruco> secuenciaTruco = new();
        private List<CantoFlor> secuenciaFlor = new();
        private Jugador jugador1;
        private Jugador jugador2;
        public List<Ronda> Rondas => rondas;
        public List<CantoEnvido> SecuenciaEnvido => secuenciaEnvido;
        public List<CantoTruco> SecuenciaTruco => secuenciaTruco;
        public List<CantoFlor> SecuenciaFlor => secuenciaFlor;
        public int PuntosEnvido => ClrPuntosEnvido();
        public int PuntosTruco => ClrPuntosTruco();
        public int PuntosFlor => ClrPuntosFlor();
        private int PuntosPartida => CalcularResto();
        [JsonConstructor]
        public Mano(Jugador jugadorMano, Jugador jugadorPie){
            this.jugador1 = jugadorMano;
            this.jugador2 = jugadorPie;
        }
        public void AgregarRonda(Ronda ronda){
            rondas.Add(ronda);
        }
        public void AgregarCanto(TipoEnvido envido, string jugador){
            secuenciaEnvido.Add(new CantoEnvido(envido,jugador,PuntosPartida));
        }
        public void AgregarCanto(TipoTruco truco, string jugador){
            secuenciaTruco.Add(new CantoTruco(truco,jugador));
        }
        public void AgregarCanto(TipoFlor flor,string jugador){
            secuenciaFlor.Add(new CantoFlor(flor,jugador,Puntos));
        }
        private int CalcularResto(){
            if (jugador1.Puntaje > jugador2.Puntaje)
            {
                return (Puntos - jugador1.Puntaje);
            }else{
                return (Puntos - jugador2.Puntaje);
            }
        }
        private int ClrPuntosEnvido(){
            if (secuenciaEnvido.Any(canto => canto.Tipo == TipoEnvido.FaltaEnvido))
            {
                return PuntosPartida;
            }else{
                return secuenciaEnvido.Sum(canto => canto.Puntos);
            }
        }
        private int ClrPuntosTruco(){
            return secuenciaTruco.Count > 0 ? secuenciaTruco.Last().Puntos : 0;
        }
        private int ClrPuntosFlor(){
            return secuenciaFlor.Count > 0 ? secuenciaFlor.Last().Puntos : 0;
        }

    }
}