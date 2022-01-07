using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Utils
{
    static public string RandomName(int length)
    {
        string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder sb = new StringBuilder(length);

        while(sb.Length < length) {
            sb.Append(str[Random.Range(0, str.Length)]);
        }

        return sb.ToString();
        // return new string(Enumerable.Repeat(str.Length, length).Select(s => str[Random.Range(0, str.Length)]).ToArray());
    }

    static public T RandomThreat<T>() where T : System.Enum => (T)System.Enum.Parse(typeof(T), Random.Range(0, System.Enum.GetValues(typeof(T)).Length).ToString());
}
