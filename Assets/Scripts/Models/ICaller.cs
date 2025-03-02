using System;

public interface ICaller
{
    public event Action<string> OnAction;
}
