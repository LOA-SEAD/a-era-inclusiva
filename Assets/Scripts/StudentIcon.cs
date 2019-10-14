using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StudentIcon : MonoBehaviour
{
    private ClassAluno _student;
    public Image image;
    public ClassAluno Student
    {
        get => _student;
        set { _student = value;
            Load();
        }
    }

    private void Load()
    {
        //image.color = new Color(1,1,1,0);
        
            var filePath = Application.streamingAssetsPath + "/Illustrations/CharacterPortraits/Students/"+_student.id+".png";  //Get path of folder
 
            byte[] pngBytes = System.IO.File.ReadAllBytes(filePath);
 
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);
 
            Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
 
            image.sprite = fromTex;
            //image.CrossFadeAlpha(1f, 0.1f, false);
        }


}