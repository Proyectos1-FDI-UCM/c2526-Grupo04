//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Spawner : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //Tiempo que tarda en aparecer un enemigo desde la aparición del último enemigo anterior a él
    [SerializeField] private float SpawnInterval;

    //Ponemos UnityEngine.Camera
    [SerializeField] private Camera MainCamera;

    //Introducimos el prefab del enemigo a generar
    [SerializeField] private GameObject Enemy;

    //Variable para evitar que los enemigos aparezcan muy lejos de la cámara
    [SerializeField] private float DistanceFromCamera;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _minX, _maxX, _minY, _maxY; //Valores de los límites del mapa en x e y

    private float _realSpawnInterval = 0; //Variable que usaremos para controlar cada cuanto tiempo aparecen enemigos

    private float _cameraHeight; //Distancia que hay desde el centro de la cámara hasta la parte superior de esta
    private float _cameraWidth; //Distancia que hay desde el centro de la cámara hasta cualquiera de los dos laterales

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
        //Obtenemos los límites del mapa llamando al método GetMapLimits del Level Manager
        LevelManager.Instance.GetMapLimits(out _maxX, out _minX, out _maxY, out _minY);

        //Obtenemos el valor de _cameraHeight
        _cameraHeight = MainCamera.orthographicSize;
        _cameraWidth = _cameraHeight * MainCamera.aspect; //Usamos el aspect ratio de la cámara para obtener el valor de _cameraWidth
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Time.time > _realSpawnInterval)
        {
            float _cameraMinX, _cameraMaxX, _cameraMinY, _cameraMaxY; //Límites de la cámara

            //Obtenemos los límites de la cámara
            _cameraMaxY = MainCamera.transform.position.y + _cameraHeight;
            _cameraMinY = MainCamera.transform.position.y - _cameraHeight;
            _cameraMaxX = MainCamera.transform.position.x + _cameraWidth;
            _cameraMinX = MainCamera.transform.position.x - _cameraWidth;

            //Posición de aparición del enemigo
            float _spawnPosX, _spawnPosY;
            Vector3 _spawnPos;

            //Obtenemos una posición aleatoria para el spawn del enemigo
            do
            {
                _spawnPosX = Random.Range(_cameraMinX - DistanceFromCamera, _cameraMaxX + DistanceFromCamera);
                _spawnPosY = Random.Range(_cameraMinY - DistanceFromCamera, _cameraMaxY + DistanceFromCamera);
            } while ((_spawnPosY < _minY || _spawnPosY > _maxY || _spawnPosX < _minX || _spawnPosX > _maxX) 
            || _spawnPosY > _cameraMinY && _spawnPosY < _cameraMaxY && _spawnPosX > _cameraMinX && _spawnPosX < _cameraMaxX);
            _spawnPos = new Vector3(_spawnPosX, _spawnPosY, 0);

            //Generamos el enemigo en la posición obtenida
            GameObject _newEnemy = Instantiate(Enemy, _spawnPos, Quaternion.identity);

            //Aumentamos el valor de _realSpawnInterval para que el juego espere el intervalo deseado hasta generar el siguient enemigo
            _realSpawnInterval = Time.time + SpawnInterval;
        }
    }
    #endregion

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

} // class Spawner 
// namespace
