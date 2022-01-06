using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Command
{
    public abstract void Execute(Animator anim, bool isReverse = false);
}

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool isReverse = false)
    {
        anim.SetTrigger("isJumping" + (isReverse ? "R" : ""));
    }
}

public class PerformPunch : Command
{
    public override void Execute(Animator anim, bool isReverse = false)
    {
        anim.SetTrigger("isPunching" + (isReverse ? "R" : ""));
    }
}

public class PerformKick : Command
{
    public override void Execute(Animator anim, bool isReverse = false)
    {
        anim.SetTrigger("isKicking" + (isReverse ? "R" : ""));
    }
}

public class MoveFoward : Command
{
    public override void Execute(Animator anim, bool isReverse = false)
    {
        anim.SetTrigger("isWalking" + (isReverse ? "R" : ""));
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator anim, bool isReverse = false)
    {

    }
}