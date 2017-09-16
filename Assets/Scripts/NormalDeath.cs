using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeath : MonoBehaviour
{
    public GameObject[] deathPrefabs;
    public float minSize = 0.2f;
    public float maxSize = 0.3f;

	void Start ()
    {
        int total = deathPrefabs.Length;
        
        GameObject prefab = Instantiate(deathPrefabs[Random.Range(0, total)]);
        prefab.transform.position = transform.position;
        float size = Random.Range(minSize, maxSize);
        prefab.transform.localScale = new Vector3(size, size, 1);
        prefab.transform.Rotate(0, 0, Random.Range(0, 360f));
        
        prefab.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.8f, 1f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), 0.2f);

        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
