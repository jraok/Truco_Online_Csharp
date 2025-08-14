using EspacioCarta;
using EspacioJugador;
using EspacioMazo;
using EspacioEnvido;

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

        jugador1.CalcularEnvido();
        jugador1.CalcularFlor();
        jugador2.CalcularEnvido();
        jugador2.CalcularFlor();

        Console.WriteLine($"Puntos del envido de Jugador1: {jugador1.PuntosEnvido}");
        Console.WriteLine($"Puntos del envido de Jugador2: {jugador2.PuntosEnvido}");
        Console.WriteLine($"Puntos de flor de Jugador1: {jugador1.PuntosFlor}");
        Console.WriteLine($"Puntos de flor de Jugador2: {jugador2.PuntosFlor}");
        
        int opc = 0;
        SecuenciaEnvido secuencia = new SecuenciaEnvido();
        do
        {
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Jugar Envido");
            Console.WriteLine("2. Jugar RealEnvido");
            Console.WriteLine("3. Jugar FaltaEnvido");
            Console.WriteLine("4. Salir");
            string opcion = Console.ReadLine();
            opc = int.Parse(opcion);

            switch (opc)
            {
                case 1:
                    CantoEnvido Canto = new CantoEnvido(TipoEnvido.Envido, jugador1.Nombre, 14);
                    secuencia.AgregarCanto(Canto);
                    Console.WriteLine("Envido!!!");
                    break;
                case 2:
                    CantoEnvido Canto1 = new CantoEnvido(TipoEnvido.RealEnvido, jugador1.Nombre, 14);
                    secuencia.AgregarCanto(Canto1);
                    Console.WriteLine("RealEnvido!!!");
                    break;
                case 3:
                    CantoEnvido Canto2 = new CantoEnvido(TipoEnvido.FaltaEnvido, jugador1.Nombre, 14);
                    secuencia.AgregarCanto(Canto2);
                    Console.WriteLine("FaltaEnvido!!!");
                    break;
                
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        } while (opc != 0);

        secuencia.CalcularPuntos();
        Console.WriteLine($"Puntos de la jugada: {secuencia.PuntosJugada}");
        secuencia.Jugada.ForEach(canto => Console.WriteLine($"{canto.Jugador} ha dicho {canto.Tipo} y ha ganado {canto.Puntos} puntos"));


    }
}
