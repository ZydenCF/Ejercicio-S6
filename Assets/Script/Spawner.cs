using UnityEngine;

public class Spawner : MonoBehaviour, IObserver
{
    public GameObject[] enemigos;
    public Transform[] puntosSpawn;
    public float tiempoSpawn;
    private float tiempoActual;

    void Start()
    {
        tiempoActual = 0;

        if (GameManager.instancia != null)
        {
            GameManager.instancia.Adjuntar(this);
        }
    }

    void Update()
    {
        tiempoActual += Time.deltaTime;

        if (tiempoActual >= tiempoSpawn)
        {
            tiempoActual = 0;
            GenerarEnemigo();
        }
    }

    void GenerarEnemigo()
    {
        if (enemigos.Length > 0 && puntosSpawn.Length > 0)
        {
            int tipo = Random.Range(0, enemigos.Length);
            int punto = Random.Range(0, puntosSpawn.Length);

            GameObject nuevoEnemigo = Instantiate(enemigos[tipo], puntosSpawn[punto].position, Quaternion.identity);
            nuevoEnemigo.tag = "Enemigo";
        }
    }

    public void Ejecutar(ISubject sujeto)
    {
        if (sujeto is GameManager)
        {
            if (tiempoSpawn > 0.5f)
            {
                tiempoSpawn -= 0.2f;
            }
        }
    }

    void OnDestroy()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.Quitar(this);
        }
    }
}
