using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HomeWork.Tests
{
    [TestClass]
    public class HomeWorkTests
    {
        [TestMethod]
        public void BaseClass_Constructor_CorrectValues_Test()
        {
            // Arrange
            string expected = "The point has coodinates (0, 0, 0)";

            // Act
            Point point = new Point("0", "0", "0");
            string actual = point.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BaseClass_Constructor_IncorrectValues_Test()
        {
            // Arrange
            string expected = "The point has coodinates (0, 0, 0)";

            // Act
            Point point = new Point("This", "Incorrect", "Values");
            string actual = point.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChildClass_Constructor_CorrectValues_Test()
        {
            // Arrange
            string expected = "The point has coodinates (0, 0, 0)\n" +
                $"The modulus of the vector is equal to 0\n" +
                $"The angle between the vector and the Ox plane is 0°\n" +
                $"The angle between the vector and the Oy plane is 0°\n" +
                $"The angle between the vector and the Oz plane is 0°"; ;

            // Act
            PointOn3D point = new PointOn3D("0", "0", "0");
            string actual = point.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChildClass_Constructor_IncorrectValues_Test()
        {
            // Arrange
            string expected = "The point has coodinates (0, 0, 0)\n" +
                $"The modulus of the vector is equal to 0\n" +
                $"The angle between the vector and the Ox plane is 0°\n" +
                $"The angle between the vector and the Oy plane is 0°\n" +
                $"The angle between the vector and the Oz plane is 0°";

            // Act
            PointOn3D point = new PointOn3D("This", "Incorrect", "Values");
            string actual = point.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BaseClass_MethodMultiplication_Test1()
        {
            // Arrange
            double expected = 0;

            // Act
            Point point = new Point("124523,312412", "412342142,524634324", "0");
            double actual = point.Multiplication();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BaseClass_MethodMultiplication_Test2()
        {
            // Arrange
            double expected = 24233548.106235;

            // Act
            Point point = new Point("123,45", "345,67", "567,89");
            double actual = point.Multiplication();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BaseClass_CopyConstructor_Test()
        {
            // Arrange
            string expected = "The point has coodinates (4523, 1535, 6246)";
            Point FirstPoint = new Point("4523", "1535", "6246");

            // Act
            Point ResultPoint = new Point(FirstPoint);
            string actual = ResultPoint.ToString();

            // Assert
            Assert.AreEqual(expected , actual);
        }

        [TestMethod]
        public void Test4()
        {
            // Arrange

            // Act

            // Assert
        }

    }
}
