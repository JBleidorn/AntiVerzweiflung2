using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platzhalter : MonoBehaviour
{
    public Texture tex;
    GameObject image;
    Mesh meshImage;
    List<Vector3> verticesImage = new List<Vector3>();
    List<int> trianglesImage = new List<int>();
    List<Vector3> normalsImage = new List<Vector3>();
    List<Vector2> uvImage = new List<Vector2>();
    System.Random rand = new System.Random();
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < 10; i++){
            int x = rand.Next(-10,10);
            int z = rand.Next(-10,10);
            Texture texture = Resources.Load(rand.Next(1,4).ToString()) as Texture;
            
            //Cube
            GameObject cube =  GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.Translate(x,0,z);
            
            Renderer rendCube = cube.GetComponent<Renderer>();
            rendCube.material.mainTexture = texture;

            //image
            image = new GameObject();
            image.AddComponent<MeshFilter>();
            image.AddComponent<MeshRenderer>();
            
            Renderer rendImage = image.GetComponent<Renderer>();
            rendImage.material.mainTexture = texture;

            createGallery(x,z);

            meshImage = new Mesh();
            image.GetComponent<MeshFilter>().mesh = meshImage;


            meshImage.vertices = verticesImage.ToArray();
            meshImage.triangles = trianglesImage.ToArray();
            meshImage.normals = normalsImage.ToArray();
            meshImage.uv = uvImage.ToArray();
        }

        

    }

    void createGallery(int x, int z){
        Vector3 a = new Vector3(x,2,z);
        Vector3 b = new Vector3(x,2+1,z);
        Vector3 c = new Vector3(x+1,2,z);
        Vector3 d = new Vector3(x+1,2+1,z);

        createImage(a,b,c,d);
        createImage(a,c,b,d);
    }

    void createImage(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
        Vector3 normal = getNormal(c, b, a);

        verticesImage.Add(a); normalsImage.Add(normal); uvImage.Add(new Vector2(0,0));
        verticesImage.Add(b); normalsImage.Add(normal); uvImage.Add(new Vector2(1,0));
        verticesImage.Add(c); normalsImage.Add(normal); uvImage.Add(new Vector2(0,1));
        verticesImage.Add(d); normalsImage.Add(normal); uvImage.Add(new Vector2(1,1));

        trianglesImage.Add(i);
        trianglesImage.Add(i+2);
        trianglesImage.Add(i+1);
        trianglesImage.Add(i+2);
        trianglesImage.Add(i+3);
        trianglesImage.Add(i+1);

        i += 4; 
    }

    private Vector3 getNormal(Vector3 a, Vector3 b, Vector3 c){
        Vector3 ba = new Vector3(b.x, b.y, b.z);
        Vector3 ca = new Vector3(c.x, c.y, c.z);
        ba = b-a;
        ca = c-a;
        return (Vector3.Cross(ba, ca));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
