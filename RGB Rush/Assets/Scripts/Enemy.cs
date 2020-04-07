using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform earth = null;
    [SerializeField] float speed = 1;
    [SerializeField] Material[] materials = null;
    [SerializeField] ParticleSystem[] explosionParticles;

    private Vector3 direction;
    private Vector3 rotation;
    private Material selfMaterial;
    private int materialIndex;

    // Start is called before the first frame update
    void Start()
    {
        direction = (earth.position - transform.position).normalized;
        rotation = RandomRotation(2.5f);
        materialIndex = Random.Range(0, materials.Length);
        selfMaterial = materials[materialIndex];
        gameObject.GetComponent<MeshRenderer>().material = selfMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = transform.position + direction * speed * Time.deltaTime;
        gameObject.transform.Rotate(rotation);
    }

    private Vector3 RandomRotation(float max)
    {
        float x = Random.Range(-max, max);
        float y = Random.Range(-max, max);
        float z = Random.Range(-max, max);

        return new Vector3(x, y, z);
    }

    public bool DestroyMe(float R, float G, float B)
    {
        if(selfMaterial.color.r == R && selfMaterial.color.g == G && selfMaterial.color.b == B)
        {
            Destroy(gameObject);
            
            return true;
        }
        else return false;
    }

    private void OnDestroy()
    {
        Instantiate(explosionParticles[materialIndex], gameObject.transform.position, explosionParticles[materialIndex].transform.rotation);
    }
}
