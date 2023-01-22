// package io.envoyproxy.pgv;
namespace Envoy.Pgv;


// import java.util.Comparator;

/**
 * {@code ComparativeValidation} implements PGV validation rules for ordering relationships.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ComparativeValidation
{
    public static void lessThan<T>(string field, T value, T limit, IComparer<T> comparator) where T : IComparable<T>
    {
        if (!lt(comparator.Compare(value, limit)))
        {
            throw new ValidationException(field, value, "must be less than " + limit.ToString());
        }
    }

    public static void lessThanOrEqual<T>(string field, T value, T limit, IComparer<T> comparator) where T : IComparable<T>
    {
        if (!lte(comparator.Compare(value, limit)))
        {
            throw new ValidationException(field, value, "must be less than or equal to " + limit.ToString());
        }
    }

    public static void greaterThan<T>(string field, T value, T limit, IComparer<T> comparator) where T : IComparable<T>
    {
        if (!gt(comparator.Compare(value, limit)))
        {
            throw new ValidationException(field, value, "must be greater than " + limit.ToString());
        }
    }

    public static void greaterThanOrEqual<T>(string field, T value, T limit, IComparer<T> comparator) where T : IComparable<T>
    {
        if (!gte(comparator.Compare(value, limit)))
        {
            throw new ValidationException(field, value, "must be greater than or equal to " + limit.ToString());
        }
    }

    public static void range<T>(string field, T value, T lt, T lte, T gt, T gte, IComparer<T> comparator) where T : IComparable<T>
    {
        T ltx = first(lt, lte);
        bool ltxInc = lte != null;

        T gtx = first(gt, gte);
        bool gtxInc = gte != null;

        // Inverting the values of lt(e) and gt(e) is valid and creates an exclusive range.
        // {gte:30, lt: 40} => x must be in the range [30, 40)
        // {lt:30, gte:40} => x must be outside the range [30, 40)
        if (lte(comparator.Compare(gtx, ltx)))
        {
            between(field, value, gtx, gtxInc, ltx, ltxInc, comparator);
        }
        else
        {
            outside(field, value, ltx, !ltxInc, gtx, !gtxInc, comparator);
        }
    }

    public static void between<T>(string field, T value, T lower, bool lowerInclusive, T upper, bool upperInclusive, IComparer<T> comparator) where T : IComparable<T>
    {
        if (!between(value, lower, lowerInclusive, upper, upperInclusive, comparator))
        {
            throw new ValidationException(field, value, "must be in the range " + range(lower, lowerInclusive, upper, upperInclusive));
        }
    }

    public static void outside<T>(string field, T value, T lower, bool lowerInclusive, T upper, bool upperInclusive, IComparer<T> comparator) where T : IComparable<T>
    {
        if (between(value, lower, lowerInclusive, upper, upperInclusive, comparator))
        {
            throw new ValidationException(field, value, "must be outside the range " + range(lower, lowerInclusive, upper, upperInclusive));
        }
    }

    private static bool between<T>(T value, T lower, bool lowerInclusive, T upper, bool upperInclusive, IComparer<T> comparator) where T : IComparable<T>
    {
        return (lowerInclusive ? gte(comparator.Compare(value, lower)) : gt(comparator.Compare(value, lower))) &&
               (upperInclusive ? lte(comparator.Compare(value, upper)) : lt(comparator.Compare(value, upper)));
    }

    private static string range<T>(T lower, bool lowerInclusive, T upper, bool upperInclusive) where T : IComparable<T>
    {
        return (lowerInclusive ? "[" : "(") + lower.ToString() + ", " + upper.ToString() + (upperInclusive ? "]" : ")");
    }

    private static bool lt(int comparatorResult)
    {
        return comparatorResult < 0;
    }

    private static bool lte(int comparatorResult)
    {
        return comparatorResult <= 0;
    }

    private static bool gt(int comparatorResult)
    {
        return comparatorResult > 0;
    }

    private static bool gte(int comparatorResult)
    {
        return comparatorResult >= 0;
    }

    private static T first<T>(T lhs, T rhs) where T : IComparable<T>
    {
        return lhs != null ? lhs : rhs;
    }

}

// public final class ComparativeValidation {
//     private ComparativeValidation() {
//     }

//     public static <T> void lessThan(String field, T value, T limit, Comparator<T> comparator) throws ValidationException {
//         if (!lt(comparator.compare(value, limit))) {
//             throw new ValidationException(field, value, "must be less than " + limit.toString());
//         }
//     }

//     public static <T> void lessThanOrEqual(String field, T value, T limit, Comparator<T> comparator) throws ValidationException {
//         if (!lte(comparator.compare(value, limit))) {
//             throw new ValidationException(field, value, "must be less than or equal to " + limit.toString());
//         }
//     }

//     public static <T> void greaterThan(String field, T value, T limit, Comparator<T> comparator) throws ValidationException {
//         if (!gt(comparator.compare(value, limit))) {
//             throw new ValidationException(field, value, "must be greater than " + limit.toString());
//         }
//     }

//     public static <T> void greaterThanOrEqual(String field, T value, T limit, Comparator<T> comparator) throws ValidationException {
//         if (!gte(comparator.compare(value, limit))) {
//             throw new ValidationException(field, value, "must be greater than or equal to " + limit.toString());
//         }
//     }

//     public static <T> void range(String field, T value, T lt, T lte, T gt, T gte, Comparator<T> comparator) throws ValidationException {
//         T ltx = first(lt, lte);
//         boolean ltxInc = lte != null;

//         T gtx = first(gt, gte);
//         boolean gtxInc = gte != null;

//         // Inverting the values of lt(e) and gt(e) is valid and creates an exclusive range.
//         // {gte:30, lt: 40} => x must be in the range [30, 40)
//         // {lt:30, gte:40} => x must be outside the range [30, 40)
//         if (lte(comparator.compare(gtx, ltx))) {
//             between(field, value, gtx, gtxInc, ltx, ltxInc, comparator);
//         } else {
//             outside(field, value, ltx, !ltxInc, gtx, !gtxInc, comparator);
//         }
//     }

//     public static <T> void between(String field, T value, T lower, boolean lowerInclusive, T upper, boolean upperInclusive, Comparator<T> comparator) throws ValidationException {
//         if (!between(value, lower, lowerInclusive, upper, upperInclusive, comparator)) {
//             throw new ValidationException(field, value, "must be in the range " + range(lower, lowerInclusive, upper, upperInclusive));
//         }
//     }

//     public static <T> void outside(String field, T value, T lower, boolean lowerInclusive, T upper, boolean upperInclusive, Comparator<T> comparator) throws ValidationException {
//         if (between(value, lower, lowerInclusive, upper, upperInclusive, comparator)) {
//             throw new ValidationException(field, value, "must be outside the range " + range(lower, lowerInclusive, upper, upperInclusive));
//         }
//     }

//     private static <T> boolean between(T value, T lower, boolean lowerInclusive, T upper, boolean upperInclusive, Comparator<T> comparator) {
//         return (lowerInclusive ? gte(comparator.compare(value, lower)) : gt(comparator.compare(value, lower))) &&
//                (upperInclusive ? lte(comparator.compare(value, upper)) : lt(comparator.compare(value, upper)));
//     }

//     private static <T> String range(T lower, boolean lowerInclusive, T upper, boolean upperInclusive) {
//         return (lowerInclusive ? "[" : "(") + lower.toString() + ", " + upper.toString() + (upperInclusive ? "]" : ")");
//     }

//     private static boolean lt(int comparatorResult) {
//         return comparatorResult < 0;
//     }

//     private static boolean lte(int comparatorResult) {
//         return comparatorResult <= 0;
//     }

//     private static boolean gt(int comparatorResult) {
//         return comparatorResult > 0;
//     }

//     private static boolean gte(int comparatorResult) {
//         return comparatorResult >= 0;
//     }

//     private static <T> T first(T lhs, T rhs) {
//         return lhs != null ? lhs : rhs;
//     }
// }
