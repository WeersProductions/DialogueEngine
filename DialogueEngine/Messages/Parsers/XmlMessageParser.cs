using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DialogueEngine.Messages.Parsers
{
    public class XmlMessageParser: IMessageParser
    {
        private string _baseText = "BaseText";
        
        private class TempStyleInfo
        {
            public string Type;
            // Inclusive.
            public int Start;
            // Exclusive.
            public int End;
            public Dictionary<string, string> Arguments = new Dictionary<string, string>();

            public StyleInfo ToStyleInfo()
            {
                // TODO: check fieldname.
                return new StyleInfo(Type, Start, End, Arguments);
            }
        }

        private void TryAddToDictionary<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary.TryGetValue(key, out var styleInfoList))
            {
                styleInfoList.Add(value);
            }
            else
            {
                dictionary.Add(key, new List<TValue> {value});
            }
        }
        
        public ParsedMessage Parse(string rawString)
        {
            string text = null;
            Dictionary<string, List<StyleInfo>> styleInfos = new Dictionary<string, List<StyleInfo>>();
            
            int cursorPosition = 0;
            Stack<TempStyleInfo> styleInfosStack = new Stack<TempStyleInfo>();

            var xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = false;

            using (StringReader stringReader = new StringReader($"<{_baseText} xml:space='preserve'>{rawString}</{_baseText}>"))
            using (XmlReader reader = XmlReader.Create(stringReader, xmlReaderSettings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.None:
                            break;
                        case XmlNodeType.Element:
                            var newTempStyleInfo = new TempStyleInfo
                                {Type = reader.Name, Start = cursorPosition, End = cursorPosition};
                            
                            // Check for attributes (e.g. key='value').
                            if (reader.HasAttributes)
                            {
                                for (int attributeIndex = 0; attributeIndex < reader.AttributeCount; attributeIndex++)
                                {
                                    reader.MoveToAttribute(attributeIndex);
                                    newTempStyleInfo.Arguments.Add(reader.Name, reader.Value);
                                }

                                reader.MoveToElement();
                            }
                            
                            if (reader.IsEmptyElement)
                            {
                                // This is an empty element, e.g. '<type/>, or <type key=value/>
                                // Since this does not trigger an 'endElement', we need to finish it immediately.
                                var styleInfo = newTempStyleInfo.ToStyleInfo();
                                TryAddToDictionary(styleInfos, newTempStyleInfo.Type, styleInfo);
                            }
                            else
                            {
                                styleInfosStack.Push(newTempStyleInfo);
                            }
                            break;
                        case XmlNodeType.Attribute:
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.SignificantWhitespace:
                        case XmlNodeType.Text:
                            text += reader.Value;
                            cursorPosition += reader.Value.Length;
                            break;
                        case XmlNodeType.CDATA:
                            break;
                        case XmlNodeType.EntityReference:
                            break;
                        case XmlNodeType.Entity:
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            break;
                        case XmlNodeType.Comment:
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            break;
                        case XmlNodeType.DocumentFragment:
                            break;
                        case XmlNodeType.Notation:
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        case XmlNodeType.EndElement:
                            var tempStyleInfo = styleInfosStack.Pop();
                            
                            if (tempStyleInfo.Type.Equals(_baseText))
                            {
                                // We don't close our baseText.
                                break;
                            }
                            
                            // Add our end data.
                            tempStyleInfo.End = cursorPosition;

                            // Add to the dictionary.
                            var newStyleInfo = tempStyleInfo.ToStyleInfo();
                            TryAddToDictionary(styleInfos, tempStyleInfo.Type, newStyleInfo);
                            break;
                        case XmlNodeType.EndEntity:
                            break;
                        case XmlNodeType.XmlDeclaration:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            var finalDictionary = new Dictionary<string, StyleInfo[]>();
            foreach (var element in styleInfos)
            {
                finalDictionary.Add(element.Key, element.Value.ToArray());
            }
            return new ParsedMessage(text, finalDictionary);
        }
    }
}