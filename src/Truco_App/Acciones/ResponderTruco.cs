using Truco.Core.Juego;
using Truco.Core.Modelos;
using Truco.Core.Reglas;

namespace Truco.App.Acciones
{
    public static class ResponderTruco
    {
        public static void Ejecutar(Partida partida, string nombreJugador, bool acepta)
        {
            if (partida.ManoActual == null) 
                throw new InvalidOperationException("No hay mano en juego");
            if (partida.ManoActual.SecuenciaTruco.Count == 0) 
                throw new InvalidOperationException("No hay truco para jugar");
            
            var ultimoCanto = partida.ManoActual.SecuenciaTruco.Last();
            if (ultimoCanto.Jugador == nombreJugador) 
                throw new InvalidOperationException("No puedes contestar tu canto");
            if (!acepta)
            {
                var cantosAnteriores = partida.ManoActual.SecuenciaTruco.Take(partida.ManoActual.SecuenciaTruco.Count -1).ToList();
                var puntosEnJuego = Operador.SumaDeTruco(cantosAnteriores);
                var ganador = (ultimoCanto.Jugador == partida.Jugador1.Nombre) 
                ? partida.Jugador1 
                : partida.Jugador2;
                ganador.SumarPuntos(puntosEnJuego);
            
                partida.ManoActual.FinalizarMano();
            }else{
                partida.CambiarTurno();
            }
        }
    }
}