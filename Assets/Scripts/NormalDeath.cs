using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeath : MonoBehaviour
{
    public GameObject[] deathPrefabs;
    public float minSize = 0.2f;
    public float maxSize = 0.3f;
    private GameObject prefab;
    private ParticleSystem particles;

    void Start ()
    {
        gameObject.SetActive(false);
        particles = gameObject.GetComponent<ParticleSystem>();

        int total = deathPrefabs.Length;

        prefab = Instantiate(deathPrefabs[Random.Range(0, total)]);
        float size = Random.Range(minSize, maxSize);
        prefab.transform.localScale = new Vector3(size, size, 1);
        prefab.transform.Rotate(0, 0, Random.Range(0, 360f));
        
        prefab.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.8f, 1f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), 0.2f);
    }

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        prefab.transform.position = transform.position;
        particles.Play();
        gameObject.SetActive(true);
    }
}
