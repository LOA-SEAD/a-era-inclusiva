using System;
using System.Collections.Generic;

[Serializable]
public class ClassFala
{
    public static string padrao = "sereno";

    public List<string> frases;
    public string local;
    public List<string> expressoes;

    // Nova lista: um áudio para cada frase
    public List<string> audios;

    public bool introducao;

    public string GetFrase(int index)
    {
        if (frases == null || index < 0 || index >= frases.Count)
        {
            return "";
        }

        return frases[index];
    }

    public string GetExpressao(int index)
    {
        if (expressoes == null || index < 0 || index >= expressoes.Count)
        {
            return padrao;
        }

        return expressoes[index];
    }

    public string GetAudio(int index)
    {
        if (audios == null || index < 0 || index >= audios.Count)
        {
            return "";
        }

        return audios[index];
    }
}