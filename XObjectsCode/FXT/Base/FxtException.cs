//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Xml.Fxt
{
    public class FxtException : Exception
    {
        public FxtException() : base()
        {
        }

        public FxtException(string msg) : base(msg)
        {
        }
    }
}