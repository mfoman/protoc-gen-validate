// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import com.google.protobuf.Duration;
// import com.google.protobuf.Timestamp;
// import com.google.protobuf.util.Durations;
// import com.google.protobuf.util.Timestamps;

// public final class TimestampValidation {
//     private TimestampValidation() { }

//     public static void within(String field, Timestamp value, Duration duration, Timestamp when) throws ValidationException {
//         Duration between = Timestamps.between(when, value);
//         if (Long.compare(Math.abs(Durations.toNanos(between)), Math.abs(Durations.toNanos(duration))) == 1) {
//             throw new ValidationException(field, value, "value must be within " + Durations.toString(duration) + " of " + Timestamps.toString(when));
//         }
//     }

//     /**
//      * Converts {@code seconds} and {@code nanos} to a protobuf {@code Timestamp}.
//      */
//     public static Timestamp toTimestamp(long seconds, int nanos) {
//         return Timestamp.newBuilder()
//                 .setSeconds(seconds)
//                 .setNanos(nanos)
//                 .build();
//     }

//     /**
//      * Converts {@code seconds} and {@code nanos} to a protobuf {@code Duration}.
//      */
//     public static Duration toDuration(long seconds, long nanos) {
//         return Duration.newBuilder()
//                 .setSeconds(seconds)
//                 .setNanos((int) nanos)
//                 .build();
//     }

//     public static Timestamp currentTimestamp() {
//         return Timestamps.fromMillis(System.currentTimeMillis());
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

/**
 * {@code TimestampValidation} implements PGV validation for protobuf {@code Timestamp} fields.
 */
public sealed class TimestampValidation
{
    public static void within(string field, Timestamp value, Duration duration, Timestamp when)
    {
        Duration between = Timestamps.Between(when, value);
        if (Math.Abs(Durations.ToNanos(between)).CompareTo(Math.Abs(Durations.ToNanos(duration))) == 1)
        {
            throw new ValidationException(field, value, "value must be within " + Durations.ToString(duration) + " of " + Timestamps.ToString(when));
        }
    }

    /**
     * Converts {@code seconds} and {@code nanos} to a protobuf {@code Timestamp}.
     */
    public static Timestamp toTimestamp(long seconds, int nanos)
    {
        return Timestamp.FromDateTimeOffset(new DateTimeOffset(seconds, nanos, TimeSpan.Zero));
    }

    /**
     * Converts {@code seconds} and {@code nanos} to a protobuf {@code Duration}.
     */
    public static Duration toDuration(long seconds, long nanos)
    {
        return Duration.FromTimeSpan(new TimeSpan(seconds, nanos));
    }

    public static Timestamp currentTimestamp()
    {
        return Timestamp.FromDateTimeOffset(DateTimeOffset.Now);
    }
}
