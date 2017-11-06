using Microsoft.VisualStudio.TestTools.UnitTesting;
using DodgingRocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DodgingRocks.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void MainTest()
        {
            //Test Instructions Lower
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("i{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Direct the space craft {1} through the asteroids *.{0}Use the left and right arrow keys to move side{0}to side.{0}The difficulty changes the number of asteroids.{0}Press any key to return to main menu.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine, '\u25BC');

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Instructions Upper
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("I{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Direct the space craft {1} through the asteroids *.{0}Use the left and right arrow keys to move side{0}to side.{0}The difficulty changes the number of asteroids.{0}Press any key to return to main menu.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine, '\u25BC');

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Start Lower
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("s{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"\"{0}is not recognised as a number between 1 and 20.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Start Upper
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("S{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"\"{0}is not recognised as a number between 1 and 20.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Empty String
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(Environment.NewLine))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Unexpected String
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Random text{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format("Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}Press i for instructions or press s to start.{0}Input not recognised. Please try again.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod()]
        public void StartGameTest()
        {
            //Test Level = 0
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.StartGame();

                    string expected = string.Format("Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"0\"{0}is not recognised as a number between 1 and 20.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Level = -1
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("-1{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.StartGame();

                    string expected = string.Format("Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"-1\"{0}is not recognised as a number between 1 and 20.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Level = 21
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("21{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.StartGame();

                    string expected = string.Format("Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"21\"{0}is not recognised as a number between 1 and 20.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Level = int32.MaxValue+1
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("{0}{1}", 2147483648, Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.StartGame();

                    string expected = string.Format("Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"2147483648\"{0}is not recognised as a number between 1 and 20.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Test Level = string
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Random Text{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.StartGame();

                    string expected = string.Format("Please enter a difficulty between 1 and 20{0}then press enter to start game.{0}I'm sorry, your input of \"Random Text\"{0}is not recognised as a number between 1 and 20.{0}", Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }

            //Would be good to add a valid test here, but the code does too much to make this efficient
        }

        [TestMethod()]
        public void GenerateMapTest()
        {
            //Simple test that calling this method returns a char[250, 50] as per the hard coded width height values
            //If code modified later for variable map sizes, this test becomes more valuable
            //Other tests e.g. characters in right places are code duplication and offer limited value as values hard coded in function
            char[,] Map = Program.GenerateMap();

            Assert.AreEqual<int>(250, Map.GetLength(0));
            Assert.AreEqual<int>(50, Map.GetLength(1));

        }
    }
}