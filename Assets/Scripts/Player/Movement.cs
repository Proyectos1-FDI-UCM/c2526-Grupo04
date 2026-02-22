//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Rodrigo Ceña Álvarez
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Movement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private float Velocity;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private Rigidbody2D _rb;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Vector2 movement = InputManager.Instance.MovementVector;
        movement = SnapTo8Directions(movement);
        _rb.linearVelocity = movement * Velocity;

        if (movement != Vector2.zero)
        {
            // Calculamos el ángulo de rotación. Obtenemos el ángulo en radianes y lo convertimos a grados, y después le restamos 90
            //  (pues el triángulo apunta hacia arriba la punta)
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;

            // Una vez tenemos el ángulo, rotamos el player a esa dirección
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private Vector2 SnapTo8Directions(Vector2 input)
    {
        if (input.magnitude < 0.1f) return Vector2.zero;

        // Cálculo del ángulo en radianes y lo pasamos a grados
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;

        // Redondeamos al múltiplo de 45 más cercano (para las 8 direcciones)
        angle = Mathf.Round(angle / 45f) * 45f;

        // Lo pasamos de vuelta a radianes
        float rad = angle * Mathf.Deg2Rad;

        // Creamos un vector de dirección normalizado a partir del ángulo
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
    #endregion

} // class Movement 
// namespace
