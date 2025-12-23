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
            // EsperandoRespuestaFlor
        }
        private readonly Partida partida = new(nombreJ1, nombreJ2);
        public Partida Partida => partida;
        private EstadoMano estadoMano = EstadoMano.EsperandoMano;

        public void IniciarMano()
        {
            if (estadoMano != EstadoMano.EsperandoMano)
                throw new InvalidOperationException("No es momento de iniciar una mano");
            
            var mazo = new Mazo();
            mazo.Barajar();
            partida.SumarMano();

            var JMano = partida.JugadorMano;
            var JPie = partida.JugadorPie;

            JMano.LimpiarCartas();
            JPie.LimpiarCartas();
            JMano.RecibirCartas(mazo.Repartir(3));
            JPie.RecibirCartas(mazo.Repartir(3));

            partida.AsignarMano(new Mano(JMano, JPie));
            partida.ManoActual.IniciarSiguienteRonda();
            estadoMano = EstadoMano.EnJuego;
            partida.AsignarTurno(JMano);
        }
        public void JugarCarta(Jugador jugador, int indiceCarta)
        {
            if (!PuedeJugarCarta)
                throw new InvalidOperationException("No se puede jugar la carta en este momento");

            if (jugador != partida.TurnoActual)
                throw new InvalidOperationException("No es tu turno");

            var carta = jugador.TirarCarta(indiceCarta);
            partida.ManoActual.RondaActual!.AgregarTurno(new Turno(jugador.Nombre, carta));

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
        public void CantarTruco(Jugador jugador, TipoTruco tipo)
        {
            if (jugador != partida.TurnoActual)
                throw new InvalidOperationException("No es tu turno");
            if (!PuedeCantarTruco)
                throw new InvalidOperationException("No es momento de cantar truco");

            var ultimoCanto = partida.ManoActual!.SecuenciaTruco.LastOrDefault();

            if (ultimoCanto != null)
            {
                if (ultimoCanto.Jugador == jugador.Nombre)
                    throw new InvalidOperationException("No podes subir tu propio canto");

                if (ultimoCanto.Tipo == TipoTruco.Truco && tipo != TipoTruco.Retruco)
                    throw new InvalidOperationException("Solo se puede cantar retruco");

                if (ultimoCanto.Tipo == TipoTruco.Retruco && tipo != TipoTruco.ValeCuatro)
                    throw new InvalidOperationException("Solo se puede cantar vale cuatro");
            }
            else if (tipo != TipoTruco.Truco)
                throw new InvalidOperationException("El primer canto debe ser truco");

            partida.ManoActual!.AgregarCanto(tipo, jugador.Nombre);
            partida.CambiarTurno();
            estadoMano = EstadoMano.EsperandoRespuestaTruco;
        }
        public void ResponderTruco(Jugador jugador, bool acepta)
        {
            if (!PuedeResponderTruco) 
                throw new InvalidOperationException("No hay truco para responder");
            if (jugador != partida.TurnoActual)
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
        public void CantarEnvido(Jugador jugador, TipoEnvido tipo)
        {
            if (!PuedeCantarEnvido)
                throw new InvalidOperationException("No se puede cantar envido ahora");
            if (partida.ManoActual.SecuenciaEnvido.Where(c => c.Tipo == TipoEnvido.Envido).Count() >= 2 && tipo == TipoEnvido.Envido)
                throw new InvalidOperationException("El canto debe ser Real envido o Falta envido");
            if (jugador != partida.TurnoActual)
                throw new InvalidOperationException("No es tu turno");
            if (partida.ManoActual!.RondaActual!.RondaCompleta())
                throw new InvalidOperationException("Ya no se puede cantar envido");

            var ultimo = partida.ManoActual.SecuenciaEnvido.LastOrDefault();
            if (ultimo != null && ultimo.Jugador == jugador.Nombre)
                throw new InvalidOperationException("No podés responder tu propio envido");
            if (!EnvidoValido(tipo, ultimo?.Tipo))
                throw new InvalidOperationException("Canto invalido");

            if (estadoMano == EstadoMano.EsperandoRespuestaTruco)
                partida.ManoActual.SecuenciaTruco.Clear();

            partida.ManoActual.AgregarCanto(tipo, jugador.Nombre);
            partida.CambiarTurno();
            estadoMano = EstadoMano.EsperandoRespuestaEnvido;
        }
        public void ResponderEnvido(Jugador jugador, bool acepta)
        {
            if (!PuedeResponderEnvido)
                throw new InvalidOperationException("No hay envido para responder");
            if (jugador != partida.TurnoActual)
                throw new InvalidOperationException("No es tu turno");

            var ultimoCanto = partida.ManoActual.SecuenciaEnvido.LastOrDefault() 
                ?? throw new InvalidOperationException("No hay envido para responder (estado inconsistente)");
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
            partida.ManoActual.ResolverEnvido();
            partida.CambiarTurno();
            estadoMano = EstadoMano.EnJuego;
        }
        // public void CantarFlor(Jugador jugador, TipoFlor tipo)
        // {
        //     if (!PuedeCantarFlor)
        //         throw new InvalidOperationException("No es momento de cantar flor");
        //     if (jugador != partida.TurnoActual)
        //         throw new InvalidOperationException("No es tu turno");

        //     var ultimo = partida.ManoActual.SecuenciaFlor.LastOrDefault();
        //     if (!FlorValida(tipo, ultimo?.Tipo))
        //         throw new InvalidOperationException("Canto de flor inválido");
    
        //     if (estadoMano == EstadoMano.EsperandoRespuestaEnvido)
        //         partida.ManoActual.SecuenciaEnvido.Clear();

        //     partida.ManoActual!.AgregarCanto(tipo, jugador.Nombre);
        //     partida.CambiarTurno();
        //     estadoMano = EstadoMano.EsperandoRespuestaFlor;
        // }
        // public void ResponderFlor(Jugador jugador, bool acepta)
        // {
        //     if (!PuedeResponderFlor)
        //         throw new InvalidOperationException("No hay flor para responder");
        //     if (jugador != partida.TurnoActual)
        //         throw new InvalidOperationException("No es tu turno");
        //     if (Operador.CalcularFlor(partida.TurnoActual.Cartas) == 0)
        //     {
        //         var ultimo = partida.ManoActual.SecuenciaFlor.Last();
        //         var ganador = ultimo.Jugador == partida.Jugador1.Nombre
        //             ? partida.Jugador1
        //             : partida.Jugador2;

        //         var puntos = Operador.SumaDeFlor(partida.ManoActual.SecuenciaFlor);
        //         ganador.SumarPuntos(puntos);

        //         partida.CambiarTurno();
        //         estadoMano = EstadoMano.EnJuego;
        //         return;
        //     }

        //     var ultimoCanto = partida.ManoActual.SecuenciaFlor.LastOrDefault() 
        //         ?? throw new InvalidOperationException("No hay flor para responder (estado inconsistente)");

        //     if (acepta)
        //     {
        //         var puntos = Operador.SumaDeFlor(partida.ManoActual.SecuenciaFlor);

        //         var florJ1 = partida.Jugador1.PuntosFlor;
        //         var florJ2 = partida.Jugador2.PuntosFlor;

        //         if (florJ1 > florJ2)
        //             partida.Jugador1.SumarPuntos(puntos);
        //         else if (florJ2 > florJ1)
        //             partida.Jugador2.SumarPuntos(puntos);
        //         else
        //             partida.ManoActual.JugadorMano.SumarPuntos(puntos);
        //     }else{
        //         var cantosPrevios = partida.ManoActual.SecuenciaFlor
        //             .Take(partida.ManoActual.SecuenciaFlor.Count - 1)
        //             .ToList();

        //         var puntos = Operador.SumaDeFlor(cantosPrevios);

        //         var ganador = ultimoCanto.Jugador == partida.Jugador1.Nombre
        //             ? partida.Jugador1
        //             : partida.Jugador2;

        //         ganador.SumarPuntos(puntos);
        //     }
        //     partida.CambiarTurno();
        //     estadoMano = EstadoMano.EnJuego;
        // }
        public void IrAlMazo(Jugador jugador)
        {
            if (estadoMano != EstadoMano.EnJuego)
                throw new InvalidOperationException("No se puede ir al mazo ahora");

            if (jugador != partida.TurnoActual)
                throw new InvalidOperationException("No es tu turno");

            var puntos = Operador.SumaDeTruco(partida.ManoActual!.SecuenciaTruco);
            if(partida.ManoActual.SecuenciaEnvido.Count == 0)
                puntos++;
                
            var ganador = jugador == partida.Jugador1
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
        // private static bool FlorValida(TipoFlor nuevo, TipoFlor? anterior)
        // {
        //     if (anterior == null)
        //         return nuevo == TipoFlor.Flor;

        //     return anterior switch
        //     {
        //         TipoFlor.Flor => nuevo == TipoFlor.ContraFlor,
        //         TipoFlor.ContraFlor => nuevo == TipoFlor.ContraFlorResto,
        //         _ => false
        //     };
        // }

        public bool PuedeCantarEnvido => (estadoMano == EstadoMano.EnJuego
                                        || estadoMano == EstadoMano.EsperandoRespuestaEnvido
                                        || (estadoMano == EstadoMano.EsperandoRespuestaTruco
                                        && !partida.ManoActual!.RondaActual!.RondaCompleta()))
                                        && !partida.ManoActual.SecuenciaEnvido.Any(c => c.Tipo == TipoEnvido.FaltaEnvido)
                                        && !partida.ManoActual.EnvidoResuelto
                                        && !partida.ManoActual.SecuenciaTruco.Any(c => c.Tipo == TipoTruco.Retruco)
                                        && !partida.ManoActual.SecuenciaTruco.Any(c => c.Tipo == TipoTruco.ValeCuatro);
        // public bool PuedeCantarFlor
        // {
        //     get
        //     {
        //         if (partida.ManoActual == null)
        //             return false;
        //         bool esMomento = estadoMano == EstadoMano.EnJuego || estadoMano == EstadoMano.EsperandoRespuestaEnvido;
        //         bool estaCompletaRonda = partida.ManoActual.Rondas.Count > 1 || partida.ManoActual.RondaActual!.RondaCompleta();
        //         bool tieneFlor = partida.TurnoActual.PuntosFlor != 0;
            
        //         if (!esMomento || estaCompletaRonda || !tieneFlor)
        //             return false;
                
        //         var ultimoCanto = partida.ManoActual.SecuenciaFlor.LastOrDefault();
        //         if (ultimoCanto == null)
        //             return true;

        //         if (ultimoCanto.Jugador == partida.TurnoActual!.Nombre)
        //             return false;
        //         return true;
        //     }
        // }
        public bool PuedeJugarCarta => estadoMano == Arbitro.EstadoMano.EnJuego;
        public bool PuedeCantarTruco => (estadoMano == Arbitro.EstadoMano.EnJuego
                                        || estadoMano == EstadoMano.EsperandoRespuestaTruco)
                                        && !partida.ManoActual.SecuenciaTruco.Any(c => c.Tipo == TipoTruco.ValeCuatro);
        public bool PuedeResponderTruco => estadoMano == Arbitro.EstadoMano.EsperandoRespuestaTruco;
        public bool PuedeResponderEnvido => estadoMano == Arbitro.EstadoMano.EsperandoRespuestaEnvido;
        // public bool PuedeResponderFlor => estadoMano == EstadoMano.EsperandoRespuestaFlor;
        public bool PuedeIrAlMazo => estadoMano == Arbitro.EstadoMano.EnJuego;
    }

}
