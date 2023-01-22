// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

/**
 * {@code ConstantValidation} implements PVG validators for constant values.
 */
// public final class ConstantValidation {
//     private ConstantValidation() {
//     }

//     public static <T> void constant(String field, T value, T expected) throws ValidationException {
//         if (!value.equals(expected)) {
//             throw new ValidationException(field, value, "must equal " + expected.toString());
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

public static class ConstantValidation
{
    public static void constant<T>(string field, T value, T expected)
    {
        if (!value.Equals(expected))
        {
            throw new ValidationException(field, value, "must equal " + expected.ToString());
        }
    }
}
