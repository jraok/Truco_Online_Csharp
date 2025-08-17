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
            Ronda Ronda = new(i);
            Ronda.AgregarTurno(new Turno(Player1.Nombre,Player1.JugarCarta(0)));
            Ronda.AgregarTurno(new Turno(Player2.Nombre,Player2.JugarCarta(0)));
            Rondas.Add(Ronda);
            Console.WriteLine($"Ganador de la ronda: {i+1} ---> jugador = {Ronda.DeterminarGanador().ToString()}");
        }
       

    }
}