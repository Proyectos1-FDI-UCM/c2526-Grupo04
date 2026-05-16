//---------------------------------------------------------
// Contiene el método para que suene el efecto de sonido
// del menú de opciones
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

public class OptionsSFXButton : MonoBehaviour
{
    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    ///Pone el sonido
    /// </summary>
    public void PlayTestSound()
    {
        AudioManager.Instance.PlayPoisonSound();
    }
    #endregion

} // class OptionsSFXButton 
// namespace
