using System;
using System.Collections.Generic;

namespace DialogueEngine.Messages
{
    public readonly struct ParsedMessage
    {
        /// <summary>
        /// The normal text.
        /// </summary>
        public readonly string Text;
        
        /// <summary>
        /// DecoratorType: 
        /// </summary>
        public readonly Dictionary<string, StyleInfo[]> CustomDecorators;
        
        /// <summary>
        /// DefaultType: 
        /// </summary>
        public readonly Dictionary<StandardDataType, StyleInfo[]> DefaultDecorators;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">The undecorated message</param>
        /// <param name="customDecorators"></param>
        /// <param name="defaultDecorators"></param>
        public ParsedMessage(string text, Dictionary<string, StyleInfo[]> customDecorators, Dictionary<StandardDataType, StyleInfo[]> defaultDecorators)
        {
            CustomDecorators = customDecorators;
            DefaultDecorators = defaultDecorators;
            Text = text;
        }

        public ParsedMessage(string text, Dictionary<string, StyleInfo[]> decorators)
        {
            Text = text;
            DefaultDecorators = new Dictionary<StandardDataType, StyleInfo[]>();
            CustomDecorators = new Dictionary<string, StyleInfo[]>();
            
            foreach (var decorator in decorators)
            {
                // Check whether it's default type, if true, add it to default types otherwise keep it.
                if (StandardDataTypeHelper.IsStandardDataType(decorator.Key, out var dataType))
                {
                    DefaultDecorators.Add(dataType, decorator.Value);
                }
                else
                {
                    CustomDecorators.Add(decorator.Key, decorator.Value);
                }
            }
        }

        public ParsedMessage(Dictionary<string, StyleInfo[]> decorators): this("", decorators)
        {
            
        }
    }
}