﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class ApprovalTest
    {
        [Test]
        public void ThirtyDays()
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ThirtyDays.txt");

            var lines = File.ReadAllText(filePath);

            StringBuilder fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Console.SetIn(new StringReader("\n"));

            Program.Main(new string[] { });
            String output = fakeoutput.ToString();

            var outputLines = output.Split('\n');
            var referenceLInes = lines.Split('\n');

            for (var i = 0; i < Math.Min(lines.Length, outputLines.Length); i++)
            {
                Assert.AreEqual(referenceLInes[i], outputLines[i]);
            }
        }
    }
}
