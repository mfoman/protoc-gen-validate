// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import java.util.HashSet;
// import java.util.List;
// import java.util.Set;

// public final class RepeatedValidation {
//     private RepeatedValidation() {
//     }

//     public static <T> void minItems(String field, List<T> values, int expected) throws ValidationException {
//         if (values.size() < expected) {
//             throw new ValidationException(field, values, "must have at least " + expected + " items");
//         }
//     }

//     public static <T> void maxItems(String field, List<T> values, int expected) throws ValidationException {
//         if (values.size() > expected) {
//             throw new ValidationException(field, values, "must have at most " + expected + " items");
//         }
//     }

//     public static <T> void unique(String field, List<T> values) throws ValidationException {
//         Set<T> seen = new HashSet<>();
//         for (T value : values) {
//             // Abort at the first sign of a duplicate
//             if (!seen.add(value)) {
//                 throw new ValidationException(field, values, "must have all unique values");
//             }
//         }
//     }

//     @FunctionalInterface
//     public interface ValidationConsumer<T> {
//         void accept(T value) throws ValidationException;
//     }

//     public static <T> void forEach(List<T> values, ValidationConsumer<T> consumer) throws ValidationException {
//         for (T value : values) {
//             consumer.accept(value);
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/**
 * {@code RepeatedValidation} implements PGV validators for collection-type validators.
 */
public sealed static class RepeatedValidation
{
    public static void minItems<T>(string field, List<T> values, int expected)
    {
        if (values.Count < expected)
        {
            throw new ValidationException(field, values, "must have at least " + expected + " items");
        }
    }

    public static void maxItems<T>(string field, List<T> values, int expected)
    {
        if (values.Count > expected)
        {
            throw new ValidationException(field, values, "must have at most " + expected + " items");
        }
    }

    public static void unique<T>(string field, List<T> values)
    {
        var seen = new HashSet<T>();
        foreach (var value in values)
        {
            // Abort at the first sign of a duplicate
            if (!seen.Add(value))
            {
                throw new ValidationException(field, values, "must have all unique values");
            }
        }
    }

    public static void forEach<T>(List<T> values, Action<T> consumer)
    {
        foreach (var value in values)
        {
            consumer(value);
        }
    }
}
