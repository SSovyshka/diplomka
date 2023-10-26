using System.Collections;
using UnityEngine;

public class Flober : Pistol
{
    private Coroutine reloadCoroutine; // ��������� ���������� ��� �������� ������ �� �������� �����������
    [SerializeField] private AudioSource reloadBulletSound;

    public override void Reload()
    {
        if (ammo < ammoCapacity)
        {
            isReloading = true; // ������������� ���� �����������
            
            // ����� ���������� ����������� ������� ����
            StartCoroutine(CompleteReload());
        }
    }

    private IEnumerator CompleteReload()
    {
        if (ammo < ammoCapacity)
        {
            reloadBulletSound.Play();
            yield return new WaitForSeconds(reloadTime); // ���� ����� �����������
            ammo++; // �������� �� ����� ����
            StartCoroutine(CompleteReload());
        }
        else
        {
            reloadSound.Play();
            yield return new WaitForSeconds(reloadTime);
            isReloading = false; // ���������� ���� �����������
        }
    }
}
