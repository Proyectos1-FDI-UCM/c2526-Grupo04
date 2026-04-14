//---------------------------------------------------------
// Contiene el componente de InputManager
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


/// <summary>
/// Manager para la gestión del Input. Se encarga de centralizar la gestión
/// de los controles del juego. Es un singleton que sobrevive entre
/// escenas.
/// La configuración de qué controles realizan qué acciones se hace a través
/// del asset llamado InputActionSettings que está en la carpeta Settings.
/// 
/// A modo de ejemplo, este InputManager tiene métodos para consultar
/// el estado de dos acciones:
/// - Move: Permite acceder a un Vector2D llamado MovementVector que representa
/// el estado de la acción Move (que se puede realizar con el joystick izquierdo
/// del gamepad, con los cursores...)
/// - Fire: Se proporcionan 3 métodos (FireIsPressed, FireWasPressedThisFrame
/// y FireWasReleasedThisFrame) para conocer el estado de la acción Fire (que se
/// puede realizar con la tecla Space, con el botón Sur del gamepad...)
///
/// Dependiendo de los botones que se quieran añadir, será necesario ampliar este
/// InputManager. Para ello:
/// - Revisar lo que se hace en Init para crear nuevas acciones
/// - Añadir nuevos métodos para acceder al estado que estemos interesados
///  
/// </summary>

public enum CurrentMap {Controller, Keyboard}; 

