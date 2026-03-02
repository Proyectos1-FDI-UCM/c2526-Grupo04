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
public class SistemaMagia : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    [SerializeField]
    private float TotalReloadTime; // tiempo que tarda en rellenar el tanque al completo

    [SerializeField]
    private TMPro.TextMeshProUGUI MagicTank; //texto que se muestra en pantalla

    [SerializeField]
    private float MaxMagic; //capacidad máxima de magia

    // ---- ATRIBUTOS PRIVADOS ----
    private PlayerStats playerStats;
    private AbilityAttack abilityAttack;
    private float nowMagic; //cantidad de magia actual
    private float nowReloadTime; //tiempo que tarda en recargar la magia en el momento actual


    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    void Start()
    {
        // inicializa playerstats como las stats del jugador (que tiene este componente)
        playerStats = gameObject.GetComponent<PlayerStats>();
        nowMagic = 0f; //establecemos valores iniciales
        MaxMagic = playerStats.GetMaxMagic(); // la stat magia máxima del jugador
        nowReloadTime = TotalReloadTime;
    }

    void Update()
    { 
        if (nowMagic < MaxMagic) //aumento progresivo de la magia actual, cálculo del tiempo de recarga en función del parámetro del tiempo total de recarga y de los valores de magia actual y máxima
        {
            nowReloadTime = (MaxMagic - nowMagic) * TotalReloadTime / MaxMagic;
            nowMagic += (Time.deltaTime * (MaxMagic - nowMagic)) / nowReloadTime;
            nowMagic = Mathf.Clamp(nowMagic, 0, MaxMagic);            
            
        }

        UpdateGUI();
    }

    void UpdateGUI() //actualizar el texto en pantalla limitando el valor de la magia actual a dos decimales para que se vea mejor
    {
        MagicTank.text = nowMagic.ToString("F0") + " / " + MaxMagic;
    }

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController
     
    private void LoseMagic(float cost)
    {
        nowMagic -= cost;
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
