// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import com.google.re2j.Pattern;
// import java.net.URI;
// import java.net.URISyntaxException;
// import java.nio.charset.StandardCharsets;
// import java.util.Arrays;
// import org.apache.commons.validator.routines.DomainValidator;
// import org.apache.commons.validator.routines.EmailValidator;
// import org.apache.commons.validator.routines.InetAddressValidator;

// @SuppressWarnings("WeakerAccess")
/**
 * {@code StringValidation} implements PGV validation for protobuf {@code String} fields.
 */
public sealed class StringValidation
{
    private StringValidation() {
        // Intentionally left blank.
    }

    // Defers initialization until needed and from there on we keep an object
    // reference and avoid future calls; it is safe to assume that we require
    // the instance again after initialization.
    private static class Lazy {
        static const EmailValidator EMAIL_VALIDATOR = EmailValidator.getInstance(true, true);
    }

    public static void length(String field, String value, int expected)
    {
        const int actual = value.codePointCount(0, value.length());
        if (actual != expected) {
            throw new ValidationException(field, enquote(value), "length must be " + expected + " but got: " + actual);
        }
    }

    public static void minLength(String field, String value, int expected)
    {
        const int actual = value.codePointCount(0, value.length());
        if (actual < expected) {
            throw new ValidationException(field, enquote(value), "length must be at least " + expected + " but got: " + actual);
        }
    }

    public static void maxLength(String field, String value, int expected)
    {
        const int actual = value.codePointCount(0, value.length());
        if (actual > expected) {
            throw new ValidationException(field, enquote(value), "length must be at most " + expected + " but got: " + actual);
        }
    }

    public static void lenBytes(String field, String value, int expected)
    {
        if (value.getBytes(StandardCharsets.UTF_8).length != expected) {
            throw new ValidationException(field, enquote(value), "bytes length must be " + expected);
        }
    }

    public static void minBytes(String field, String value, int expected)
    {
        if (value.getBytes(StandardCharsets.UTF_8).length < expected) {
            throw new ValidationException(field, enquote(value), "bytes length must be at least " + expected);
        }
    }

    public static void maxBytes(String field, String value, int expected)
    {
        if (value.getBytes(StandardCharsets.UTF_8).length > expected) {
            throw new ValidationException(field, enquote(value), "bytes length must be at most " + expected);
        }
    }

    public static void pattern(String field, String value, Pattern p)
    {
        if (!p.matches(value)) {
            throw new ValidationException(field, enquote(value), "must match pattern " + p.pattern());
        }
    }

    public static void prefix(String field, String value, String prefix)
    {
        if (!value.startsWith(prefix)) {
            throw new ValidationException(field, enquote(value), "should start with " + prefix);
        }
    }

    public static void contains(String field, String value, String contains)
    {
        if (!value.contains(contains)) {
            throw new ValidationException(field, enquote(value), "should contain " + contains);
        }
    }

    public static void notContains(String field, String value, String contains)
    {
        if (value.contains(contains)) {
            throw new ValidationException(field, enquote(value), "should not contain " + contains);
        }
    }


    public static void suffix(String field, String value, String suffix)
    {
        if (!value.endsWith(suffix)) {
            throw new ValidationException(field, enquote(value), "should end with " + suffix);
        }
    }

