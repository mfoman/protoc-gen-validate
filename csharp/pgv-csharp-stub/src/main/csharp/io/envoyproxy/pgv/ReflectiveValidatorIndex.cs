// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import java.sql.Ref;
// import java.util.concurrent.ConcurrentHashMap;

/**
 * {@code ReflectiveValidatorIndex} uses reflection to discover {@link Validator} implementations lazily the first
 * time a type is validated. If no validator can be found for {@code type}, a fallback validator
 * will be used (default ALWAYS_VALID).
 */
// public final class ReflectiveValidatorIndex implements ValidatorIndex {
//     private final ConcurrentHashMap<Class, Validator> VALIDATOR_INDEX = new ConcurrentHashMap<>();
//     private final ValidatorIndex fallbackIndex;

//     public ReflectiveValidatorIndex() {
//         this(ValidatorIndex.ALWAYS_VALID);
//     }

//     /**
//      * @param fallbackIndex a {@link ValidatorIndex} implementation to use if reflective validator discovery fails.
//      */
//     public ReflectiveValidatorIndex(ValidatorIndex fallbackIndex) {
//         this.fallbackIndex = fallbackIndex;
//     }

//     /**
//      * Returns the validator for {@code <T>}, or {@code ALWAYS_VALID} if not found.
//      */
//     @Override
//     @SuppressWarnings("unchecked")
//     public <T> Validator<T> validatorFor(Class clazz) {
//         return VALIDATOR_INDEX.computeIfAbsent(clazz, c -> {
//             try {
//                 return reflectiveValidatorFor(c);
//             } catch (ReflectiveOperationException ex) {
//                 return fallbackIndex.validatorFor(clazz);
//             }
//         });
//     }

//     @SuppressWarnings("unchecked")
//     private Validator reflectiveValidatorFor(Class clazz) throws ReflectiveOperationException {
//         Class enclosingClass = clazz;
//         while (enclosingClass.getEnclosingClass() != null) {
//             enclosingClass = enclosingClass.getEnclosingClass();
//         }

//         String validatorClassName = enclosingClass.getName() + "Validator";
//         Class validatorClass = clazz.getClassLoader().loadClass(validatorClassName);
//         ValidatorImpl impl = (ValidatorImpl) validatorClass.getDeclaredMethod("validatorFor", Class.class).invoke(null, clazz);

//         return proto -> impl.assertValid(proto, ReflectiveValidatorIndex.this);
// }
// }

using System;
using System.Collections.Concurrent;
using System.Reflection;


public sealed class ReflectiveValidatorIndex : ValidatorIndex
{
    private readonly ConcurrentDictionary<Type, Validator> validatorIndex = new ConcurrentDictionary<Type, Validator>();
    private readonly ValidatorIndex fallbackIndex;

    public ReflectiveValidatorIndex()
    {
        this(ValidatorIndex.ALWAYS_VALID);
    }

    public ReflectiveValidatorIndex(ValidatorIndex fallbackIndex)
    {
        this.fallbackIndex = fallbackIndex;
    }

    public override Validator<T> ValidatorFor<T>(Type clazz)
    {
        return validatorIndex.GetOrAdd(clazz, c =>
        {
            try
            {
                return ReflectiveValidatorFor(c);
            }
            catch (Exception ex)
            {
                return fallbackIndex.ValidatorFor<T>(clazz);
            }
        });
    }

    private Validator ReflectiveValidatorFor(Type clazz)
    {
        Type enclosingClass = clazz;
        while (enclosingClass.DeclaringType != null)
        {
            enclosingClass = enclosingClass.DeclaringType;
        }

        string validatorClassName = enclosingClass.FullName + "Validator";
        Type validatorClass = clazz.Assembly.GetType(validatorClassName);
        var validatorForMethod = validatorClass.GetMethod("validatorFor", new Type[] { typeof(Type) });
        var impl = validatorForMethod.Invoke(null, new object[] { clazz }) as ValidatorImpl;

        return proto => impl.AssertValid(proto, this);
    }
}
