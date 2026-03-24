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
public class ItemSelectionManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //interfaz visual del juego y del menú de elección
    [SerializeField] private GameObject SelectionMenu;
    [SerializeField] private GameObject GameCanvas;

    //prefabs de aarmas y habilidades
    [SerializeField] private GameObject Lanza;
    [SerializeField] private GameObject Maza;
    [SerializeField] private GameObject Espada;
    [SerializeField] private GameObject Rayo;
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject Poison;

    //textos de los botones con las distintas elecciones
    [SerializeField] private TMPro.TextMeshProUGUI Texto1;
    [SerializeField] private TMPro.TextMeshProUGUI Texto2;
    [SerializeField] private TMPro.TextMeshProUGUI Texto3;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    //array con todas las opciones posibles que pueden salir
    private string[] _options = {"Lanza", "Maza", "Espada", "Rayo", "Fireball", "Poison", "Casco", "Pocima", "Sello", "Pesa", "Tunica", "Orbe"};
    private int option1, option2, option3; //numeros aleatorios de las elecciones
    private int queue = 0;
    private Potenciadores _potenciadores;

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
        _potenciadores = GetComponent<Potenciadores>(); //obtenemos componente potenciadores
    }

    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (queue > 0)
            {
                PowerUpScreen();
                queue--;
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void PowerUpScreen() //método para pausar el juego, escondemos la UI del juego, mostramos el menú y paramos el resto
    {
        GameCanvas.SetActive(false);
        SelectionMenu.SetActive(true);
        LevelManager.Instance.PauseGame();
        RandomOptions(); //elegimos aleatoriamente las opciones que se mostrarán en el menú
    }

    public void Option1() //se ejecuta cuando el jugador elige la primera opcion
    {
        Selection(option1); 
        ResumeGame();
    }

    public void Option2() //se ejecuta cuando el jugador elige la segunda opcion
    {        
        Selection(option2);
        ResumeGame();
    }

    public void Option3() //se ejecuta cuando el jugador elige la tercera opcion
    {
        Selection(option3);   
        ResumeGame();
    }

    public void QueueUp()
    {
        queue++;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void RandomOptions() //elegimos al azar las opciones entre todas las disponibles
    {
        do
        {
            option1 = Random.Range(0, _options.Length);
            Texto1.text = _options[option1];
        } while (_options[option1] == null); //nos aseguramos de que no se haya elegido antes

        do {
            option2 = Random.Range(0, _options.Length);
            Texto2.text = _options[option2];
        } while (option2 == option1 || _options[option2] == null); //ademas en las siguientes nos aseguramos de que no sean igual que las anteriores

        do
        {
            option3 = Random.Range(0, _options.Length);
            Texto3.text = _options[option3];
        } while (option3 == option1 || option3 == option2 || _options[option3] == null); //aqui igual
    }

    private void ResumeGame() //metodo para reanudar el juego cuando se haya elgido alguna de las opciones, hace lo opuesto al metodo de pausa
    {
        SelectionMenu.SetActive(false);
        GameCanvas.SetActive(true);
        LevelManager.Instance.PauseGame();
    }

    private void Selection(int option) //metodo para ejecutar la accion seleccionada
    {
        switch (option) //si es un arma o habilidad (0-5) se instancia, sino se llama al metodo potencia del componente potenciadores con el atributo correspondiente (6-11)
        { // en caso de armas y potenciadores se marca esa posicion del array como null para que no vuelva a salir
            case 0:
                GameObject.Instantiate(Lanza);
                _options[option] = null;
                break;

            case 1:
                GameObject.Instantiate(Maza);
                _options[option] = null;
                break;

            case 2:
                GameObject.Instantiate(Espada);
                _options[option] = null;
                break;

            case 3:
                GameObject.Instantiate(Rayo);
                _options[option] = null;
                break;

            case 4:
                GameObject.Instantiate(Fireball);
                _options[option] = null;
                break;

            case 5:
                GameObject.Instantiate(Poison);
                _options[option] = null;
                break;

            case 6:
                _potenciadores.Potencia("Vida");
                break;

            case 7:
                _potenciadores.Potencia("Daño");
                break;

            case 8:
                _potenciadores.Potencia("Magia");
                break;
                
            case 9:
                _potenciadores.Potencia("Vida");
                _potenciadores.Potencia("Daño");
                break;

            case 10:
                _potenciadores.Potencia("Vida");
                _potenciadores.Potencia("Magia");
                break;

            case 11:
                _potenciadores.Potencia("Daño");
                _potenciadores.Potencia("Magia");
                break;
        }
    }   

    #endregion   

} // class ItemSelectionManager 
// namespace
