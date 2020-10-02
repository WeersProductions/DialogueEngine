using System;

namespace DialogueEngine
{
    public enum StandardDataType
    {
        ERROR,
        TYPEWRITER,
        BOLD,
        ITALICS,
        EMOJI
    }

    public static class StandardDataTypeHelper
    {
        public const string ErrorValue = "error";
        public const string TypewriterValue = "typewriter";
        public const string BoldValue = "b";
        public const string ItalicsValue = "i";
        public const string EmojiValue = "emoji";
        
        public static bool IsStandardDataType(string type, out StandardDataType standardDataType)
        {
            switch (type)
            {
                case EmojiValue:
                    standardDataType = StandardDataType.EMOJI;
                    return true;
                case BoldValue:
                    standardDataType = StandardDataType.BOLD;
                    return true;
                case TypewriterValue:
                    standardDataType = StandardDataType.TYPEWRITER;
                    return true;
                case ItalicsValue:
                    standardDataType = StandardDataType.ITALICS;
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
                case StandardDataType.BOLD:
                    return BoldValue;
                case StandardDataType.EMOJI:
                    return EmojiValue;
                case StandardDataType.ITALICS:
                    return ItalicsValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }
    }
}