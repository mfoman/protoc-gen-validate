// package io.envoyproxy.pgv.grpc;
namespace Envoy.Proxy.Pgv.Grpc;

// import io.envoyproxy.pgv.ValidationException;
// import io.envoyproxy.pgv.ValidatorIndex;
// import io.grpc.*;
using Envoyproxy.Pgv;
using Grpc;
using Grpc.Core;
using Envoyproxy.Pgv.ValidatorIndex;

/**
 * {@code ValidatingClientInterceptor} is a gRPC {@code ClientInterceptor} that validates outbound gRPC messages before
 * transmission. Non-compliant messages result in an {@code INVALID_ARGUMENT} status exception.
 */
public class ValidatingClientInterceptor : ClientInterceptor
{
    private readonly ValidatorIndex index;

    public ValidatingClientInterceptor(ValidatorIndex index)
    {
        this.index = index;
    }

    public override ClientCall<ReqT, RespT> InterceptCall<ReqT, RespT>(MethodDescriptor<ReqT, RespT> method, CallOptions callOptions, Channel next)
    {
        var call = next.NewCall(method, callOptions);
        var forwardingClientCall = new ForwardingClientCall<ReqT, RespT>(call);

        forwardingClientCall.SendMessage = (message) =>
        {
            try
            {
                index.ValidatorFor(message.GetType()).AssertValid(message);
                forwardingClientCall.SendMessage(message);
            }
            catch (ValidationException ex)
            {
                throw ValidationExceptions.AsStatusRuntimeException(ex);
            }
        };

        return forwardingClientCall;
    }

    //     public override ClientCall<ReqT, RespT> interceptCall(MethodDescriptor<ReqT, RespT> method, CallOptions callOptions, Channel next)
    //     {
    //         return new ForwardingClientCall.SimpleForwardingClientCall<ReqT, RespT>(next.newCall(method, callOptions)) {
    //             @Override
    //             public void sendMessage(ReqT message)
    //         {
    //             try
    //             {
    //                 index.validatorFor(message.getClass()).assertValid(message);
    //                 super.sendMessage(message);
    //             }
    //             catch (ValidationException ex)
    //             {
    //                 throw ValidationExceptions.asStatusRuntimeException(ex);
    //             }
    //         }
    //     };
    // }
}
