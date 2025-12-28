# **Truco Argentino - Core Engine (Console Edition)**
[![License: MIT](https://img.shields.io/github/license/JRA-OK/Truco-Console-Csharp)](LICENSE)

Implementación del clásico juego de cartas argentino en C#, jugable desde la terminal.

---

## 📖 Descrición 

Este proyecto es una implementación completa del Truco Argentino para 2 jugadores, desarrollado en C# con .NET 9. El juego se ejecuta completamente en consola con una interfaz de texto clara y colorida.
El objetivo principal es demostrar el dominio de las últimas características de C# y la capacidad de modelar una lógica de negocio compleja (el reglamento del Truco) de forma mantenible y desacoplada.

### Características implementadas

- ✅ Reglas básicas del Truco Argentino
- ✅ Sistema completo de Envido (Envido, Real Envido, Falta Envido)
- ✅ Sistema completo de Truco (Truco, Retruco, Vale Cuatro)
- ✅ Cálculo automático de tantos y jerarquía de cartas
- ✅ Sistema de turnos alternados entre Mano y Pie
- ✅ Interfaz colorida según palos de las cartas
- ✅ Validaciones de jugadas y cantos

### Reglas no implementadas (aún)

- ⏳ Flor (código preparado, comentado)
- ⏳ Modo multijugador en red
- ⏳ Persistencia de partidas

---

## 💻 Stack Tecnológico

- **Lenguaje:** C# 13.

- **Framework:** .NET 9 SDK.

- **Paradigma:** Programación Orientada a Objetos (POO) con un enfoque funcional en el motor de reglas.

---

## 📖 Características Técnicas 
Para los que vienen a ver el código, aquí destaco lo más interesante:

- **Uso de C# Moderno:** Implementación de Primary Constructors en clases clave como Ronda y Mano, y Records para estructuras de datos inmutables como Carta y Turno.

- **Motor de Reglas Funcional:** La clase Operador actúa como una biblioteca de funciones puras para calcular jerarquías, puntos de envido y sumas de truco, facilitando el testeo y la reutilización.

- **Pattern Matching Avanzado:** Aprovechamiento de las switch expressions para manejar la compleja jerarquía de cartas y las respuestas de los cantos.

- **Arquitectura Desacoplada:** Separación total entre la lógica de juego (Truco.Core) y la interfaz de usuario (Truco.UI), permitiendo cambiar la consola por una interfaz gráfica en el futuro sin tocar el núcleo.

---

## ⚙️ Instalación y Ejecución
Asegurate de tener instalado el SDK de .NET 9.
- **Clonar el repositorio:**
  ```bash
  git clone https://github.com/tu-usuario/truco-csharp.git
  ```
- **Entrar a la carpeta del proyecto:**
  ```bash
  cd truco-csharp
  ```
- **Ejecutar el proyecto:**
  ```bash
  dotnet run --project src/Truco_Online_Csharp.csproj
  ```

---

## 🏗️ Arquitectura y Flujo de Datos
El diseño del motor se basa en una separación estricta de responsabilidades para garantizar que la lógica del Truco sea independiente de la interfaz de salida.
```mermaid
graph TD
    subgraph UI [Capa de Interfaz]
        P[Pantallas.cs] --> |Muestra estado| Console[Consola]
        P --> |Captura input| Program[Program.cs]
    end

    subgraph Core [Motor de Juego]
        Program --> |Comanda acciones| A[Arbitro.cs]
        A --> |Gestiona| Par[Partida.cs]
        Par --> |Contiene| M[Mano.cs]
        M --> |Fracciona en| R[Ronda.cs]
    end

    subgraph Reglas [Capa de Lógica Pura]
        A --> |Consulta validación| O[Operador.cs]
        M --> |Consulta puntos| O
        O --> |Evalúa| C[Carta.cs]
    end

    subgraph Modelos [Entidades]
        Par --> J[Jugador.cs]
        A --> Mazo[Mazo.cs]
        Mazo --> |Reparte| C
    end

    style UI fill:#f9f,stroke:#333,stroke-width:2px
    style Reglas fill:#bbf,stroke:#333,stroke-width:2px
    style Core fill:#dfd,stroke:#333,stroke-width:2px
```

### Componentes Principales
- *El Árbitro (Orquestador de Estado):* Es la única entidad que conoce el estado global de la mano. Controla el flujo mediante una máquina de estados interna que valida si una acción (cantar truco, jugar carta o envido) es legal en el contexto actual.

- *El Operador (Lógica Pura):* Es un componente estático y sin estado (stateless). Se encarga exclusivamente de las matemáticas del juego: jerarquías de cartas, cálculo de puntos de envido y resolución de valores de los cantos. Al ser lógica pura, facilita enormemente la implementación de pruebas unitarias automáticas.

- *Inmutabilidad con Records:* Se utilizan records para representar entidades como Carta y Turno, asegurando que la información que fluye a través del sistema no sufra efectos secundarios indeseados.

- *Desacoplamiento de UI:* La capa de Pantallas solo tiene acceso de lectura al estado del Arbitro para renderizar la información en consola, pero no puede modificar las reglas del juego directamente.

---

## 📂 Estructura del proyecto
```
src/
├── Program.cs                      # Punto de entrada
├── Truco_Core/                     # Lógica del juego
│   ├── Juego/
│   │   ├── Arbitro.cs             # Controlador principal del juego
│   │   ├── Partida.cs             # Modelo de la partida
│   │   ├── Mano.cs                # Modelo de cada mano
│   │   ├── Ronda.cs               # Modelo de cada ronda (1 carta por jugador)
│   │   └── Turno.cs               # Registro de una jugada
│   ├── Modelos/
│   │   ├── Carta.cs               # Representación de cartas
│   │   ├── Jugador.cs             # Modelo del jugador
│   │   └── Mazo.cs                # Baraja española (40 cartas)
│   └── Reglas/
│       ├── Operador.cs            # Lógica de cálculos (envido, jerarquías)
│       ├── Envido.cs              # Tipos y modelos de Envido
│       └── Truco.cs               # Tipos y modelos de Truco
└── Truco_UI/
    └── Pantallas.cs               # Sistema de visualización en consola
```

### Decisiones de Diseño

- *Separación de responsabilidades:* La lógica del juego (Truco_Core) está completamente independiente de la interfaz (Truco_UI)
- *Árbitro centralizado:* El Arbitro es el único punto de entrada para todas las acciones del juego
- *Validaciones estrictas:* Cada acción valida el estado del juego antes de ejecutarse
- *Inmutabilidad selectiva:* Uso de record para modelos de datos que no cambian (Carta, Turno)

---

## 🧪 Testing

*Nota: El proyecto actualmente no incluye tests unitarios.*
Para agregar tests en el futuro:

```bash
    dotnet new xunit -n Truco.Tests
    dotnet add Truco.Tests reference src/Truco_Core
```

---

## 🤝 Contribuciones
Las contribuciones son bienvenidas. Si querés agregar funcionalidades o mejorar el código:

1. Fork el proyecto
2. Creá una rama para tu feature (git checkout -b feature/nueva-funcionalidad)
3. Commiteá tus cambios (git commit -m 'Agrega nueva funcionalidad')
4. Push a la rama (git push origin feature/nueva-funcionalidad)
5. Abrí un Pull Request

---
## Autor

Joel Román Arancibia
GitHub: @JRA-OK

---

### ⭐ Si te gustó el proyecto, dejá una estrella en GitHub!
