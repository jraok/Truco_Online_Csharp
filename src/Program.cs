using EspacioJugador;
using EspacioMazo;
using EspacioTurno;

class Program{
    static void Main(){
        Jugador Roman = new("Román");
        Mazo mazo = new();
        List<Turno> turnos = new();

        mazo.Barajar();
        Roman.RecibirCartas(mazo.Repartir(3));
        foreach (var carta in Roman.Mano)
        {
            Console.WriteLine($"{carta.Nombre}");
        }
        
        for (int i = 0; i < 3; i++)
        {
            turnos.Add(new Turno(Roman.Nombre,Roman.JugarCarta(0)));
        }

        foreach (var turno in turnos)
        {
            Console.WriteLine($"Jugador: {turno.Jugador}   Carta: {turno.CartaJugada.Nombre}");
        }
    }
}