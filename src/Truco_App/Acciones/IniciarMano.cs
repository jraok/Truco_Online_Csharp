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
}