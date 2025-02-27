// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

/**
 * {@code BytesValidation} implements PGV validators for protobuf {@code Byte} fields.
 */
public static class BytesValidation
{
    public static void length(string field, ByteString value, int expected)
    {
        if (value.Length != expected)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "length must be " + expected);
        }
    }

    public static void minLength(string field, ByteString value, int expected)
    {
        if (value.Length < expected)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "length must be at least " + expected);
        }
    }

    public static void maxLength(string field, ByteString value, int expected)
    {
        if (value.Length > expected)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "length must be at most " + expected);
        }
    }

    public static void prefix(string field, ByteString value, byte[] prefix)
    {
        if (!value.StartsWith(prefix))
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should start with " + prefix);
        }
    }

    public static void contains(string field, ByteString value, byte[] contains)
    {
        if (!value.Contains(contains))
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should contain " + contains);
        }
    }

    public static void suffix(string field, ByteString value, byte[] suffix)
    {
        if (!value.EndsWith(suffix))
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should end with " + suffix);
        }
    }

    public static void pattern(string field, ByteString value, Regex p)
    {
        if (!p.IsMatch(value.ToStringUtf8()))
        {
            throw new ValidationException(field, value.ToStringUtf8(), "must match pattern " + p);
        }
    }

    public static void ip(string field, ByteString value)
    {
        if (value.Length != 4 && value.Length != 16)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should be valid ip address " + value);
        }
    }

    public static void ipv4(string field, ByteString value)
    {
        if (value.Length != 4)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should be valid ipv4 address " + value);
        }
    }

    public static void ipv6(string field, ByteString value)
    {
        if (value.Length != 16)
        {
            throw new ValidationException(field, value.ToStringUtf8(), "should be valid ipv6 address " + value);
        }
    }
}


// import com.google.common.primitives.Bytes;
// import com.google.protobuf.ByteString;
// import com.google.re2j.Pattern;

// import java.util.Arrays;

// public final class BytesValidation {
//     private BytesValidation() {
//     }

//     public static void length(String field, ByteString value, int expected) throws ValidationException {
//         if (value.size() != expected) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "length must be " + expected);
//         }
//     }

//     public static void minLength(String field, ByteString value, int expected) throws ValidationException {
//         if (value.size() < expected) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "length must be at least " + expected);
//         }
//     }

//     public static void maxLength(String field, ByteString value, int expected) throws ValidationException {
//         if (value.size() > expected) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "length must be at most " + expected);
//         }
//     }

//     public static void prefix(String field, ByteString value, byte[] prefix) throws ValidationException {
//         if (!value.startsWith(ByteString.copyFrom(prefix))) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should start with " + Arrays.toString(prefix));
//         }
//     }

//     public static void contains(String field, ByteString value, byte[] contains) throws ValidationException {
//         if (Bytes.indexOf(value.toByteArray(), contains) == -1) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should contain " + Arrays.toString(contains));
//         }
//     }

//     public static void suffix(String field, ByteString value, byte[] suffix) throws ValidationException {
//         if (!value.endsWith(ByteString.copyFrom(suffix))) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should end with " + Arrays.toString(suffix));
//         }
//     }

//     public static void pattern(String field, ByteString value, Pattern p) throws ValidationException {
//         if (!p.matches(value.toStringUtf8())) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "must match pattern " + p.pattern());
//         }
//     }

//     public static void ip(String field, ByteString value) throws ValidationException {
//         if (value.toByteArray().length != 4 && value.toByteArray().length != 16) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should be valid ip address " + value);
//         }
//     }

//     public static void ipv4(String field, ByteString value) throws ValidationException {
//         if (value.toByteArray().length != 4) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should be valid ipv4 address " + value);
//         }
//     }

//     public static void ipv6(String field, ByteString value) throws ValidationException {
//         if (value.toByteArray().length != 16) {
//             throw new ValidationException(field, Arrays.toString(value.toByteArray()), "should be valid ipv6 address " + value);
//         }
//     }
// }
