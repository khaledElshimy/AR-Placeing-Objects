using System.Collections.Generic;

namespace arplace.Data.interfaces
{
    /// <summary>
    /// Defines the contract for data management within the application.
    /// This interface outlines the methods and properties required for managing AR objects' data.
    /// </summary>
    public interface IDataManager
    {
        // Provides read-only access to the dictionary storing AR objects data.
        Dictionary<string, ObjectData> ObjectsData { get; }

        /// <summary>
        /// Adds a new AR object's data to the data management system.
        /// </summary>
        /// <param name="objectData">The ObjectData to add.</param>
        void AddObjectData(ObjectData objectData);

        /// <summary>
        /// Retrieves the data for a specified AR object by name.
        /// </summary>
        /// <param name="objectName">The name of the AR object.</param>
        /// <returns>The ObjectData associated with the given name.</returns>
        ObjectData GetObjectData(string objectName);
    }
}
