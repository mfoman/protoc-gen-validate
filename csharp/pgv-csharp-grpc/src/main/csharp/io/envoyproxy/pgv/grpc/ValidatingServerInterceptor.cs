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
 * {@code ValidatingServerInterceptor} is a gRPC {@code ServerInterceptor} that validates inbound gRPC messages before
 * processing. Non-compliant messages result in an {@code INVALID_ARGUMENT} status response.
 */
public class ValidatingServerInterceptor : ServerInterceptor
{
    private readonly ValidatorIndex index;

    public ValidatingServerInterceptor(ValidatorIndex index)
    {
        this.index = index;
    }

    // <ReqT, RespT>
    public override ServerCall.Listener<ReqT> InterceptCall<ReqT, RespT>(ServerCall<ReqT, RespT> call, Metadata headers, ServerCallHandler<ReqT, RespT> next)
    {
        var listener = next.StartCall(call, headers);
        var forwardingServerCallListener = new ForwardingServerCallListener<ReqT>(listener);

        bool aborted = false;

        forwardingServerCallListener.OnMessage = (message) =>
        {
            try
            {
                index.ValidatorFor(message.GetType()).AssertValid(message);
                forwardingServerCallListener.OnMessage(message);
            }
            catch (ValidationException ex)
            {
                var status = ValidationExceptions.AsStatusRuntimeException(ex);
                aborted = true;
                call.Close(status.Status, status.Trailers);
            }
        };

        forwardingServerCallListener.OnHalfClose = () =>
        {
            if (!aborted)
                forwardingServerCallListener.OnHalfClose();
        };

        return forwardingServerCallListener;
    }

    // public override ServerCall.Listener<ReqT> interceptCall(ServerCall<ReqT, RespT> call, Metadata headers, ServerCallHandler<ReqT, RespT> next)
    // {
    //     return new ForwardingServerCallListener.SimpleForwardingServerCallListener<ReqT>(next.startCall(call, headers))
    //     {

    //         // Implementations are free to block for extended periods of time. Implementations are not
    //         // required to be thread-safe.
    //         private boolean aborted = false;

    // public override void onMessage(ReqT message)
    // {
    //     try
    //     {
    //         index.validatorFor(message.getClass()).assertValid(message);
    //         super.onMessage(message);
    //     }
    //     catch (ValidationException ex)
    //     {
    //         StatusRuntimeException status = ValidationExceptions.asStatusRuntimeException(ex);
    //         aborted = true;
    //         call.close(status.getStatus(), status.getTrailers());
    //     }
    // }

    // public override void onHalfClose()
    // {
    //     if (!aborted)
    //     {
    //         super.onHalfClose();
    //     }
    // }
};
