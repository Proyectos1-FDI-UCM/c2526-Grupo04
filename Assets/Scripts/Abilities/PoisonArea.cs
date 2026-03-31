//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Diagnostics;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PoisonArea : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private float Duration;
    [Tooltip("Cada cuanto ataca")]
    [SerializeField] private float AttackSpeed;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private CircleCollider2D _hitbox;
    private float _spawnTime;
    private float _lastAttackTime;
    private SpriteRenderer _debug;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Awake()
    {
        _hitbox = GetComponent<CircleCollider2D>();
        _debug = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _hitbox.enabled = false;
        _spawnTime = Time.time;
        _lastAttackTime = Time.time;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (_spawnTime + Duration < Time.time)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Duration += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!LevelManager.Instance.GetPause())
        {
            float latency = 1 / AttackSpeed;

            if (_lastAttackTime + latency < Time.time)
            {
                _hitbox.enabled = true;
                _debug.color = Color.green;

                _lastAttackTime = Time.time;

            }
            else if (_lastAttackTime < Time.time)
            {
                _hitbox.enabled = false;
            }
            //Esto es temporal para que se distinga el ataque
            if (_lastAttackTime + 0.2 < Time.time)
            {
                _debug.color = new Color(0.63f, 0.05f, 0.55f);
            }
        }
        else
        {
            _lastAttackTime += Time.deltaTime;
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

} // class PoisonArea 
// namespace
