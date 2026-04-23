//---------------------------------------------------------
// Componente que gestiona la OST y los SFX por medio de los componentes AudioSource del manager
// Arturo Ramos Romero
// MMDM (Meteorito Monstruos Duendes Matar)
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Clase que gestiona la reprodución de la OST y los SFX a lo largo del juego.
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

    [Header("SFX del juego")]
    [SerializeField] private AudioClip ButtonSound; // Sonido mp3 que se reproduce al pulsar un botón.
    
    [SerializeField] private AudioClip[] DeathSound; // Sonidos mp3 posibles que se reproducirán al morir los enemigos.
    
    [SerializeField] private AudioClip DamageSound; // Sonido mp3 que se reproduce cuando el jugador reciba daño.
    
    [SerializeField] private AudioClip DefeatSound; // Sonido mp3 que se reproduce tras la derrota del jugador.
    
    [SerializeField] private AudioClip FireballSound; // Sonido mp3 que se reproduce al usar la habilidad de la bola de fuego.
    
    [SerializeField] private AudioClip FireballExplosionSound; // Sonido mp3 que se reproduce cuando explota la bola de fuego.
    
    [SerializeField] private AudioClip LightningSound; // Sonido mp3 que se reproduce al usar la habilidad del rayo.

    [SerializeField] private AudioClip PoisonSound; // Sonido mp3 que se reproducen al usar la habilidad de la zona de veneno.
    
    [SerializeField] private AudioClip PoisonedFloorSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip UpgradeSound; // Sonido mp3 que se reproducen al subir de nivel.

    [SerializeField] private AudioClip SwordSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip SpearSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip HammerSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip DeathBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip HealBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip LaserBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [SerializeField] private AudioClip StoneBossSound; // Sonido mp3 que se reproducen durante la zona de veneno.

    [Header("OST del juego")]
    // Sonido mp3 que se reproduce en la escena de juego principal.
    [SerializeField] private AudioClip GameSceneMusic;

    // Sonido mp3 que se reproduce en los menús del juego.
    [SerializeField] private AudioClip MenuMusic;

    // Sonido mp3 que se reproduce al enfrentarse contra el jefe final.
    [SerializeField] private AudioClip BossFightMusic; 

    // Sonido mp3 que se reproduce al derrotar al jefe final.
    [SerializeField] private AudioClip VictoryMusic; 

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static AudioManager _instance; // Instancia del AudioManager
    private AudioSource _musicAudioSource; // Componente AudioSource para la ost.
    private AudioSource _soundEffectsAudioSource; // Componente AudioSource para los efectos de sonido.
    private int _numAudioSources = 2; // Número de AudioSources que maneja el Manager

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
        if (ButtonSound != null) PlaySound(ButtonSound);
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
        if (DefeatSound != null) PlaySound(DefeatSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador reciba daño.
    /// </summary>
    public void PlayerDamageSound()
    {
        if (DamageSound != null) PlaySound(DamageSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la bola de fuego.
    /// </summary>
    public void PlayFireballSound()
    {
        if (FireballSound != null) PlaySound(FireballSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la bola de fuego.
    /// </summary>
    public void PlayFireballExplosionSound()
    {
        if (FireballExplosionSound != null) PlaySound(FireballExplosionSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad del rayo.
    /// </summary>
    public void PlayLightningSound()
    {
        if (LightningSound != null) PlaySound(LightningSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jugador use la habilidad de la zona de veneno.
    /// </summary>
    public void PlayPoisonSound()
    {
        if (PoisonSound != null) PlaySound(PoisonSound);
    }

    /// <summary>
    /// Método al que llamaremos mientras esté la zona de veneno.
    /// </summary>
    public void PlayPoisonedFloorSound()
    {
        if (PoisonedFloorSound != null) PlaySound(PoisonedFloorSound);
    }

    /// <summary>
    /// Método al que llamaremos al subir de nivel.
    /// </summary>
    public void PlayUpgradeSound()
    {
        if (UpgradeSound != null) PlaySound(UpgradeSound);
    }

    /// <summary>
    /// Método al que llamaremos cuando el jefe final sea derrotado.
    /// </summary>
    public void PlayDeathBossSound()
    {
        if (DeathBossSound != null) PlaySound(DeathBossSound);
    }

    /// <summary>
    /// Método al que se llama cuando se usa una de las tres armas disponibles.
    /// </summary>
    /// <param name="weapon">El arma concreta que usa el jugador.</param>
    public void PlayWeaponSound(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.Sword: 
                if (SwordSound != null) PlaySound(SwordSound); 
                break;
            case Weapon.Spear: 
                if (SpearSound != null) PlaySound(SpearSound); 
                break;
            case Weapon.Hammer: 
                if (HammerSound != null) PlaySound(HammerSound); 
                break;
        }
    }

    /// <summary>
    /// Método al que se llama cuando el jefe final ejecuta alguno de sus ataques.
    /// </summary>
    /// <param name="rangedAtacks">El ataque concreto que usa el jefe.</param>
    public void PlayRangedAtack(RangedAtacks rangedAtacks)
    {
        switch (rangedAtacks)
        {
            case RangedAtacks.Laser: 
                if (LaserBossSound != null) PlaySound(LaserBossSound); 
                break;
            case RangedAtacks.Rock: 
                if (StoneBossSound != null) PlaySound(StoneBossSound); 
                break;
        }
    }


    /// <summary>
    /// Método al que llamaremos para cambiar la canción de fondo y reproducir la correspondiente al 
    /// jefe final.
    /// </summary>
    public void ChangeToVictoryMusic()
    {
        if (VictoryMusic != null)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = VictoryMusic;
            _musicAudioSource.Play();
        }
    }

    /// <summary>
    /// Método al que llamaremos para cambiar la canción de fondo y reproducir la
    /// correspondiente a la escena de juego principal.
    /// </summary>
    public void ChangeToGameSceneMusic()
    {
        if (GameSceneMusic != null)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = GameSceneMusic;
            _musicAudioSource.Play();
        }
    }

    /// <summary>
    /// Método al que llamaremos para cambiar la canción de fondo y reproducir la correspondiente a la
    /// victoria del jugador.
    /// </summary>
    public void ChangeToBossFigthMusic()
    {
        if (BossFightMusic != null)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = BossFightMusic;
            _musicAudioSource.Play();
        }
    }

    /// <summary>
    /// Método al que llamaremos para cambiar la canción de fondo y reproducir la correspondiente a los
    /// menús, siempre y cuando no se venga de una escena de menú.
    /// </summary>
    /// <param name="preSceneIndex">Índice de la escena anterior para no cambiar la música al
    /// saltar entre menús.</param>
    public void ChangeToMenuMusic(int preSceneIndex)
    {
        if (MenuMusic != null && preSceneIndex == 1)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = MenuMusic;
            _musicAudioSource.Play();
        }
    }

    /// <summary>
    /// Método al que llamaremos para cambiar el volumen del AudioSource encargado de la OST.
    /// </summary>
    public void SetOSTVolume(float newVolume)
    {
        _musicAudioSource.volume = newVolume;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// Método de inicialización del Audio Manager.
    /// </summary>
    private void Init()
    {
        // Guardamos todos los componentes AudioSource del AudioManager en un array.
        AudioSource[] _audioSources = GetComponents<AudioSource>(); 

        // Comprobamos si hay más componentes AudioSource de los necesarios en el AudioManager.
        if (_audioSources.Length > _numAudioSources)
        {
            // Borramos el exceso de componentes AudioSource.
            for (int i = _numAudioSources;  i < _audioSources.Length; i++) DestroyImmediate(_audioSources[i]);
        }
        else if (_audioSources.Length < _numAudioSources)
        {
            // Creamos nuevos componentes AudioSource en caso de que no haya suficientes.
            for (int i = _audioSources.Length; i < _numAudioSources; i++) gameObject.AddComponent<AudioSource>();
            _audioSources = GetComponents<AudioSource>();
        }

        // Asignamos los AudioSources con su respectiva utilidad.
        _musicAudioSource = _audioSources[0];
        _musicAudioSource.loop = true;
        if (MenuMusic != null)
        {
            _musicAudioSource.clip = MenuMusic;
            _musicAudioSource.Play();
        }
        _soundEffectsAudioSource = _audioSources[1];
    }

    /// <summary>
    /// Método para reproducir AudioClips a un determinado volumen.
    /// </summary>
    /// <param name="clip">AudioClip que se va a reproducir</param>
    /// <param name="volume">Volumen al que se va a reproducir el AudioClip</param>
    private void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null) _soundEffectsAudioSource.PlayOneShot(clip, volume);
    }

    #endregion  

} // class AudioManager 
// namespace
