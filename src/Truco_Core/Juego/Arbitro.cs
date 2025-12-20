using Truco.Core.Reglas;
using Truco.Core.Modelos;

namespace Truco.Core.Juego
{
    public class Arbitro(string nombreJ1, string nombreJ2)
    {
        private enum EstadoMano
        {
            EsperandoMano,
            EnJuego,
            EsperandoRespuestaTruco,
            EsperandoRespuestaEnvido,
            EsperandoRespuestaFlor,
            Terminada
        }
        private readonly Partida partida = new Partida(nombreJ1, nombreJ2);
        public Partida Partida => partida;
        private EstadoMano estadoMano = EstadoMano.EsperandoMano;

        public void IniciarMano()
        {
            if (estadoMano != EstadoMano.EsperandoMano)
                throw new InvalidOperationException("No es momento de iniciar una mano");
            
            var JMano = partida.JugadorMano;
            var JPie = partida.JugadorPie;

            var mazo = new Mazo();
            mazo.Barajar();
            partida.SumarMano();

            JMano.LimpiarCartas();
            JPie.LimpiarCartas();
            JMano.RecibirCartas(mazo.Repartir(3));
            JPie.RecibirCartas(mazo.Repartir(3));
            JMano.AsignarEnvido(Operador.CalcularEnvido(JMano.Cartas));
            JPie.AsignarEnvido(Operador.CalcularEnvido(JPie.Cartas));

            partida.AsignarMano(new Mano(JMano, JPie));
            partida.ManoActual.IniciarSiguienteRonda();
            estadoMano = EstadoMano.EnJuego;
            partida.AsignarTurno(JMano);
        }
        public void JugarCarta(string nombreJugador, int indiceCarta)
        {
            if (estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede jugar la carta en este momento");

            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");

            var carta = partida.TurnoActual.TirarCarta(indiceCarta);
            partida.ManoActual.RondaActual!.AgregarTurno(new Turno(nombreJugador, carta));

            if (partida.ManoActual.RondaActual.RondaCompleta())
            {
                ResolverGanadorRonda();
                partida.ManoActual.RegistrarGanadorRonda();

                if (partida.ManoActual.Finalizada){
                    ResolverPuntosTruco();
                    estadoMano = EstadoMano.EsperandoMano;
                }else{
                    partida.ManoActual.IniciarSiguienteRonda();
                }
            }else{
                partida.CambiarTurno();
            }
        }
        public void CantarTruco(string nombreJugador, TipoTruco tipo)
        {
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");
            if (estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No es momento de cantar truco");

            var ultimoCanto = partida.ManoActual!.SecuenciaTruco.LastOrDefault();

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
                throw new InvalidOperationException("El primer canto debe ser truco");

            partida.ManoActual!.AgregarCanto(tipo, nombreJugador);
            partida.CambiarTurno();
            estadoMano = EstadoMano.EsperandoRespuestaTruco;
        }
        public void ResponderTruco(string nombreJugador, bool acepta)
        {
            if (estadoMano != EstadoMano.EsperandoRespuestaTruco) 
                throw new InvalidOperationException("No hay truco para responder");
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");
            var ultimoCanto = partida.ManoActual.SecuenciaTruco.Last();
            
            if (!acepta)
            {
                var cantosAnteriores = partida.ManoActual.SecuenciaTruco.Take(partida.ManoActual.SecuenciaTruco.Count -1).ToList();
                var puntosEnJuego = Operador.SumaDeTruco(cantosAnteriores);
                var ganador = (ultimoCanto.Jugador == partida.Jugador1.Nombre) 
                ? partida.Jugador1 
                : partida.Jugador2;
                ganador.SumarPuntos(puntosEnJuego);

                partida.ManoActual.FinalizarMano();
                estadoMano = EstadoMano.EsperandoMano;
            }else{
                partida.CambiarTurno();
                estadoMano = EstadoMano.EnJuego;
            }
        }
        public void CantarEnvido(string nombreJugador, TipoEnvido tipo)
        {
            if (estadoMano != EstadoMano.EsperandoRespuestaEnvido && estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede cantar envido ahora");
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");
            
            var ultimo = partida.ManoActual.SecuenciaEnvido.LastOrDefault();
            if (ultimo != null && ultimo.Jugador == nombreJugador)
                throw new InvalidOperationException("No podés responder tu propio envido");

            if (partida.ManoActual!.RondaActual!.RondaCompleta())
                throw new InvalidOperationException("Ya no se puede cantar envido");

            if (!EnvidoValido(tipo, ultimo?.Tipo))
                throw new InvalidOperationException("Canto invalido");

            partida.ManoActual.AgregarCanto(tipo, nombreJugador);
            partida.CambiarTurno();
            estadoMano = EstadoMano.EsperandoRespuestaEnvido;
        }
        public void ResponderEnvido(string nombreJugador, bool acepta)
        {
            if (estadoMano != EstadoMano.EsperandoRespuestaEnvido)
                throw new InvalidOperationException("No hay envido para responder");
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");

            var ultimoCanto = partida.ManoActual!.SecuenciaEnvido.Last();

            var resto = Operador.CalcularResto(partida.Jugador1, partida.Jugador2, partida.PuntosPartida);

            if (acepta)
            {
                var puntos = Operador.SumaDeEnvido(partida.ManoActual.SecuenciaEnvido,resto);

                var tantosJ1 = partida.Jugador1.PuntosEnvido;
                var tantosJ2 = partida.Jugador2.PuntosEnvido;

                if (tantosJ1 > tantosJ2)
                    partida.Jugador1.SumarPuntos(puntos);
                else if (tantosJ2 > tantosJ1)
                    partida.Jugador2.SumarPuntos(puntos);
                else
                    partida.ManoActual.JugadorMano.SumarPuntos(puntos);
            }else{
                var cantosPrevios = partida.ManoActual.SecuenciaEnvido
                    .Take(partida.ManoActual.SecuenciaEnvido.Count - 1)
                    .ToList();

                var puntos = Operador.SumaDeEnvido(cantosPrevios, 0);
                var ganador = ultimoCanto.Jugador == partida.Jugador1.Nombre
                    ? partida.Jugador1
                    : partida.Jugador2;

                ganador.SumarPuntos(puntos);
            }
            partida.CambiarTurno();
            estadoMano = EstadoMano.EnJuego;
        }
        public void CantarFlor(string nombreJugador, TipoFlor tipo)
        {
            if (estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No es momento de cantar flor");
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");

            var ultimo = partida.ManoActual.SecuenciaFlor.LastOrDefault();
            if (!PuedeCantarFlor(nombreJugador, tipo) || !FlorValida(tipo, ultimo?.Tipo))
                throw new InvalidOperationException("Canto de flor inválido");

            partida.ManoActual!.AgregarCanto(tipo, nombreJugador);
            partida.CambiarTurno();
            estadoMano = EstadoMano.EsperandoRespuestaFlor;
        }
        public void ResponderFlor(string nombreJugador, bool acepta)
        {
            if (estadoMano != EstadoMano.EsperandoRespuestaFlor)
                throw new InvalidOperationException("No hay envido para responder");
            
            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");

            var ultimoCanto = partida.ManoActual!.SecuenciaFlor.Last();
            var resto = Operador.CalcularResto(partida.Jugador1, partida.Jugador2, partida.PuntosPartida);
            if (acepta)
            {
                var puntos = Operador.SumaDeFlor(partida.ManoActual.SecuenciaFlor, resto);

                var florJ1 = Operador.CalcularFlor(partida.Jugador1.Cartas);
                var florJ2 = Operador.CalcularFlor(partida.Jugador2.Cartas);

                if (florJ1 > florJ2)
                    partida.Jugador1.SumarPuntos(puntos);
                else if (florJ2 > florJ1)
                    partida.Jugador2.SumarPuntos(puntos);
                else
                    partida.ManoActual.JugadorMano.SumarPuntos(puntos);
            }else{
                var cantosPrevios = partida.ManoActual.SecuenciaFlor
                    .Take(partida.ManoActual.SecuenciaFlor.Count - 1)
                    .ToList();

                var puntos = Operador.SumaDeFlor(cantosPrevios, 0);

                var ganador = ultimoCanto.Jugador == partida.Jugador1.Nombre
                    ? partida.Jugador1
                    : partida.Jugador2;

                ganador.SumarPuntos(puntos);
            }
            partida.CambiarTurno();
            estadoMano = EstadoMano.EnJuego;
        }
        public void IrAlMazo(string nombreJugador)
        {
            if (estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede ir al mazo ahora");

            if (nombreJugador != partida.TurnoActual!.Nombre)
                throw new InvalidOperationException("No es tu turno");

            var puntos = Operador.SumaDeTruco(partida.ManoActual!.SecuenciaTruco);

            var ganador = nombreJugador == partida.Jugador1.Nombre
                ? partida.Jugador2
                : partida.Jugador1;

            ganador.SumarPuntos(puntos);
            partida.ManoActual.FinalizarMano();
            estadoMano = EstadoMano.EsperandoMano;
        }

        private void ResolverGanadorRonda()
        {
            var ronda = partida.ManoActual!.RondaActual!;
            var ganador = ronda.GanadorRonda;

            if (ganador != "Empate")
                partida.AsignarTurno(
                    ganador == partida.Jugador1.Nombre
                    ? partida.Jugador1
                    : partida.Jugador2
                );
            else
                partida.AsignarTurno(
                    ronda.Turnos[0].Jugador == partida.Jugador1.Nombre
                    ? partida.Jugador1
                    : partida.Jugador2
                );
        }
        private void ResolverPuntosTruco()
        {
            var nombreGanador = partida.ManoActual!.GanadorMano();
            if (nombreGanador == null) return;

            var puntos = Operador.SumaDeTruco(partida.ManoActual.SecuenciaTruco);

            var jugador = nombreGanador == partida.Jugador1.Nombre
                ? partida.Jugador1
                : partida.Jugador2;

            jugador.SumarPuntos(puntos);
        }
        private static bool EnvidoValido(TipoEnvido nuevo, TipoEnvido? anterior)
        {
            if (anterior == null)
                return true;

            return anterior switch
            {
                TipoEnvido.Envido => nuevo == TipoEnvido.Envido || nuevo == TipoEnvido.RealEnvido || nuevo == TipoEnvido.FaltaEnvido,
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
        private bool PuedeCantarFlor(string nombreJugador, TipoFlor tipo)
        {
            var mano = partida.ManoActual!;

            if (mano.Rondas.Count > 1 || mano.RondaActual!.RondaCompleta())
                return false;

            var jugador = nombreJugador == partida.Jugador1.Nombre
                ? partida.Jugador1
                : partida.Jugador2;

            if (Operador.CalcularFlor(jugador.Cartas) == 0)
                return false;

            var ultimo = mano.SecuenciaFlor.LastOrDefault();

            if (ultimo == null)
                return tipo == TipoFlor.Flor;

            if (ultimo.Jugador == nombreJugador)
                return false;

            return ultimo.Tipo switch
            {
                TipoFlor.Flor => tipo == TipoFlor.ContraFlor,
                TipoFlor.ContraFlor => tipo == TipoFlor.ContraFlorResto,
                _ => false
            };
        }
    }

}
