using Truco.Core.Reglas;
namespace Truco.App.Acciones
{
    public static class CantarTruco
    {
        public static void Ejecutar(Partida partida, string nombreJugador, TipoTruco tipo)
        { 
            if (partida.ManoActual == null) 
                throw new InvalidOperationException("No hay mano en juego");
            if (partida.TurnoActual.Nombre != nombreJugador) 
                throw new InvalidOperationException("No es el turno del jugador");

            var ultimoCanto = partida.ManoActual.SecuenciaTruco.LastOrDefault();;

            if (ultimoCanto != null)
            {
                if (ultimoCanto.Jugador == nombreJugador) 
                    throw new InvalidOperationException("No podes subir tu propio canto");
                if (ultimoCanto.Tipo == TipoTruco.Truco && tipo != TipoTruco.Retruco)
                    throw new InvalidOperationException("Solo se puede cantar retruco");
                if (ultimoCanto.Tipo == TipoTruco.Retruco && tipo != TipoTruco.ValeCuatro)
                    throw new InvalidOperationException("Solo se puede cantar vale cuatro");
                if (ultimoCanto.Tipo == TipoTruco.ValeCuatro)
                    throw new InvalidOperationException("No se puede retrucar más");
            }else{
                if (tipo != TipoTruco.Truco) throw new InvalidOperationException("El primer canto debe ser truco");
            }
            partida.ManoActual.AgregarCanto(tipo,nombreJugador);
            partida.CambiarTurno();
        }
    }
}