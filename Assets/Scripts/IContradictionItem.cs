using UnityEngine;

public interface IContradictionItem 
{
    /// <summary>
    /// Returns true if data is not the same as entity own data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool IsContradiction(Blackboard data);
}
