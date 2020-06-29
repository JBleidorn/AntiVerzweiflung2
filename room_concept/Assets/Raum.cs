using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raum : MonoBehaviour
{
    GameObject floor;
    GameObject ceiling;
    GameObject roof;
    GameObject wall1;
    GameObject wall2;
    GameObject image;
    GameObject partitionWall;

    Mesh meshWall;
    Mesh meshWall2;
    Mesh meshImage;
    Mesh meshPartitionWall;
    List<Vector3> verticeswall = new List<Vector3>();
    List<Vector3> normalswall = new List<Vector3>();
    List<Vector2> uvwall = new List<Vector2>();
    List<int> triangleswall = new List<int>();
    List<Vector3> verticeswall2 = new List<Vector3>();
    List<Vector3> normalswall2 = new List<Vector3>();
    List<Vector2> uvwall2 = new List<Vector2>();
    List<int> triangleswall2 = new List<int>();
    List<Vector3> verticesImage = new List<Vector3>();
    List<int> trianglesImage = new List<int>();
    List<Vector3> normalsImage = new List<Vector3>();
    List<Vector2> uvImage = new List<Vector2>();
    List<Vector3> verticespartition = new List<Vector3>();
    List<Vector3> normalspartition = new List<Vector3>();
    List<Vector2> uvpartition = new List<Vector2>();
    List<int> trianglespartition = new List<int>();
    
    public Material farbeFloor;
    public Material farbeRoof;
    public Material farbeCeiling;
    public Material farbeWall;
    public Material farbeWall2;
    public Material farbePartitionWall;

    Vector3 normal1;
    Vector3 normal2;
    Vector3 normal3;
    Vector3 normal4;
    Vector3 normal5;
    Vector3 normal6;
    Vector3 normal7;
    Vector3 normal8;
    Vector3 normal9;
    Vector3 normal10;
    Vector3 normal11;
    Vector3 normal12;
    Vector3 normal13;
    Vector3 normal14;
    Vector3 normal15;
    Vector3 normal16;

    float size;
    float s;
    int place;
    float height = 15.0f;

    int i = 0;
    public int podestHeight = 3;
    System.Random rand = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        s = 5;
        size = s * 5.0f; 
        place = rand.Next(Mathf.RoundToInt((int)-size+(4*s)), Mathf.RoundToInt((int)size-(4*s)));

        //Boden
        floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Boden";

        floor.transform.localScale = new Vector3(s, 1, s);
        var floorRenderer = floor.GetComponent<Renderer>();
        floorRenderer.material = farbeFloor; 
        //Debug.Log(floor.vertices);

        //Dach
        roof = GameObject.CreatePrimitive(PrimitiveType.Plane);
        roof.name = "Dach";

        roof.transform.localScale = new Vector3(s, 1, s);
        roof.transform.position += new Vector3(0, height , 0);
        var roofRenderer = roof.GetComponent<Renderer>();
        roofRenderer.material = farbeRoof; 
        
        //Zimmerdecke
        ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ceiling.name = "Zimmerdecke";

        ceiling.transform.localScale = new Vector3(s, 1, s);
        ceiling.transform.localRotation = Quaternion.Euler(180.0f, 0, 0);
        ceiling.transform.position += new Vector3(0, height , 0);
        
        
        var ceilingRenderer = ceiling.GetComponent<Renderer>();
        ceilingRenderer.material = farbeCeiling; 

        //äußere Wände
        wall1 = new GameObject();
        wall1.name = "Außenwände";
        wall1.AddComponent<MeshFilter>();
        wall1.AddComponent<MeshRenderer>();

        makeouterwalls(size);

        meshWall = new Mesh();
        wall1.GetComponent<MeshFilter>().mesh = meshWall;
        meshWall.RecalculateNormals();

        meshWall.vertices = verticeswall.ToArray();
        meshWall.triangles = triangleswall.ToArray();
        meshWall.normals = normalswall.ToArray();
        meshWall.uv = uvwall.ToArray();

        Renderer rendWall = wall1.GetComponent<Renderer>();
        rendWall.material = new Material(Shader.Find("Specular"));
        Texture wallTexture = Resources.Load("putz.jpg") as Texture;
        rendWall.material.mainTexture = wallTexture;
        //rendWall.material = farbeWall;

        //innere Wände
        wall2 = new GameObject();
        wall2.name = "Innenwände";
        wall2.AddComponent<MeshFilter>();
        wall2.AddComponent<MeshRenderer>();

        makeinnerwalls(size);

        meshWall2 = new Mesh();
        wall2.GetComponent<MeshFilter>().mesh = meshWall2;
        meshWall2.RecalculateNormals();

        meshWall2.vertices = verticeswall2.ToArray();
        meshWall2.triangles = triangleswall2.ToArray();
        meshWall2.normals = normalswall2.ToArray();
        meshWall2.uv = uvwall2.ToArray();

        Renderer rendWall2 = wall2.GetComponent<MeshRenderer>();
        rendWall2.material = new Material(Shader.Find("Specular"));
        Texture wall2Texture = Resources.Load("tapete.jpg") as Texture;
        rendWall2.material.mainTexture = wall2Texture;
        //rendWall2.material = farbeWall2;

        createArtObj();

        //Trennwände
        partitionWall = new GameObject();
        partitionWall.name = "Trennwände";
        partitionWall.AddComponent<MeshFilter>();
        partitionWall.AddComponent<MeshRenderer>();

        makepartitions(size, place);

        meshPartitionWall = new Mesh();
        partitionWall.GetComponent<MeshFilter>().mesh = meshPartitionWall;
        meshPartitionWall.RecalculateNormals();

        meshPartitionWall.vertices = verticespartition.ToArray();
        meshPartitionWall.triangles = trianglespartition.ToArray();
        meshPartitionWall.normals = normalspartition.ToArray();
        meshPartitionWall.uv = uvpartition.ToArray();

        Renderer rendPartition = partitionWall.GetComponent<MeshRenderer>();
        rendPartition.material = new Material(Shader.Find("Specular"));
        Texture partitionTexture = Resources.Load("tapete.jpg") as Texture;
        rendPartition.material.mainTexture = partitionTexture;
        //rendWall2.material = farbeWall2;
    }

    /*************** CREATE ART OBJECTS **********************/
    void createArtObj() {
        for(int i = 0; i < 10; i++){
            //int x = rand.Next(-10,10);
            //int z = rand.Next(-10,10);
            float x = -(size/2);
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

            x += 4;
        }

    }

        void createGallery(float x, int z){
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

    /*************** CREATE ROOM  **********************/
    public void makeouterwalls(float size){
        Vector3 a = new Vector3(size, 0, size);//0
        Vector3 b = new Vector3(size, 0, -size);//1
        Vector3 c = new Vector3(-size, 0, -size);//2
        Vector3 d = new Vector3(-size, 0, size);//3

        Vector3 e = new Vector3(size, height, size);//4
        Vector3 f = new Vector3(size, height, -size);//5
        Vector3 g = new Vector3(-size, height, -size);//6
        Vector3 h = new Vector3(-size, height, size);//7

        verticeswall.Add(a);
        verticeswall.Add(b);
        verticeswall.Add(c);
        verticeswall.Add(d);
        verticeswall.Add(e);
        verticeswall.Add(f);
        verticeswall.Add(g);
        verticeswall.Add(h);

        normal1 = getNormal(a, c, e);
        normal2 = getNormal(b, d, a);
        normal3 = getNormal(c, a, d);
        normal4 = getNormal(d, b, c);
        normal5 = getNormal(e, g, f);
        normal6 = getNormal(f, h, e);
        normal7 = getNormal(g, e, h);
        normal8 = getNormal(h, f, g);

        normalswall.Add(normal1);
        normalswall.Add(normal2);
        normalswall.Add(normal3);
        normalswall.Add(normal4);
        normalswall.Add(normal5);
        normalswall.Add(normal6);
        normalswall.Add(normal7);
        normalswall.Add(normal8);

        uvwall.Add(new Vector2(0,0));
        uvwall.Add(new Vector2(1,0));
        uvwall.Add(new Vector2(1,1));
        uvwall.Add(new Vector2(0,1));
        uvwall.Add(new Vector2(0,0));
        uvwall.Add(new Vector2(1,0));
        uvwall.Add(new Vector2(0,1));
        uvwall.Add(new Vector2(1,1));

        makeoutertriangles();
    }

    public void makeinnerwalls(float size){
        Vector3 a = new Vector3(size, 0, size);//0
        Vector3 b = new Vector3(size, 0, -size);//1
        Vector3 c = new Vector3(-size, 0, -size);//2
        Vector3 d = new Vector3(-size, 0, size);//3

        Vector3 e = new Vector3(size, height, size);//4
        Vector3 f = new Vector3(size, height, -size);//5
        Vector3 g = new Vector3(-size, height, -size);//6
        Vector3 h = new Vector3(-size, height, size);//7

        verticeswall2.Add(a);
        verticeswall2.Add(b);
        verticeswall2.Add(c);
        verticeswall2.Add(d);
        verticeswall2.Add(e);
        verticeswall2.Add(f);
        verticeswall2.Add(g);
        verticeswall2.Add(h);

        normal1 = getNormal(a, c, e);
        normal2 = getNormal(b, d, a);
        normal3 = getNormal(c, a, d);
        normal4 = getNormal(d, b, c);
        normal5 = getNormal(e, g, f);
        normal6 = getNormal(f, h, e);
        normal7 = getNormal(g, e, h);
        normal8 = getNormal(h, f, g);

        normalswall2.Add(normal1);
        normalswall2.Add(normal2);
        normalswall2.Add(normal3);
        normalswall2.Add(normal4);
        normalswall2.Add(normal5);
        normalswall2.Add(normal6);
        normalswall2.Add(normal7);
        normalswall2.Add(normal8);

        uvwall2.Add(new Vector2(0,0));
        uvwall2.Add(new Vector2(1,0));
        uvwall2.Add(new Vector2(0,1));
        uvwall2.Add(new Vector2(1,1));
        uvwall2.Add(new Vector2(0,0));
        uvwall2.Add(new Vector2(1,0));
        uvwall2.Add(new Vector2(1,1));
        uvwall2.Add(new Vector2(0,1));

        makeinnertriangles();
    }

    public void makeoutertriangles(){
        //left
        triangleswall.Add(0);
        triangleswall.Add(1);
        triangleswall.Add(4);
        triangleswall.Add(1);
        triangleswall.Add(5);
        triangleswall.Add(4);

        //front
        triangleswall.Add(1);
        triangleswall.Add(2);
        triangleswall.Add(6);
        triangleswall.Add(1);
        triangleswall.Add(6);
        triangleswall.Add(5);

        //right
        triangleswall.Add(3);
        triangleswall.Add(7);
        triangleswall.Add(6);
        triangleswall.Add(2);
        triangleswall.Add(3);
        triangleswall.Add(6);

        //back
        triangleswall.Add(0);
        triangleswall.Add(4);
        triangleswall.Add(3);
        triangleswall.Add(3);
        triangleswall.Add(4);
        triangleswall.Add(7);
    }

    public void makeinnertriangles(){
        //left
        triangleswall2.Add(0);
        triangleswall2.Add(4);
        triangleswall2.Add(1);
        triangleswall2.Add(1);
        triangleswall2.Add(4);
        triangleswall2.Add(5);

        //front
        triangleswall2.Add(1);
        triangleswall2.Add(6);
        triangleswall2.Add(2);
        triangleswall2.Add(1);
        triangleswall2.Add(5);
        triangleswall2.Add(6);

        //right
        triangleswall2.Add(3);
        triangleswall2.Add(6);
        triangleswall2.Add(7);
        triangleswall2.Add(2);
        triangleswall2.Add(6);
        triangleswall2.Add(3);

        //back
        triangleswall2.Add(0);
        triangleswall2.Add(3);
        triangleswall2.Add(4);
        triangleswall2.Add(3);
        triangleswall2.Add(7);
        triangleswall2.Add(4);
    }

    public void makepartitions(float size, float place){
        //Wand 1
        Vector3 a = new Vector3(place, 0, size);//0
        Vector3 b = new Vector3(place, 0, size - 8);//1
        Vector3 c = new Vector3(place, height, size);//2
        Vector3 d = new Vector3(place, height, size - 8);//3

        //Wand 2
        Vector3 e = new Vector3(place, 0, -size);//4
        Vector3 f = new Vector3(place, 0, -size + 8);//5
        Vector3 g = new Vector3(place, height, -size);//6
        Vector3 h = new Vector3(place, height, -size + 8);//7

        //Wand 3
        Vector3 i = new Vector3(size, 0, place);//4
        Vector3 j = new Vector3(size - 8, 0, place);//5
        Vector3 k = new Vector3(size, height, place);//6
        Vector3 l = new Vector3(size - 8, height, place);//7

        //Wand 4
        Vector3 m = new Vector3(-size, 0, place);//4
        Vector3 n = new Vector3(-size + 8, 0, place);//5
        Vector3 o = new Vector3(-size, height, place);//6
        Vector3 p = new Vector3(-size + 8, height, place);//7

        verticespartition.Add(a);
        verticespartition.Add(b);
        verticespartition.Add(c);
        verticespartition.Add(d);
        verticespartition.Add(e);
        verticespartition.Add(f);
        verticespartition.Add(g);
        verticespartition.Add(h);
        verticespartition.Add(i);
        verticespartition.Add(j);
        verticespartition.Add(k);
        verticespartition.Add(l);
        verticespartition.Add(m);
        verticespartition.Add(n);
        verticespartition.Add(o);
        verticespartition.Add(p);

        normal1 = getNormal(a, c, b); normalspartition.Add(normal1);
        normal2 = getNormal(b, d, a); normalspartition.Add(normal2);
        normal3 = getNormal(c, a, d); normalspartition.Add(normal3);
        normal4 = getNormal(d, b, c); normalspartition.Add(normal4);
        normal5 = getNormal(e, g, f); normalspartition.Add(normal5);
        normal6 = getNormal(f, h, e); normalspartition.Add(normal6);
        normal7 = getNormal(g, e, h); normalspartition.Add(normal7);
        normal8 = getNormal(h, f, g); normalspartition.Add(normal8);
        normal9 = getNormal(i, k, j); normalspartition.Add(normal9);
        normal10 = getNormal(j, l, i); normalspartition.Add(normal10);
        normal11 = getNormal(k, i, l); normalspartition.Add(normal11);
        normal12 = getNormal(l, j, k); normalspartition.Add(normal12);
        normal13 = getNormal(m, o, n); normalspartition.Add(normal13);
        normal14 = getNormal(n, p, m); normalspartition.Add(normal14);
        normal15 = getNormal(o, m, p); normalspartition.Add(normal15);
        normal16 = getNormal(p, n, o); normalspartition.Add(normal16);

        uvwall2.Add(new Vector2(0,0));
        uvwall2.Add(new Vector2(1,0));
        uvwall2.Add(new Vector2(0,1));
        uvwall2.Add(new Vector2(1,1));
        uvwall2.Add(new Vector2(0,0));
        uvwall2.Add(new Vector2(1,0));
        uvwall2.Add(new Vector2(1,1));
        uvwall2.Add(new Vector2(0,1));

        makepartitiontriangles();
    }

    public void makepartitiontriangles(){
        //left
        trianglespartition.Add(0);
        trianglespartition.Add(3);
        trianglespartition.Add(1);
        trianglespartition.Add(0);
        trianglespartition.Add(2);
        trianglespartition.Add(3);

        trianglespartition.Add(0);
        trianglespartition.Add(1);
        trianglespartition.Add(3);
        trianglespartition.Add(0);
        trianglespartition.Add(3);
        trianglespartition.Add(2);

        //front
        trianglespartition.Add(4);
        trianglespartition.Add(6);
        trianglespartition.Add(5);
        trianglespartition.Add(5);
        trianglespartition.Add(6);
        trianglespartition.Add(7);

        trianglespartition.Add(4);
        trianglespartition.Add(5);
        trianglespartition.Add(6);
        trianglespartition.Add(5);
        trianglespartition.Add(7);
        trianglespartition.Add(6);

        //right
        trianglespartition.Add(8);
        trianglespartition.Add(9);
        trianglespartition.Add(10);
        trianglespartition.Add(9);
        trianglespartition.Add(11);
        trianglespartition.Add(10);

        trianglespartition.Add(8);
        trianglespartition.Add(10);
        trianglespartition.Add(9);
        trianglespartition.Add(9);
        trianglespartition.Add(10);
        trianglespartition.Add(11);

        //back
        trianglespartition.Add(12);
        trianglespartition.Add(13);
        trianglespartition.Add(14);
        trianglespartition.Add(13);
        trianglespartition.Add(15);
        trianglespartition.Add(14);

        trianglespartition.Add(12);
        trianglespartition.Add(14);
        trianglespartition.Add(13);
        trianglespartition.Add(13);
        trianglespartition.Add(14);
        trianglespartition.Add(15);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
