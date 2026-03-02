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
    private float cost; //coste del ataque (provisional)

    [SerializeField]
    private TMPro.TextMeshProUGUI MagicTank; //texto que se muestra en pantalla

    [SerializeField]
    private float MaxMagic; //capacidad máxima de magia

    // ---- ATRIBUTOS PRIVADOS ----
    private float NowMagic; //cantidad de magia actual
    private float NowReloadTime; //tiempo que tarda en recargar la magia en el momento actual
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    void Start()
    {
        NowMagic = 0f; //establecemos valores iniciales
        MaxMagic = 100;
        NowReloadTime = TotalReloadTime;
    }

    void Update()
    { 
        if (NowMagic < MaxMagic) //aumento progresivo de la magia actual, cálculo del tiempo de recarga en función del parámetro del tiempo total de recarga y de los valores de magia actual y máxima
        {
            NowReloadTime = (MaxMagic - NowMagic) * TotalReloadTime / MaxMagic;
            NowMagic += (Time.deltaTime * (MaxMagic - NowMagic)) / NowReloadTime;
            NowMagic = Mathf.Clamp(NowMagic, 0, MaxMagic);            
            
        }

        if (InputManager.Instance.FireWasPressedThisFrame() && NowMagic > cost) //gasto de magia al atacar, comprobando que el botón de ataque se ha pulsado y que hay suficiente magia para gastar
        {
            NowMagic -= cost;
        }

        UpdateGUI();
    }

    void UpdateGUI() //actualizar el texto en pantalla limitando el valor de la magia actual a dos decimales para que se vea mejor
    {
        MagicTank.text = NowMagic.ToString("F2") + " / " + MaxMagic;
    }

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

} // class Magia 
// namespace
