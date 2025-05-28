using UnityEngine;

public class BossPatron4 : IBossStrategy
{
    private bool inicializado;

    public void Ejecutar(Boss boss)
    {
        if (!inicializado)
        {
            Vector3 direccionInicial = (boss.objetivo.position - boss.transform.position).normalized;
            boss.EstablecerDireccion(direccionInicial);
            inicializado = true;
        }


        boss.transform.position += boss.ObtenerDireccion() * 20f * Time.deltaTime;
    }
}

