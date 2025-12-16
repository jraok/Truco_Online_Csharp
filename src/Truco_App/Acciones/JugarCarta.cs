using Truco.Core.Modelos;
using Truco.Core.Juego;
using Truco.Core.Reglas;

namespace Truco.App.Acciones
{
    public static class JugarCarta
    {
        private static void ResolverGanadorRonda(Partida partida)
        {
            string NombreGanador = partida.ManoActual!.RondaActual!.GanadorRonda;
            if (NombreGanador != "Empate")
            {
                partida.AsignarTurno((NombreGanador == partida.Jugador1.Nombre) 
                ? partida.Jugador1 
                : partida.Jugador2);
            }else{
                partida.AsignarTurno(partida.ManoActual!.RondaActual!.Turnos[0].Jugador == partida.Jugador1.Nombre
                ? partida.Jugador1
                : partida.Jugador2);
            }
        }
        public static void Ejecutar(Partida partida, string nombreJugador, int indiceCarta)
        {
            if (partida.ManoActual == null)throw new InvalidOperationException("No hay mano en juego");
            if (nombreJugador != partida.TurnoActual.Nombre) throw new InvalidOperationException($"No es el turno de {nombreJugador}");

            var carta = partida.TurnoActual.TirarCarta(indiceCarta);
            partida.ManoActual.RondaActual!.AgregarTurno(new Turno(partida.TurnoActual.Nombre, carta));
            if (partida.ManoActual.RondaActual.RondaCompleta())
            {
                ResolverGanadorRonda(partida);
                partida.ManoActual.RegistrarGanadorRonda();

                if (partida.ManoActual.Finalizada) {
                    ResolverPuntosTruco(partida);
                }else{
                    partida.ManoActual.IniciarSiguienteRonda();
                }
            }else{
                partida.CambiarTurno();
            }
        }
        private static void ResolverPuntosTruco(Partida partida)
        {
            var nombreGanador = partida.ManoActual.GanadorMano();
            if (nombreGanador == null) return;
            var puntosTruco = Operador.SumaDeTruco(partida.ManoActual.SecuenciaTruco);
            
            var ganador = (nombreGanador == partida.Jugador1.Nombre) 
            ? partida.Jugador1 
            : partida.Jugador2;

            ganador.SumarPuntos(puntosTruco);
        }
    }
}