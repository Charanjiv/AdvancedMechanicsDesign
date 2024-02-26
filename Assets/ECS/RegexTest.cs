using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class RegexTest : MonoBehaviour
{
    [SerializeField] private string inputText;
    [SerializeField] private string regexPattern;

    private void OnValidate()
    {
        Regex regex = new Regex(regexPattern);

        Match regexMatch = regex.Match(inputText);

        if(regexMatch.Success)
        {
            print($"Matched text {regexMatch.Groups[0]}");

            for(int i = 1; i < regexMatch.Groups.Count; i++)
            {
                print($"Capture group {i} = {regexMatch.Groups[i].Value}");
            }
        }
    }
}
