using System.Collections.Generic;
using DialogueEngine;
using DialogueEngine.Messages.Parsers;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class XmlMessageParserTests
    {
        private void CompareStyleInfo(StyleInfo expected, StyleInfo actual)
        {
            Assert.AreEqual(expected.FieldName, actual.FieldName);
            Assert.AreEqual(expected.Start, actual.Start);
            Assert.AreEqual(expected.End, actual.End);
            Assert.AreEqual(expected.Arguments, actual.Arguments);
        }
        
        [Test]
        public void Parse_OnlyText_NoDecorators()
        {
            string message = "This is just text!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(message);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_SingleDefaultDecorator_SingleDecorator()
        {
            string message = "This is just text!";
            string xml = "This is just <b>text</b>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(1, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.BOLD].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.BoldValue, 13, message.Length - 1, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.BOLD][0]);
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_NestedDefaultDecorator_TwoDecorators()
        {
            string message = "This is just text!";
            string xml = "This is <i>just <b>text</b></i>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(2, parsedMessage.DefaultDecorators.Count);
            // Check bold
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.BOLD].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.BoldValue, 13, message.Length - 1, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.BOLD][0]);
            // Check italics
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.ITALICS].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.ItalicsValue, 8, message.Length - 1, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.ITALICS][0]);
            
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_MultipleDifferentDefaultDecorators_TwoDecorators()
        {
            string message = "This is just text!";
            string xml = "This is <i>just</i> <b>text</b>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(2, parsedMessage.DefaultDecorators.Count);
            // Check bold
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.BOLD].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.BoldValue, 13, message.Length - 1, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.BOLD][0]);
            // Check italics
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.ITALICS].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.ItalicsValue, 8, 12, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.ITALICS][0]);
            
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_MultipleSameDefaultDecorators_TwoDecorators()
        {
            string message = "This is just text!";
            string xml = "This is <i>just</i> <i>text</i>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(1, parsedMessage.DefaultDecorators.Count);
            // Check bold
            Assert.AreEqual(2, parsedMessage.DefaultDecorators[StandardDataType.ITALICS].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.ItalicsValue, 8, 12, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.ITALICS][0]);
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.ItalicsValue, 13, message.Length - 1, new Dictionary<string, string>()) , 
                parsedMessage.DefaultDecorators[StandardDataType.ITALICS][1]);
            
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_CustomSingleDecorator_SingleDecorator()
        {
            string message = "This is just !";
            string xml = "This is just <npc/>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(1, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(1, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 13, 13, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["npc"][0]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_CustomMultipleDifferentDecorators_MultipleDecorators()
        {
            string message = "This is just ! He has heard of .";
            string xml = "This is just <npc/>! He has heard of <player/>.";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(2, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "player", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(1, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 13, 13, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["npc"][0]);
            Assert.AreEqual(1, parsedMessage.CustomDecorators["player"].Length); 
            CompareStyleInfo(
                new StyleInfo("player", 31, 31, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["player"][0]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_CustomMultipleDecorators_MultipleDecorators()
        {
            string message = "This is just !  is awesome.";
            string xml = "This is just <npc/>! <npc/> is awesome.";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(1, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(2, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 13, 13, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["npc"][0]);
            CompareStyleInfo(
                new StyleInfo("npc", 15, 15, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["npc"][1]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }

        [Test]
        public void Parse_SingleCustomDecoratorWithArgument_SingleDecoratorWithArgument()
        {
            string message = "His job is !";
            string xml = "His job is <npc request='job'/>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(1, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(1, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 11, 11, new Dictionary<string, string> {{"request", "job"}}) , 
                parsedMessage.CustomDecorators["npc"][0]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_MultipleDifferentCustomDecoratorsWithArgument_MultipleDifferentDecoratorsWithArgument()
        {
            string message = "His job is ! He wonders why you're a ?";
            string xml = "His job is <npc request='job'/>! He wonders why you're a <player request='job'/>?";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(2, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "player", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(1, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 11, 11, new Dictionary<string, string> {{"request", "job"}}) , 
                parsedMessage.CustomDecorators["npc"][0]);
            Assert.AreEqual(1, parsedMessage.CustomDecorators["player"].Length); 
            CompareStyleInfo(
                new StyleInfo("player", 37, 37, new Dictionary<string, string> {{"request", "job"}}) , 
                parsedMessage.CustomDecorators["player"][0]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_CustomMultipleDecoratorsWithArgument_MultipleDecoratorsWithArgument()
        {
            string message = "This is just ! His job is .";
            string xml = "This is just <npc/>! His job is <npc request='job'/>.";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(1, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(2, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 13, 13, new Dictionary<string, string>()) , 
                parsedMessage.CustomDecorators["npc"][0]);
            CompareStyleInfo(
                new StyleInfo("npc", 26, 26, new Dictionary<string, string> {{"request", "job"}}) , 
                parsedMessage.CustomDecorators["npc"][1]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_SingleCustomDecoratorWithArguments_SingleDecoratorWithArguments()
        {
            string message = "His job is !";
            string xml = "His job is <npc request='job' type='happy'/>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(1, parsedMessage.CustomDecorators.Count);
            CollectionAssert.Contains(parsedMessage.CustomDecorators.Keys, "npc", "A custom decorator called 'npc' should have been created.");
            Assert.AreEqual(1, parsedMessage.CustomDecorators["npc"].Length); 
            CompareStyleInfo(
                new StyleInfo("npc", 11, 11, new Dictionary<string, string> {{"request", "job"}, {"type", "happy"}}) , 
                parsedMessage.CustomDecorators["npc"][0]);
            
            Assert.AreEqual(0, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_SingleDefaultDecoratorWithArgument_SingleDecoratorWithArgument()
        {
            string message = "He is big!";
            string xml = "He is <b style='extreme'>big</b>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(1, parsedMessage.DefaultDecorators.Count);
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.BOLD].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.BoldValue, 6, message.Length - 1, new Dictionary<string, string> {{"style", "extreme"}}) , 
                parsedMessage.DefaultDecorators[StandardDataType.BOLD][0]);
            Assert.AreEqual(message, parsedMessage.Text);
        }
        
        [Test]
        public void Parse_NestedDefaultDecoratorWithArguments_TwoDecoratorsWithArguments()
        {
            string message = "This is just text!";
            string xml = "This is <i style='minimal'>just <b style='extreme'>text</b></i>!";
            
            var xmlMessageParser = new XmlMessageParser();
            var parsedMessage = xmlMessageParser.Parse(xml);
            
            Assert.NotNull(parsedMessage);
            Assert.AreEqual(0, parsedMessage.CustomDecorators.Count);
            Assert.AreEqual(2, parsedMessage.DefaultDecorators.Count);
            // Check bold
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.BOLD].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.BoldValue, 13, message.Length - 1, new Dictionary<string, string> {{"style", "extreme"}}) , 
                parsedMessage.DefaultDecorators[StandardDataType.BOLD][0]);
            // Check italics
            Assert.AreEqual(1, parsedMessage.DefaultDecorators[StandardDataType.ITALICS].Length); 
            CompareStyleInfo(
                new StyleInfo(StandardDataTypeHelper.ItalicsValue, 8, message.Length - 1, new Dictionary<string, string> {{"style", "minimal"}}) , 
                parsedMessage.DefaultDecorators[StandardDataType.ITALICS][0]);
            
            Assert.AreEqual(message, parsedMessage.Text);
        }
    }
}