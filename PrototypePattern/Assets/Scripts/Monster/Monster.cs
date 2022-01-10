using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    abstract public class Monster
    {
        abstract public Monster Clone();
        abstract public void Talk();
    }
}