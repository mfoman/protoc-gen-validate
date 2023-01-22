// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import com.google.protobuf.ProtocolMessageEnum;

// public final class EnumValidation {
//     private EnumValidation() {
//     }

//     public static void definedOnly(String field, ProtocolMessageEnum value) throws ValidationException {
//         if (value.toString().equals("UNRECOGNIZED")) {
//             throw new ValidationException(field, value, "value is not a defined Enum value " + value);
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

/**
 * {@code EnumValidation} implements PGV validation for protobuf enumerated types.
 */
public sealed static class EnumValidation
{
    public static void definedOnly(string field, ProtocolMessageEnum value)
    {
        if (value.ToString().Equals("UNRECOGNIZED"))
        {
            throw new ValidationException(field, value, "value is not a defined Enum value " + value);
        }
    }
}
