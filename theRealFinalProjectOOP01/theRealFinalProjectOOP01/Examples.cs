using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace theRealFinalProjectOOP01
{
    internal class Examples
    {
        // Method and operator overloading (Polymorphism) (2022110803)


        public static int add(int a, int b)
        {
            return a + b;

        }
        public static int add(int a, int b, int c)
        {
            return a + b + c;

        }
        public static float add(float a, float b, float c, float d)
        {
            return a + b + c + d;

        }

        // Enum usage (2022110811)


        enum DaysOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }


        public static void CheckWeekend()
        {


            DaysOfWeek today = DaysOfWeek.Monday;


            if (today == DaysOfWeek.Saturday || today == DaysOfWeek.Sunday)
            {
                Console.WriteLine("Today is a weekend day.");
            }
        }

        class Student
        {
            private string name;
            private int age;

            public Student(string name, int age)
            {
                this.name = name;
                this.age = age;
            }

            public void PrintInformation()
            {
                // This keyword usage example (2022110806)

                Console.WriteLine("Name: " + this.name);
                Console.WriteLine("Age: " + this.age);
            }
        }




        // Abstract class usage (2022110814)
        abstract class Shape
        {
            public abstract double GetArea();
        }

        class Circle : Shape
        {
            private double radius;

            public Circle(double radius)
            {
                this.radius = radius;
            }
            //Method overriding with virtual methods and class inheritance (Dynamic  Polymorphism) (2022110813)

            public override double GetArea()
            {
                return Math.PI * radius * radius;
            }
        }

        class Rectangle : Shape
        {
            private double width;
            private double height;

            public Rectangle(double width, double height)
            {
                this.width = width;
                this.height = height;
            }
            //Method overriding with virtual methods and class inheritance (Dynamic  Polymorphism) (2022110813)
            public override double GetArea()
            {
                return width * height;
            }
        }

        // Generic class usage (2022110815)

        public class MyGenericClass<T>
        {
            private T _value;

            public MyGenericClass(T value)
            {
                _value = value;
            }

            public T GetValue()
            {
                return _value;
            }
        }
        //Sealed class usage (2022110816) 

        //used to restrict the users from inheriting the class
        // Sealed class
        sealed class SealedClass
        {
            public int Add(int x, int y)
            {
                return x + y;
            }
        }


        //   Reference passing vs value passing method usage(2022110817)

        // Ref keyword in methods (2022110834)

        private static void refPassingMethod(ref string stidkWhat)
        {
            stidkWhat = stidkWhat + "Maybe you know ";

            //stidkWhat will change to  stidkWhat + "Maybe you know "

        }
        private static void refPassingMethod(string stidkWhat)
        {
            stidkWhat = stidkWhat + "Maybe you know ";

            //while here it will stay as stidkWhat 

        }



        // Structs (2022110821)

        public struct Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void DisplayPoint()
            {
                Console.WriteLine("X = {0}, Y = {1}", X, Y);
            }
        }

        //Default parameters in methods (2022110823) 

        public int AddNumbers(int a = 5, int b = 10)
        {
            return a + b;
        }



        // Anonymous Method (2022110824)

        //As the name suggests, an anonymous method is a method without a name. Anonymous methods in C# can be defined using the delegate keyword and can be assigned to a variable of delegate type.

        class Test
        {
            delegate int Del(int i);

            private void Anonymous()
            {
                Del sqr = delegate (int x) { return x * x; };
                Console.WriteLine(sqr(5));
            }
        }

        // Conditional Operator (2022110825)
        //The conditional operator is also known as a ternary operator. The conditional statements are the decision-making statements which depends upon the output of the expression. It is represented by two symbols, i.e., '?' and ':'. 

        private string ConditionalOperator(int num)
        {

            string result = (num > 0) ? "Positive" : "Negative";
            return result;
        }




        // Reflections (2022110831)
        //In the given code snippet, the method ReflectOnObjectType takes an object parameter named obj. The purpose of this method is to perform reflection on the type of the object passed as an argument and print some information about the type using the Console.WriteLine method.
        public void ReflectOnObjectType(object obj)
        {
            Type type = obj.GetType();
            Console.WriteLine("Type Name: " + type.Name);
            Console.WriteLine("Full Name: " + type.FullName);
            Console.WriteLine("Namespace: " + type.Namespace);
        }

        //Serialization and Deserialization e.g. JSON (2022110832)

        private void something()
        {

            // Serialize an object to JSON
            MyClass myObj = new MyClass { Id = 1, Name = "John Doe" };
            string json = JsonConvert.SerializeObject(myObj);
            Console.WriteLine(json);

            // Deserialize JSON to an object
            MyClass myDeserializedObj = JsonConvert.DeserializeObject<MyClass>(json);
            Console.WriteLine(myDeserializedObj.Id + " " + myDeserializedObj.Name);
        }
        public class MyClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // StringBuilder (2022110833)
        private void stringbuilder()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Hello ");
            sb.Append("world!");

            Console.WriteLine(sb.ToString());
        }








        public class GetOddNumbers
        {
            // IEnumerable and IEnumerator (2022110836)
            //By implementing these interfaces, you can iterate over the elements of MyCollection using a foreach loop or manually by calling the methods of the enumerator.
            public static IEnumerable<int> GenerateSequence()
            {
                int previous = 0;
                int current = 1;

                while (true)
                {// Yield Keyword (2022110835)
                    // It temporarily suspends the execution of the method and returns the value to the caller. When the caller iterates over the returned 

                    yield return current;

                    int next = previous + current;
                    previous = current;
                    current = next;
                }
            }

            public static void caller()
            {
                foreach (int number in GenerateSequence().Take(10))
                {
                    Console.WriteLine(number);
                }
                //output
                /*
                 * 1
1
2
3
5
8
13
21
34
55*/
            }
        }
        // Optional Arguments, Named Arguments, and Generic Arguments 
        // (2022110843)

        public void PrintMessage(string message, ConsoleColor textColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
        }

        public void ExampleUsage()
        {
            // Optional Arguments
            PrintMessage("Hello");  // Uses default values for textColor and backgroundColor

            // Named Arguments
            PrintMessage("Hi", backgroundColor: ConsoleColor.Blue);  // Uses specified backgroundColor and default textColor

            // Generic Arguments
            var genericList = new List<int> { 1, 2, 3, 4, 5 };
            PrintList(genericList);
        }

        public void PrintList<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }






        // Params Keyword (2022110844)
        //It allows you to pass an arbitrary number of arguments to a method without explicitly creating an array or specifying the number of arguments.
        public static int Sum(params int[] numbers)
        {
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }
            return sum;
        }

        private static void Test1()
        {
            int result = Sum(1, 2, 3, 4, 5);
            Console.WriteLine(result); // Output: 15
        }


        // Local Functions or Nested Functions (2022110845)
        //re functions defined within the body of another function
        public static int SumPositiveNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return 0;
            }

            return SumPositiveNumbersInternal();

            int SumPositiveNumbersInternal()
            {
                int sum = 0;
                foreach (var number in numbers)
                {
                    if (number > 0)
                    {
                        sum += number;
                    }
                }

                return sum;
            }
        }




        private class delegate12
        {

            // Delegates and Delegate Multicast and Generic Delegates (2022110846)


            public delegate int Operation(int x, int y);

            public int Add(int x, int y)
            {
                return x + y;
            }

            public int Subtract(int x, int y)
            {
                return x - y;
            }

            public void TestDelegate()
            {
                Operation operation = Add;
                int result = operation(3, 4); // result is 7

                operation = Subtract;
                result = operation(3, 4); // result is -1
            }


            public delegate void MessageDelegate(string message);

            public void PrintToConsole(string message)
            {
                Console.WriteLine(message);
            }

            public void PrintToDebug(string message)
            {
                Debug.WriteLine(message);
            }

            public void TestDelegateMulticast()
            {
                MessageDelegate messageDelegate = PrintToConsole;
                messageDelegate += PrintToDebug;
                messageDelegate("Hello, World!"); // prints "Hello, World!" to both console and debug output
            }











            public delegate T Operation<T>(T x, T y);

       

            public double Add(double x, double y)
            {
                return x + y;
            }

            public void TestGenericDelegate()
            {
                Operation<int> intOperation = Add;
                int intResult = intOperation(3, 4); // intResult is 7

                Operation<double> doubleOperation = Add;
                double doubleResult = doubleOperation(3.2, 4.5); // doubleResult is 7.7
            }



        }

        //Value Tuples, Value Nested Tuples, Value Tuples with Methods 
       // (2022110847)

        private void valuetruples()
        {

            var myTuple = (1, "hello", 3.14);

            var nestedTuple = (1, (2, 3), 4);



            var myNamedTuple = (Id: 1, Message: "hello", Pi: 3.14);

        }


        // KeyValuePair (2022110848)
        private void keyvaluePairSomething()
        {

            // Create a new key-value pair
            KeyValuePair<string, int> pair = new KeyValuePair<string, int>("apple", 5);

            // Access the key and value
            string key = pair.Key;
            int value = pair.Value;

            // Modify the value
            pair = new KeyValuePair<string, int>(key, value + 1);

            // Create a dictionary and add the key-value pair to it
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(pair.Key, pair.Value);



        }


        // NameValueCollection (2022110849)


        public void HandleFormSubmit(NameValueCollection formData)
        {
            // Get the value of a single form field
            string name = formData["name"];

            // Get all the values for a form field
            string[] colors = formData.GetValues("colors");

            // Check if a form field exists
            bool hasAge = formData.AllKeys.Contains("age");

            // Loop through all the form fields
            foreach (string key in formData.Keys)
            {
                string[] values = formData.GetValues(key);
                foreach (string value in values)
                {
                    Console.WriteLine("{0}: {1}", key, value);
                }
            }
        }
        /* 
         *  Generic List, Generic Dictionary, Generic SortedList, Generic 
SortedDictionary, 
Generic Stack, Generic Queue (2022110850)*/
        private void genericSomething()
        {

            MyList<int> numbers = new MyList<int>();
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }


        class MyList<T>
        {
            private List<T> items = new List<T>();

            public void Add(T item)
            {
                items.Add(item);
            }
        }



        // Interfaces usage (2022110818)

        public interface ICharacter
        {
            string Name { get; set; }
            public void Attack()
            {
                Console.WriteLine("Player attacks!");
            }
        }


        public class Knight : ICharacter
        {
            public string Name { get; set; }

            public void Attack()
            {
                Console.WriteLine("The knight attacks with a sword!");
            }
        }

        public class Wizard : ICharacter
        {
            public string Name { get; set; }

            public void Attack()
            {
                Console.WriteLine("The wizard attacks with spells!");
            }
        }



        //Concurrent Collections (ConcurrentBag) (2022110828)
        //They enable multiple threads to safely access and manipulate collection data without the need for external synchronization.
        private void ConcurrentBag1()
        {



            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            bag.Add(1);
            bag.Add(2);
            bag.Add(3);

            int result;
            if (bag.TryTake(out result))
            {
                Console.WriteLine(result);
            }
            if (bag.TryPeek(out result))
            {
                Console.WriteLine("Peek: " + result);
            }
        }



    }
}
