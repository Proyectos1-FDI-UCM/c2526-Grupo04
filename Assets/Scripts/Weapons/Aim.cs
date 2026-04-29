//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Aim : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints




    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private Transform _playerTransform;
    private bool movement;

    private float angle = 0;
    private Vector2 aim = Vector2.zero;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _playerTransform = LevelManager.Instance.GetPlayer();
        movement = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_playerTransform != null)

        if (!LevelManager.Instance.GetPause())
        {
            // Comprobamos si el gameObject tiene el componente WeaponAttack
            // En ese caso, el gameObject es un arma y copia la rotación del jugador
            if (gameObject.GetComponent<WeaponAttack>() != null)
            {
                if (InputManager.Instance.MovementVector != Vector2.zero) aim = SnapTo8Directions(InputManager.Instance.MovementVector);
                if (movement) angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90f;
                if (aim != Vector2.zero && movement)
                {
                    transform.position = _playerTransform.position;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }

            // En caso contrario, se tratará de una habilidad y su rotación dependerá de la ubicación del ratón
            else
            {
                aim = InputManager.Instance.AimVector;
                angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90f;
                transform.position = _playerTransform.position;
                if (aim != Vector2.zero)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            } 
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

    public void SetMovement(bool condition)
    {
        movement = condition;
    }


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

} // class WeaponBehaviour 
// namespace
