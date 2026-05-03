//---------------------------------------------------------
// Componente que gestiona el funcionamiento de los proyectiles.
// Arturo Ramos Romero
// MMDM (Meteorito Monstruos Duendes Matar)
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Projectile : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField] private float ProjectileSpeed; //Velocidad del proyectil

    [SerializeField] private bool BossProjectile; //Indica si el proyectil pertenece al jefe

    [SerializeField] private float ProjectileDuration; //Duración del proyectil

    [SerializeField] private bool FollowsPlayer; //Si se marca, el proyectil sigue al jugador
    [SerializeField] private float RotationSpeed = 1;

    [Header("Solo para las sombras de los meteoritos y los rayos del jefe")]
    [SerializeField] private bool Shadow; //Indica si el GameObject con el componente Projectile es la sombra de otro GameObject
                                          //Un GameObject Shadow predice la futura ubicación de aparición del GameObject real

    [SerializeField] private GameObject NonShadowObject; //Se corresponde con el GameObject real

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private Vector3 _direction; //Dirección del proyectil
    private float _actualDuration; //Variable auxiliar que se usa para gestionar la desaparición del proyectil
    private Animator _animator;
    private Animator _bossAnimator;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    private void Awake()
    {
        //Le damos a _actualDuration el valor en segundos que el Time.time deberá superar para destruir el proyectil
        _actualDuration = ProjectileDuration + Time.time;
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            if (FollowsPlayer) _animator.Play("FireballThrown");
            else _animator.Play("Arrow");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (Time.time > _actualDuration)
            {
                if (Shadow) // Si es una sombra, es decir, se quiere instanciar el proyectil real tras su desaparición
                {
                    // Se instancia el proyectil real
                    GameObject realGameObject = Instantiate(NonShadowObject, transform.position, transform.rotation);
                    realGameObject.GetComponent<Projectile>().SetAnimator(_bossAnimator); 
                }
                if (BossProjectile && _bossAnimator != null)_bossAnimator.SetBool("IsAttacking", false);
                Destroy(gameObject);
            }
            else
            {
                if (FollowsPlayer) // Si se ha marcado la opción de FollowsPlayer
                {
                    _direction = Vector3.RotateTowards(_direction, (LevelManager.Instance.GetPlayer().transform.position - transform.position).normalized, RotationSpeed * Time.deltaTime, 0.0f);
                    
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                // Actualizamos su posición
                transform.position += _direction * ProjectileSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ProjectileSpeed > 0) Destroy(gameObject);
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void ProjectileDirection(Vector3 direction)
    {
        //Obtenemos la dirección que debe seguir el proyectil
        _direction = direction;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void SetAnimator(Animator animator)
    {
        _bossAnimator = animator;
    }

    #endregion 

} // class Projectile 
// namespace
