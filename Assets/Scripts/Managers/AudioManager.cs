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
public class AudioManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [Header("Efecto de sonido al pulsar botones")]
    [SerializeField] private AudioClip ButtonSound; // Sonido mp3 que se reproduce al pulsar un botón.
    
    [Header("Efectos de sonido de muerte de los enemigos")]
    [SerializeField] private AudioClip[] DeathSound; // Sonidos mp3 posibles que se reproducirán al morir los enemigos.
    
    [Header("Efecto de sonido de recibir daño del jugador")]
    [SerializeField] private AudioClip DamageSound; // Sonido mp3 que se reproduce cuando el jugador reciba daño.
    
    [Header("Efecto de sonido para la derrota del jugador")]
    [SerializeField] private AudioClip DefeatSound; // Sonido mp3 que se reproduce tras la derrota del jugador.
    
    [Header("Efecto de sonido cuando se usa la habilidad de la bola de fuego")]
    [SerializeField] private AudioClip FireballSound; // Sonido mp3 que se reproduce al usar la habilidad de la bola de fuego.
    
    [Header("Efecto de sonido cuando explota la bola de fuego")]
    [SerializeField] private AudioClip FireballExplosionSound; // Sonido mp3 que se reproduce cuando explota la bola de fuego.
    
    [Header("Efecto de sonido cuando se usa la habilidad del rayo")]
    [SerializeField] private AudioClip LightningSound; // Sonido mp3 que se reproduce al usar la habilidad del rayo.
    
    [Header("Efectos de sonido cuando se usa la habilidad de la zona de veneno")]
    [SerializeField] private AudioClip PoisonSound; // Sonido mp3 que se reproducen al usar la habilidad de la zona de veneno.
    
    [Header("Efectos de sonido mientras esté la zona de veneno")]
    [SerializeField] private AudioClip PoisonedFloorSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido al subir de nivel")]
    [SerializeField] private AudioClip UpgradeSound; // Sonido mp3 que se reproducen al subir de nivel.

    [Header("Efectos de sonido espada")]
    [SerializeField] private AudioClip SwordSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido lanza")]
    [SerializeField] private AudioClip SpearSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido martillo")]
    [SerializeField] private AudioClip HammerSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido muerte del Boss")]
    [SerializeField] private AudioClip DeathBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido curación de los pilares")]
    [SerializeField] private AudioClip HealBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido para el ataque laser del boss")]
    [SerializeField] private AudioClip LaserBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("Efectos de sonido para el ataque de piedras del boss")]
    [SerializeField] private AudioClip StoneBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static AudioManager _instance;
    private AudioSource _audioSource;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    protected void Awake()
    {
        if (_instance != null)
        { 

            // Y ahora nos destruímos del todo. DestroyImmediate y no Destroy para evitar
            // que se inicialicen el resto de componentes del GameObject para luego ser
            // destruídos. Esto es importante dependiendo de si hay o no más managers
            // en el GameObject.
            DestroyImmediate(this.gameObject);
        }
        else
        {
            // Somos el primer GameManager.
            // Queremos sobrevivir a cambios de escena.
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        } // if-else somos instancia nueva o no.
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static AudioManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Método al que llamaremos siempre que se pulse un botón.
    /// </summary>
    public void ClickSound()
    {
        PlaySound(ButtonSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando muera un enemigo.
    /// Selecciona uno de los posibles sonidos mp3 de muerte y después lo reproduce.
    /// </summary>
    public void EnemiesDeathSound()
    {
        if (DeathSound.Length > 0) PlaySound(DeathSound[Random.Range(0, DeathSound.Length)]);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador sea derrotado.
    /// </summary>
    public void PlayerDefeatSound()
    {
        PlaySound(DefeatSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador reciba daño.
    /// </summary>
    public void PlayerDamageSound()
    {
        PlaySound(DamageSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la bola de fuego.
    /// </summary>
    public void PlayFireballSound()
    {
        PlaySound(FireballSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la bola de fuego.
    /// </summary>
    public void PlayFireballExplosionSound()
    {
        PlaySound(FireballExplosionSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad del rayo.
    /// </summary>
    public void PlayLightningSound()
    {
        PlaySound(LightningSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la zona de veneno.
    /// </summary>
    public void PlayPoisonSound()
    {
        PlaySound(PoisonSound);
    }

    /// <summary>
    /// Método al que llamaremos mientras esté la zona de veneno.
    /// </summary>
    public void PlayPoisonedFloorSound()
    {
        PlaySound(PoisonedFloorSound);
    }

    /// <summary>
    /// Método al que llamaremos al subir de nivel.
    /// </summary>
    public void PlayUpgradeSound()
    {
        PlaySound(UpgradeSound);
    }

    public void PlayDeathBossSound()
    {
        PlaySound(DeathBossSound);
    }

    public void PlayWeaponSound(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.Sword: PlaySound(SwordSound); break;
            case Weapon.Spear: PlaySound(SpearSound); break;
            case Weapon.Hammer: PlaySound(HammerSound); break;
        }
    }

    public void PlayRangedAtack(RangedAtacks rangedAtacks)
    {
        switch (rangedAtacks)
        {
            case RangedAtacks.Laser: PlaySound(LaserBossSound); break;
            case RangedAtacks.Rock: PlaySound(StoneBossSound); break;
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    // Método de inicialización del Audio Manager
    private void Init()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Método para reproducir AudioClips a un determinado volumen.
    /// </summary>
    /// <param name="clip">AudioClip que se va a reproducir</param>
    /// <param name="volume">Volumen al que se va a reproducir el AudioClip</param>
    private void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null) _audioSource.PlayOneShot(clip, volume);
    }

    #endregion   

} // class AudioManager 
// namespace
