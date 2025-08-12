using EspacioCarta;
using EspacioJugador;
using EspacioMazo;

class Program{
    static void Main()
    {
        // instancias de jugadores y mazo
        Mazo mazo = new Mazo();
        Jugador jugador1 = new Jugador("Jugador1");
        Jugador jugador2 = new Jugador("Jugador2");

        // mezclado de las cartas
        mazo.Barajar();

        // repartir las cartas
        jugador1.RecibirCartas(mazo.Repartir(3));
        jugador2.RecibirCartas(mazo.Repartir(3));

        // mostrar la mano de cada jugador
        Console.WriteLine($"{jugador1.Nombre}");
        foreach (var naipe in jugador1.Mano){
            Console.WriteLine($"- {naipe.Nombre}");
        }
        
        Console.WriteLine($"{jugador2.Nombre}");
        foreach (var naipe in jugador2.Mano){
            Console.WriteLine($"- {naipe.Nombre}");
        }

        Console.WriteLine($"Cantidad de cartas en Mazo: {mazo.Naipes.Count}");

    }
}