public class InputManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    #region Atributos del Inspector (serialized fields)

    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static InputManager _instance;

    /// <summary>
    /// Controlador de las acciones del Input. Es una instancia del asset de 
    /// InputAction que se puede configurar desde el editor y que está en
    /// la carpeta Settings
    /// </summary>
    private InputSystem_Actions _theController;


    // Variable para el mapa de acciones
    private InputActionMap _activeMap;
    private CurrentMap map;

    /// <summary>
    /// Acción para Fire. Si tenemos más botones tendremos que crear más
    /// acciones como esta (y crear los métodos que necesitemos para
    /// conocer el estado del botón)
    /// </summary>
    private InputAction _fireball;
    private InputAction _lighting;
    private InputAction _poison;

    /// <summary>
    /// Acción para abrir y cerrar el menú de pausa.
    /// </summary>
    private InputAction _pause;

    /// <summary>
    /// Evitar suscripciones múltiples a eventos estáticos
    /// </summary>
    private bool _inputSystemSubscribed = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour

    /// <summary>
    /// Método llamado en un momento temprano de la inicialización.
    /// 
    /// En el momento de la carga, si ya hay otra instancia creada,
    /// nos destruimos (al GameObject completo)
    /// </summary>
    protected void Awake()
    {
        if (_instance != null)
        {
            // No somos la primera instancia. Se supone que somos un
            // InputManager de una escena que acaba de cargarse, pero
            // ya había otro en DontDestroyOnLoad que se ha registrado
            // como la única instancia.
            // Nos destruímos. DestroyImmediate y no Destroy para evitar
            // que se inicialicen el resto de componentes del GameObject para luego ser
            // destruídos. Esto es importante dependiendo de si hay o no más managers
            // en el GameObject.
            DestroyImmediate(this.gameObject);
        }
        else
        {
            // Somos el primer InputManager.
            // Queremos sobrevivir a cambios de escena.
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            _theController = new InputSystem_Actions();
            if (Gamepad.current == null) UseKeyboard();
            else UseController();

        }
    } // Awake

    /// <summary>
    /// Método llamado cuando se destruye el componente.
    /// </summary>
    protected void OnDestroy()
    {
        if (this == _instance)
        {
            if (_inputSystemSubscribed)
            {
                InputSystem.onEvent -= OnInputEvent;
                _inputSystemSubscribed = false;
            }
            // Éramos la instancia de verdad, no un clon.
            _instance = null;
        } // if somos la instancia principal
    } // OnDestroy

    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static InputManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    } // Instance

    /// <summary>
    /// Devuelve cierto si la instancia del singleton está creada y
    /// falso en otro caso.
    /// Lo normal es que esté creada, pero puede ser útil durante el
    /// cierre para evitar usar el GameManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>True si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }


    /// <summary>
    /// Cambia el mapa de acciones al del Mando (asegurandose de que desactive el otro)
    /// </summary>
    public void UseController()
    {
        if (_activeMap != _theController.PlayerController.Get())
        {
            _activeMap?.Disable();
            Dis();
            _activeMap = _theController.PlayerController.Get();
            map = CurrentMap.Controller;
            Init();
        }
    }

    /// <summary>
    /// Cambia el mapa de acciones al del Teclado (asegurandose de que desactive el otro)
    /// </summary>
    public void UseKeyboard()
    {
        if (_activeMap != _theController.PlayerKeyboard.Get())
        {
            _activeMap?.Disable();
            Dis();
            _activeMap = _theController.PlayerKeyboard.Get();
            map = CurrentMap.Keyboard;
            Init();
        }
    }

    /// <summary>
    /// Devuelve el mapa de acciones actual como string, "keyb" o "cont", y "null" si no hay un mapa de acciones asignado
    /// </summary>
    public CurrentMap GetInputMap()
    {
        return map;
    }

    /// <summary>
    /// Propiedad para acceder al vector de movimiento.
    /// Según está configurado el InputActionController,
    /// es un vector normalizado 
    /// </summary>
    public Vector2 MovementVector { get; private set; }

    /// <summary>
    /// Propiedad para acceder a la dirección de apuntado del jugador
    /// Según está configurado el InputActionController,
    /// es un vector normalizado 
    /// </summary>

    public Vector2 AimVector { get; private set; }


    /// <summary>
    /// Método para saber si el botón de Fireball está pulsado
    /// Devolverá true en todos los frames en los que se mantenga pulsado
    /// <returns>True, si el botón está pulsado</returns>
    /// </summary>
    public bool FireballIsPressed()
    {
        return _fireball.IsPressed();
    }

    /// <summary>
    /// Método para saber si el botón de Lighting está pulsado
    /// Devolverá true en todos los frames en los que se mantenga pulsado
    /// <returns>True, si el botón está pulsado</returns>
    /// </summary>
    public bool LightingIsPressed()
    {
        return _lighting.IsPressed();
    }

    /// <summary>
    /// Método para saber si el botón de Poison está pulsado
    /// Devolverá true en todos los frames en los que se mantenga pulsado
    /// <returns>True, si el botón está pulsado</returns>
    /// </summary>
    public bool PoisonIsPressed()
    {
        return _poison.IsPressed();
    }

    /// <summary>
    /// Método para saber si el botón de pausa (Pause) está pulsado
    /// Devolverá true en todos los frames en los que se mantenga pulsado
    /// <returns>True, si el botón está pulsado</returns>
    /// </summary>
    public bool PauseIsPressed()
    {
        return _pause.IsPressed();
    }

    /// <summary>
    /// Método para saber si el botón de Fireball se ha pulsado en este frame
    /// <returns>Devuelve true, si el botón ha sido pulsado en este frame
    /// y false, en otro caso
    /// </returns>
    /// </summary>
    public bool FireballWasPressedThisFrame()
    {
        return _fireball.WasPressedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de Lighting se ha pulsado en este frame
    /// <returns>Devuelve true, si el botón ha sido pulsado en este frame
    /// y false, en otro caso
    /// </returns>
    /// </summary>
    public bool LightingWasPressedThisFrame()
    {
        return _lighting.WasPressedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de Poison se ha pulsado en este frame
    /// <returns>Devuelve true, si el botón ha sido pulsado en este frame
    /// y false, en otro caso
    /// </returns>
    /// </summary>
    public bool PoisonWasPressedThisFrame()
    {
        return _poison.WasPressedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de pausa (Pause) se ha pulsado en este frame
    /// <returns>Devuelve true, si el botón ha sido pulsado en este frame
    /// y false, en otro caso
    /// </returns>
    /// </summary>
    public bool PauseWasPressedThisFrame()
    {
        return _pause.WasPressedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de Fireball ha dejado de pulsarse
    /// durante este frame
    /// <returns>Devuelve true, si el botón se ha dejado de pulsar en
    /// este frame; y false, en otro caso.
    /// </returns>
    /// </summary>
    public bool FireballWasReleasedThisFrame()
    {
        return _fireball.WasReleasedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de Lighting ha dejado de pulsarse
    /// durante este frame
    /// <returns>Devuelve true, si el botón se ha dejado de pulsar en
    /// este frame; y false, en otro caso.
    /// </returns>
    /// </summary>
    public bool LightingWasReleasedThisFrame()
    {
        return _lighting.WasReleasedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de Poison ha dejado de pulsarse
    /// durante este frame
    /// <returns>Devuelve true, si el botón se ha dejado de pulsar en
    /// este frame; y false, en otro caso.
    /// </returns>
    /// </summary>
    public bool PosionWasReleasedThisFrame()
    {
        return _poison.WasReleasedThisFrame();
    }

    /// <summary>
    /// Método para saber si el botón de pausa (Pause) ha dejado de pulsarse
    /// durante este frame
    /// <returns>Devuelve true, si el botón se ha dejado de pulsar en
    /// este frame; y false, en otro caso.
    /// </returns>
    /// </summary>
    public bool PauseWasReleasedThisFrame()
    {
        return _pause.WasReleasedThisFrame();
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Desabilita el mapa activo y se desuscribe de las acciones de OnAim y OnMove
    /// </summary>
    private void Dis()
    {
        if (_activeMap == null) return;

        InputAction movement = _activeMap.FindAction("Move");
        if (movement != null)
        {
            movement.performed -= OnMove;
            movement.canceled -= OnMove;
        }

        InputAction aim = _activeMap.FindAction("Aim");
        if (aim != null)
        {
            aim.performed -= OnAim;
            aim.canceled -= OnAim;
        }
    }

    /// <summary>
    /// Dispara la inicialización.
    /// </summary>
    private void Init()
    {
        // Nos suscribimos a eventos estáticos UNA sola vez para evitar duplicados
        if (!_inputSystemSubscribed)
        {
            InputSystem.onEvent += OnInputEvent; // Escucha eventos de entrada para detectar qué dispositivo se está usando
            _inputSystemSubscribed = true;
        }

        HUDManager.Instance.ChangeActiveMap(map);

        // Creamos el controlador del input y activamos los controles del jugador
        _activeMap.Disable();
        _activeMap.Enable();


        // Cacheamos la acción de movimiento

        InputAction movement = _activeMap.FindAction("Move");

        // Para el movimiento, actualizamos el vector de movimiento usando
        // el método OnMove
        if (movement != null)
        {
            movement.performed -= OnMove;
            movement.canceled -= OnMove;

            movement.performed += OnMove;
            movement.canceled += OnMove;
        }

        InputAction aim = _activeMap.FindAction("Aim");

        // Para el apuntado, actualizamos el vector de la dirección de apuntado usando
        // el método OnAim

        if (aim != null)
        {
            aim.performed -= OnAim;
            aim.canceled -= OnAim;

            aim.performed += OnAim;
            aim.canceled += OnAim;
        }

        // Para el disparo solo cacheamos la acción de disparo.
        // El estado lo consultaremos a través de los métodos públicos que 
        // tenemos (FireIsPressed, FireWasPressedThisFrame 
        // y FireWasReleasedThisFrame)
        _fireball = _activeMap.FindAction("Fireball");
        _lighting = _activeMap.FindAction("Lighting");
        _poison = _activeMap.FindAction("Poison");

        // Asignación del botón de pausa
        _pause = _activeMap.FindAction("Pause");
    }


    

    /// <summary>
    /// Detecta el dispositivo que está generando entradas en tiempo real y
    /// cambia el mapa de acciones según el dispositivo usado (teclado/ratón vs mando).
    /// </summary>
    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (device == null) return;

        // Si el dispositivo que generó el evento es un mando, usamos el mapa de mando
        if (device is Gamepad)
        {
            // Solo cambiar si es distinto para evitar re-inicializaciones innecesarias
            if (_activeMap != _theController.PlayerController.Get())
            {
                UseController();
            }
        }
        // Si el dispositivo es teclado o ratón (u otro dispositivo basado en teclado), usamos teclado
        else if (device is Keyboard || device is Mouse)
        {
            if (_activeMap != _theController.PlayerKeyboard.Get())
            {
                UseKeyboard();
            }
        }
       
    }


    /// <summary>
    /// Método que es llamado por el controlador de input cuando se producen
    /// eventos de movimiento (relacionados con la acción Move)
    /// </summary>
    /// <param name="context">Información sobre el evento de movimiento</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        MovementVector = context.ReadValue<Vector2>();
    }

    /// <summary>
    ///Si se usa el ratón se lee la posición del ratón 
    ///[(0,0) abajo a la izquierda y (1600, 900) arriba a la derecha] 
    ///y se calcula la dirección asumiendo que el jugador está en el centro
    ///</summary>
    private void OnAim(InputAction.CallbackContext context)
    {
        float _screenX = 800, _screenY = 450;

        AimVector = context.ReadValue<Vector2>();

        if (_activeMap == _theController.PlayerController.Get()) AimVector = context.ReadValue<Vector2>();

        else 
        {
            Vector2 _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            AimVector = (context.ReadValue<Vector2>() - _screenCenter).normalized; 
        }
    }

    #endregion
} // class InputManager 
// namespace