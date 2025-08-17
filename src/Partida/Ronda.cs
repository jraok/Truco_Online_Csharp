using System.Text.Json.Serialization;
using EspacioEnvido;
using EspacioTruco;
using EspacioFlor;
using EspacioTurno;

namespace EspacioRonda{
    public class Ronda{
        private int numero;
        private int puntosJ1;
        private int puntosJ2;
        private SecuenciaEnvido secEnvido = new();
        private SecuenciaFlor secFlor = new();
        private SecuenciaTruco secTruco = new();
        private List<Turno> turnos = new();

        public int Numero => numero;
        public int PuntosJ1 => puntosJ1;
        public int PuntosJ2 => puntosJ2;
        public SecuenciaEnvido SecEnvido => secEnvido;
        public SecuenciaFlor SecFlor => secFlor;
        public SecuenciaTruco SecTruco => secTruco;
        public List<Turno> Turnos => turnos;
        [JsonConstructor]
        public Ronda(int numero){
            this.numero = numero;
        }
        public void AgregarTurno(Turno turno){
            turnos.Add(turno);
        }
        public void AsignarPuntos(int jugador1, int jugador2){
            puntosJ1 = jugador1;
            puntosJ2 = jugador2;
        }
        public void AgregarCanto(CantoEnvido envido){
            secEnvido.AgregarCanto(envido);
        }
        public void AgregarCanto(CantoTruco truco){
            secTruco.AgregarCanto(truco);
        }
        public void AgregarCanto(CantoFlor flor){
            secFlor.AgregarCanto(flor);
        }
    }
}