using Truco.Core.Reglas;

namespace Truco.App.Acciones
{
    public static class CantarFlor
    {
        public static void Ejecutar(Partida partida, string nombreJugador, TipoFlor tipo)
        {
            if (partida.ManoActual == null)
                throw new InvalidOperationException("No hay mano en juego");
            if (partida.TurnoActual.Nombre != nombreJugador)
                throw new InvalidOperationException($"No es el turno de {nombreJugador}");
            if (partida.ManoActual.Rondas.Count > 1)
                throw new InvalidOperationException("Ya no se puede cantar flor después de la primera ronda");
            if (partida.ManoActual.RondaActual.RondaCompleta()) 
                throw new InvalidOperationException("Ya no se puede cantar flor");

            var cantor = (nombreJugador == partida.Jugador1.Nombre)
            ? partida.Jugador1
            : partida.Jugador2;

            if (Operador.CalcularFlor(cantor.Cartas) == 0) 
                throw new InvalidOperationException("No tenes flor para cantar");

            var ultimoCanto = partida.ManoActual.SecuenciaFlor.LastOrDefault();
            if (ultimoCanto != null){
                if (ultimoCanto.Jugador == nombreJugador) 
                    throw new InvalidOperationException("No podes subir tu propio canto");
                if (ultimoCanto.Tipo == TipoFlor.Flor && tipo != TipoFlor.ContraFlor)
                    throw new InvalidOperationException("Solo se puede cantar contra flor");
                if (ultimoCanto.Tipo == TipoFlor.ContraFlor && tipo != TipoFlor.ContraFlorResto)
                    throw new InvalidOperationException("Solo se puede cantar contra flor al resto");
            }else{
                if (tipo != TipoFlor.Flor)
                    throw new InvalidOperationException("El primer canto debe ser flor");
            }
            partida.ManoActual.AgregarCanto(tipo,nombreJugador);
            partida.CambiarTurno();
        }
    }
}
