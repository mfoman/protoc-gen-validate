// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

/**
 * Base class for failed field validations.
 */
// public class ValidationException extends Exception {
//     private String field;
//     private Object value;
//     private String reason;

//     public ValidationException(String field, Object value, String reason) {
//         super(field + ": " + reason + " - Got " + value.toString());
//         this.field = field;
//         this.value = value;
//         this.reason = reason;
//     }

//     public String getField() {
//         return field;
//     }

//     public Object getValue() {
//         return value;
//     }

//     public String getReason() {
//         return reason;
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

public class ValidationException : Exception
{
    private string field;
    private object value;
    private string reason;

    public ValidationException(string field, object value, string reason) : base(field + ": " + reason + " - Got " + value.ToString())
    {
        this.field = field;
        this.value = value;
        this.reason = reason;
    }

    public string getField()
    {
        return field;
    }

    public object getValue()
    {
        return value;
    }

    public string getReason()
    {
        return reason;
    }
}
