//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Javier Hoyos Giunta
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using Mono.Cecil;
using System;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Potenciadores : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    enum Potenciador {Vida, Daño, Magia}
    [SerializeField] Potenciador tipoPotenciador;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private GameObject _player;
    private PlayerStats _playerStats;

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
        // Como el jugador es un singleton, va a ser el unico elemento con comp. PlayerStats, por lo que lo buscamos por componente
        _playerStats = GameObject.FindAnyObjectByType<PlayerStats>();
        if (_playerStats == null)
        {
            Console.WriteLine("No se ha encontrado ningún elemento con componente PlayerStats");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    // El método Potencia() llama a uno de los métodos de potenciación en función del tipo de potenciador que es
    public void Potencia()
    {
        switch (tipoPotenciador)
        {
            case Potenciador.Vida:
                PotenciaV();
                break;

            case Potenciador.Daño:
                PotenciaD();
                break;

            case Potenciador.Magia:
                PotenciaM();
                break;

            default:
                Console.WriteLine("No se ha asignado un tipo al potenciador (vida, daño o magia)");
                break;
        }
    }

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void PotenciaV()
    {
        _playerStats.VidaUpload();
    }

    private void PotenciaD()
    {
        _playerStats.DmgUpload();
    }

    private void PotenciaM()
    {
        _playerStats.MagiaUpload();
    }
    #endregion   

} // class Potenciadores 
// namespace
