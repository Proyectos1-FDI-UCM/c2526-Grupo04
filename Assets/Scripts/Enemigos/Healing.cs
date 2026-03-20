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
public class Healing : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField] private float HealingTimePerUnit;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints


    private GameObject _boss;
    private Health _bossHealth;
    private Health _pillarHealth;
    private Transform _bossPos;
    private float regen = 0f;
    private float _currentHealth;
    private int _maxHealth;
    private bool _bossFase2;

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
        _boss = FindAnyObjectByType<Boss>().gameObject;
        _pillarHealth = GetComponent<Health>();
        if (_boss != null)
        {
            _bossHealth = _boss.GetComponent<Health>();
            _bossPos = _boss.GetComponent<Transform>();
            _maxHealth = _bossHealth.GetMaxHealth();
        }
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (true)
            {
                if (_pillarHealth.IsDead())
                {
                    if (LevelManager.Instance.PillarDestroyed() == 0 && !_bossFase2) //comprueba que es el último pilar y que el boss esté en la fase 1
                    {
                        _bossHealth.LoseHealth(_maxHealth + 1);
                    }
                }
                BossHeal();
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


    private void BossHeal()
    {

        //Si el boss es nulo busca de nuevo (normalmente cuando pase a fase 2)
        if (_boss == null)
        {
            _boss = GameObject.FindGameObjectWithTag("Boss");
            if (_boss != null)
            {
                if (_maxHealth != _bossHealth.GetMaxHealth()) _bossFase2 = true;
                _bossHealth = _boss.GetComponent<Health>();
                _bossPos = _boss.GetComponent<Transform>();
                _maxHealth = _bossHealth.GetMaxHealth();
            }
        }

        //Mismo comportamiento que la regenHealth
        else
        {
            regen = Time.deltaTime / HealingTimePerUnit;
            _currentHealth = _bossHealth.GetCurrentHealth();

            if (_currentHealth < _maxHealth) _bossHealth.LoseHealth(-regen);
            else _bossHealth.LoseHealth(_currentHealth - _maxHealth);
            Debug.Log(_currentHealth);
        }
    }
    #endregion   

} // class Healing 
// namespace
