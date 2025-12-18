using Truco.Core.Reglas;
namespace Truco.App.Acciones
{
    public static class ResponderFlor
    {
        public static void Ejecutar(Partida partida, string nombreJugador, bool acepta)
        {
            if (partida.ManoActual == null)
                throw new InvalidOperationException("No hay mano en juego");
            if (nombreJugador != partida.TurnoActual.Nombre) 
                throw new InvalidOperationException($"No es el turno de {nombreJugador}");
            if (partida.ManoActual.SecuenciaFlor.Count == 0) 
                throw new InvalidOperationException("No hay flor para responder");
                
            var ultimoCanto = partida.ManoActual.SecuenciaFlor.Last();
            if (ultimoCanto.Jugador == nombreJugador) 
                throw new InvalidOperationException("No puedes contestar tu canto");

            var quienResponde = (nombreJugador == partida.Jugador1.Nombre) 
                ? partida.Jugador1 
                : partida.Jugador2;

            if (Operador.CalcularFlor(quienResponde.Cartas) == 0)
                throw new InvalidOperationException("No tienes flor para aceptar");

            var resto = Operador.CalcularResto(partida.Jugador1, partida.Jugador2, partida.PuntosPartida);

            if (acepta){
                var puntosFlor = Operador.SumaDeFlor(partida.ManoActual.SecuenciaFlor, resto);

                var florJ1 = Operador.CalcularFlor(partida.Jugador1.Cartas);
                var florJ2 = Operador.CalcularFlor(partida.Jugador2.Cartas);
                if (florJ1 > florJ2){
                    partida.Jugador1.SumarPuntos(puntosFlor);
                }else if (florJ2 > florJ1){
                    partida.Jugador2.SumarPuntos(puntosFlor);
                }else{
                    partida.ManoActual.JugadorMano.SumarPuntos(puntosFlor);
                }
            }else{
                var cantosAnteriores = partida.ManoActual.SecuenciaFlor.Take(partida.ManoActual.SecuenciaFlor.Count - 1).ToList();
                var puntosFlor = Operador.SumaDeFlor(cantosAnteriores, 0);
                var ganador = (ultimoCanto.Jugador == partida.Jugador1.Nombre) 
                    ? partida.Jugador1 
                    : partida.Jugador2;
                ganador.SumarPuntos(puntosFlor);
            }
        }
    }
}
