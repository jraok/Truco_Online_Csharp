using EspacioJugador;
using EspacioMazo;
using EspacioRonda;
using EspacioTurno;
using EspacioMano;
using EspacioEnvido;
using EspacioTruco;
using EspacioFlor;

class Program{
    static void Main(){
        Jugador Player1 = new("Román");
        Jugador Player2 = new("Daniel");
        Mazo mazo = new();

        mazo.Barajar();
        Player1.RecibirCartas(mazo.Repartir(3));
        Player2.RecibirCartas(mazo.Repartir(3));
        
        Mano mano = new(Player1,Player2);

        foreach (var carta in Player1.Cartas)
        {
            Console.WriteLine($"{Player1.Nombre} - {carta.Nombre}");
        }
        Console.WriteLine("------------------");

        foreach (var carta in Player2.Cartas)
        {
            Console.WriteLine($"{Player2.Nombre} - {carta.Nombre}");
        }
        for (int i = 0; i < 3; i++)
        {
            Ronda RondaNueva = new(i + 1);
            RondaNueva.AgregarTurno(new Turno(Player1.Nombre,Player1.JugarCarta(0)));
            RondaNueva.AgregarTurno(new Turno(Player2.Nombre,Player2.JugarCarta(0)));
            mano.AgregarRonda(RondaNueva);
        }

        Player1.SumarPuntos(20);
        mano.AgregarCanto(TipoEnvido.Envido, Player1.Nombre);
        mano.AgregarCanto(TipoEnvido.RealEnvido, Player2.Nombre);
        mano.AgregarCanto(TipoEnvido.FaltaEnvido, Player2.Nombre);
        mano.AgregarCanto(TipoTruco.Truco, Player2.Nombre);
        mano.AgregarCanto(TipoTruco.ValeCuatro, Player1.Nombre);
        mano.AgregarCanto(TipoFlor.ContraFlorResto, Player2.Nombre);
        Console.WriteLine("----------------------------");

        foreach (var canto in mano.SecuenciaEnvido)
        {
            Console.WriteLine($"{canto.Jugador}: {canto.Tipo.ToString()} por {canto.Puntos}");
        }
        Console.WriteLine("----------------------------");
        foreach (var canto in mano.SecuenciaTruco)
        {
            Console.WriteLine($"{canto.Jugador}: {canto.Tipo.ToString()} por {canto.Puntos}");
        }
        Console.WriteLine("----------------------------");
        foreach (var canto in mano.SecuenciaFlor)
        {
            Console.WriteLine($"{canto.Jugador}: {canto.Tipo.ToString()} por {canto.Puntos}");
        }
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Valor de la secuencia envido: {mano.PuntosEnvido}");
        Console.WriteLine($"Valor de la secuencia truco: {mano.PuntosTruco}");
        Console.WriteLine($"Valor de la secuencia flor: {mano.PuntosFlor}");
        
    }
}