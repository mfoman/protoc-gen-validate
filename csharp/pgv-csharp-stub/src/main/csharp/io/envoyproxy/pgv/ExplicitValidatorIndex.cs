// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

// import java.util.concurrent.ConcurrentHashMap;

// public final class ExplicitValidatorIndex implements ValidatorIndex {
//     private final ConcurrentHashMap<Class, ValidatorImpl> VALIDATOR_IMPL_INDEX = new ConcurrentHashMap<>();
//     private final ConcurrentHashMap<Class, Validator> VALIDATOR_INDEX = new ConcurrentHashMap<>();
//     private final ValidatorIndex fallbackIndex;

//     public ExplicitValidatorIndex() {
//         this(ValidatorIndex.ALWAYS_VALID);
//     }

//     public ExplicitValidatorIndex(ValidatorIndex fallbackIndex) {
//         this.fallbackIndex = fallbackIndex;
//     }

//     /**
//      * Adds a {@link Validator} to the set of known validators.
//      * @param forType the type to validate
//      * @param validator the validator to apply
//      * @return this
//      */
//     public <T> ExplicitValidatorIndex add(Class<T> forType, ValidatorImpl<T> validator) {
//         VALIDATOR_IMPL_INDEX.put(forType, validator);
//         return this;
//     }

//     @SuppressWarnings("unchecked")
//     public <T> Validator<T> validatorFor(Class clazz) {
//         return VALIDATOR_INDEX.computeIfAbsent(clazz, c ->
//                 proto -> VALIDATOR_IMPL_INDEX.getOrDefault(c, (p, i) -> fallbackIndex.validatorFor(c))
//                         .assertValid(proto, ExplicitValidatorIndex.this));
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

/**
 * {@code ExplicitValidatorIndex} is an explicit registry of {@link Validator} instances. If no validator is registered
 * for {@code type}, a fallback validator will be used (default ALWAYS_VALID).
 */
public sealed class ExplicitValidatorIndex : ValidatorIndex
{
    private readonly Dictionary<Type, ValidatorImpl> VALIDATOR_IMPL_INDEX = new Dictionary<Type, ValidatorImpl>();
    private readonly Dictionary<Type, Validator> VALIDATOR_INDEX = new Dictionary<Type, Validator>();
    private readonly ValidatorIndex fallbackIndex;

    public ExplicitValidatorIndex()
    {
        this(ValidatorIndex.ALWAYS_VALID);
    }

    public ExplicitValidatorIndex(ValidatorIndex fallbackIndex)
    {
        this.fallbackIndex = fallbackIndex;
    }

    /**
     * Adds a {@link Validator} to the set of known validators.
     * @param forType the type to validate
     * @param validator the validator to apply
     * @return this
     */
    public ExplicitValidatorIndex add<T>(ValidatorImpl<T> validator)
    {
        VALIDATOR_IMPL_INDEX.Add(typeof(T), validator);
        return this;
    }

    public Validator<T> validatorFor<T>(Type clazz)
    {
        return VALIDATOR_INDEX.GetOrAdd(clazz, c =>
                proto => VALIDATOR_IMPL_INDEX.GetOrDefault(c, (p, i) => fallbackIndex.validatorFor(c)).assertValid(proto, ExplicitValidatorIndex));
    }
}
