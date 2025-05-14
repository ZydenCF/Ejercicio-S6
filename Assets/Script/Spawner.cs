using UnityEngine;
using System.Collections.Generic;
using System;

public class Spawner : MonoBehaviour, IObserver
{
    public GameObject[] enemigos;
    public Transform[] puntosSpawn;
    public float tiempoSpawn;
    private float tiempoActual;
    private int indiceSecuencia = 0;
    private Func<int, int> calcularFibonacci;

    void Start()
    {
        Dictionary<int, int> memo = new Dictionary<int, int>();
        calcularFibonacci = null;
        calcularFibonacci = (n) => {
            if (n <= 0) return 0;
            if (n == 1) return 1;
            if (memo.ContainsKey(n)) return memo[n];
            int result = calcularFibonacci(n - 1) + calcularFibonacci(n - 2);
            memo[n] = result;
            return result;
        };

        tiempoActual = 0;
        if (GameManager.instancia != null)
        {
            GameManager.instancia.Adjuntar(this);
        }

        GameManager.OnNivelCambiado += CambiarDificultad;
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
            int fibValue = calcularFibonacci(indiceSecuencia % 10);
            int tipo = fibValue % enemigos.Length;

            int punto = UnityEngine.Random.Range(0, puntosSpawn.Length);
            GameObject nuevoEnemigo = Instantiate(enemigos[tipo],
                puntosSpawn[punto].position, Quaternion.identity);
            nuevoEnemigo.tag = "Enemigo";

            indiceSecuencia++;
        }
    }

    private void CambiarDificultad(int nuevoNivel)
    {
        if (tiempoSpawn > 0.5f)
        {
            tiempoSpawn -= 0.1f;
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

        GameManager.OnNivelCambiado -= CambiarDificultad;
    }
}