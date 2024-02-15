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

        ParserExample blockParser = new ParserExample();
        blocks = blockParser.ParseFromFile(inputFile);

    }

    private ParsedBlock currentBlock = new ParsedBlock();
    private List<ParsedBlock> block = new List<ParsedBlock>();

    private enum ParserState
    {
        InsideBlockBody, InsideBlockHeader, OutsideBlock
    };

    private ParserState state = ParserState.OutsideBlock;

    private string charBuffer = "";
    private int charIndex = 0;

    private string fileContent= "";



    private void ClearBuffer() => charBuffer = "";

    private bool ReachedEnd() => charIndex >= fileContent.Length;

    private char NextChar()
    {
        charBuffer += fileContent[charIndex];
        return fileContent[charIndex++];
    }

    private void ChangeState(ParserState state)
    {
        this.state = state;
        ClearBuffer();
    }

    private bool BufferHas(string token) => charBuffer.EndsWith(token);

    private bool BufferHasAny(params string[] tokens)
    {
        foreach(var token in tokens)
        {
            if(BufferHas(token)) return true;
        }
        return false;
    }

    public List<ParsedBlock> ParseFromFile(string filePath)
    {
        charIndex = 0;
        charBuffer = "";
        state = ParserState.OutsideBlock;

        fileContent = File.ReadAllText(filePath);

        while(!ReachedEnd())
        {
            if(state == ParserState.OutsideBlock)
            {
                //ParseOutsideBlock();
            }
            else if (state == ParserState.InsideBlockBody)
            {
                //ParseInsideBlock();
            }
            else if(state == ParserState.InsideBlockHeader)
            {
                //ParseInsideBlockHeader();
            }
        }

        return blocks;
    }

    private void ParseOutsideBlock()
    {
        while (!BufferHasAny("cluster", "type", "wave") && !ReachedEnd())
            NextChar();

        if (ReachedEnd())
            return;
    }






}
[System.Serializable]
public struct ParsedBlock
{
    public string type;
    public string name;
    public int id;
    public string content;

    public override string ToString() =>
        $"ParesdBlock(type={type}, id ={id}, name = {name}, content={content})";
}



