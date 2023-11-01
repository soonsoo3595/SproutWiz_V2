using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameBase
{
    protected GridPosition[] AffectPositions;

    public abstract GridPosition[] Execute();
}
