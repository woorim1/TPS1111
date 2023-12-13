using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void VFXPlay(GameObject particle, Vector3 position, float time)
    {
        GameObject effect = Instantiate(particle, position, Camera.main.transform.rotation);

        Destroy(effect, time);
    }

    public void VFXPlay(GameObject particle, Vector3 position, float time, GameObject Parent)
    {
        GameObject effect = Instantiate(particle, position, Camera.main.transform.rotation, Parent.transform);

        Destroy(effect, time);
    }

    public void VFXPlay(GameObject particle, Vector3 position, Transform target, float time)
    {
        Vector3 dir = target.transform.position - transform.position;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        GameObject effect = Instantiate(particle, position, rot);

        Destroy(effect, time);
    }
}
