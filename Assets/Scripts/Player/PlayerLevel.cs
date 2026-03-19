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
public class PlayerLevel : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField] private float InitialLimit;
    [SerializeField] private float Increment;
    [SerializeField] private TMPro.TextMeshProUGUI XpTank;
    [SerializeField] private TMPro.TextMeshProUGUI Level;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _experience = 0;
    private int _level = 1;
    private float _currentLimit;
    private float _previousLimit;
    private ItemSelectionManager _itemSelectionManager;

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
        _currentLimit = InitialLimit;
        _itemSelectionManager = GetComponent<ItemSelectionManager>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            UpdateGUI();
            // Si se cumplen las condiciones, realiza las acciones de subida de nivel
            if (IsLevelUpgraded()) LevelUpgrade();
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

    // Suma la experienca obtenida
    public void XpUpdate(int drop)
    {
        _experience += drop;
    }

    

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)


    // Comprueba si se cumple el requisito para subir de nivel

    private void UpdateGUI()
    {
        XpTank.text = _experience + " / " + _currentLimit;
        Level.text = "Nivel: " + _level;
    }

    private bool IsLevelUpgraded()
    {
        return _experience >= _currentLimit;
    }

    // Realiza las acciones de subida de nivel
    private void LevelUpgrade()
    {
        _previousLimit = _currentLimit;
        _currentLimit = (InitialLimit * Mathf.Pow(Increment, _level));
        _currentLimit = Mathf.Round(_currentLimit);
        _level++;
        _experience -= _previousLimit;
        _itemSelectionManager.QueueUp();
    }

    #endregion   

} // class PlayerLevel 
// namespace
