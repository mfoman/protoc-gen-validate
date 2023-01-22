// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import java.util.Arrays;

/**
 * {@code CollectiveValidation} implements PGV validators for the collective {@code in} and {@code notIn} rules.
 */
public sealed class CollectiveValidation
{
    private CollectiveValidation() {
    }

    public static void In<T>(string field, T value, T[] set)
    {
        if (!set.Contains(value))
        {
            throw new ValidationException(field, value, $"must be in {string.Join(",", set)}");
        }
    }

    public static void NotIn<T>(string field, T value, T[] set)
    {
        if (set.Contains(value))
        {
            throw new ValidationException(field, value, $"must not be in {string.Join(",", set)}");
        }
    }

    // public static <T> void in(String field, T value, T[] set) throws ValidationException {
    //     for (T i : set) {
    //         if (value.equals(i)) {
    //             return;
    //         }
    //     }

    //     throw new ValidationException(field, value, "must be in " + Arrays.toString(set));
    // }

    // public static <T> void notIn(String field, T value, T[] set) throws ValidationException {
    //     for (T i : set) {
    //         if (value.equals(i)) {
    //             throw new ValidationException(field, value, "must not be in " + Arrays.toString(set));
    //         }
    //     }
    // }
}
