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
public class RangedEnemiesMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField] private Transform PlayerPosition; //Transform del jugador para que el enemigo pueda seguirlo

    [SerializeField] private float MaxDistance; //Distancia máxima a la que se puede acercar el tirador al jugador

    [SerializeField] private float Speed; //Velocidad a la que se mueve el enemigo

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

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
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Comprobar que el transform del jugador no es nulo para evitar errores
        if (PlayerPosition != null)
        {
            //Obtenemos el vector distancia para comprobar si el tirador debe avanzar o no 
            Vector3 distance = PlayerPosition.position - transform.position;
            //Obtenemos la dirección que tiene que seguir
            Vector3 direction = distance.normalized;
            //Comprobamos si la magnitud del vector distancia es mayor que la distancia máxima a la que se puede acercar el tirador
            if (distance.magnitude > MaxDistance)
            {
                //Le hacemos avanzar en dicha dirección a la velocidad definida desde el editor
                transform.position += direction * Speed * Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<RangedEnemiesAttack>().Shoot(direction);
            }

            //Obtenemos el ángulo que el tirador debe girar para apuntar al jugador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            //Rotamos al jugador dicho ángulo respecto al eje z
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

    #endregion   

} // class RangedEnemiesMovement 
// namespace
