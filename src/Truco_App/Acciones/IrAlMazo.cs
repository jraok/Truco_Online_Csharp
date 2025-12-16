using Truco.Core.Reglas;
namespace Truco.App.Acciones
{
    public static class IrAlMazo
    {
        public static void Ejecutar(Partida partida, string nombreJugador)
        {
            if (partida.ManoActual == null)
                throw new InvalidOperationException("No hay mano en juego");
            if (partida.TurnoActual.Nombre != nombreJugador)
                throw new InvalidOperationException($"No es el turno de {nombreJugador}");

            var puntosTruco = Operador.SumaDeTruco(partida.ManoActual.SecuenciaTruco);
            var ganador = (nombreJugador == partida.Jugador1.Nombre)
                ? partida.Jugador2
                : partida.Jugador1;
            ganador.SumarPuntos(puntosTruco);
            partida.ManoActual.FinalizarMano();
        }
    }
}
