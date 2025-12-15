using Truco.Core.Reglas;
using Truco.Core.Modelos;
using Truco.Core.Juego;
namespace Truco.App
{
    public class Partida(string nombreJ1, string nombreJ2)
    {
        public Jugador Jugador1 { get; private set; } = new Jugador(nombreJ1);
        public Jugador Jugador2 { get; private set; } = new Jugador(nombreJ2);
        public Jugador? TurnoActual { get; private set; }
        public Mano? ManoActual { get; private set; }
        private readonly int PuntosPartida = 30;
        private int ManosJugadas = 0;

        public void CambiarTurno(){
            TurnoActual = (TurnoActual == Jugador1) ? Jugador2 : Jugador1;
        }
        public void ResolverGanadorRonda()
        {
            string NombreGanador = ManoActual!.RondaActual!.GanadorRonda;
            if (NombreGanador != "Empate")
            {
                TurnoActual = (NombreGanador == Jugador1.Nombre) ? Jugador1 : Jugador2;
            }else{
                TurnoActual = (ManoActual!.RondaActual!.Turnos[0].Jugador == Jugador1.Nombre) ? Jugador1 : Jugador2;
            }
        }
        public void IniciarMano()
        {
            if (Jugador1.Puntaje >= PuntosPartida || Jugador2.Puntaje >= PuntosPartida) throw new InvalidOperationException("La partida terminó");

            var mazo = new Mazo();
            mazo.Barajar();

            Jugador Mano, Pie;
            ManosJugadas++;
            if (ManosJugadas % 2 != 0)
            {
                Mano = Jugador1;
                Pie = Jugador2;
            }else{
                Mano = Jugador2;
                Pie = Jugador1;
            }

            Mano.LimpiarCartas();
            Pie.LimpiarCartas();

            Mano.RecibirCartas(mazo.Repartir(3));
            Pie.RecibirCartas(mazo.Repartir(3));

            ManoActual = new Mano(Mano, Pie);
            ManoActual.IniciarSiguienteRonda();
            TurnoActual = Mano;
        }
        public void JugarCarta(string nombreJugador, int indiceCarta){
            if (ManoActual == null) throw new InvalidOperationException("No hay mano en juego");
            if (nombreJugador != TurnoActual.Nombre) throw new InvalidOperationException($"No es el turno de {nombreJugador}");
            
            var carta = TurnoActual.TirarCarta(indiceCarta);
            ManoActual.RondaActual!.AgregarTurno(new Turno(TurnoActual.Nombre, carta));
            if (ManoActual.RondaActual.RondaCompleta())
            {
                ResolverGanadorRonda();
                ManoActual.RegistrarGanadorRonda();
            }else{
                CambiarTurno();
            }
        }       
    }
}