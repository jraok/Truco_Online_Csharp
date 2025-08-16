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
        mazo.Barajar();
        jugador1.RecibirCartas(mazo.Repartir(3));

        // mostrar la mano de cada jugador
        Console.WriteLine($"{jugador1.Nombre}");
        foreach (var naipe in jugador1.Mano){
            Console.WriteLine($"- {naipe.Nombre}");
        }
        Console.WriteLine($"Puntos del envido de Jugador1: {jugador1.PuntosEnvido}");
        Console.WriteLine($"Puntos de flor de Jugador1: {jugador1.PuntosFlor}");
        int? opc;
        SecuenciaTruco secuencia1 = new SecuenciaTruco();
        SecuenciaEnvido secuencia2 = new SecuenciaEnvido();
        SecuenciaFlor secuencia3 = new SecuenciaFlor();

        do
        {
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Jugar truco");
            Console.WriteLine("2. Jugar retruco");
            Console.WriteLine("3. Jugar valeCuatro");
            Console.WriteLine("4. Jugar envido");
            Console.WriteLine("5. Jugar realEnvido");
            Console.WriteLine("6. Jugar faltaEnvido");
            Console.WriteLine("7. Jugar flor");
            Console.WriteLine("8. Jugar contraFlor");
            Console.WriteLine("9. Jugar contraFlorResto");
            string? opcion = Console.ReadLine();
            opc = int.Parse(opcion);

            switch (opc)
            {
                case 1:
                    secuencia1.AgregarCanto(new CantoTruco(TipoTruco.Truco, jugador1.Nombre));
                    Console.WriteLine("TRUCO!!!");
                    break;
                case 2:
                    secuencia1.AgregarCanto(new CantoTruco(TipoTruco.Retruco, jugador1.Nombre));
                    Console.WriteLine("RETRUCO!!!");
                    break;
                case 3:
                    secuencia1.AgregarCanto(new CantoTruco(TipoTruco.ValeCuatro, jugador1.Nombre));
                    Console.WriteLine("VALE CUATRO!!!");
                    break;
                case 4:
                    secuencia2.AgregarCanto(new CantoEnvido(TipoEnvido.Envido, jugador1.Nombre, 14));
                    Console.WriteLine("ENVIDO!!!");
                    break;
                case 5:
                    secuencia2.AgregarCanto(new CantoEnvido(TipoEnvido.RealEnvido, jugador1.Nombre,14));
                    Console.WriteLine("REALENVIDO!!!");
                    break;
                case 6:
                    secuencia2.AgregarCanto(new CantoEnvido(TipoEnvido.FaltaEnvido, jugador1.Nombre,14));
                    Console.WriteLine("FALTAENVIDO!!!");
                    break;
                case 7:
                    secuencia3.AgregarCanto(new CantoFlor(TipoFlor.Flor, jugador1.Nombre));
                    Console.WriteLine("ENVIDO!!!");
                    break;
                case 8:
                    secuencia3.AgregarCanto(new CantoFlor(TipoFlor.ContraFlor, jugador1.Nombre));
                    Console.WriteLine("REALENVIDO!!!");
                    break;
                case 9:
                    secuencia3.AgregarCanto(new CantoFlor(TipoFlor.ContraFlorResto, jugador1.Nombre));
                    Console.WriteLine("FALTAENVIDO!!!");
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        } while (opc != 0);
        foreach (var juego in secuencia1.Jugada)
        {
            Console.WriteLine($"Jugador: {juego.Jugador} dice {juego.Tipo} que vale {juego.Puntos}");
        }
        Console.WriteLine($"Valor de la jugada: {secuencia1.PuntosJugada}");
        Console.WriteLine();
        foreach (var juego in secuencia2.Jugada)
        {
            Console.WriteLine($"Jugador: {juego.Jugador} dice {juego.Tipo} que vale {juego.Puntos}");
        }
        Console.WriteLine($"Valor de la jugada: {secuencia2.PuntosJugada}");
        Console.WriteLine();
        foreach (var juego in secuencia3.Jugada)
        {
            Console.WriteLine($"Jugador: {juego.Jugador} dice {juego.Tipo} que vale {juego.Puntos}");
        }
        Console.WriteLine($"Valor de la jugada: {secuencia3.PuntosJugada}");


    }
}
