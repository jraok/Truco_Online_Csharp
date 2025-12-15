using Truco.App;
using Truco.Core.Juego;
using Truco.Core.Modelos;

namespace Truco.App.Acciones
{
    public static class IniciarMano
    {
        public static void Ejecutar(Partida partida)
        {
            var JMano = partida.JugadorMano;
            var JPie = partida.JugadorPie;
            if (JMano.Puntaje >= partida.PuntosPartida || JPie.Puntaje >= partida.PuntosPartida) throw new InvalidOperationException("La partida terminó");
            
            var mazo = new Mazo();
            mazo.Barajar();
            partida.SumarMano();

            JMano.LimpiarCartas();
            JPie.LimpiarCartas();
            JMano.RecibirCartas(mazo.Repartir(3));
            JPie.RecibirCartas(mazo.Repartir(3));

            partida.AsignarMano(new Mano(JMano, JPie));
            partida.ManoActual.IniciarSiguienteRonda();

            partida.AsignarTurno(JMano);
        }
    }
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
            }else{
                partida.CambiarTurno();
            }
        }
    }
}