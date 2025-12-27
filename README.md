# **Truco Argentino - Core Engine (Console Edition)**

Este proyecto es una implementación del clásico juego de Truco Argentino desarrollada en .NET 9. Aunque nació con una visión multijugador, actualmente se centra en ser un motor de reglas sólido y modular ejecutable por consola, diseñado bajo principios de clean code y C# moderno.

---

## 🚀 Propósito del Proyecto
Demostrar el dominio de las últimas características de C# y la capacidad de modelar una lógica de negocio compleja (el reglamento del Truco) de forma mantenible y desacoplada.

## Stack Tecnológico
- **Lenguaje:** C# 13.
- **Framework:** .NET 9 SDK.
- **Paradigma:** Programación Orientada a Objetos (POO) con un enfoque funcional en el motor de reglas.

## Características Técnicas (The "Flex" Zone)
Para los que vienen a ver el código, aquí destaco lo más interesante:

- **Uso de C# Moderno:** Implementación de Primary Constructors en clases clave como Ronda y Mano, y Records para estructuras de datos inmutables como Carta y Turno.
- **Motor de Reglas Funcional:** La clase Operador actúa como una biblioteca de funciones puras para calcular jerarquías, puntos de envido y sumas de truco, facilitando el testeo y la reutilización.
- **Pattern Matching Avanzado:** Aprovechamiento de las switch expressions para manejar la compleja jerarquía de cartas y las respuestas de los cantos.
- **Arquitectura Desacoplada:** Separación total entre la lógica de juego (Truco.Core) y la interfaz de usuario (Truco.UI), permitiendo cambiar la consola por una interfaz gráfica en el futuro sin tocar el núcleo.

## Estructura del Proyecto
- **Modelos:** Definición de entidades básicas (Carta, Mazo, Jugador).
- **Reglas:** El "corazón" matemático del juego. Define puntos y jerarquías.
- **Juego:** Controladores de flujo como el Arbitro, que gestiona los turnos y estados de la partida.
- **UI:** Interfaz de consola con manejo de colores para mejorar la experiencia de usuario.

## Instalación y Ejecución
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
  
