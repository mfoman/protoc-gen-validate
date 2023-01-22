// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// /**
//  * {@code Validator} is the base interface for all generated PGV validators.
//  * @param <T> The type to validate
//  */
// @FunctionalInterface
// public interface ValidatorImpl<T> {
//     /**
//      * Asserts validation rules on a protobuf object.
//      *
//      * @param proto the protobuf object to validate.
//      * @throws ValidationException with the first validation error encountered.
//      */
//     void assertValid(T proto, ValidatorIndex index) throws ValidationException;

//     ValidatorImpl ALWAYS_VALID = (proto, index) -> {
//         // Do nothing. Always valid.
//     };
// }

using System;

[Attribute] // C# does not have built-in support for functional interface, but you can use the `FunctionalInterfaceAttribute` from the `System` namespace
public interface ValidatorImpl<T>
{
    void AssertValid(T proto, ValidatorIndex index);

    static readonly ValidatorImpl<T> ALWAYS_VALID = (proto, index) => { };
}
