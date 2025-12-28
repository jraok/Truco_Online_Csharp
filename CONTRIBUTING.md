# ¡Gracias por tu interés en contribuir! 🎉

Este proyecto es un espacio abierto y cualquier contribución es bienvenida, ya sea reportando bugs, sugiriendo nuevas funcionalidades, mejorando la documentación o escribiendo código.

## 📋 Tabla de Contenidos

- [Código de Conducta](#código-de-conducta)
- [¿Cómo puedo contribuir?](#cómo-puedo-contribuir)
- [Reportar Bugs](#reportar-bugs)
- [Sugerir Funcionalidades](#sugerir-funcionalidades)
- [Proceso de Pull Request](#proceso-de-pull-request)
- [Guía de Estilo](#guía-de-estilo)
- [Configuración del Entorno](#configuración-del-entorno)

---

## 🤝 Código de Conducta

Al participar en este proyecto, te pedimos que seas respetuoso y constructivo. Mantené un ambiente colaborativo y amigable para todos.

---

## 🛠️ ¿Cómo puedo contribuir?

### Reportar Bugs

Si encontraste un bug:
1. **Verificá** que no haya sido reportado antes en [Issues](../../issues)
2. **Abrí un nuevo Issue** usando el template de Bug Report
3. **Incluí** toda la información necesaria:
   - Descripción clara del problema
   - Pasos para reproducirlo
   - Comportamiento esperado vs actual
   - Tu sistema operativo y versión de .NET

### Sugerir Funcionalidades

¿Tenés una idea para mejorar el juego?
1. **Verificá** que no exista ya en [Issues](../../issues)
2. **Abrí un Issue** usando el template de Feature Request
3. **Explicá** claramente qué funcionalidad querés y por qué sería útil

### Mejorar Documentación

La documentación siempre puede mejorar:
- Correcciones de typos
- Clarificación de instrucciones
- Agregar ejemplos
- Traducción a otros idiomas

---

## 🔄 Proceso de Pull Request

### 1. Fork y Clone
```bash
# Hacer fork del repositorio desde GitHub
# Luego clonar tu fork
git clone https://github.com/JRA-OK/Truco-Console-Csharp.git
cd Truco-Console-Csharp
```

### 2. Crear una Rama
```bash
# Crear rama desde main
git checkout -b feature/nombre-descriptivo

# Ejemplos de nombres de rama:
# - feature/implementar-flor
# - fix/bug-empate-doble
# - docs/mejorar-readme
```

### 3. Hacer tus Cambios

- Escribí código limpio y legible
- Seguí las convenciones del proyecto (ver Guía de Estilo)
- Agregá tests si es posible
- Actualizá la documentación si es necesario

### 4. Commit
```bash
# Seguí el formato de commits convencionales
git commit -m "tipo: descripción breve"

# Ejemplos:
# feat: agregar sistema de Flor
# fix: corregir bug en doble empate
# docs: actualizar instrucciones de instalación
# refactor: simplificar lógica de Arbitro
```

**Tipos de commit:**
- `feat`: Nueva funcionalidad
- `fix`: Corrección de bug
- `docs`: Documentación
- `refactor`: Refactorización de código
- `test`: Agregar o modificar tests
- `chore`: Tareas de mantenimiento

### 5. Push y Pull Request
```bash
# Push a tu fork
git push origin feature/nombre-descriptivo

# Luego abrí un Pull Request desde GitHub
```

**En el Pull Request:**
- Describí qué cambios hiciste y por qué
- Referenciá issues relacionados (ej: "Fixes #15")
- Agregá capturas si cambiaste la UI

---

## 🎨 Guía de Estilo

### C# / .NET

- **Convenciones de nombres:**
  - `PascalCase` para clases, métodos, propiedades
  - `camelCase` para variables locales y parámetros
  - `_camelCase` para campos privados
  
- **Formato:**
  - Indentación: 4 espacios (no tabs)
  - Llaves en nueva línea (estilo Allman)
  - Usar `var` solo cuando el tipo es obvio

**Ejemplo:**
```csharp
public class Jugador
{
    private readonly string _nombre;
    
    public string Nombre => _nombre;
    
    public void JugarCarta(int indice)
    {
        var carta = _cartas[indice];
        // ...
    }
}
```

### Arquitectura

- Mantené la separación entre `Truco.Core` (lógica) y `Truco.UI` (interfaz)
- Las reglas del juego van en `Truco.Core.Reglas`
- Preferí inmutabilidad cuando sea posible (usa `record` para datos)
- Validá inputs antes de procesar

### Tests (cuando los agreguemos)

- Nombres descriptivos: `DeberiaRetornarGanadorCuandoHayDosVictorias()`
- Arrange-Act-Assert pattern
- Un assert por test cuando sea posible

---

## 🔧 Configuración del Entorno

### Requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Un editor de código (VS Code, Visual Studio, Rider)

### Setup
```bash
# Clonar el repositorio
git clone https://github.com/JRA-OK/Truco_Console_Csharp.git
cd Truco-Console-Csharp

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run --project src/Truco_Console_Csharp.csproj

# Ejecutar tests (cuando existan)
dotnet test
```

---

## ❓ Preguntas

Si tenés dudas sobre cómo contribuir, podés:
- Abrir un Issue con la etiqueta `question`
- Contactar al mantenedor del proyecto

---

## 📝 Licencia

Al contribuir, aceptás que tus contribuciones serán licenciadas bajo la [MIT License](LICENSE).

---

¡Gracias por contribuir! 🚀
