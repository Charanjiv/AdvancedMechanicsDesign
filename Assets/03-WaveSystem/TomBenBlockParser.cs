using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

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
        blocks.Add(currentBlock);

        //Regex Type = new Regex(@"(cluster|type|wave)", RegexOptions.IgnoreCase);
        //Match TypeMatch = Type.Match(fileContent);
        //currentBlock.type = TypeMatch.Value;

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
        Regex Content = new Regex(@"_Tom([\w\s\W]*)_Ben", RegexOptions.IgnoreCase);
        Match ContentMatch = Content.Match(charBuffer);
        currentBlock.content = ContentMatch.Value;
        
        ChangeState(ParserState.OutsideBlock);

    }

    private void ParseInsideBlockHeader()
    {
        //while (!BufferHasAny("_Tom", "_Ben") && !ReachedEnd())
        while(!BufferHasAny("_Tom") && !ReachedEnd())
        NextChar();

        if (ReachedEnd())
            return;


        Regex ID = new Regex(@"(\d)");
        Match MatchID = ID.Match(charBuffer);
        

        Regex Name = new Regex(@"(?:\((.*)\))");
        Match NameMatch = Name.Match(charBuffer);

        int.TryParse(MatchID.Groups[1].Value, out int result);

        currentBlock.name = NameMatch.Groups[1].Value;
        currentBlock.id = result;
        

        

        //if (ReachedEnd())
        //    return;

        //currentBlock.type = GetLastMatchedBlockType();
        
        ChangeState(ParserState.InsideBlockBody);

    }

    private string GetLastMatchedBlockType()
    {
        string lasttMatched = null;

        lasttMatched ??= (BufferHas("cluster") ? "cluster" : null);
        lasttMatched ??= (BufferHas("wave") ? "wave" : null);
        lasttMatched ??= (BufferHas("type") ? "type" : null);

        return lasttMatched;
    }







}
