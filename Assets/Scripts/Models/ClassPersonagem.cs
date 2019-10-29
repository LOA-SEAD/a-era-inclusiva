using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ClassPersonagem
{
    private static string CharacterImageLocation =
        Application.streamingAssetsPath + "/Illustrations/CharacterPortraits/SchoolStaff/";

    public List<ClassFala> dialogos;

    public List<string> expressoes;
    public string nome;
    public Dictionary<string, Sprite> images;

    public void LoadExpressions()
    {
        if (images == null)
        {
            images = new Dictionary<string, Sprite>();
        }
        foreach (var expressao in expressoes)
        {
            if(images.ContainsKey(expressao)) return;
            var filePath = Path.Combine(CharacterImageLocation, nome, expressao+".png"); //Get path of folder

            byte[] pngBytes = File.ReadAllBytes(filePath);

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);

            images[expressao] = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),
                100.0f);
        }
    }
}