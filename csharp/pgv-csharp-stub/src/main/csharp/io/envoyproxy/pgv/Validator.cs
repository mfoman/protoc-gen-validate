// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

/**
 * {@code Validator} asserts the validity of a protobuf object.
 * @param <T> The type to validate
 */
// @FunctionalInterface
// public interface Validator<T> {
//     /**
//      * Asserts validation rules on a protobuf object.
//      *
//      * @param proto the protobuf object to validate.
//      * @throws ValidationException with the first validation error encountered.
//      */
//     void assertValid(T proto) throws ValidationException;

//     /**
//      * Checks validation rules on a protobuf object.
//      *
//      * @param proto the protobuf object to validate.
//      * @return {@code true} if all rules are valid, {@code false} if not.
//      */
//     default boolean isValid(T proto) {
//         try {
//             assertValid(proto);
//             return true;
//         } catch (io.envoyproxy.pgv.ValidationException ex) {
//             return false;
//         }
//     }

//     Validator ALWAYS_VALID = (proto) -> {
//         // Do nothing. Always valid.
//     };

//     Validator ALWAYS_INVALID = (proto) -> {
//         throw new ValidationException("UNKNOWN", new Object(), "Explicitly invalid");
//     };
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

[Attribute] // C# does not have built-in support for functional interface, but you can use the `FunctionalInterfaceAttribute` from the `System` namespace
public interface Validator<T>
{
    void AssertValid(T proto);

    bool IsValid(T proto)
    {
        try
        {
            AssertValid(proto);
            return true;
        }
        catch (ValidationException ex)
        {
            return false;
        }
    }

    static readonly Validator<T> ALWAYS_VALID = (proto) => { };

    static readonly Validator<T> ALWAYS_INVALID = (proto) =>
    {
        throw new ValidationException("UNKNOWN", new object(), "Explicitly invalid");
    };
}
