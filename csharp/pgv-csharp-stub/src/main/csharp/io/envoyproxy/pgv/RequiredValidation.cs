// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import com.google.protobuf.GeneratedMessageV3;

/**
 * {@code RequiredValidation} implements PGV validation for required fields.
 */
// public final class RequiredValidation {
//     private RequiredValidation() {
//     }

//     public static void required(String field, GeneratedMessageV3 value) throws ValidationException {
//         if (value == null) {
//             throw new ValidationException(field, "null", "is required");
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

public sealed class RequiredValidation
{
    public static void required(string field, GeneratedMessageV3 value)
    {
        if (value == null)
        {
            throw new ValidationException(field, "null", "is required");
        }
    }
}
