using EspacioCarta;
using EspacioJugador;
using EspacioMazo;
using EspacioEnvido;
using EspacioFlor;

class Program{
    static void Main()
    {
        List<Carta> maso = new List<Carta>(3);
        for (int i = 0; i < 3; i++)
        {
            maso.Add(new Carta(Palos.Oro,i));
        }
        Jugador jugador1 = new Jugador("Jugador1");

        // repartir las cartas
        jugador1.RecibirCartas(maso);

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
        SecuenciaFlor secuencia = new SecuenciaFlor();

        do
        {
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Jugar Flor");
            Console.WriteLine("2. Jugar ContraFlor");
            Console.WriteLine("3. Jugar ContraFlorResto");
            Console.WriteLine("0. Salir");
            string opcion = Console.ReadLine();
            opc = int.Parse(opcion);

            switch (opc)
            {
                case 1:
                    CantoFlor Canto = new CantoFlor(TipoFlor.Flor, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto);
                    Console.WriteLine("FLOR!!!");
                    break;
                case 2:
                    CantoFlor Canto1 = new CantoFlor(TipoFlor.ContraFlor, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto1);
                    Console.WriteLine("CONTRAFLOR!!!");
                    break;
                case 3:
                    CantoFlor Canto2 = new CantoFlor(TipoFlor.ContraFlorResto, jugador1.Nombre);
                    secuencia.AgregarCanto(Canto2);
                    Console.WriteLine("CONTRA FLOR AL RESTO!!!");
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
