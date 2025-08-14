using EspacioCarta;
using EspacioJugador;
using EspacioMazo;
using EspacioEnvido;
using EspacioFlor;
using EspacioTruco;

class Program{
    static void Main()
    {
        Mazo mazo = new Mazo();
        Jugador jugador1 = new("Jugador 1");
        // repartir las cartas
        jugador1.RecibirCartas(mazo.Repartir(3));

        // mostrar la mano de cada jugador
        Console.WriteLine($"{jugador1.Nombre}");
        foreach (var naipe in jugador1.Mano){
            Console.WriteLine($"- {naipe.Nombre}");
        }

        jugador1.CalcularEnvido();
        jugador1.CalcularFlor();

        Console.WriteLine($"Puntos del envido de Jugador1: {jugador1.PuntosEnvido}");
        Console.WriteLine($"Puntos de flor de Jugador1: {jugador1.PuntosFlor}");
        int opc;
        SecuenciaTruco secuencia = new SecuenciaTruco();

        do
        {
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Jugar truco");
            Console.WriteLine("2. Jugar retruco");
            Console.WriteLine("3. Jugar valeCuatro");
            Console.WriteLine("0. Salir");
            string opcion = Console.ReadLine();
            opc = int.Parse(opcion);

            switch (opc)
            {
                case 1:
                    CantoTruco Canto = new CantoTruco(TipoTruco.Truco, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto);
                    Console.WriteLine("TRUCO!!!");
                    break;
                case 2:
                    CantoTruco Canto1 = new CantoTruco(TipoTruco.Retruco, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto1);
                    Console.WriteLine("RETRUCO!!!");
                    break;
                case 3:
                    CantoTruco Canto2 = new CantoTruco(TipoTruco.ValeCuatro, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto2);
                    Console.WriteLine("VALE CUATRO!!!");
                    break;

                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        } while (opc != 0);
        foreach (var juego in secuencia.Jugada)
        {
            Console.WriteLine($"Jugador: {juego.Jugador} dice {juego.Tipo} que vale {juego.Puntos}");
        }
        secuencia.CalcularPuntos();
        Console.WriteLine($"Valor de la jugada: {secuencia.PuntosJugada}");



    }
}
