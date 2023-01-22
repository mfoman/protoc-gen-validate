// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

/**
 * {@code UnimplementedException} indicates a PGV validation is unimplemented for Java.
 */
// public class UnimplementedException extends ValidationException {
//     public UnimplementedException(String field, String reason) {
//         super(field, "UNIMPLEMENTED", reason);
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

public class UnimplementedException : ValidationException
{
    public UnimplementedException(string field, string reason) : base(field, "UNIMPLEMENTED", reason)
    {
    }
}
