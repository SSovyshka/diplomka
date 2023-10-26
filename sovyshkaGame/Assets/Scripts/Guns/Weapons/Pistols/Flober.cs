using System.Collections;
using UnityEngine;

public class Flober : Pistol
{
    private Coroutine reloadCoroutine; // Добавляем переменную для хранения ссылки на корутину перезарядки
    [SerializeField] private AudioSource reloadBulletSound;

    public override void Reload()
    {
        if (ammo < ammoCapacity)
        {
            isReloading = true; // Устанавливаем флаг перезарядки
            
            // После завершения перезарядки сбросим флаг
            StartCoroutine(CompleteReload());
        }
    }

    private IEnumerator CompleteReload()
    {
        if (ammo < ammoCapacity)
        {
            reloadBulletSound.Play();
            yield return new WaitForSeconds(reloadTime); // Ждем время перезарядки
            ammo++; // Заряжаем по одной пуле
            StartCoroutine(CompleteReload());
        }
        else
        {
            reloadSound.Play();
            yield return new WaitForSeconds(reloadTime);
            isReloading = false; // Сбрасываем флаг перезарядки
        }
    }
}
