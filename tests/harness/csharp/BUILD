load("@rules_dotnet//dotnet:defs.bzl", "csharp_binary")

csharp_binary(
    name = "csharp-harness",
    # main_class = "io.envoyproxy.pgv.validation.CsharpHarness",
    main_class = "Envoy.Proto.Validate.CsharpHarness",
    visibility = ["//tests/harness:__subpackages__"],
    runtime_deps = [
        "//csharp/pgv-csharp-validation/src/main/csharp/io/envoyproxy/pgv/validation:csharp_harness",
    ],
)
