using Truco.Core.Modelos;
using Truco.Core.Reglas;

namespace Truco.Core.Juego
{
    public static class Validador
    {
        public static void ValidarTurno(Jugador jugador, Jugador turnoActual)
        {
            if (jugador != turnoActual)
                throw new InvalidOperationException("No es tu turno");
        }
        public static void ValidarJugarCarta(EstadoMano estado)
        {
            if (estado != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede jugar carta en este momento");
        }
        public static void ValidarCantarTruco(EstadoMano estado)
        {
            if (estado != EstadoMano.EnJuego)
                throw new InvalidOperationException("No es momento de cantar truco");
        }
        public static void ValidarResponderTruco(EstadoMano estado)
        {
            if (estado != EstadoMano.EsperandoRespuestaTruco)
                throw new InvalidOperationException("No hay truco para responder");
        }
        public static void ValidarCantarEnvido(EstadoMano estado, bool rondaCompleta)
        {
            bool estadoValido = estado == EstadoMano.EnJuego
                || estado == EstadoMano.EsperandoRespuestaEnvido
                || (estado == EstadoMano.EsperandoRespuestaTruco && !rondaCompleta);

            if (!estadoValido)
                throw new InvalidOperationException("No se puede cantar envido ahora");

            if (rondaCompleta)
                throw new InvalidOperationException("Ya no se puede cantar envido");
        }
        public static void ValidarResponderEnvido(EstadoMano estado)
        {
            if (estado != EstadoMano.EsperandoRespuestaEnvido)
                throw new InvalidOperationException("No hay envido para responder");
        }
        public static void ValidarCantarFlor(EstadoMano estado)
        {
            if (estado != EstadoMano.EnJuego && estado != EstadoMano.EsperandoRespuestaEnvido)
                throw new InvalidOperationException("No es momento de cantar flor");
        }
        public static void ValidarResponderFlor(EstadoMano estado)
        {
            if (estado != EstadoMano.EsperandoRespuestaFlor)
                throw new InvalidOperationException("No hay flor para responder");
        }
        public static void ValidarIrAlMazo(EstadoMano estado)
        {
            if (estado != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede ir al mazo ahora");
        }
        public static void ValidarSecuenciaTruco(TipoTruco tipo, CantoTruco ultimoCanto, string nombreJugador)
        {
            if (ultimoCanto != null)
            {
                if (ultimoCanto.Jugador == nombreJugador)
                    throw new InvalidOperationException("No podes subir tu propio canto");

                if (ultimoCanto.Tipo == TipoTruco.Truco && tipo != TipoTruco.Retruco)
                    throw new InvalidOperationException("Solo se puede cantar retruco");

                if (ultimoCanto.Tipo == TipoTruco.Retruco && tipo != TipoTruco.ValeCuatro)
                    throw new InvalidOperationException("Solo se puede cantar vale cuatro");
            }
            else if (tipo != TipoTruco.Truco)
            {
                throw new InvalidOperationException("El primer canto debe ser truco");
            }
        }
        public static void ValidarSecuenciaEnvido(TipoEnvido tipo, CantoEnvido ultimoCanto, string nombreJugador)
        {
            if (ultimoCanto != null && ultimoCanto.Jugador == nombreJugador)
                throw new InvalidOperationException("No podés responder tu propio envido");

            if (!EnvidoValido(tipo, ultimoCanto?.Tipo))
                throw new InvalidOperationException("Canto inválido");
        }
        public static void ValidarSecuenciaFlor(TipoFlor tipo, CantoFlor ultimoCanto, string nombreJugador)
        {
            if (ultimoCanto != null && ultimoCanto.Jugador == nombreJugador)
                throw new InvalidOperationException("No podés responder tu propia flor");

            if (!FlorValida(tipo, ultimoCanto?.Tipo))
                throw new InvalidOperationException("Canto de flor inválido");
        }
        public static void ValidarTieneFlor(Jugador jugador)
        {
            if (Operador.CalcularFlor(jugador.Cartas) == 0)
                throw new InvalidOperationException("No tenés flor");
        }
        private static bool EnvidoValido(TipoEnvido nuevo, TipoEnvido? anterior)
        {
            if (anterior == null)
                return true;

            return anterior switch
            {
                TipoEnvido.Envido => nuevo == TipoEnvido.Envido 
                    || nuevo == TipoEnvido.RealEnvido 
                    || nuevo == TipoEnvido.FaltaEnvido,
                TipoEnvido.RealEnvido => nuevo == TipoEnvido.FaltaEnvido,
                TipoEnvido.FaltaEnvido => false,
                _ => false
            };
        }
        private static bool FlorValida(TipoFlor nuevo, TipoFlor? anterior)
        {
            if (anterior == null)
                return nuevo == TipoFlor.Flor;

            return anterior switch
            {
                TipoFlor.Flor => nuevo == TipoFlor.ContraFlor,
                TipoFlor.ContraFlor => nuevo == TipoFlor.ContraFlorResto,
                _ => false
            };
        }
    }

    public enum EstadoMano
    {
        EsperandoMano,
        EnJuego,
        EsperandoRespuestaTruco,
        EsperandoRespuestaEnvido,
        EsperandoRespuestaFlor
    }
}