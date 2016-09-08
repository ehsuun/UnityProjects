using UnityEngine;
using System.Collections;
using System;
using System.IO;



public class BinaryReadUnity : MonoBehaviour {

    public string file = "AppSettings.dat";
    public Texture2D tex;
    public int width;
    public int height;

    public TextAsset asset;


    // Use this for initialization
    void Start () {
        tex = new Texture2D(width, height, TextureFormat.RGBAFloat, false);

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
            tex.SetPixel(x, y, pix);
        }
        tex.Apply();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
