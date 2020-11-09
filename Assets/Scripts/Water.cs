using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ground
{
    override public bool HasEffect()
    {
        return true;
    }

    public override void Effect(GolfBall ball)
    {
        ball.ResetBall();
    }

    override public bool HasFriction()
    {
        return false;
    }
}
