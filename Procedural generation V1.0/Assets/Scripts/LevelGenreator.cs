using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenreator : MonoBehaviour
{
    GameObject cube;
    int width = 50; // width of terrain // x in unity
    int length = 250; // length of terrain // z in unity

    float amplitude = 2.5f;
    float frequency = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x <= width - 1; x++)
        {
            for(int z = 0; z <= length - 1; z++)
            {

            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Vector3 randposition = new Vector3();

            randposition.x = x;
            //randposition.y = Random.Range(-0.1f, 0.1f);
            //randposition.y = Mathf.Sin(((float)x+z)/ width*(Mathf.PI * 2));
            randposition.y = amplitude * Mathf.PerlinNoise((float)x / frequency, (float)z / frequency);
            randposition.z = z;

            cube.transform.position = randposition;
                
            }
        }
        //instantiate a cube
        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    // Update is called once per frame
    void Update()
    {
       // gameObject.transform.Translate(Vector3.up * Time.deltaTime); // moves the level generator
        
        // move a cube
        cube.transform.Translate(Vector3.up * Time.deltaTime);

    }
}
