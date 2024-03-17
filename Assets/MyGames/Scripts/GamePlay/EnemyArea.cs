using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    private bool isInEnemyArea = false;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBGM(AUDIO.BGM_SNIPER3D);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_DRAMACIC);
            }
            isInEnemyArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_SNIPER3D);
            }
            isInEnemyArea = false;
        }
    }
}
