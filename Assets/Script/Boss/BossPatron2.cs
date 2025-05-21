using UnityEngine;

public class BossPatron2 : IBossStrategy
{
    private bool inicializado;

    public void Ejecutar(Boss boss)
    {
        if (!inicializado)
        {
            Vector3 direccionInicial = new Vector3(1f, 0f, 1f).normalized;
            boss.EstablecerDireccion(direccionInicial);
            inicializado = true;
        }
        boss.transform.position += boss.ObtenerDireccion() * boss.velocidad * Time.deltaTime;
    }
}

