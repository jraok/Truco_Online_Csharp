using Truco.Core.Reglas;

namespace Truco.App.Acciones
{
    public static class ResponderEnvido
    {
        public static void Ejecutar(Partida partida, string nombreJugador, bool acepta)
        {
            if (partida.ManoActual.SecuenciaEnvido.Count == 0) 
                throw new InvalidOperationException("No hay envido para jugar");
            var ultimoCanto = partida.ManoActual.SecuenciaEnvido.Last();
            if (ultimoCanto.Jugador == nombreJugador) 
                throw new InvalidOperationException("No puedes contestar tu canto");

            var resto = Operador.CalcularResto(partida.Jugador1, partida.Jugador2, partida.PuntosPartida);
            var puntosEnJuego = Operador.SumaDeEnvido(partida.ManoActual.SecuenciaEnvido, resto);
            
            if (acepta)
            {
                var tantosJ1 = Operador.CalcularEnvido(partida.Jugador1.Cartas);
                var tantosJ2 = Operador.CalcularEnvido(partida.Jugador2.Cartas);

                if (tantosJ1 > tantosJ2)
                {
                    partida.Jugador1.SumarPuntos(puntosEnJuego);
                }else if (tantosJ2 > tantosJ1) {
                    partida.Jugador2.SumarPuntos(puntosEnJuego);
                }else{
                    partida.ManoActual.JugadorMano.SumarPuntos(puntosEnJuego);
                }
            }else
            {
                var cantosAnteriores = partida.ManoActual.SecuenciaEnvido.Take(partida.ManoActual.SecuenciaEnvido.Count - 1).ToList();
                puntosEnJuego = Operador.SumaDeEnvido(cantosAnteriores, 0);

                if (ultimoCanto.Jugador == partida.Jugador1.Nombre)
                {
                    partida.Jugador1.SumarPuntos(puntosEnJuego);
                }else{
                    partida.Jugador2.SumarPuntos(puntosEnJuego);
                }
            }
        }
    }
}