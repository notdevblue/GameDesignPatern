using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    abstract public class Command
    {
        abstract public void Execute();

        abstract public void Undo();
    }
}