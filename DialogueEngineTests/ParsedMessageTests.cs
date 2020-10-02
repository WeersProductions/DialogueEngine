using System.Collections.Generic;
using DialogueEngine;
using DialogueEngine.Messages;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class ParsedMessageTests
    {
        private static readonly StyleInfo StyleInfoExample1 = new StyleInfo("myField", 0, 1);
        private static readonly StyleInfo StyleInfoExample2 = new StyleInfo("otherField", 2, 3);

        [Test]
        public void Constructor_CustomTypesOnly_StandardEmpty()
        {
            var input = new Dictionary<string, StyleInfo[]>()
            {
                {"ImCustom!", new []{ StyleInfoExample1,  }}
            };
            var message = new ParsedMessage(input);
            
            Assert.IsEmpty(message.DefaultDecorators);
            Assert.AreEqual(input, message.CustomDecorators);
        }
        
        [Test]
        public void Constructor_MultipleCustomTypesOnly_StandardEmpty()
        {
            var input = new Dictionary<string, StyleInfo[]>()
            {
                {"ImCustom!", new []{ StyleInfoExample1 }},
                {"ImCustomToo", new []{ StyleInfoExample2 }}
            };
            var message = new ParsedMessage(input);
            
            Assert.IsEmpty(message.DefaultDecorators);
            Assert.AreEqual(input, message.CustomDecorators);
        }
        
        [Test]
        public void Constructor_StandardTypesOnly_CustomEmpty()
        {
            var input = new Dictionary<string, StyleInfo[]>()
            {
                {StandardDataTypeHelper.EmojiValue, new []{ StyleInfoExample1 }},
                {StandardDataTypeHelper.BoldValue, new []{ StyleInfoExample2 }}
            };
            var expectedOutput = new Dictionary<StandardDataType, StyleInfo[]>()
            {
                {StandardDataType.EMOJI, new []{ StyleInfoExample1 }},
                {StandardDataType.BOLD, new []{ StyleInfoExample2 }}
            };
            var message = new ParsedMessage(input);
            
            Assert.IsEmpty(message.CustomDecorators);
            Assert.AreEqual(expectedOutput, message.DefaultDecorators);
        }
        
        [Test]
        public void Constructor_AllTypes_BothTypesFilled()
        {
            var input = new Dictionary<string, StyleInfo[]>()
            {
                {"ImCustom!", new []{ StyleInfoExample1 }},
                {StandardDataTypeHelper.BoldValue, new []{ StyleInfoExample2 }}
            };
            var expectedOutputDefault = new Dictionary<StandardDataType, StyleInfo[]>()
            {
                {StandardDataType.BOLD, new []{ StyleInfoExample2 }}
            };
            var expectedOutputCustom = new Dictionary<string, StyleInfo[]>()
            {
                {"ImCustom!", new []{ StyleInfoExample1 }}
            };
            var message = new ParsedMessage(input);
            
            Assert.AreEqual(expectedOutputCustom, message.CustomDecorators);
            Assert.AreEqual(expectedOutputDefault, message.DefaultDecorators);
        }
        
        [Test]
        public void Constructor_Empty_Empty()
        {
            var input = new Dictionary<string, StyleInfo[]>();
            var message = new ParsedMessage(input);
            
            Assert.IsEmpty(message.CustomDecorators);
            Assert.IsEmpty(message.DefaultDecorators);
        }
    }
}