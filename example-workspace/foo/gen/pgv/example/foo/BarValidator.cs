// Code generated by protoc-gen-validate. DO NOT EDIT.
// source: example-workspace/foo/bar.proto

namespace pgv.example.foo;



#pragma warning disable 0169, 0414, anyothernumber
public class BarValidator
{
    public static io.envoyproxy.pgv.ValidatorImpl validatorFor(Class clazz)
    {

        if (clazz.equals(pgv.example.foo.Bar.BarNone)) return new BarNoneValidator();
        if (clazz.equals(pgv.example.foo.Bar.BarOne)) return new BarOneValidator();
        if (clazz.equals(pgv.example.foo.Bar.Bars)) return new BarsValidator();
        return null;
    }


    /**
         * Validates {@code BarNone} protobuf objects.
         */
    public static class BarNoneValidator : io.envoyproxy.pgv.ValidatorImpl<pgv.example.foo.Bar.BarNone>
    {





        public void assertValid(pgv.example.foo.Bar.BarNone proto, io.envoyproxy.pgv.ValidatorIndex index)
        {
            // no validation rules for Value



        }
    }
    /**
         * Validates {@code BarOne} protobuf objects.
         */
    public static class BarOneValidator : io.envoyproxy.pgv.ValidatorImpl<pgv.example.foo.Bar.BarOne>
    {

        private readonly Integer[] VALUE__NOT_IN = new Integer[] { 1, };




        public void assertValid(pgv.example.foo.Bar.BarOne proto, io.envoyproxy.pgv.ValidatorIndex index)
        {

            io.envoyproxy.pgv.CollectiveValidation.notIn(".pgv.example.foo.BarOne.value", proto.getValue(), VALUE__NOT_IN);


        }
    }
    /**
         * Validates {@code Bars} protobuf objects.
         */
    public static class BarsValidator : io.envoyproxy.pgv.ValidatorImpl<pgv.example.foo.Bar.Bars>
    {
        public void assertValid(pgv.example.foo.Bar.Bars proto, io.envoyproxy.pgv.ValidatorIndex index)
        {

            // Validate bar_none
            if (proto.hasBarNone()) index.validatorFor(proto.getBarNone()).assertValid(proto.getBarNone());

            // Validate bar_one
            if (proto.hasBarOne()) index.validatorFor(proto.getBarOne()).assertValid(proto.getBarOne());
        }
    }
}

