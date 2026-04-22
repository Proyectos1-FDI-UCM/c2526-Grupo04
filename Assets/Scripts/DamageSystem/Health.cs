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
public class Health : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [Header("Relevante solo para enemigos")]
    [SerializeField] private int _maxHealth;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private PlayerStats _playerStats;
    private float _currentHealth;

    private EnemyXP _enemyXP;
    private Boss _boss;

    private Healing _healing;
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
        _playerStats = gameObject.GetComponent<PlayerStats>();
        _enemyXP = gameObject.GetComponent<EnemyXP>();
        _healing = gameObject.GetComponent<Healing>();
        _boss = gameObject.GetComponent<Boss>();
        if (_playerStats != null)
            UpdateMaxHealth();
        _currentHealth = _maxHealth;        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void LateUpdate()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (IsDead()) Die();
            // vuelve al inicio / pantalla de muerte 
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

    // Método público para ser llamado por Damage (u otros) (si el daño introducido es negativo, currentHealth aumenta)
    public void LoseHealth(float damage)
    {
        // Reproducimos el sonido de recibir daño del jugador
        if (_playerStats != null && damage > 0) AudioManager.Instance.PlayerDamageSound();
        _currentHealth -= damage;

        if (damage > 0 && _enemyXP != null)
        {
            // Le enviamos el daño al LevelManager
            LevelManager.Instance.AddDamage(damage);
        }
    }

    public void UpdateMaxHealth()
    {
        _maxHealth = (int)_playerStats.GetMaxHealth();
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public bool IsDead()
    {
        bool dead = _currentHealth <= 0;
        return dead;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)


    private void Die()
    {
        // Si es enemigo llama al sistema de experiencia del enemigo
        if (_enemyXP != null)
        {
            LevelManager.Instance.DestroyEnemy();
            // Reproducimos el sonido de muerte
            AudioManager.Instance.EnemiesDeathSound();
            _enemyXP.DeathXpDrop();
            LevelManager.Instance.Addkill();
            if (_boss == null) Destroy(gameObject); // No destruimos al jefe.
            // Al no destruirlo, permitimos que el Update del componente Boss se ejecute, instanciando
            // la segunda fase y destruyendo la primera instantaneamente.
        }

        // aqui un else que te saque la pantalla de derrota (else pq solo si es el player)
        else if (_playerStats!= null)
        {
            AudioManager.Instance.PlayerDefeatSound(); // Reproducimos el sonido de derrota del jugador
            LevelManager.Instance.PlayerDead();
        }

        else if (_healing != null)
        {
            Destroy(gameObject);
        }
    }



    #endregion  

} // class Health 
// namespace
