using NUnit.Framework;
using arplace.Data;
using System.Runtime.CompilerServices;
using System.Linq;

namespace arplace.Data.Tests
{
    [TestFixture]
    public class DataManagerTests 
    {
        private DataManager dataManager;

        [SetUp]
        public void Setup()
        {
            dataManager = new DataManager();
        }

        [TearDown]
        public void Teardown()
        {
            dataManager = null;
        }

        [Test]
        public void Constructor_loads_object_data_list_from_resource()
        {
            Assert.NotNull(dataManager.ObjectsData); // Ensure ObjectsData is initialized
            Assert.IsNotEmpty(dataManager.ObjectsData);  // Ensure some data is loaded  
        }

        [Test]
        public void AddObjectData_adds_new_object_data_if_not_present()
        {
            // Arrange
            string newObjectName = "TestObject";
            ObjectData newObjectData = new ObjectData { objectName = newObjectName };

            // Act
            dataManager.AddObjectData(newObjectData);

            // Assert
            Assert.True(dataManager.ObjectsData.ContainsKey(newObjectName));
            Assert.AreEqual(newObjectData, dataManager.ObjectsData[newObjectName]);
        }

        [Test]
        public void AddObjectData_IgnoresDuplicate_IfPresent()
        {
            // Arrange
            var dataManager = new DataManager();
            var existingObject = dataManager.ObjectsData.Values.FirstOrDefault();  // Get an existing object

            // Act
            dataManager.AddObjectData(existingObject);  // Add the same object again

            // Assert
            // No need to assert the number of objects (remains the same)
            // Existing object should still be present
            Assert.True(dataManager.ObjectsData.ContainsKey(existingObject.objectName));
        }

        [Test]
        public void GetObjectData_ReturnsObject_ForExistingName()
        {
            // Arrange
            var existingObject = dataManager.ObjectsData.Values.FirstOrDefault();  // Get an existing object

            // Act
            var retrievedData = dataManager.GetObjectData(existingObject.objectName);

            // Assert
            Assert.NotNull(retrievedData);
            Assert.AreEqual(existingObject, retrievedData);
        }

        [Test]
        public void GetObjectData_ReturnsNull_ForNonexistentName()
        {
            // Arrange
            var dataManager = new DataManager();
            var nonExistentName = "NonExistingObject";  // Name not present in loaded data

            // Act
            var retrievedData = dataManager.GetObjectData(nonExistentName);

            // Assert
            Assert.Null(retrievedData);
        }
    }
}
