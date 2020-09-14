using System;

namespace DialogueEngine
{
    public enum StandardDataType
    {
        ERROR,
        TYPEWRITER,
        STYLE,
        EMOJI
    }

    public static class StandardDataTypeHelper
    {
        public const string ErrorValue = "error";
        public const string TypewriterValue = "typewriter";
        public const string StyleValue = "style";
        public const string EmojiValue = "emoji";
        
        public static bool IsStandardDataType(string type, out StandardDataType standardDataType)
        {
            switch (type)
            {
                case EmojiValue:
                    standardDataType = StandardDataType.EMOJI;
                    return true;
                case StyleValue:
                    standardDataType = StandardDataType.STYLE;
                    return true;
                case TypewriterValue:
                    standardDataType = StandardDataType.TYPEWRITER;
                    return true;
                default:
                    standardDataType = StandardDataType.ERROR;
                    return false;
            }
        }

        public static string GetString(StandardDataType dataType)
        {
            switch (dataType)
            {
                case StandardDataType.ERROR:
                    return ErrorValue;
                case StandardDataType.TYPEWRITER:
                    return TypewriterValue;
                case StandardDataType.STYLE:
                    return StyleValue;
                case StandardDataType.EMOJI:
                    return EmojiValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }
    }
}