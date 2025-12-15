using Truco.Core.Reglas;
namespace Truco.App.Acciones
{
    public static class CantarTruco
    {
        public static void Ejecutar(Partida partida, string nombreJugador, TipoTruco tipo)
        { 
            if (partida.ManoActual == null) 
                throw new Exception("No hay mano en juego");
            if (partida.TurnoActual.Nombre != nombreJugador) 
                throw new Exception("No es el turno del jugador");

            var ultimoCanto = partida.ManoActual.SecuenciaTruco.Last();

            if (ultimoCanto.Jugador == nombreJugador) 
                throw new Exception("No podes subir tu propio canto");

            if (ultimoCanto != null)
            {
                if (ultimoCanto.Tipo == TipoTruco.Truco && tipo != TipoTruco.Retruco)
                    throw new Exception("Solo se puede cantar retruco");
                if (ultimoCanto.Tipo == TipoTruco.Retruco && tipo != TipoTruco.ValeCuatro)
                    throw new Exception("Solo se puede cantar vale cuatro");
                if (ultimoCanto.Tipo == TipoTruco.ValeCuatro)
                    throw new Exception("No se puede retrucar más");
            }else{
                if (tipo != TipoTruco.Truco) throw new Exception("El primer canto debe ser truco");
            }
            partida.ManoActual.AgregarCanto(tipo,nombreJugador);
            partida.CambiarTurno();
        }
    }
}