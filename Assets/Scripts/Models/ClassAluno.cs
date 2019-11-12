using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ClassAluno
{
    private static string CharacterPortraitLocation =  "/Illustrations/CharacterPortraits/Students/";
    private static string CharacterImageLocation = "/Illustrations/Character/";

    public string deficiencia;
    public string descricao;
    public int id;
    public bool importante;
    public string nome;
    public Sprite portrait;
    public Sprite image;
    public Sprite LoadPortrait()
    {
        if (portrait != null) return portrait;
        var filePath = CharacterPortraitLocation + id + ".png"; //Get path of folder

        using (var stream = BetterStreamingAssets.OpenRead(filePath))
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Texture2D tex = new Texture2D(100, 100);

            tex.LoadImage(ms.ToArray());
            portrait = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0.5f),
                100.0f);
        }
        return portrait;
    }
    public Sprite LoadImage()
    {
        if (image != null) return image;
        var filePath = CharacterImageLocation + id + ".png"; //Get path of folder

        using (var stream = BetterStreamingAssets.OpenRead(filePath))
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Texture2D tex = new Texture2D(100, 100);

            tex.LoadImage(ms.ToArray());
            image = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0.5f),
                100.0f);
        }

        return image;
    }
    
}
