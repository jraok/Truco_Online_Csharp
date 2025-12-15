using Truco.Core.Juego;
using Truco.Core.Modelos;
using Truco.Core.Reglas;

namespace Truco.App.Acciones
{
    public static class CantarEnvido
    {
        public static void Ejecutar(Partida partida, string nombreJugador, TipoEnvido tipo){
            if (partida.ManoActual == null) throw new InvalidOperationException("No hay mano en juego");
            if (partida.TurnoActual.Nombre != nombreJugador) throw new InvalidOperationException("No es el turno del jugador");
            if (partida.ManoActual.RondaActual.Turnos.Count > 1) throw new InvalidOperationException("Ya no se puede cantar envido");
            partida.ManoActual.AgregarCanto(tipo,nombreJugador);
        }
    }
}
