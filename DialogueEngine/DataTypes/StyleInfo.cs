using System.Collections.Generic;

namespace DialogueEngine
{
    /// <summary>
    /// Contains info about how a style is applied within a range.
    /// </summary>
    public readonly struct StyleInfo
    {
        /// <summary>
        /// Name of the field name within a dataType.
        /// </summary>
        public readonly string FieldName;
        /// <summary>
        /// From this character onwards this style should be applied (included).
        /// </summary>
        public readonly int Start;
        /// <summary>
        /// Till this character this style should be applied (excluded).
        /// </summary>
        public readonly int End;
        /// <summary>
        /// Can be used to pass more arguments.
        /// </summary>
        public readonly Dictionary<string, object> Arguments;

        public StyleInfo(string fieldName, int start, int end, Dictionary<string, object> arguments = null)
        {
            FieldName = fieldName;
            Start = start;
            End = end;
            Arguments = arguments;
        }
    }
}