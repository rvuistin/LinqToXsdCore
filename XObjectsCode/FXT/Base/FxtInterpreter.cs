//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Xml.Fxt
{
    public delegate void interpreter(XmlSchemaSet schemas, XElement trafo, FxtLog log, List<IFxtTransformation> trafos);

    public static class FxtInterpreter
    {
        internal static XNamespace FxtNs = "http://www.microsoft.com/FXT";

        internal static FxtException ex = new FxtInterpreterException(
            "Requested Xsd transformation not understood.");

        public static FxtLog Run(XmlSchemaSet schemas, XElement trafo, interpreter i)
        {
            var trafos = new List<IFxtTransformation>();
            var log = new FxtLog();

            // Compile if necessary
            if (!schemas.IsCompiled)
                schemas.Compile();
            if (trafo == null)
                return log;

            // Interpret trafo
            i(schemas, trafo, log, trafos);

            // Execute trafos
            foreach (var x in trafos)
                x.Run();

            // Re-compile
            foreach (var x in schemas.XmlSchemas())
                schemas.Reprocess(x);
            schemas.Compile();
            return log;
        }
    }
}