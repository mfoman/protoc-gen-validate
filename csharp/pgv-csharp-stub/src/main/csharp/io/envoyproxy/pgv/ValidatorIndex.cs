// package io.envoyproxy.pgv;
namespace Envoy.Pgv;

/**
 * {@code ValidatorIndex} defines the entry point for finding {@link Validator} instances for a given type.
 */
// @FunctionalInterface
// public interface ValidatorIndex {
//     /**
//      * Returns the validator for {@code clazz}, or {@code ALWAYS_VALID} if not found.
//      */
//     <T> Validator<T> validatorFor(Class clazz);

//     /**
//      * Returns the validator for {@code <T>}, or {@code ALWAYS_VALID} if not found.
//      */
//     @SuppressWarnings("unchecked")
//     default <T> Validator<T> validatorFor(Object instance) {
//         return validatorFor(instance == null ? null : instance.getClass());
//     }

//     ValidatorIndex ALWAYS_VALID = new ValidatorIndex() {
//         @Override
//         @SuppressWarnings("unchecked")
//         public <T> Validator<T> validatorFor(Class clazz) {
//             return Validator.ALWAYS_VALID;
//         }
//     };

//     ValidatorIndex ALWAYS_INVALID = new ValidatorIndex() {
//         @Override
//         @SuppressWarnings("unchecked")
//         public <T> Validator<T> validatorFor(Class clazz) {
//             return Validator.ALWAYS_INVALID;
//         }
//     };
// }

// public interface IValidatorIndex
// {
//     Validator<T> validatorFor<T>(Type clazz);
//     Validator<T> validatorFor<T>(object instance);

//     IValidatorIndex ALWAYS_VALID = new IValidatorIndex()
//     {
//         public Validator<T> validatorFor<T>(Type clazz)
//     {
//         return Validator<T>.ALWAYS_VALID;
//     }
// };

// IValidatorIndex ALWAYS_INVALID = new IValidatorIndex()
// {
//         public Validator<T> validatorFor<T>(Type clazz)
// {
//     return Validator<T>.ALWAYS_INVALID;
// }
//     };
// }

[Attribute]
public interface IValidatorIndex
{
    Validator<T> validatorFor<T>(Type clazz);
    Validator<T> validatorFor<T>(object instance);

    IValidatorIndex AlwaysValid { get; }
    IValidatorIndex AlwaysInvalid { get; }
}

public class ValidatorIndex : IValidatorIndex
{
    public Validator<T> validatorFor<T>(Type clazz)
    {
        // implementation
    }

    public Validator<T> validatorFor<T>(object instance)
    {
        // implementation
    }

    public IValidatorIndex AlwaysValid => new ValidatorIndex(Validator<T>.ALWAYS_VALID);
    public IValidatorIndex AlwaysInvalid => new ValidatorIndex(Validator<T>.ALWAYS_INVALID);
}
