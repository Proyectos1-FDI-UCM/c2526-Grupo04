//---------------------------------------------------------
// Gestiona los cheats de desarrollo
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
/// Proporciona cheats activables mediante teclas para facilitar
/// el testeo del juego. Incluye invulnerabilidad, reducción del
/// temporizador, carga de armas/habilidades y magia infinita.
/// F1: Activar/desactivar invulnerabilidad
/// F2: Reducir el tiempo restante a 10 segundos
/// F3: Dar todas las armas y habilidades
/// F4: Activar/desactivar magia infinita
/// </summary>
public class CheatManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //Prefabs de las armas disponibles en el juego
    [SerializeField] private GameObject LanzaPrefab;
    [SerializeField] private GameObject MazaPrefab;
    [SerializeField] private GameObject EspadaPrefab;
    [SerializeField] private GameObject RayoPrefab;
    [SerializeField] private GameObject FireballPrefab;
    [SerializeField] private GameObject PoisonPrefab;

    //Segundos a los que se reducirá el temporizador al usar el cheat
    [SerializeField] private float TimerCheatValue = 10f;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private Health _playerHealth;
    private ItemSelection _itemSelection;
    private MagicSystem _magicSystem;
    private bool _isInvulnerable = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Start()
    {
        _playerHealth = LevelManager.Instance.GetPlayer().GetComponent<Health>();
        _itemSelection = LevelManager.Instance.GetPlayer().GetComponent<ItemSelection>();
        _magicSystem = LevelManager.Instance.GetPlayer().GetComponent<MagicSystem>();
    }

    void Update()
    {
        //F1: Activar/desactivar invulnerabilidad
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleInvulnerability();
        }

        //F2: Reducir el temporizador a TimerCheatValue segundos
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ReduceTimer();
        }

        //F3: Dar todas las armas y habilidades
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GiveAllItems();
        }
        //F4: Magia máxima
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GiveMagic();
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Cambia el estado de invulnerabilidad del jugador
    /// </summary>
    private void ToggleInvulnerability()
    {
        _playerHealth.ToggleInvulnerability();
    }

    /// <summary>
    /// Establece el tiempo que queda para que aparezca el jefe
    /// </summary>
    private void ReduceTimer()
    {
        LevelManager.Instance.SetTimer(TimerCheatValue);
    }
    /// <summary>
    /// Cambia el estado de infinidad de la magia
    /// </summary>
    private void GiveMagic()
    {
        if (_magicSystem != null)
        {
            _magicSystem.ToggleInfiniteMagic();
        }
    }

    /// <summary>
    /// Te da todas las armas y habilidades, asegurandose de no duplicarlas
    /// </summary>
    private void GiveAllItems()
    {
        GameObject[] items = { LanzaPrefab, MazaPrefab, EspadaPrefab, RayoPrefab, FireballPrefab, PoisonPrefab };

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && GameObject.Find(items[i].name + "(Clone)") == null)
                Instantiate(items[i]);
        }
    }

    #endregion

} // class CheatManager
  // namespace