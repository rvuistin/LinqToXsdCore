//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Xml.Schema.Linq.CodeGen
{
    internal class StateNameSource
    {
        private int nextName = 1;

        internal int Next()
        {
            return nextName++;
        }

        internal void Reset()
        {
            nextName = 1;
        }
    }
}