//---------------------------------------------------------
// Contiene el script para la gestión de la magia (prototipo)
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
public class MagicSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    [SerializeField]
    private float TotalReloadTime; // tiempo que tarda en rellenar el tanque al completo

    // ---- ATRIBUTOS PRIVADOS ----
    private PlayerStats _playerStats;
    private AbilityAttack _abilityAttack;
    private float CurrentMagic; //cantidad de magia actual
    private float CurrentReloadTime; //tiempo que tarda en recargar la magia en el momento actual
    private float MaxMagic;

    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    void Start()
    {
        // inicializa playerstats como las stats del jugador (que tiene este componente)
        _playerStats = gameObject.GetComponent<PlayerStats>();
        CurrentMagic = 0f; //establecemos valores iniciales
        MaxMagic = _playerStats.GetMaxMagic(); // la stat magia máxima del jugador
        CurrentReloadTime = TotalReloadTime;
    }

    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (CurrentMagic < MaxMagic) //aumento progresivo de la magia actual, cálculo del tiempo de recarga en función del parámetro del tiempo total de recarga y de los valores de magia actual y máxima
            {
                CurrentReloadTime = (MaxMagic - CurrentMagic) * TotalReloadTime / MaxMagic;
                CurrentMagic += (Time.deltaTime * (MaxMagic - CurrentMagic)) / CurrentReloadTime;
                CurrentMagic = Mathf.Clamp(CurrentMagic, 0, MaxMagic);
            }

            HUDManager.Instance.UpdateMagicGUI(CurrentMagic, MaxMagic);
        }
    }



    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void UpdateMaxMagic()
    {
        MaxMagic = _playerStats.GetMaxMagic();
    }

    public bool UseMagic(float cost)
    {
        bool canUseMagic = CurrentMagic >= cost;
        if (canUseMagic) CurrentMagic -= cost;
        return canUseMagic;

    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class Magia 
// namespace
