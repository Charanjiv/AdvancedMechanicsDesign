using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

public class ParserExample : MonoBehaviour
{
    [SerializeField] private string inputFile;
    public List<ParsedBlock> blocks;


    private void Awake()
    {
        if (!File.Exists(inputFile))
            throw new UnityException("Can't open file");

        TomBenBlockParser blockParser = new TomBenBlockParser();
        blocks = blockParser.ParseFromFile(inputFile);

    }

    
}



