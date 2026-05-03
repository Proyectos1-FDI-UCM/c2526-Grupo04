//---------------------------------------------------------
// Componente con el que se gestionan los ataques de los enemigos a distancia.
// Arturo Ramos Romero
// MMDM (Meteoritos Monstruos Duendes Matar)
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using

public enum RangedAtacks
{
    None,
    Laser,
    Rock
}

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class RangedEnemiesAttack : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

   

    [SerializeField] private Projectile Projectile; // Proyectil.

    [SerializeField] private bool IsBoss; // Indica si el proyectil pertenece al jefe.

    [Tooltip("Tiempo entre cada ataque")]
    [SerializeField] private float AttackSpeed; // Velocidad de ataque de la entidad.

    [SerializeField] private float ProjectileDistance; //Distancia del enemigo al proyectil al disparar.

    [SerializeField] private Animator _animator;

    [SerializeField] private RangedAtacks RangedAtacks;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _nextAttack = 0; // Variable que usaremos para gestionar el tiempo entre disparos.
    private Transform _playerTransform; // Transform del jugador.

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
        _playerTransform = LevelManager.Instance.GetPlayer(); // Obtenemos el transform del jugador.
        _nextAttack = Time.time + AttackSpeed;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Si no esta pausado el juego.
        if (!LevelManager.Instance.GetPause())
        {
            // Obtenemos la dirección de disparo.
            Vector3 direction;
            if (_playerTransform != null) direction = (_playerTransform.position - transform.position).normalized;
            else direction = new Vector3(0, 0, 0);

            // Si no ha pasado la cantidad de tiempo definida en el editor, el tirador no dispara.
            if (Time.time > _nextAttack)
            {
                if (IsBoss) _animator.SetBool("IsAttacking", true);
                AudioManager.Instance.PlayRangedAtack(RangedAtacks); // Reproducimos el sonido del disparo.
                // Así, hasta que no haya pasado el tiempo de AttackSpeed, no se dispara.
                _nextAttack = Time.time + AttackSpeed; 
                // Instanciamos el proyectil.
                GameObject newProjectile = Instantiate(Projectile.gameObject, transform.position + transform.up * ProjectileDistance, transform.rotation);
                if (IsBoss) newProjectile.GetComponent<Projectile>().SetAnimator(_animator);
                newProjectile.GetComponent<Projectile>().ProjectileDirection(direction);                
            }
        }
        else
        {
            _nextAttack += Time.deltaTime;
        }
    }
    #endregion 
} // class RangedEnemiesAttack 
// namespace
