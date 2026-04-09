//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Arturo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemyStateMachine : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //Transform del jugador para que el enemigo pueda seguirlo

    [SerializeField] private float MaxDistance; //Distancia máxima a la que se puede acercar el tirador al jugador

    [SerializeField] private float Speed; //Velocidad a la que se mueve el enemigo

    [SerializeField] private float KnockbackSpeed; //Velocidad a la que retrocede el enemigo

    [SerializeField] private float KnockbackDuration; //Duración del retroceso

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private float minX, maxX, minY, maxY;
    protected enum State
    {
        Chasing,
        Knockback
    }

    private State _currentState;

    private float _actualKnockbackDuration;

    private Transform _playerTransform;

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
        _currentState = State.Chasing;
        LevelManager.Instance.GetMapLimits(out maxX, out minX, out maxY, out minY);
        _playerTransform = LevelManager.Instance.GetPlayer();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            //Comprobar que el transform del jugador no es nulo para evitar errores
            if (_playerTransform != null)
            {
                //Obtenemos el vector distancia para comprobar si el tirador debe avanzar o no 
                Vector3 distance = _playerTransform.position - transform.position;
                //Obtenemos la dirección que tiene que seguir
                Vector3 direction = distance.normalized;

                switch (_currentState)
                {
                    case State.Chasing:

                        //Comprobamos si la magnitud del vector distancia es mayor que la distancia máxima a la que se puede acercar el tirador
                        if (distance.magnitude > MaxDistance)
                        {
                            //Le hacemos avanzar en dicha dirección a la velocidad definida desde el editor
                            transform.position += direction * Speed * Time.deltaTime;

                            Vector3 pos = transform.position;

                            pos.x = Mathf.Clamp(pos.x, minX, maxX);
                            pos.y = Mathf.Clamp(pos.y, minY, maxY);

                            transform.position = pos;
                        }
                        break;

                    case State.Knockback:
                        if (Time.time > _actualKnockbackDuration)
                        {
                            _currentState = State.Chasing;
                        }
                        else transform.position -= direction * KnockbackSpeed * Time.deltaTime;
                        break;
                }

                //Obtenemos el ángulo que el tirador debe girar para apuntar al jugador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                //Rotamos al jugador dicho ángulo respecto al eje z
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponAttack weapon = collision.gameObject.GetComponent<WeaponAttack>();
        if (weapon!= null || collision.gameObject.layer == 6)
        {
            _currentState = State.Knockback;
            _actualKnockbackDuration = Time.time + KnockbackDuration;
        }
    }

    #endregion   

} // class RangedEnemiesMovement 
// namespace
