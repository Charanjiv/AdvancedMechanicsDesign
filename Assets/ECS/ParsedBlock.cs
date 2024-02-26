using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

