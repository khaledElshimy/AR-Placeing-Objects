// <summary>
/// Defines a contract for objects that can respond to double-click interactions.
/// Implementing this interface allows an object to specify actions taken when a double-click is detected.
/// </summary>
public interface IDoubleClickAction
{
    /// <summary>
    /// Method to be executed when a double-click interaction is detected on the implementing object.
    /// </summary>
    void OnDoubleClick();
}
