using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platzhalter : MonoBehaviour
{
    public Texture tex;
    public Material MatBl;
    GameObject image;
    Mesh meshImage;
    List<Vector3> verticesImage = new List<Vector3>();
    List<int> trianglesImage = new List<int>();
    List<Vector3> normalsImage = new List<Vector3>();
    List<Vector2> uvImage = new List<Vector2>();
    System.Random rand = new System.Random();

    //public GameObject myModel;

    int i = 0;
    public int podestHeight = 5;
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0, x = -10; i < 10; i++, x += 4){
            //int x = rand.Next(-10,10);
            //int z = rand.Next(-10,10);
            int z = 1;
            Texture texture = Resources.Load(rand.Next(1,4).ToString()) as Texture;
            
            //Podest
            GameObject cube =  GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.Translate(x,podestHeight/2,z);
            cube.transform.localScale = new Vector3(1, podestHeight, 1);
            Renderer rendCube = cube.GetComponent<Renderer>();
            rendCube.material.mainTexture = texture;

            // create 3d Art Object 
            GameObject artObj = (GameObject)Resources.Load(rand.Next(1,3).ToString(), typeof(GameObject));
            artObj.name = "3d Art Obj";
            artObj.transform.position = new Vector3(x,podestHeight-0.5f,z);
            Instantiate(artObj);

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

        //createImage(a,b,c,d);
       // createImage(a,c,b,d);
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
