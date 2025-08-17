using EspacioEnvido;
using EspacioFlor;
using EspacioTruco;
using EspacioJugador;
using EspacioMazo;
using EspacioRonda;
using EspacioTurno;

class Program{
    static void Main(){
        Jugador Player1 = new("Román");
        Jugador Player2 = new("Daniel");
        Mazo mazo = new();
        List<Ronda> Rondas = new(3);
        mazo.Barajar();
        Player1.RecibirCartas(mazo.Repartir(3));
        Player2.RecibirCartas(mazo.Repartir(3));
        foreach (var carta in Player1.Mano)
        {
            Console.WriteLine($"{Player1.Nombre} - {carta.Nombre}");
        }
        Console.WriteLine("------------------");

        foreach (var carta in Player2.Mano)
        {
            Console.WriteLine($"{Player2.Nombre} - {carta.Nombre}");
        }

        for (int i = 0; i < 3; i++)
        {
            Ronda Ronda = new(i+1);
            Ronda.AgregarTurno(new Turno(Player1.Nombre, Player1.JugarCarta(0)));
            Ronda.AgregarTurno(new Turno(Player1.Nombre, Player2.JugarCarta(0)));
            Rondas.Add(Ronda);
        }
        foreach (var Ronda in Rondas)
        {
            Ronda.AgregarCanto(new CantoEnvido(TipoEnvido.Envido, Player1.Nombre, 10));
            Ronda.AgregarCanto(new CantoTruco(TipoTruco.Truco, Player2.Nombre));
            Ronda.AgregarCanto(new CantoFlor(TipoFlor.Flor, Player1.Nombre));
            Ronda.AsignarPuntos(11,12);
        }
        foreach (var Ronda in Rondas)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"Ronda: {Ronda.Numero}");
            foreach (var turno in Ronda.Turnos)
            {
                Console.WriteLine($"{turno.Jugador} - {turno.CartaJugada.Nombre}");
            }
            Console.WriteLine("Secuencia envido");
            foreach (var envido in Ronda.SecEnvido.Jugada)
            {
                Console.WriteLine($"{envido.Jugador} - {envido.Tipo} - {envido.Puntos}");
            }
            Console.WriteLine("Secuencia truco");

            foreach (var truco in Ronda.SecTruco.Jugada)
            {
                Console.WriteLine($"{truco.Jugador} - {truco.Tipo} - {truco.Puntos}");
            }
            Console.WriteLine("Secuencia flor");

            foreach (var flor in Ronda.SecFlor.Jugada)
            {
                Console.WriteLine($"{flor.Jugador} - {flor.Tipo} - {flor.Puntos}");
            }
        }
    }
}