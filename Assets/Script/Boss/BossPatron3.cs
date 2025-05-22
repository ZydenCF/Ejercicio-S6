using UnityEngine;

public class BossPatron3 : IBossStrategy
{
    public void Ejecutar(Boss boss)
    {
        boss.Disparar();
    }
}