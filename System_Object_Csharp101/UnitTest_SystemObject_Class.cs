using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace System_Object_Csharp101
{


    public class UnitTest_SystemObject_Class
    {

        private readonly ITestOutputHelper _output;

        public UnitTest_SystemObject_Class(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void UnitTest_Get_Base_Class_Of_DotNet_Classes()
        {
            //get the assembly name of .net core
            string assemblyFullName = "System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";

            //load the assembly
            Assembly assembly = Assembly.Load(assemblyFullName);

            //filter base on the following criteria
            // Doesn't have a base type because System.Object is the root of all types within the .NET Core
            // System.Object is a class and ansiclass
            TypeInfo baseOfAllClasses = assembly.DefinedTypes.FirstOrDefault(x => x.BaseType == null && x.IsClass && x.IsAnsiClass);

            string objectAlias = string.Empty;

            using (var provider = new CSharpCodeProvider())
            {
                objectAlias = provider.GetTypeOutput(new CodeTypeReference(baseOfAllClasses));
            }

            Assert.True(objectAlias == "object");

        }

        [Fact]
        public void UnitTest_Check_Base_Class_Of_System_Object()
        {
            object obj = new object(); //create new instance of object

            Assert.IsType<object>(obj); //check if object

            Type objType = obj.GetType();

            Assert.Null(objType.BaseType); //doesn't have a base type

            Assert.True(objType.Name == "Object");
            Assert.True(objType.FullName == "System.Object");
            Assert.True(objType.Namespace == "System");

            Assert.True(objType.IsAnsiClass);//is ansiclass
            Assert.True(objType.IsClass);//is class

        }


        [Fact]
        public void Prove_That_All_Classes_Reference_And_Value_Types_Are_Derived_From_System_Object()
        {
            Assert.True(typeof(string).IsSubclassOf(typeof(object)));

            Assert.True(typeof(Array).IsSubclassOf(typeof(object)));

            // this is the main reason why value types and primitive types behaves like an object
            Assert.True(typeof(ValueType).IsSubclassOf(typeof(object)));
        }

        [Fact]
        public void UnitTest_ToString()
        {
            Assert.True("5" == 5.ToString());
            Assert.True((89.99m).ToString() == "89.99");
            Assert.True(true.ToString() == "True");
            Assert.True(true.ToString() == bool.TrueString);
            Assert.True(false.ToString() == "False");
            Assert.True(false.ToString() == bool.FalseString);
        }

        [Fact]
        public void UnitTest_Object_Equals_Static_And_Virtual()
        {

            Assert.True(5.Equals(5));

            object first = null;
            object second = null;

            Assert.True(object.ReferenceEquals(first, second));

            first = new object();

            Assert.False(object.ReferenceEquals(first, second));

            second = first;

            Assert.True(object.ReferenceEquals(first, second));
        }

        [Fact]
        public void Unit_Test_Object_GetType()
        {
            int num = 5;
            decimal num2 = 5.23m;
            string name = "jin";

            Assert.True(num.GetType() == typeof(int));
            Assert.True(num2.GetType() == typeof(decimal));
            Assert.True(name.GetType() == typeof(string));
           
        }

        [Fact]
        public void Unit_Test_GetHashCode()
        {
            string name = "";

            name.GetHashCode();
        }
    }
}
