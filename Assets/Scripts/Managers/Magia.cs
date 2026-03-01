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
    private float tiempoTotalRecarga; // tiempo que tarda en rellenar el tanque al completo

    [SerializeField]
    private float coste; //coste del ataque

    [SerializeField]
    private TMPro.TextMeshProUGUI tanqueMagia; //texto que se muestra en pantalla

    [SerializeField]
    private float magiaMax; //capacidad máxima de magia

    // ---- ATRIBUTOS PRIVADOS ----
    private float magiaActual; //cantidad de magia actual
    private float tiempoActualRecarga; //tiempo que tarda en recargar la magia en el momento actual
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    void Start()
    {
        magiaActual = 0f; //establecemos valores iniciales
        magiaMax = 100;
        tiempoActualRecarga = tiempoTotalRecarga;
    }

    void Update()
    { 
        if (magiaActual < magiaMax) //aumento progresivo de la magia actual, cálculo del tiempo de recarga en función del parámetro del tiempo total de recarga y de los valores de magia actual y máxima
        {
            tiempoActualRecarga = (magiaMax - magiaActual) * tiempoTotalRecarga / magiaMax;
            magiaActual += (Time.deltaTime * (magiaMax - magiaActual)) / tiempoActualRecarga;
            magiaActual = Mathf.Clamp(magiaActual, 0, magiaMax);            
            
        }

        if (InputManager.Instance.FireWasPressedThisFrame() && magiaActual > coste) //gasto de magia al atacar, comprobando que el botón de ataque se ha pulsado y que hay suficiente magia para gastar
        {
            magiaActual -= coste;
        }

        UpdateGUI();
    }

    void UpdateGUI() //actualizar el texto en pantalla limitando el valor de la magia actual a dos decimales para que se vea mejor
    {
        tanqueMagia.text = magiaActual.ToString("F2") + " / " + magiaMax;
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
