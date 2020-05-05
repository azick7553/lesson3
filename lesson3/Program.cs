using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace lesson3
{
    class Program
    {

        static void Main(string[] args)
        {
            //List<string> list = new List<string>();

            //list.Add("test");
            //list.AddRange(new List<string>{ "dd", "fff", "dd" });
            //int index = list.IndexOf("dd");
            //list.Insert(0, "InsertedElement");
            //list.InsertRange(1, new List<string> { "test1", "test2" });
            //list.Remove("dd");
            //list.RemoveAt(list.IndexOf("dd"));
            //list.Sort();

            //Dictionary<int, Person> listDic = new Dictionary<int, Person>();
            //listDic.Add(1123, new Person() { FullName = "Test" });
            //Dictionary<int, string> listDic = new Dictionary<int, string>();
            //listDic.Add(972, "TJS");
            //listDic.Add(840, "USD");
            //Dictionary<int, string> listDic = new Dictionary<int, string>() 
            //{
            //    [972] = "TJS",
            //    [840] = "USD"
            //};
            //listDic.Add(972, "TJS");
            //listDic.Add(840, "USD");
            //foreach (var item in listDic)
            //{
            //    Console.WriteLine($"Key:{item.Key} | Value:{item.Value}");
            //}

            //int[] number = { 0, 2, 3, 4, 5 };

            //IEnumerator ieList = number.GetEnumerator();
            //while (ieList.MoveNext())
            //{
            //    Console.WriteLine(ieList.Current);
            //}
            //ieList.Reset();

            //yield return

            //yield break;



            //var x = GetSomeNumber(5);
            City c = new City();
            foreach (var item in c.GetPersons(100))
            {
                Console.WriteLine(item.FullName);
            }
            Console.ReadKey();
        }
        //static IEnumerable GetSomeNumber(int number)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (number == i)
        //        {
        //            yield return i;
        //        }
        //    }

        //    yield break;
        //}
    }
    public class Person
    {
        public string FullName { get; set; }

        public Person(string FullName)
        {
            this.FullName = FullName;
        }
    }

    public class City
    {
        private Person[] People;
        public City()
        {
            People = new Person[] { new Person("P1"), new Person("P2"), new Person("P3") };
        }
        public int Length { get { return People.Length; } }

        public IEnumerable<Person> GetPersons(int max)
        {
            for (int i = 0; i < max; i++)
            {
                if(i == People.Length)
                {
                    yield break;
                }
                else
                {
                    yield return People[i];
                }
            }
        }
    }
}
