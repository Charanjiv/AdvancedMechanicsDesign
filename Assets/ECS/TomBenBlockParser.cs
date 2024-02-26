
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;


public class TomBenBlockParser : MonoBehaviour
{
    private ParsedBlock currentBlock = new ParsedBlock();
    private List<ParsedBlock> blocks = new List<ParsedBlock>();

    private enum ParserState
    {
        InsideBlockBody, InsideBlockHeader, OutsideBlock
    };

    private ParserState state = ParserState.OutsideBlock;

    private string charBuffer = "";
    private int charIndex = 0;

    private string fileContent = "";



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
        foreach (var token in tokens)
        {
            if (BufferHas(token)) return true;
        }
        return false;
    }

    public List<ParsedBlock> ParseFromFile(string filePath)
    {
        charIndex = 0;
        charBuffer = "";
        state = ParserState.OutsideBlock;

        fileContent = File.ReadAllText(filePath);

        while (!ReachedEnd())
        {
            if (state == ParserState.OutsideBlock)
            {
                ParseOutsideBlock();
            }
            else if (state == ParserState.InsideBlockBody)
            {
                ParseInsideBlock();
            }
            else if (state == ParserState.InsideBlockHeader)
            {
                ParseInsideBlockHeader();
            }
        }

        
        return blocks;
    }

    private void ParseOutsideBlock()
    {
        while (!BufferHasAny( "cluster", "type", "wave") && !ReachedEnd())
            NextChar();

        if (ReachedEnd())
            return;
        currentBlock.type = GetLastMatchedBlockType();
       // blocks.Add(currentBlock);

        ChangeState(ParserState.InsideBlockHeader);
    }

    private void ParseInsideBlock()
    {
        while (!BufferHasAny("_Ben") && !ReachedEnd())
            NextChar();


        if (ReachedEnd())
            return;

        currentBlock.type = GetLastMatchedBlockType();


        //string regexPattern = "(health | speed | damage)=>(\d*)";
        //Regex regex = new Regex(regexPattern);
        //Match regexMatch = regex.Match(inputFile)
        //Regex Content = new Regex(@"_Tom([\w\s\W]*)_Ben", RegexOptions.IgnoreCase);
        Regex Content = new Regex(@"(?:\s*(.*)\s*)");
        Match ContentMatch = Content.Match(charBuffer);
        currentBlock.content = ContentMatch.Groups[1].Value;

        

        ChangeState(ParserState.OutsideBlock);

    }

    private void ParseInsideBlockHeader()
    {
        //while (!BufferHasAny("_Tom", "_Ben") && !ReachedEnd())
        while(!BufferHasAny("_Tom") && !ReachedEnd())
        NextChar();

        if (ReachedEnd())
            return;


        Regex ID = new Regex(@"(\d+)");
        Regex Name = new Regex(@"(?:\((.*)\))");

        Match MatchID = ID.Match(charBuffer);
        Match NameMatch = Name.Match(charBuffer);
        
        int.TryParse(MatchID.Groups[1].Value, out int result);

        currentBlock.name = NameMatch.Groups[1].Value;
        currentBlock.id = result;
        
        ChangeState(ParserState.InsideBlockBody);

        

        //if (ReachedEnd())
        //    return;

        //currentBlock.type = GetLastMatchedBlockType();
        

    }

    private string GetLastMatchedBlockType()
    {
        string lastMatched = null;

        lastMatched ??= (BufferHas("cluster") ? "cluster" : null);
        lastMatched ??= (BufferHas("wave") ? "wave" : null);
        lastMatched ??= (BufferHas("type") ? "type" : null);
        
        return lastMatched;
    }







}
