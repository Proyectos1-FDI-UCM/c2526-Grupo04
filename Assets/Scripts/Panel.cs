//---------------------------------------------------------
// Gestiona los elementos del panel de la galería de personajes
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Panel : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField] Button[] botonesGaleria;
    [SerializeField] Button cerrarBoton;

    [SerializeField] GameObject panel;
    [SerializeField] Image imagen;
    [SerializeField] TMP_Text nombre;
    [SerializeField] TMP_Text descripcion;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    ///Desactiva el panel al entrar en la escena
    /// </summary>
    void Start()
    {
        panel.SetActive(false);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    ///Muestra toda la info del personaje seleccionado
    /// </summary>
    public void MostrarDuende(Sprite sprite, string nombreTxt, string desc)
    {
        panel.SetActive(true);

        imagen.sprite = sprite;
        nombre.text = nombreTxt;
        descripcion.text = desc;

        foreach (Button boton in botonesGaleria)
            boton.interactable = false;

        EventSystem.current.SetSelectedGameObject(cerrarBoton.gameObject);

    }
    /// <summary>
    ///Cierra el panel y reposiciona en el primer elemento de la galería
    /// </summary>
    public void Cerrar()
    {
        panel.SetActive(false);

        foreach (Button boton in botonesGaleria)
            boton.interactable = true;

        EventSystem.current.SetSelectedGameObject(botonesGaleria[0].gameObject);
    }
    #endregion

} // class Panel
// namespace
