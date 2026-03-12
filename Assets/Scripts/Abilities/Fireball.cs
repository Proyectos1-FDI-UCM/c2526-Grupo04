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
public class Fireball : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private float ExplosionRadius = 1f;
    [SerializeField] private float Duration = 1f;
    [SerializeField] private float ExplosionTime = 0.2f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private Collider2D _hitbox;
    private GameObject _explosion;
    private CircleCollider2D _explosionHitbox;
    private float _spawnTime, _explosionHitboxStart;
    private bool attacking = false;
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

    private void Awake()
    {
        _explosion = transform.GetChild(0).gameObject;
        _spawnTime = Time.time;
        _hitbox = GetComponent<Collider2D>();
        _explosionHitbox = _explosion.GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        _hitbox.enabled = true;
        _explosion.transform.localScale = Vector3.one * ExplosionRadius;

        _explosionHitbox.enabled = false;
        _explosion.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_spawnTime + Duration < Time.time)
        {
            Explode();
        }

        if (_explosionHitbox.enabled && Time.time > _explosionHitboxStart + 0.03f)
        {
            _explosionHitbox.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (!attacking) transform.Translate(Vector3.up * (0.1f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
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

    private void Explode()
    {
        if (!attacking)
        {
            attacking = true;

            _hitbox.enabled = false;
            _hitbox.GetComponent<SpriteRenderer>().enabled = false;
            _explosion.SetActive(true);

            _explosionHitbox.enabled = true;
            _explosionHitboxStart = Time.time;

            Destroy(gameObject, ExplosionTime);
        }
    }
    #endregion

} // class Fireball 
// namespace
