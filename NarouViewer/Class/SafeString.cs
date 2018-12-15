namespace System
{
    public static class HTMLSafeString
    {
        public enum EncodeType
        {
            semicolon,
            ampersand
        }

        public static string Encode(string str, EncodeType encodeType = EncodeType.ampersand)
        {
            switch(encodeType)
            {
                case EncodeType.semicolon:
                    return str.Replace("\"", "quot;").Replace("<", "lt;").Replace(">", "gt;").Replace("&", "amp;");

                default:
                case EncodeType.ampersand:
                    return str.Replace("\"", "&quot").Replace("<", "&lt").Replace(">", "&gt").Replace("&", "&amp");
            }
        }
        public static string Decode(string str, EncodeType encodeType = EncodeType.ampersand)
        {
            switch (encodeType)
            {
                case EncodeType.semicolon:
                    return str.Replace("quot;", "\"").Replace("lt;", "<").Replace("gt;", ">").Replace("amp;", "&");

                default:
                case EncodeType.ampersand:
                    return str.Replace("&quot", "\"").Replace("&lt", "<").Replace("&gt", ">").Replace("&amp", "&");
            }
        }
    }
}
