namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Defines the contract for actions triggered by a long press interaction.
    /// Classes implementing this interface can respond to long press gestures by the user.
    /// </summary>
    public interface ILongPressAction
    {
        /// <summary>
        /// Method to be called when a long press is detected.
        /// Implementations should define the specific actions taken in response to a long press.
        /// </summary>
        void OnlongPress();
    }
}
