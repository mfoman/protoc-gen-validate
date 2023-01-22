// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import java.util.Collection;
// import java.util.Map;

// public final class MapValidation {
//     private MapValidation() {
//     }

//     public static void min(String field, Map value, int expected) throws ValidationException {
//         if (Math.min(value.size(), expected) != expected) {
//             throw new ValidationException(field, value, "value size must be at least " + expected);
//         }
//     }

//     public static void max(String field, Map value, int expected) throws ValidationException {
//         if (Math.max(value.size(), expected) != expected) {
//             throw new ValidationException(field, value, "value size must not exceed " + expected);
//         }
//     }

//     public static void noSparse(String field, Map value) throws ValidationException {
//         throw new UnimplementedException(field, "no_sparse validation is not implemented for Java because protobuf maps cannot be sparse in Java");
//     }

//     @FunctionalInterface
//     public interface MapValidator<T> {
//         void accept(T val) throws ValidationException;
//     }

//     public static <T> void validateParts(Collection<T> vals, MapValidator<T> validator) throws ValidationException {
//        for (T val : vals) {
//            validator.accept(val);
//        }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

/**
 * {@code MapValidation} implements PGV validation for protobuf {@code Map} fields.
 */
public sealed class MapValidation
{
    public static void min(string field, Map value, int expected)
    {
        if (Math.Min(value.Count, expected) != expected)
        {
            throw new ValidationException(field, value, "value size must be at least " + expected);
        }
    }

    public static void max(string field, Map value, int expected)
    {
        if (Math.Max(value.Count, expected) != expected)
        {
            throw new ValidationException(field, value, "value size must not exceed " + expected);
        }
    }

    public static void noSparse(string field, Map value)
    {
        throw new UnimplementedException(field, "no_sparse validation is not implemented for Java because protobuf maps cannot be sparse in Java");
    }

    public delegate void MapValidator<T>(T val);

    public static void validateParts<T>(IEnumerable<T> vals, MapValidator<T> validator)
    {
        foreach (var val in vals)
        {
            validator(val);
        }
    }
}