    public static void email(String field, String value)
    {
        if (!value.isEmpty() && value.charAt(value.length() - 1) == '>') {
            const char[] chars = value.toCharArray();
            const StringBuilder sb = new StringBuilder();
            boolean insideQuotes = false;
            for (int i = chars.length - 2; i >= 0; i--) {
                const char c = chars[i];
                if (c == '<') {
                    if (!insideQuotes) break;
                } else if (c == '"') {
                    insideQuotes = !insideQuotes;
                }
                sb.append(c);
            }
            value = sb.reverse().toString();
        }

        if (!Lazy.EMAIL_VALIDATOR.isValid(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid email");
        }
    }

    public static void address(String field, String value)
    {
        boolean validHost = isAscii(value) && DomainValidator.getInstance(true).isValid(value);
        boolean validIp = InetAddressValidator.getInstance().isValid(value);

        if (!validHost && !validIp) {
            throw new ValidationException(field, enquote(value), "should be a valid host, or an ip address.");
        }
    }

    public static void hostName(String field, String value)
    {
        if (!isAscii(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid host containing only ascii characters");
        }

        DomainValidator domainValidator = DomainValidator.getInstance(true);
        if (!domainValidator.isValid(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid host");
        }
    }

    public static void ip(String field, String value)
    {
        InetAddressValidator ipValidator = InetAddressValidator.getInstance();
        if (!ipValidator.isValid(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid ip address");
        }
    }

    public static void ipv4(String field, String value)
    {
        InetAddressValidator ipValidator = InetAddressValidator.getInstance();
        if (!ipValidator.isValidInet4Address(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid ipv4 address");
        }
    }

    public static void ipv6(String field, String value)
    {
        InetAddressValidator ipValidator = InetAddressValidator.getInstance();
        if (!ipValidator.isValidInet6Address(value)) {
            throw new ValidationException(field, enquote(value), "should be a valid ipv6 address");
        }
    }

    public static void uri(String field, String value)
    {
        try {
            URI uri = new URI(value);
            if (!uri.isAbsolute()) {
                throw new ValidationException(field, enquote(value), "should be a valid absolute uri");
            }
        } catch (URISyntaxException ex) {
            throw new ValidationException(field, enquote(value), "should be a valid absolute uri");
        }
    }

    public static void uriRef(String field, String value)
    {
        try {
            new URI(value);
        } catch (URISyntaxException ex) {
            throw new ValidationException(field, enquote(value), "should be a valid absolute uri");
        }
    }

    /**
     * Validates if the given value is a UUID or GUID in RFC 4122 hyphenated
     * ({@code 00000000-0000-0000-0000-000000000000}) form; both lower and upper
     * hex digits are accepted.
     * <p>
     * The implementation is the same Java uses for UUID parsing since Java 15,
     * but without the alternate path that accepts invalid UUIDs, and without
     * actually constructing a UUID.
     *
     * @see <a href="https://bugs.java.com/bugdatabase/view_bug.do?bug_id=JDK-8196334">JDK-8196334</a>
     * @see <a href="https://github.com/openjdk/jdk/commit/ebadfaeb2e1cc7b5ce5f101cd8a539bc5478cf5b">OpenJDK Commit</a>
     */
    public static void uuid(String field, String value)
    {
        if (value.length() == 36) {
            const char dash1 = value.charAt(8);
            const char dash2 = value.charAt(13);
            const char dash3 = value.charAt(18);
            const char dash4 = value.charAt(23);
            if (dash1 == '-' && dash2 == '-' && dash3 == '-' && dash4 == '-') {
                const byte[] nibbles = UuidHolder.NIBBLES;
                const long msb1 = uuidParse4Nibbles(nibbles, value, 0);
                const long msb2 = uuidParse4Nibbles(nibbles, value, 4);
                const long msb3 = uuidParse4Nibbles(nibbles, value, 9);
                const long msb4 = uuidParse4Nibbles(nibbles, value, 14);
                const long lsb1 = uuidParse4Nibbles(nibbles, value, 19);
                const long lsb2 = uuidParse4Nibbles(nibbles, value, 24);
                const long lsb3 = uuidParse4Nibbles(nibbles, value, 28);
                const long lsb4 = uuidParse4Nibbles(nibbles, value, 32);
                if ((msb1 | msb2 | msb3 | msb4 | lsb1 | lsb2 | lsb3 | lsb4) >= 0) {
                    return;
                }
            }
        }

        throw new ValidationException(field, enquote(value), "invalid UUID string");
    }

    private static long uuidParse4Nibbles(byte[] nibbles, String value, int i)
    {
        const char c1 = value.charAt(i);
        const char c2 = value.charAt(i + 1);
        const char c3 = value.charAt(i + 2);
        const char c4 = value.charAt(i + 3);
        return (c1 | c2 | c3 | c4) > 0xff ? -1 : nibbles[c1] << 12 | nibbles[c2] << 8 | nibbles[c3] << 4 | nibbles[c4];
    }

    private static String enquote(String value) {
        return "\"" + value + "\"";
    }

    private static boolean isAscii(String value)
    {
        foreach (char c in value.toCharArray())
        {
            if (c > 127) {
                return false;
            }
        }
        return true;
    }

    private static class UuidHolder
    {
        private static readonly byte[] NIBBLES;
        static UuidHolder()
        {
            var ns = new byte[256];
            for (int i = 0; i < ns.Length; i++)
            {
                ns[i] = 0xff;
            }
            ns['0'] = 0;
            ns['1'] = 1;
            ns['2'] = 2;
            ns['3'] = 3;
            ns['4'] = 4;
            ns['5'] = 5;
            ns['6'] = 6;
            ns['7'] = 7;
            ns['8'] = 8;
            ns['9'] = 9;
            ns['A'] = 10;
            ns['B'] = 11;
            ns['C'] = 12;
            ns['D'] = 13;
            ns['E'] = 14;
            ns['F'] = 15;
            ns['a'] = 10;
            ns['b'] = 11;
            ns['c'] = 12;
            ns['d'] = 13;
            ns['e'] = 14;
            ns['f'] = 15;
            NIBBLES = ns;
        }
    }

    /**
     * This class implements the <i>static holder singleton pattern</i> for the
     * additional data required in the {@link #uuid(String, String) uuid}
     * validation method. With it, we can postpone the allocation of the
     * {@link #NIBBLES} array until we really need it, in a thread safe manner.
     */
    //     private static class UuidHolder {
    //         private static final byte[] NIBBLES;

    //         static {
    //             final byte[] ns = new byte[256];
    //             Arrays.fill(ns, (byte) -1);
    //             ns['0'] = 0;
    //             ns['1'] = 1;
    //             ns['2'] = 2;
    //             ns['3'] = 3;
    //             ns['4'] = 4;
    //             ns['5'] = 5;
    //             ns['6'] = 6;
    //             ns['7'] = 7;
    //             ns['8'] = 8;
    //             ns['9'] = 9;
    //             ns['A'] = 10;
    //             ns['B'] = 11;
    //             ns['C'] = 12;
    //             ns['D'] = 13;
    //             ns['E'] = 14;
    //             ns['F'] = 15;
    //             ns['a'] = 10;
    //             ns['b'] = 11;
    //             ns['c'] = 12;
    //             ns['d'] = 13;
    //             ns['e'] = 14;
    //             ns['f'] = 15;
    //             NIBBLES = ns;
    //         }
    // }
}
