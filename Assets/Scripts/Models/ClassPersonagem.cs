using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ClassPersonagem
{
    private static string CharacterImageLocation = "/Illustrations/CharacterPortraits/SchoolStaff";

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
        string nameWithoutAccentuation;

        foreach (var expressao in expressoes)
        {
            if (images.ContainsKey(expressao)) continue;

            if (nome == "André")
                nameWithoutAccentuation = "Andre";
            else if (nome == "Valéria")
                nameWithoutAccentuation = "Valeria";
            else
                nameWithoutAccentuation = nome;

            var filePath = CharacterImageLocation + "/" + nameWithoutAccentuation + "/" + expressao + ".png"; //Get path of folder
            Debug.Log(filePath);
            if (!BetterStreamingAssets.FileExists(filePath)) continue;
            Texture2D tex = new Texture2D(2, 2);
            
            using (var stream = BetterStreamingAssets.OpenRead(filePath))
            {
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                tex.LoadImage(ms.ToArray());
                images[expressao] = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f),
                    100.0f);
            }
        }
    }
}