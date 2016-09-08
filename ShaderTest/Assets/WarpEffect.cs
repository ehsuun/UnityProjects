using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(RealtimeCubemap))]
public class WarpEffect : MonoBehaviour {

    public Material mat;
    public Shader shader;
    public Texture2D warpMap;
    public RenderTexture tex;

    public string file = "AppSettings.dat";
    public int width;
    public int height;

    public TextAsset asset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!tex)
        {
            SetCalibrationTex();
            tex = GetComponent<RealtimeCubemap>().rtex;
            if (tex)
            {
                mat = new Material(shader);
                mat.SetTexture("_WarpTex", warpMap);
                mat.SetTexture("_Cube", tex);
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat) Graphics.Blit(source, destination, mat);
    }




    // Use this for initialization
    void SetCalibrationTex()
    {
        warpMap = new Texture2D(width, height, TextureFormat.RGBAFloat, false);

        Stream s = new MemoryStream(asset.bytes);
        BinaryReader br = new BinaryReader(s);
        for (int i = 0; i < width * height; i++)
        {
            float r = br.ReadSingle();
            float g = br.ReadSingle();
            float b = br.ReadSingle();
            float a = br.ReadSingle();



            Color pix = new Color(r, g, b, a);


            int x = i % width;
            int y = Mathf.FloorToInt((float)i / (float)width);
            warpMap.SetPixel(x, y, pix);
        }
        warpMap.Apply();

    }

}
